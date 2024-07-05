using GHQ.Data.Enums;

namespace GHQ.Core.TraitGroupLogic.Requests;


public class AddTraitGroupRequest
{
    public string TraitGroupName { get; set; } = default!;
    public TraitType? Type { get; set; }
    public int CharacterId { get; set; }
}
