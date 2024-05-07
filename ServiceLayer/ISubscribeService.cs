using EntityLayer;

namespace ServiceLayer
{
    public interface ISubscribeService
    {
        Task<List<Subscribe>> GetSubscribesAsync();
        Task<Subscribe> GetSubscribeAsync(int id);
        Task AddSubscribeAsync(Subscribe subscribe);
        Task UpdateSubscribeAsync(Subscribe subscribe);
        Task DeleteSubscribeAsync(int id);
        Task<bool> SubscribeExistsAsync(int id);
    }
}
