namespace GHQ.Core.TraitLogic.Requests;
public class UpdateTraitRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? Value { get; set; }
    public int? Level { get; set; }
}