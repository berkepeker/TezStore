using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using TezStore.Filters;
using TezStore.Models;
// Program.cs (.NET 6+ örneği)
var builder = WebApplication.CreateBuilder(args);

// Azure SQL Server bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AdminAuthorizationFilter>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}"
);
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!db.SellableItems.Any())
    {
        db.SellableItems.AddRange(new[]
        {
            new SellableItem { Name="Ürün A", Description="Açıklama A", Price=10 },
            new SellableItem { Name="Ürün B", Description="Açıklama B", Price=20 },
        });
        db.SaveChanges();
    }
}

app.Run();

