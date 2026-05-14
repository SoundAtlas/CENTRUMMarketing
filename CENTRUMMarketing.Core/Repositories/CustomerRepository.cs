using CENTRUMMarketing.Core.Interfaces;
using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly JsonRepository<Customer> _jsonRepository;

        public CustomerRepository()
        {
            _jsonRepository = new JsonRepository<Customer>(@"..\..\..\..\CENTRUMMarketing.Core\Data\customers.json");
        }


        public List<Customer> GetAll()
        {
            return _jsonRepository.GetAll();
        }

        public void Add(Customer customer)
        {
            _jsonRepository.Add(customer);
        }

        public Customer? GetById(int id)
        {
            foreach (Customer customer in _jsonRepository.GetAll())
            {
                if (customer.Id == id)
                {
                    return customer;
                }
            }

            return null;
        }



        public void Save()
        {
            _jsonRepository.Save();
        }


    }
}
