using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TezStore.Models;

namespace TezStore.Extensions
{
    public static class AdminSeedExtension
    {
        public static void SeedAdminUser(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                if (!context.Users.Any(u => u.Durum == "Admin"))
                {
                    var adminUser = new User
                    {
                        TelefonNo = "5555555555", // Varsayılan admin telefon
                        Sifre = "123456",         // Varsayılan admin şifre
                        Ad = "Admin",
                        Soyad = "User",
                        Cinsiyet = "Erkek",
                        Durum = "Admin",
                        KayitTarihi = DateTime.Now
                    };
                    
                    context.Users.Add(adminUser);
                    context.SaveChanges();
                }
            }
        }
    }
}