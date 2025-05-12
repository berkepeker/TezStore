using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TezStore.Models;
using TezStore.ViewModels;

namespace TezStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var phone = HttpContext.Session.GetString("UserPhone");
            if (string.IsNullOrEmpty(phone))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.TelefonNo == phone);
            if (user == null)
                return NotFound();

            var groupedStoreTransactions = _context.Transactions
            .Include(t => t.SellableItem)
            .Where(t => t.UserId == phone)
            .OrderByDescending(t => t.TransactionDate)
            .AsEnumerable() // EF'den çıktı, belleğe aldı
            .GroupBy(t => t.TransactionGroupId) // aynı sepetteki ürünleri grupla
            .ToList();


            var recyclingTransactions = _context.RecyclingTransactions
                .Where(r => r.UserId == phone)
                .OrderByDescending(r => r.IslemTarihi)
                .ToList();

            var model = new ProfileViewModel
            {
                User = user,
                GroupedStoreTransactions = groupedStoreTransactions, // ✔️ doğru isim
                RecyclingTransactions = recyclingTransactions
            };
            ViewBag.UserRole = user?.Durum; // veya user?.Role ya da Admin kontrolü neyse

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var phone = HttpContext.Session.GetString("UserPhone");
            if (string.IsNullOrEmpty(phone))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.TelefonNo == phone);
            if (user == null)
                return NotFound();

            var model = new UserProfileEditViewModel
            {
                TelefonNo = user.TelefonNo,
                Ad = user.Ad,
                Soyad = user.Soyad,
                Cinsiyet = user.Cinsiyet,
                DogumTarihi = user.DogumTarihi
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserProfileEditViewModel model)
        {
            var phone = HttpContext.Session.GetString("UserPhone");
            if (string.IsNullOrEmpty(phone))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.TelefonNo == phone);
            if (user == null)
                return NotFound();

            // Bilgileri güncelle
            user.Ad = model.Ad;
            user.Soyad = model.Soyad;
            user.Cinsiyet = model.Cinsiyet;
            user.DogumTarihi = model.DogumTarihi;

            // Şifre değişikliği isteniyorsa
            if (!string.IsNullOrWhiteSpace(model.MevcutSifre) || !string.IsNullOrWhiteSpace(model.YeniSifre) || !string.IsNullOrWhiteSpace(model.YeniSifreTekrar))
            {
                if (string.IsNullOrWhiteSpace(model.MevcutSifre) || string.IsNullOrWhiteSpace(model.YeniSifre) || string.IsNullOrWhiteSpace(model.YeniSifreTekrar))
                {
                    ModelState.AddModelError("", "Şifre değiştirmek için tüm şifre alanlarını doldurmalısınız.");
                    return View(model);
                }
                if (user.Sifre != model.MevcutSifre)
                {
                    ModelState.AddModelError("MevcutSifre", "Mevcut şifreniz yanlış.");
                    return View(model);
                }
                if (model.YeniSifre != model.YeniSifreTekrar)
                {
                    ModelState.AddModelError("YeniSifreTekrar", "Yeni şifreler eşleşmiyor.");
                    return View(model);
                }
                if (model.YeniSifre.Length != 6 || !long.TryParse(model.YeniSifre, out _))
                {
                    ModelState.AddModelError("YeniSifre", "Şifre 6 haneli ve sadece rakamlardan oluşmalıdır.");
                    return View(model);
                }
                if (model.YeniSifre == model.MevcutSifre)
                {
                    ModelState.AddModelError("YeniSifre", "Yeni şifre mevcut şifrenizden farklı olmalıdır.");
                    return View(model);
                }
                user.Sifre = model.YeniSifre;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
            TempData["Success"] = "Profiliniz başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
