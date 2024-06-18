namespace GHQ.Data.Entities;

public class Player : BaseEntity
{
    public string UserName { get; set; } = default!;
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public ICollection<Game> PlayerGames { get; set; } = [];
    public ICollection<Game> DmGames { get; set; } = [];
    public ICollection<Character> Characters { get; set; } = [];
}
