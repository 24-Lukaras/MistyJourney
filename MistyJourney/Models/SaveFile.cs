namespace MistyJourney.Models;

public class SaveFile
{
    public string? DirectoryName { get; set; }
    public string? CharacterName { get; set; }
    public string? Localization { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastPlayed { get; set; }
    public string? GameVersion { get; set; }
}
