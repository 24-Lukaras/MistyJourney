using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MistyJourney.Pages
{
    public class GameModel : PageModel
    {
        public string? CharacterName { get; set; }
        
        public void OnGet(string? characterName)
        {
            CharacterName = characterName;
        }
    }
}
