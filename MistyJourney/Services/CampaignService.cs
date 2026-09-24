using System.Text.Json;
using Microsoft.Extensions.Options;
using MistyJourney.Models;

namespace MistyJourney.Services;

public class CampaignService
{
    private readonly AppSettings _appSettings;
    
    public CampaignService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public async Task AddDialogueAsync(string characterName, Dialogue dialogue)
    {
        var characterFolderPath = GetCharacterFolderPath(characterName);
        var guid = Guid.NewGuid();
        var fileName = $"dialogue_{guid}.json";
        var filePath = Path.Combine(characterFolderPath, fileName);
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        var json = JsonSerializer.Serialize(dialogue, options);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<Dialogue?> GetDialogueAsync(string characterName)
    {
        var characterFolderPath = GetCharacterFolderPath(characterName);
        
        if (!Directory.Exists(characterFolderPath))
        {
            return null;
        }
        
        var dialogueFiles = Directory.GetFiles(characterFolderPath, "dialogue_*.json");
        if (dialogueFiles.Length == 0)
        {
            return null;
        }
        
        var latestFile = dialogueFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();
        var json = await File.ReadAllTextAsync(latestFile);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        return JsonSerializer.Deserialize<Dialogue>(json, options);
    }

    public string CreateCharacterFolder(string characterName, string? localization = null)
    {
        if (string.IsNullOrEmpty(_appSettings.FolderPath) || string.IsNullOrEmpty(characterName))
        {
            throw new ArgumentException("FolderPath and CharacterName must not be null or empty");
        }
        
        string characterFolderPath = GetCharacterFolderPath(characterName);
        if (!Directory.Exists(characterFolderPath))
        {
            Directory.CreateDirectory(characterFolderPath);
        }
        
        var gameSettings = new GameSettings
        {
            CharacterName = characterName,
            Localization = localization,
            CreatedAt = DateTime.Now
        };
        
        string settingsFilePath = Path.Combine(characterFolderPath, "_settings.json");
        string jsonSettings = JsonSerializer.Serialize(gameSettings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(settingsFilePath, jsonSettings);
        
        return characterFolderPath;
    }

    private string GetCharacterFolderPath(string characterName)
    {
        var safeName = string.Join("_", characterName.Split(Path.GetInvalidFileNameChars()));
        return Path.Combine(_appSettings.FolderPath, safeName);
    }
}
