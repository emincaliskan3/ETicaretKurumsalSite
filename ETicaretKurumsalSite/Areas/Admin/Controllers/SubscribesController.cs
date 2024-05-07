using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;

namespace ETicaretKurumsalSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "UserPolicy")]
    public class SubscribesController : Controller
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribesController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        // GET: Admin/Subscribes
        public async Task<IActionResult> Index()
        {
            var subscribes = await _subscribeService.GetSubscribesAsync();
            return View(subscribes);
        }

        // GET: Admin/Subscribes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribe = await _subscribeService.GetSubscribeAsync(id.Value);
            if (subscribe == null)
            {
                return NotFound();
            }

            return View(subscribe);
        }

        // GET: Admin/Subscribes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email")] Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                await _subscribeService.AddSubscribeAsync(subscribe);
                return RedirectToAction(nameof(Index));
            }
            return View(subscribe);
        }

        // GET: Admin/Subscribes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribe = await _subscribeService.GetSubscribeAsync(id.Value);
            if (subscribe == null)
            {
                return NotFound();
            }
            return View(subscribe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email")] Subscribe subscribe)
        {
            if (id != subscribe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subscribeService.UpdateSubscribeAsync(subscribe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _subscribeService.SubscribeExistsAsync(subscribe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subscribe);
        }

        // GET: Admin/Subscribes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribe = await _subscribeService.GetSubscribeAsync(id.Value);
            if (subscribe == null)
            {
                return NotFound();
            }

            return View(subscribe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subscribeService.DeleteSubscribeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
