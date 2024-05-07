using DataAccessLayer;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class SubscribeService : ISubscribeService
    {
        private readonly DatabaseContext _context;

        public SubscribeService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Subscribe>> GetSubscribesAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }

        public async Task<Subscribe> GetSubscribeAsync(int id)
        {
            return await _context.Subscribes.FindAsync(id);
        }

        public async Task AddSubscribeAsync(Subscribe subscribe)
        {
            _context.Add(subscribe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscribeAsync(Subscribe subscribe)
        {
            _context.Update(subscribe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscribeAsync(int id)
        {
            var subscribe = await _context.Subscribes.FindAsync(id);
            if (subscribe != null)
            {
                _context.Subscribes.Remove(subscribe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SubscribeExistsAsync(int id)
        {
            return await _context.Subscribes.AnyAsync(e => e.Id == id);
        }
    }
}
