using DataAccessLayer;
using EntityLayer;
using ETicaretKurumsalSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaretKurumsalSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {


                Categories = _context.Categories.Where(p => p.IsActive && p.IsHome).ToList(),
                Products = _context.Products.Where(p => p.IsActive && p.IsHome).ToList(),
                Slides = _context.Slides.ToList(),



            };
            return View(model);
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult Contact()
        {


            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Contacts.Add(contact);
                    var sonuc = _context.SaveChanges();
                    if (sonuc > 0)
                    {
                        TempData["Mesaj"] = "<div class = 'alert alert-success'>Teþekkürler.. Mesajýnýz Bize Ulaþtý..</div>";

                    }
                }
                catch (Exception)
                {

                    TempData["Mesaj"] = "<div class = 'alert alert-danger'>Hata Oluþtu! Mesajýnýz Gönderilemedi!</div>";
                }
            }


            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
