using System.Text.Json.Serialization;

namespace MistyJourney.Models;

public class DialogueModel
{
    [JsonPropertyName("paragraphs")]
    public List<string>? Paragraphs { get; set; }

    [JsonPropertyName("a")]
    public string? A { get; set; }

    [JsonPropertyName("b")]
    public string? B { get; set; }

    [JsonPropertyName("c")]
    public string? C { get; set; }

    [JsonPropertyName("d")]
    public string? D { get; set; }

    [JsonPropertyName("e")]
    public string? E { get; set; }
}
