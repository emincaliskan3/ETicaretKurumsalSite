using Microsoft.AspNetCore.Mvc;

namespace ETicaretKurumsalSite.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
