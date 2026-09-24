using System.Text.Json.Serialization;
using MistyJourney.Models;

namespace MistyJourney.Services;

public class DialogueGenerator
{
    public async Task<Dialogue> GenerateAsync(DialogueGenerationParameters parameters)
    {
        // Simulate async work
        await Task.Delay(100);

        // Generate mock data
        var random = new Random();
        var mockParagraphs = new List<string>
        {
            "You find yourself standing at a crossroads in a dense, misty forest.",
            "The path ahead splits in multiple directions, each shrouded in a thick fog.",
            "A cold wind blows from the north, carrying whispers of ancient secrets."
        };

        var mockOptions = new string[]
        {
            "Follow the path to the ancient ruins",
            "Turn back and return to the village",
            "Investigate the strange noise coming from the bushes",
            "Call out to see if anyone is nearby",
            "Sit down and rest for a moment"
        };

        // Randomly decide how many paragraphs to include (1-3)
        var paragraphCount = random.Next(1, 4);
        var paragraphs = mockParagraphs.Take(paragraphCount).ToList();

        return new Dialogue
        {
            Paragraphs = paragraphs,
            A = new DialogueOption { Text = mockOptions[0] },
            B = new DialogueOption { Text = mockOptions[1] },
            C = new DialogueOption { Text = mockOptions[2] },
            D = new DialogueOption { Text = mockOptions[3] },
            E = new DialogueOption { Text = mockOptions[4] }
        };
    }
}
