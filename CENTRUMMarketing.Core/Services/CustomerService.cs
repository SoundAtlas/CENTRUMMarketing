using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Repositories;

namespace CENTRUMMarketing.Core.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private int _nextId = 1;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer AddCustomer(string companyName, string contactPerson, string cvr, string email, string phone, CustomerStatus status)
        {
            Customer customer = new Customer(_nextId, companyName, contactPerson, cvr, email, phone, status);

            _customerRepository.Add(customer);

            _nextId++;

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public List<Customer> SearchCustomersByName(string searchTerm)
        {
            List<Customer> results = new List<Customer>();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return results;
            }

            searchTerm = searchTerm.Trim();

            foreach (Customer customer in _customerRepository.GetAll())
            {
                bool companyNameMatches = customer.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                bool contactPersonMatches = customer.ContactPerson.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);

                if (companyNameMatches || contactPersonMatches)
                {
                    results.Add(customer);
                }
            }

            return results;
        }

        public List<Customer> GetArchivedCustomers()
        {
            List<Customer> archivedCustomers = new List<Customer>();

            foreach (var customer in _customerRepository.GetAll())
            {
                if (customer.IsArchived())
                {
                    archivedCustomers.Add(customer);
                }
            }

            return archivedCustomers;
        }
    }
}
