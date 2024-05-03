using DataAccessLayer;
using EntityLayer;
using ETicaretKurumsalSite.ExtensionMethods;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            HttpContext.Session.SetInt32("IsLoggedIn", 0);

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
