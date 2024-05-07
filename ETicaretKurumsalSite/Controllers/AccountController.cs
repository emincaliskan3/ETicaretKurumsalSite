using DataAccessLayer;
using EntityLayer;
using ETicaretKurumsalSite.ExtensionMethods;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretKurumsalSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(string kemail, string kpassword)
        {
            try
            {
                var kullanici = await _context.Customers.FirstOrDefaultAsync(u => u.Email == kemail && u.Password == kpassword);
                if (kullanici != null)
                {
                    HttpContext.Session.SetString("kullanici", kullanici.Name);
                    HttpContext.Session.SetString("soyad", kullanici.Surname);
                    HttpContext.Session.SetString("hesap", kullanici.Email);
                    HttpContext.Session.SetString("tel", kullanici.Phone);
                    HttpContext.Session.SetString("sifre", kpassword);
                    HttpContext.Session.SetInt32("IsLoggedIn", 1);
                    HttpContext.Session.SetJson("musteri", kullanici);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "<p class='alert alert-danger'>Giriş Başarısız!</p>";
                }
            }
            catch (Exception hata)
            {
                TempData["Message"] = hata.InnerException?.Message;
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,Surname,Email,Phone,Password,CreateDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(customer);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            HttpContext.Session.Remove("kullanici");
            HttpContext.Session.Remove("soyad");
            HttpContext.Session.Remove("hesap");
            HttpContext.Session.Remove("tel");
            HttpContext.Session.Remove("sifre");
            HttpContext.Session.SetInt32("IsLoggedIn", 0);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Update()
        {
            var kullaniciEmail = HttpContext.Session.GetString("hesap");
            var kullanici = await _context.Customers.FirstOrDefaultAsync(u => u.Email == kullaniciEmail);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Surname,Email,Phone,Password")] Customer updatedCustomer)
        {
            if (id != updatedCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedCustomer);
                    await _context.SaveChangesAsync();

                    // Güncellenmiş bilgileri oturum verilerine yazma
                    HttpContext.Session.SetString("kullanici", updatedCustomer.Name);
                    HttpContext.Session.SetString("soyad", updatedCustomer.Surname);
                    HttpContext.Session.SetString("hesap", updatedCustomer.Email);
                    HttpContext.Session.SetString("tel", updatedCustomer.Phone);
                    HttpContext.Session.SetString("sifre", updatedCustomer.Password);


                    return RedirectToAction("Index", "Account");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(updatedCustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(updatedCustomer);
        }



        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}
