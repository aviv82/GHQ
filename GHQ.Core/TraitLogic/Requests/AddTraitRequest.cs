namespace GHQ.Core.TraitLogic.Requests;
public class AddTraitRequest
{
    public string Name { get; set; } = default!;
    public string? Details { get; set; }
    public int? Value { get; set; }
    public int? Level { get; set; }
    public int TraitGroupId { get; set; }
}