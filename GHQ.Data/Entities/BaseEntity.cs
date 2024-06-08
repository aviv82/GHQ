namespace GHQ.Data.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public byte[] Version { get; set; } = default!;

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
