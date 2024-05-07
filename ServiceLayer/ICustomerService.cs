using EntityLayer;

namespace ServiceLayer
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<bool> CustomerExists(int id);
    }
}
