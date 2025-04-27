namespace ColourMemory.Api.Models
{
    public class Card
    {
        public required string Color { get; set; }
        public bool IsMatched { get; set; } = false;
    }
}
