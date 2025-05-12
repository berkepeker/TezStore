using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using TezStore.Models;

namespace TezStore.Filters
{
    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ApplicationDbContext _context;

        public AdminAuthorizationFilter(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userPhone = context.HttpContext.Session.GetString("UserPhone");

            if (string.IsNullOrEmpty(userPhone))
            {
                context.Result = new RedirectToActionResult("giris", "Home", null);
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.TelefonNo == userPhone);
            if (user == null || user.Durum != "Admin")
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }
        }
    }
}