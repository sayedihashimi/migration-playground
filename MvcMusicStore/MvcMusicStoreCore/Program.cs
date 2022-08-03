using Microsoft.AspNetCore.SystemWebAdapters;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MvcMusicStoreCore.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MusicStoreEntitiesDataBase");
builder.Services.AddTransient<MusicStoreSeeder>();
builder.Services.AddDbContext<MusicStoreEntities>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreEntitiesDataBase")));

//builder.Services.AddDbContext<MusicStoreEntities>(options =>
//    options.UseSqlServer(connectionString));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSystemWebAdapters();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

//app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapReverseProxy();

app.Run();
