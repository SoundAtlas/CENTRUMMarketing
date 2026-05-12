using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Services
{
    public class TaskService
    {
        private List<TaskItem> _tasks = new List<TaskItem>();
        private int _nextId = 1;

        private CustomerService _customerService;

        public TaskService(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public TaskItem? AddTask(
            int customerId,
            string title,
            string description,
            DateTime deadline,
            TaskItemStatus status)
        {
            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                return null;
            }

            TaskItem task = new TaskItem(
                _nextId,
                customerId,
                title,
                description,
                deadline,
                status);

            _tasks.Add(task);
            customer.Tasks.Add(task);

            _nextId++;

            return task;
        }

        public List<TaskItem> GetAllTasks()
        {
            return _tasks;
        }

        public List<TaskItem> GetTasksByCustomerId(int customerId)
        {
            List<TaskItem> customerTasks = new List<TaskItem>();

            foreach (TaskItem task in _tasks)
            {
                if (task.CustomerId == customerId)
                {
                    customerTasks.Add(task);
                }
            }

            return customerTasks;
        }


        public TaskItem? GetTaskById(int id)
        {
            foreach (TaskItem task in _tasks)
            {
                if (task.Id == id)
                {
                    return task;
                }
            }

            return null;
        }

    }
}
