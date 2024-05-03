using DataAccessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETicaretKurumsalSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;

        public MainController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                ViewBag.TotalProductCount = products.Count;

                var users = await _context.Users.ToListAsync();
                ViewBag.TotalUserCount = users.Count;

                var customers = await _context.Customers.ToListAsync();
                ViewBag.TotalCustomerCount = customers.Count;

                var slides = await _context.Slides.ToListAsync();
                ViewBag.TotalSlideCount = slides.Count;

                var categories = await _context.Categories.ToListAsync();
                ViewBag.TotalCategoryCount = categories.Count;

                var orders = await _context.Orders.ToListAsync();
                ViewBag.TotalOrderCount = orders.Count;


                // Her kategorinin adını ve içerisindeki ürün sayısını alıp ViewBag'e ekleyelim
                var categoryProductCounts = new Dictionary<string, int>();
                foreach (var category in categories)
                {
                    var productCount = await _context.Products.CountAsync(p => p.CategoryId == category.Id);
                    categoryProductCounts.Add(category.Name, productCount);
                }
                ViewBag.CategoryProductCounts = categoryProductCounts;

                // Her ürünün adını ve stok miktarını alıp ViewBag'e ekleyelim
                var productStocks = new Dictionary<string, int>();
                foreach (var product in products)
                {
                    productStocks.Add(product.Name, product.Stock);
                }
                ViewBag.ProductStocks = productStocks;



                // Müşteri adı ve soyadını alarak ViewBag'e ekleyelim
                var customerInfo = new Dictionary<string, string>();
                foreach (var customer in customers)
                {
                    // Müşterinin adı ve soyadını birleştirerek tek bir string olarak al
                    var fullName = $"{customer.Name} {customer.Surname}";
                    // Müşteri ID'si ile birlikte bu string'i Dictionary'e ekle
                    customerInfo.Add(customer.Id.ToString(), fullName);
                }
                // ViewBag'e müşteri bilgilerini ekle
                ViewBag.CustomersInfo = customerInfo;


                var userInfo = new Dictionary<string, string>();
                foreach (var user in users)
                {
                    // Personelin adı ve soyadını birleştirerek tek bir string olarak al
                    var userfullName = $"{user.Name} {user.Surname}";
                    // Personel ID'si ile birlikte bu string'i Dictionary'e ekle
                    userInfo.Add(user.Id.ToString(), userfullName);
                }
                // ViewBag'e müşteri bilgilerini ekle
                ViewBag.UsersInfo = userInfo;



            }
            catch (Exception ex)
            {
                TempData["Message"] = "Veri alınırken bir hata oluştu: " + ex.Message;
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var kullanici = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive);
                if (kullanici == null)
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Giriş Başarısız</p>";
                }
                else
                {
                    var haklar = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email, kullanici.Email),
                        new Claim(ClaimTypes.Name, kullanici.Name),
                        new Claim(ClaimTypes.Role, kullanici.IsAdmin? "Admin" : "Personal"),


                    };
                    var kullaniciKimligi = new ClaimsIdentity(haklar, "Login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(kullaniciKimligi);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    string donusAdresi = HttpContext.Request.Query["ReturnUrl"];
                    if (!string.IsNullOrEmpty(donusAdresi))
                    {
                        return Redirect(donusAdresi);
                    }
                    return Redirect("/Admin");

                }
            }
            catch (Exception hata)
            {

                TempData["Message"] = hata.InnerException?.Message;
            }
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
