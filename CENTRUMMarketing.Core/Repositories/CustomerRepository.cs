using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public Customer? GetById(int id)
        {
            foreach (Customer customer in _customers)
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }

            return null;
        }
    }
}
