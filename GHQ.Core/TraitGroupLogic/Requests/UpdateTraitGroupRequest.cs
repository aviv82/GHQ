using GHQ.Data.Enums;

namespace GHQ.Core.TraitGroupLogic.Requests;
public class UpdateTraitGroupRequest
{
    public int Id { get; set; }
    public string TraitGroupName { get; set; } = default!;
    public TraitType? Type { get; set; }
}
