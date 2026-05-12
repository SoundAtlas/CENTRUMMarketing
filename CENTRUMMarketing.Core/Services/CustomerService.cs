using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Services
{
    public class CustomerService
    {
        private List<Customer> _customers = new List<Customer>();
        private int _nextId = 1;

        public Customer AddCustomer(string companyName, string contactPerson, string cvr, string email, string phone, CustomerStatus status)
        {
            Customer customer = new Customer(_nextId, companyName, contactPerson, cvr, email, phone, status);

            _customers.Add(customer);
            _nextId++;

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer? GetCustomerById(int id)
        {
            foreach (var customer in _customers)
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
