using System.Linq.Expressions;

namespace ServiceLayer
{
    public interface IService<T>
    {
        #region Senkron Metotlar

        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        #endregion

        #region ASenkron Metotlar

        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIncludeAsync(Expression<Func<T, bool>> expression, string table);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        #endregion
    }
}
