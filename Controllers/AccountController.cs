using Microsoft.AspNetCore.Mvc;
using TezStore.Models;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    public AccountController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ValidatePhone([FromBody] UserValidationViewModel m)
    {
        var u = _context.Users.Find(m.TelefonNo);
        if (u == null)
            return Json(new { success = false, message = "Numara kayıtlı değil." });

        HttpContext.Session.SetString("PendingUserPhone", m.TelefonNo);
        return Json(new { success = true });
    }

    [HttpPost]
    public IActionResult ValidatePassword([FromBody] PasswordValidationViewModel m)
    {
        var phone = HttpContext.Session.GetString("PendingUserPhone");
        var u = phone == null ? null : _context.Users.Find(phone);
        if (u == null || u.Sifre != m.Sifre)
            return Json(new { success = false, message = "Hatalı şifre veya oturum." });

        HttpContext.Session.Remove("PendingUserPhone");
        HttpContext.Session.SetString("UserPhone", phone!);
        HttpContext.Session.SetString("UserRole", u.Durum);

        return Json(new { success = true });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Store");
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp([FromBody] User m)
    {
        if (m.TelefonNo.Length != 10 || !long.TryParse(m.TelefonNo, out _))
            return Json(new { success = false, message = "Telefon numarası 10 haneli ve sadece rakam olmalıdır." });

        if (m.Sifre.Length != 6 || !long.TryParse(m.Sifre, out _))
            return Json(new { success = false, message = "Şifre 6 haneli ve sadece rakamlardan oluşmalıdır." });

        if (_context.Users.Any(u => u.TelefonNo == m.TelefonNo))
            return Json(new { success = false, message = "Bu telefon numarası zaten kayıtlı." });

        var newUser = new User
        {
            TelefonNo = m.TelefonNo,
            Sifre = m.Sifre,
            Ad = m.Ad,
            Soyad = m.Soyad,
            DogumTarihi = m.DogumTarihi,
            Cinsiyet = m.Cinsiyet,
            Durum = "Aktif",
            KayitTarihi = DateTime.Now,
            Balance = 0
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return Json(new { success = true, message = "Kayıt başarılı. Giriş yapabilirsiniz." });
    }
}