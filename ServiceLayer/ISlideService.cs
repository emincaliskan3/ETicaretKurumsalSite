using EntityLayer;

namespace ServiceLayer
{
    public interface ISlideService
    {
        Task<List<Slide>> GetSlidesAsync();
        Task<Slide> GetSlideAsync(int id);
        Task AddSlideAsync(Slide slide);
        Task UpdateSlideAsync(Slide slide);
        Task DeleteSlideAsync(int id);
        Task<bool> SlideExistsAsync(int id);
    }
}
