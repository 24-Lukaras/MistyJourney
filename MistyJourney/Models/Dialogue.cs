using System.Text.Json.Serialization;

namespace MistyJourney.Models;

public class Dialogue
{
    [JsonPropertyName("paragraphs")]
    public List<string>? Paragraphs { get; set; }

    [JsonPropertyName("a")]
    public DialogueOption? A { get; set; }

    [JsonPropertyName("b")]
    public DialogueOption? B { get; set; }

    [JsonPropertyName("c")]
    public DialogueOption? C { get; set; }

    [JsonPropertyName("d")]
    public DialogueOption? D { get; set; }

    [JsonPropertyName("e")]
    public DialogueOption? E { get; set; }
}
