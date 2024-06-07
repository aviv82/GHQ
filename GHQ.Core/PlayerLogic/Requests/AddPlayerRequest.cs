namespace GHQ.Core.PlayerLogic.Requests;

public class AddPlayerRequest
{
    public string UserName { get; set; } = default!;
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
}
