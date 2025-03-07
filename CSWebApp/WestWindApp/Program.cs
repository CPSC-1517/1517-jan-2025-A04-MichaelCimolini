using WestWindApp.Components;

using Microsoft.EntityFrameworkCore;
using WestWindSystem;

var builder = WebApplication.CreateBuilder(args);

//The connection string should point to a connections string in our
//appsetting.json
var connectionString = builder.Configuration.GetConnectionString("WWDB-Laptop");

//This passes our connection string to our services for use
builder.Services.WestWindExtensionServices(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
