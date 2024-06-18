using GHQ.Data.Enums;

namespace GHQ.Data.Entities;

public class TraitGroup : BaseEntity
{
    public string TraitGroupName { get; set; } = default!;
    public TraitType? Type { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; } = default!;
    public ICollection<Trait> Traits { get; set; } = [];
}