using Microsoft.AspNetCore.Mvc;
using TezStore.Models;
using TezStore.Filters;
using TezStore.ViewModels;

[ServiceFilter(typeof(AdminAuthorizationFilter))]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;
    public AdminController(ApplicationDbContext db) => _db = db;

    // GET: /Admin
    public IActionResult Index()
    {
        var userCount = _db.Users.Count();
        var itemCount = _db.SellableItems.Count();
        var lastUsers = _db.Users
            .OrderByDescending(u => u.KayitTarihi)
            .Take(5)
            .ToList();
        var lastItems = _db.SellableItems
            .OrderByDescending(i => i.Id)
            .Take(5)
            .ToList();

        ViewBag.UserCount = userCount;
        ViewBag.ItemCount = itemCount;
        ViewBag.LastUsers = lastUsers;
        ViewBag.LastItems = lastItems;

        return View();
    }

    // GET: /Admin/Items
    public IActionResult Items()
    {
        var items = _db.SellableItems.ToList();
        return View(items);
    }


    [HttpGet]
    public IActionResult AddItem()
    {
        return View(new SellableItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddItem(SellableItem item, IFormFile imageFile)
    {
        try
        {
            if (imageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                imageFile.CopyTo(stream);

                item.ImagePath = "/images/" + fileName;
            }

            _db.SellableItems.Add(item);
            _db.SaveChanges();

            return Content("OK");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }



        [HttpGet]
    public IActionResult EditItem(int id)
    {
        var item = _db.SellableItems.Find(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditItem(SellableItem updated, IFormFile? newImage)
    {
        var item = _db.SellableItems.Find(updated.Id);
        if (item == null) return NotFound();

        item.Name = updated.Name;
        item.Description = updated.Description;
        item.Price = updated.Price;

        if (newImage != null && newImage.Length > 0)
        {
            // Klasör yoksa oluştur
            var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(newImage.FileName);
            var filePath = Path.Combine(imageFolderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            newImage.CopyTo(stream);

            item.ImagePath = "/images/" + fileName;
        }

        _db.SellableItems.Update(item);
        _db.SaveChanges();

        return Content("OK");
    }



    // AJAX: /Admin/DeleteItem
    [HttpPost]
    public IActionResult DeleteItem(int id)
    {
        var item = _db.SellableItems.Find(id);
        if (item == null) return NotFound();

        _db.SellableItems.Remove(item);
        _db.SaveChanges();

        return Content("OK");
    }




    // GET: /Admin/Users
        public IActionResult Users()
    {
        var users = _db.Users.ToList();
        return View(users);
    }

    // GET: /Admin/AddUser
        [HttpGet]
        public IActionResult AddUser()
        {
            // ViewModel nesnesi oluşturup gönderiyoruz
            var model = new AddUserViewModel();
            return View(model);
        }

        // POST: /Admin/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(AddUserViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.NewUser.TelefonNo) || string.IsNullOrWhiteSpace(vm.NewUser.Sifre))
            {
                return Json(new { success = false, message = "Telefon ve şifre zorunludur." });
            }

            var existing = _db.Users.Find(vm.NewUser.TelefonNo);
            if (existing != null)
            {
                return Json(new { success = false, message = "Bu telefon numarası zaten kayıtlı." });
            }

            vm.NewUser.KayitTarihi = DateTime.Now;
            _db.Users.Add(vm.NewUser);
            _db.SaveChanges();

            return Json(new { success = true, message = "Kullanıcı başarıyla eklendi." });
        }

    // GET: /Admin/EditUser
    [HttpGet]
    public IActionResult EditUser(string telefonNo)
    {
        var user = _db.Users.Find(telefonNo);
        if (user == null) return NotFound();

        return View(user); // ✅ DOĞRU
    }

    // POST: /Admin/EditUser
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditUser(User updated)
    {
        var user = _db.Users.Find(updated.TelefonNo);
        if (user == null) return NotFound();

        user.Ad = updated.Ad;
        user.Soyad = updated.Soyad;
        user.Cinsiyet = updated.Cinsiyet;
        user.Sifre = updated.Sifre;
        user.Durum = updated.Durum;
        user.Balance = updated.Balance;

        _db.SaveChanges();
        return RedirectToAction("Users");
    }


    // AJAX: /Admin/UpdateUser
    [HttpPost]
    public IActionResult UpdateUser([FromBody] User u)
    {
        var ex = _db.Users.Find(u.TelefonNo);
        if(ex==null) return Json(new { success=false });
        ex.Ad = u.Ad; ex.Soyad = u.Soyad;
        ex.DogumTarihi = u.DogumTarihi; ex.Cinsiyet = u.Cinsiyet;
        _db.SaveChanges();
        return Json(new { success=true });
    }

    // POST: /Admin/DeleteUser
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult DeleteUser(string telefonNo)
    {
        var u = _db.Users.Find(telefonNo);
        if(u!=null){
            _db.Users.Remove(u);
            _db.SaveChanges();
        }
        return RedirectToAction("Users");
    }
}
