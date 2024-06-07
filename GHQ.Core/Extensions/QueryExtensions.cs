using GHQ.Common.Exceptions;
using GHQ.Common.Interfaces;
using System.Linq.Expressions;

namespace GHQ.Core.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, IQueryWithPagination query)
    {
        return query.PageNumber.HasValue
            ? source.Skip((query.PageNumber.Value - 1) * query.PageSize.GetValueOrDefault(25)).Take(query.PageSize.GetValueOrDefault(25))
            : source;
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, IQueryWithSorting query)
    {
        var columnName = query.GetColumnNameForSort<T>();
        if (string.IsNullOrEmpty(columnName)) return source;
        var expression = source.Expression;
        var method = query.IsSortDescending() ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy);

        var parameter = Expression.Parameter(typeof(T), "x");

        var selector = columnName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);

        expression = Expression.Call(
            typeof(Queryable),
            method,
            new[]
            {
                source.ElementType,
                selector.Type
            },
            expression,
            Expression.Quote(Expression.Lambda(selector, parameter)));

        return source.Provider.CreateQuery<T>(expression);
    }

    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> source, IQueryWithFiltering query)
    {
        var filters = query.GetFilterDictionary<T>();
        return filters.Aggregate(source, (current, filter) => ApplyFiltering(current, filter.Key, filter.Value));
    }

    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> source, string member, object value)
    {
        var item = Expression.Parameter(typeof(T), "item");
        var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
        var memberType = memberValue.Type;

        if (value != null && value.GetType() != memberType)
        {
            value = ConvertValue(member, value, memberType);
        }

        if (memberType == typeof(string))
        {
            var predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    Expression.Call(
                        memberValue,
                        nameof(string.ToUpper),
                        null),
                    nameof(string.Contains),
                    null,
                    Expression.Constant($"{value}".ToUpper())), item);
            return source.Where(predicate);
        }
        else
        {
            var condition = memberType == typeof(DateTime)
                ? Expression.GreaterThanOrEqual(memberValue, Expression.Constant(value, memberType))
                : Expression.Equal(memberValue, Expression.Constant(value, memberType));
            var predicate = Expression.Lambda<Func<T, bool>>(condition, item);
            return source.Where(predicate);
        }
    }

    private static object ConvertValue(string member, object value, Type memberType)
    {
        if (memberType.IsEnum)
        {
            try
            {
                value = Enum.Parse(memberType, (string)value);
            }
            catch (Exception)
            {
                throw new InvalidFilterException(member);
            }
        }
        else
        {
            value = Convert.ChangeType(value, memberType);
        }

        return value;
    }
}
