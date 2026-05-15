using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Interfaces;


namespace CENTRUMMarketing.Core.Services
{
    public class DashboardService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITaskRepository _taskRepository;


        public DashboardService(
            ICustomerRepository customerRepository,
            ITaskRepository taskRepository)
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

        public int GetLeadsCount()
        {
            return _customerRepository
                .GetAll()
                .Count(c => c.Status == CustomerStatus.Lead);
        }

        public int GetInvoicingReadyCustomersCount()
        {
            return _customerRepository
                .GetAll()
                .Count(c => c.InvoicingReady);
        }

        public int GetTasksDueThisWeekCount()
        {
            DateTime today = DateTime.Today;
            DateTime weekFromNow = today.AddDays(7);

            return _taskRepository
                .GetAll()
                .Count(t =>
                    t.Deadline.Date >= today &&
                    t.Deadline.Date <= weekFromNow &&
                    t.Status != TaskItemStatus.Completed);
        }

        public int GetWaitingClientTasksCount()
        {
            return _taskRepository
                .GetAll()
                .Count(t => t.Status == TaskItemStatus.WaitingClient);
        }

        public int GetCompletedTasksCount()
        {
            return _taskRepository
                .GetAll()
                .Count(t => t.Status == TaskItemStatus.Completed);
        }
    }
}
