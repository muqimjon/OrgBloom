using Telegram.Bot.Polling;
using OrgBloom.Application;
using OrgBloom.Infrastructure;
using OrgBloom.Bot.Extensions;
using OrgBloom.Bot.BotServices;
using OrgBloom.Bot.BotServices.Commons;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using OrgBloom.Infrastructure.Models;
using OrgBloom.Application.Commons.Interfaces;
using OrgBloom.Infrastructure.Repositories;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Get bot token
var token = builder.Configuration.GetValue("BotToken", string.Empty);

var googleAuthSettings = new GoogleAuthSettings();
//builder.Configuration.Bind("GoogleAuth", googleAuthSettings);
builder.Configuration.GetSection("GoogleAuth").Bind(googleAuthSettings);
var g = JsonConvert.SerializeObject(googleAuthSettings);

var configur = new SheetsConfigure()
{
    SpreadsheetId = builder.Configuration.GetValue("SpreadsheetId", string.Empty)!,
    Service = new SheetsService(new BaseClientService.Initializer()
    {
        HttpClientInitializer = GoogleCredential.FromJson(g),
        ApplicationName = builder.Configuration.GetValue("ApplicationName", string.Empty),
    }),
};
builder.Services.AddSingleton(configur);
builder.Services.AddScoped(typeof(ISheetsRepository<>), typeof(SheetsRepository<>));


// Add bot services
builder.Services.AddSingleton(new TelegramBotClient(token!));
builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
builder.Services.AddHostedService<BotBackgroundService>();

builder.Services.AddLocalization();

// Build
var app = builder.Build();

app.MigrateDatabase();
    
var supportedCultures = new[] { "uz", "ru", "en" };
var localizationOptions = new RequestLocalizationOptions()
  .SetDefaultCulture(supportedCultures[0])
  .AddSupportedCultures(supportedCultures)
  .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.Run();