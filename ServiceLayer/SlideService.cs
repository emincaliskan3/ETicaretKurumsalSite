using DataAccessLayer;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class SlideService : ISlideService
    {
        private readonly DatabaseContext _context;

        public SlideService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Slide>> GetSlidesAsync()
        {
            return await _context.Slides.ToListAsync();
        }

        public async Task<Slide> GetSlideAsync(int id)
        {
            return await _context.Slides.FindAsync(id);
        }

        public async Task AddSlideAsync(Slide slide)
        {
            _context.Add(slide);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSlideAsync(Slide slide)
        {
            _context.Update(slide);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlideAsync(int id)
        {
            var slide = await _context.Slides.FindAsync(id);
            if (slide != null)
            {
                _context.Slides.Remove(slide);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SlideExistsAsync(int id)
        {
            return await _context.Slides.AnyAsync(e => e.Id == id);
        }
    }
}
