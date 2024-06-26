﻿namespace GHQ.Data.Entities;

public class Roll : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int? Difficulty { get; set; }
    public string? Result { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; } = default!;
    public int GameId { get; set; }
    public Game Game { get; set; } = default!;
    public ICollection<Dice> DicePool { get; set; } = default!;
}
