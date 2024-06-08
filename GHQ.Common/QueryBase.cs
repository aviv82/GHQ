using GHQ.Common.Interfaces;

namespace GHQ.Core;

public abstract class QueryBase : IQueryBase
{
    public virtual bool NameEquals(string name, string target)
    {
        return name.Equals(target, StringComparison.InvariantCultureIgnoreCase);
    }

    public virtual string GetColumnName<T>(string name)
    {
        return typeof(T).GetProperties().Any(x => NameEquals(x.Name, name))
            ? name : string.Empty;
    }
}
