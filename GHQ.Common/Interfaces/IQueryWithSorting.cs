namespace GHQ.Common.Interfaces;

public interface IQueryWithSorting : IQueryBase
{
    public string Sort { get; set; }
}
