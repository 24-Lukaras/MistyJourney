using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MistyJourney.Models;
using System.Text.Json;

namespace MistyJourney.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppSettings _appSettings;
        
        public IndexModel(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public List<SaveFile> Saves { get; set; } = new List<SaveFile>();
        
        public void OnGet()
        {
            LoadSaves();
        }
        
        private void LoadSaves()
        {
            Saves.Clear();
            
            if (string.IsNullOrEmpty(_appSettings.FolderPath) || !Directory.Exists(_appSettings.FolderPath))
            {
                return;
            }
            
            foreach (var directory in Directory.GetDirectories(_appSettings.FolderPath))
            {
                var saveFile = new SaveFile
                {
                    DirectoryName = Path.GetFileName(directory)
                };
                
                string settingsPath = Path.Combine(directory, "_settings.json");
                if (System.IO.File.Exists(settingsPath))
                {
                    try
                    {
                        string json = System.IO.File.ReadAllText(settingsPath);
                        var gameSettings = JsonSerializer.Deserialize<GameSettings>(json);
                        
                        if (gameSettings != null)
                        {
                            saveFile.CharacterName = gameSettings.CharacterName;
                            saveFile.Localization = gameSettings.Localization;
                            saveFile.CreatedAt = gameSettings.CreatedAt;
                            saveFile.LastPlayed = gameSettings.LastPlayed;
                            saveFile.GameVersion = gameSettings.GameVersion;
                        }
                    }
                    catch
                    {
                        // If we can't read the settings, just use the directory name
                        saveFile.CharacterName = saveFile.DirectoryName;
                    }
                }
                else
                {
                    saveFile.CharacterName = saveFile.DirectoryName;
                }
                
                Saves.Add(saveFile);
            }
        }
    }
}
