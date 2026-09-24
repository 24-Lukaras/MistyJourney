using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MistyJourney.Pages
{
    public class CreateGameModel : PageModel
    {
        [BindProperty]
        public string CharacterName { get; set; }
        
        [BindProperty]
        public string Localization { get; set; }
        
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // Here you would typically save the game configuration
            // and redirect to the Game page or handle the creation logic
            // For now, we'll just redirect to the Game page
            return RedirectToPage("/Game");
        }
    }
}
