using ASPortStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ASPortStoreConnection"]));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.MapControllerRoute("categorized-pagination", "products/{category}/page{page}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("categorized", "products/{category:alpha}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("pagination", "products/page{page}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("default", "products", new { Controller = "Home", Action = "Index" });

app.MapDefaultControllerRoute();

app.UseSession();

SeedData.EnsurePopulated(app);

app.Run();