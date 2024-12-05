global using NewProject.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using NewProject.Hubs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Minio;
using NewProject.Authentication;
using NewProject;
using NewProject.Services.AuthenServices;
using NewProject.Services;
using NewProject.Services.IApiServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ApiServices>();
builder.Services.AddScoped<AuthenServices>();
builder.Services.AddScoped<IAuthenServices,AuthenServices>();
builder.Services.AddScoped<IApiServices, ApiServices>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddDbContext<dbAccountContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DBConnection"));

}, contextLifetime: ServiceLifetime.Transient);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(configuration.GetConnectionString("API") ?? "http://localhost:5240")
    });
builder.Services.AddRadzenComponents();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Keycloak:Authority"];
    options.Audience = builder.Configuration["Keycloak:ClientId"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddScoped<AuthenticationHeaderHandler>();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();
builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<ChatHubs>("/chat");
app.Run();
