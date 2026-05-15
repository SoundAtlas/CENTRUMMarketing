using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer? GetById(int id);
        void Add(Customer customer);
        bool Delete(int id);
        void Save();
    }
}
