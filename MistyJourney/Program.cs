using MistyJourney;
using MistyJourney.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DialogueGenerator>();
builder.Services.AddScoped<CampaignService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/api/dialogue/{characterName}", async (string characterName, CampaignService campaignService) =>
{
    var dialogue = await campaignService.GetDialogueAsync(characterName);
    if (dialogue == null)
    {
        return Results.NotFound();
    }
        
    var dialogueModel = new MistyJourney.Models.DialogueModel
    {
        Paragraphs = dialogue.Paragraphs,
        A = dialogue.A?.Text,
        B = dialogue.B?.Text,
        C = dialogue.C?.Text,
        D = dialogue.D?.Text,
        E = dialogue.E?.Text
    };
    return dialogueModel is null ? Results.NotFound() : Results.Ok(dialogueModel);
});

app.Run();
