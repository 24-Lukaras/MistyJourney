using System.Text.Json.Serialization;

namespace MistyJourney.Models;

public class DialogueOption
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}
