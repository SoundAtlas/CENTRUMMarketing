using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Interfaces;
using CENTRUMMarketing.Core.Repositories;

namespace CENTRUMMarketing.Core.Services
{
    public class DashboardService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly TaskRepository _taskRepository;

        public DashboardService(
            CustomerRepository customerRepository,
            TaskRepository taskRepository)
        {
            _customerRepository = customerRepository;
            _taskRepository = taskRepository;
        }

        public int GetActiveCustomersCount()
        {
            return _customerRepository
                .GetAll()
                .Count(c => c.Status == CustomerStatus.Active);
        }
    }
}
