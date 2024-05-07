using Entities;
using EntityLayer;
using ETicaretKurumsalSite.ExtensionMethods;
using ETicaretKurumsalSite.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace ETicaretKurumsalSite.Controllers
{

    public class CartController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<Order> _orderService;

        public CartController(IService<Product> productService, IService<Order> orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var model = new CartViewModel();
            model.Cart = GetCart();
            return View(model); // sepet sayfasına sepetteki ürünleri gönderiyoruz
        }
        [HttpPost]
        public IActionResult Index(CartViewModel cartViewModel)
        {
            cartViewModel.Cart = GetCart();
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.Add(cartViewModel.Order);
                    _orderService.Save();
                    TempData["ordermessage"] = "<div class='alert alert-success'>Siparişiniz Alındı!</div>";
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("OrderResult");
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(cartViewModel);
        }
        public IActionResult OrderResult(CartViewModel cartViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.Add(cartViewModel.Order);
                    _orderService.Save();
                    TempData["ordermessage"] = "<div class='alert alert-success'>Siparişiniz Alındı!</div>";
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("OrderResult");
                }
                catch (Exception hata)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(cartViewModel);
        }
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.Get(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var product = _productService.Get(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }
    }
}
