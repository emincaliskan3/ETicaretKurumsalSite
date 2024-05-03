using DataAccessLayer;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace ETicaretKurumsalSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly DatabaseContext _dbContext;
        private readonly IService<Product> _service;
        public ProductsController(ICategoryService categoryService, DatabaseContext dbContext, IService<Product> service)
        {
            _categoryService = categoryService;
            _dbContext = dbContext;
            _service = service;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var databaseContext = _dbContext.Products.Include(p => p.Category);
            return View(await databaseContext.ToListAsync());
        }
        public async Task<IActionResult> Search(string q)
        {
            return View(await _dbContext.Products.Where(p => p.IsActive && p.Name.Contains(q)).Include("Category").ToListAsync());
        }
        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _dbContext.Products.Where(x => x.Id == id && x.IsActive).Include(x => x.Category).FirstOrDefaultAsync();
            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
