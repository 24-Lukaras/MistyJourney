using System.Text.Json.Serialization;

namespace MistyJourney.Models;

public class GameSettings
{
    [JsonPropertyName("characterName")]
    public string? CharacterName { get; set; }
    
    [JsonPropertyName("localization")]
    public string? Localization { get; set; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [JsonPropertyName("lastPlayed")]
    public DateTime? LastPlayed { get; set; }
    
    [JsonPropertyName("gameVersion")]
    public string? GameVersion { get; set; } = "1.0";
}
