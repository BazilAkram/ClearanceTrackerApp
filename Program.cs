using ClearanceTrackerApp.Components;
using ClearanceTrackerApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// For Testing
builder.Services.AddScoped<ClearanceTrackerApp.Services.ClearanceService>();

// Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core + SQLite
builder.Services.AddDbContext<AppDb>(o =>
    o.UseSqlite("Data Source=clearance.db"));

var app = builder.Build();

// Ensure DB exists, apply migrations, seed demo rows
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    db.Database.Migrate();
    SeedData.SeedIfEmpty(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
