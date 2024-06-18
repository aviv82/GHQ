namespace GHQ.Data.Entities;

public class Character : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; } = default!;
    public int PlayerId { get; set; }
    public Player Player { get; set; } = default!;

    public ICollection<TraitGroup> TraitGroups { get; set; } = [];
    public ICollection<Roll> Rolls { get; set; } = [];
}
