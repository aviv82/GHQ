namespace GHQ.Common.Interfaces;

public interface IQueryWithFiltering : IQueryBase
{
    public string Filter { get; set; }
    public char FilterKeyValueSeparator { get; set; }
    public char FilterItemSeparator { get; set; }
}
