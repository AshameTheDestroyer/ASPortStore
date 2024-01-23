using ASPortStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ASPortStoreConnection"]));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute("categoryAndPage", "Products/{category}/Page{page}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("category", "Products/{category:alpha}", new { Controller = "Home", action = "Index", page = 1 });
app.MapControllerRoute("page", "Products/Page{page}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Products", new { Controller = "Home", action = "Index" });

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();