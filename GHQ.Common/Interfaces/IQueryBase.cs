namespace GHQ.Common.Interfaces;

public interface IQueryBase
{
    string GetColumnName<T>(string name);
}
