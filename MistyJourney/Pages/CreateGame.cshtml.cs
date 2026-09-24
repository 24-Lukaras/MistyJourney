using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MistyJourney.Models;
using MistyJourney.Services;

namespace MistyJourney.Pages
{
    public class CreateGameModel : PageModel
    {
        private readonly CampaignService _campaignService;
        private readonly DialogueGenerator _dialogueGenerator;
        
        public CreateGameModel(
            CampaignService campaignService,
            DialogueGenerator dialogueGenerator)
        {
            _campaignService = campaignService;
            _dialogueGenerator = dialogueGenerator;
        }
        
        [BindProperty]
        public string? CharacterName { get; set; }
        
        [BindProperty]
        public string? Localization { get; set; }
        
        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // Create character folder and settings file via CampaignService
            if (!string.IsNullOrEmpty(CharacterName))
            {
                _campaignService.CreateCharacterFolder(CharacterName, Localization);
                var dialogue = await _dialogueGenerator.GenerateAsync(new DialogueGenerationParameters());
                await _campaignService.AddDialogueAsync(CharacterName, dialogue);
            }
            
            // Redirect to Game page with character name as route parameter
            return RedirectToPage("/Game", new { characterName = CharacterName });
        }
    }
}
