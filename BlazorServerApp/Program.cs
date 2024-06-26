using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using dotNETCloudRun.Data;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

if(builder.Configuration["Authentication:Google:ClientId"] is null || builder.Configuration["Authentication:Google:ClientSecret"] is null)
{
    string PROJECT_ID = Environment.GetEnvironmentVariable("PROJECT_ID")!;

    if (string.IsNullOrEmpty(PROJECT_ID))
    {
        Console.WriteLine("PROJECT_ID environment variable must be set.");
        return;
    }
    AccessSecretVersionSample GoogleAuthClient = new();
    var GOOGLE_CLIENT_ID = GoogleAuthClient.AccessSecretVersion(projectId: PROJECT_ID, secretId: "GOOGLE_CLIENT_ID", secretVersionId: "2");
    var GOOGLE_CLIENT_SECRET = GoogleAuthClient.AccessSecretVersion(projectId: PROJECT_ID, secretId: "GOOGLE_CLIENT_SECRET", secretVersionId: "2");

    builder.Services.AddAuthentication(options => {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        }).AddCookie()
        .AddGoogle(options => {
        options.ClientId = GOOGLE_CLIENT_ID;
        options.ClientSecret = GOOGLE_CLIENT_SECRET;
        options.ClaimActions.MapJsonKey("urn:google:profile", "link");
        options.ClaimActions.MapJsonKey("urn:google:image", "picture");
    });
    //throw new Exception("Google Authentication settings are missing. Please check the configuration.");
}else{
    builder.Services.AddAuthentication(options => {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        }).AddCookie()
        .AddGoogle(options => {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        options.ClaimActions.MapJsonKey("urn:google:profile", "link");
        options.ClaimActions.MapJsonKey("urn:google:image", "picture");
    });
}

builder.Services.AddHttpClient();           // 注入@inject HttpClient Http
builder.Services.AddHttpContextAccessor();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
