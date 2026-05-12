using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }
    }
}
