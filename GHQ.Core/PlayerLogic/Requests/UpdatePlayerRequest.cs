namespace GHQ.Core.PlayerLogic.Requests;

public class UpdatePlayerRequest
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
