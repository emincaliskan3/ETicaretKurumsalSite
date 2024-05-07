using DataAccessLayer;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await SaveAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await SaveAsync();
            }
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await SaveAsync();
        }

        private async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
