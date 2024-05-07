using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using ETicaretKurumsalSite.Tools;

namespace ETicaretKurumsalSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "UserPolicy")]
    public class SlidesController : Controller
    {
        private readonly ISlideService _slideService;

        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        // GET: Admin/Slides
        public async Task<IActionResult> Index()
        {
            var slides = await _slideService.GetSlidesAsync();
            return View(slides);
        }

        // GET: Admin/Slides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _slideService.GetSlideAsync(id.Value);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // GET: Admin/Slides/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                slide.Image = await FileHelper.FileLoaderAsync(Image);
                await _slideService.AddSlideAsync(slide);
                return RedirectToAction(nameof(Index));
            }
            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _slideService.GetSlideAsync(id.Value);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slide slide, IFormFile? Image)
        {
            if (id != slide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        slide.Image = await FileHelper.FileLoaderAsync(Image);
                    }

                    await _slideService.UpdateSlideAsync(slide);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _slideService.SlideExistsAsync(slide.Id))
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
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _slideService.GetSlideAsync(id.Value);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _slideService.DeleteSlideAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
