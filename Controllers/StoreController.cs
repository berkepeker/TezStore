using Microsoft.AspNetCore.Mvc;
using TezStore.Models;
using System.Linq;

namespace TezStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StoreController(ApplicationDbContext db) => _db = db;

        // GET: /Store
        public IActionResult Index()
        {
            // Giriş kontrolünü kaldırıyoruz (Redirect)—
            // bu sayfayı her zaman göster, session'da telefon varsa al bakaneyi
            var phone = HttpContext.Session.GetString("UserPhone");
            decimal balance = 0;
            if (!string.IsNullOrEmpty(phone))
            {
                var user = _db.Users.Find(phone)!;
                balance = user.Balance;
            }

            ViewBag.Balance = balance;
            ViewBag.UserPhone = phone;
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            
            var items = _db.SellableItems.ToList();
            return View(items);
        }
        // bu fonksiyonda sepet işlemleri yapılıyor
        [HttpPost]
        public IActionResult PurchaseCart([FromBody] List<int> itemIds)
        {
            var phone = HttpContext.Session.GetString("UserPhone");
            if (string.IsNullOrEmpty(phone))
                return Json(new { success = false, message = "Oturum bulunamadı." });

            var user = _db.Users.Find(phone);
            if (user == null)
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });

            // 🔍 Adetleri dikkate alarak grupluyoruz
            var itemGroups = itemIds.GroupBy(id => id)
                                    .Select(g => new { Id = g.Key, Quantity = g.Count() })
                                    .ToList();

            var itemIdsDistinct = itemGroups.Select(g => g.Id).ToList();
            var items = _db.SellableItems.Where(i => itemIdsDistinct.Contains(i.Id)).ToList();

            if (items.Count == 0)
                return Json(new { success = false, message = "Sepette geçerli ürün yok." });

            decimal total = 0;
            foreach (var group in itemGroups)
            {
                var item = items.FirstOrDefault(i => i.Id == group.Id);
                if (item == null) continue;

                total += item.Price * group.Quantity;
            }

            if (user.Balance < total)
                return Json(new { success = false, message = "Yetersiz bakiye." });

            var groupId = Guid.NewGuid();

            foreach (var group in itemGroups)
            {
                var item = items.FirstOrDefault(i => i.Id == group.Id);
                if (item == null) continue;

                for (int i = 0; i < group.Quantity; i++)
                {
                    user.Balance -= item.Price;
                    _db.Transactions.Add(new Transaction
                    {
                        UserId = phone,
                        SellableItemId = item.Id,
                        Amount = item.Price,
                        BalanceAfter = user.Balance,
                        TransactionDate = DateTime.Now,
                        TransactionGroupId = groupId
                    });
                }
            }

            _db.Users.Update(user);
            _db.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Alışveriş başarıyla tamamlandı.",
                newBalance = user.Balance
            });
        }






        // POST: /Store/Purchase
        [HttpPost]
        public IActionResult Purchase(int id)
        {
            var phone = HttpContext.Session.GetString("UserPhone");
            if (string.IsNullOrEmpty(phone))
                return Json(new { success = false, message = "Oturum bulunamadı." });

            var user = _db.Users.Find(phone)!;
            var item = _db.SellableItems.Find(id)!;

            if (user.Balance < item.Price)
                return Json(new { success = false, message = "Yetersiz bakiye." });

            user.Balance -= item.Price;
            _db.Users.Update(user);

            var tx = new Transaction
            {
                UserId = phone,
                SellableItemId = id,
                Amount = item.Price,
                BalanceAfter = user.Balance,
                TransactionDate = DateTime.Now
            };
            _db.Transactions.Add(tx);
            _db.SaveChanges();

            return Json(new { success = true, newBalance = user.Balance });
        }
    }
}
