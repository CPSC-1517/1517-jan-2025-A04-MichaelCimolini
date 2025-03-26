using WestWindApp.Components;

using Microsoft.EntityFrameworkCore;
using WestWindSystem;

var builder = WebApplication.CreateBuilder(args);

//This configures our application to connect to our WestWind DB
//TODO: Change Connection String as needed
var connectionString = builder.Configuration.GetConnectionString("WWDB-Desktop");

//This registers the DB with our extension methods and services
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
