using System.Linq.Expressions;

namespace GHQ.Core.Extensions;

public static class NameOf<TSource>
{
    public static string Full(Expression<Func<TSource, object>> expression)
    {
        var memberExpression = expression.Body as MemberExpression;
        if (memberExpression == null)
        {
            if (expression.Body is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
                memberExpression = unaryExpression.Operand as MemberExpression;
        }

        if (memberExpression != null)
        {
            var result = memberExpression.ToString();
            result = result.Substring(result.IndexOf('.') + 1);

            return result;
        }

        return string.Empty;
    }
}
