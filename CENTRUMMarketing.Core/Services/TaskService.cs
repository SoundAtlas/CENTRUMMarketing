using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Repositories;

namespace CENTRUMMarketing.Core.Services
{
    public class TaskService
    {
        private readonly TaskRepository _taskRepository;
        private readonly CustomerRepository _customerRepository;
        private int _nextId = 1;



        public TaskService(
           TaskRepository taskRepository,
           CustomerRepository customerRepository)
        {
            _taskRepository = taskRepository;
            _customerRepository = customerRepository;
        }

        public TaskItem? AddTask(
            int customerId,
            string title,
            string description,
            DateTime deadline,
            TaskItemStatus status)
        {
            Customer? customer = _customerRepository.GetById(customerId);

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

            _taskRepository.Add(task);
            customer.Tasks.Add(task);

            _nextId++;

            return task;
        }

        public List<TaskItem> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }

        public List<TaskItem> GetTasksByCustomerId(int customerId)
        {
            List<TaskItem> customerTasks = new List<TaskItem>();

            foreach (TaskItem task in _taskRepository.GetAll())
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
            return _taskRepository.GetById(id);
        }

        public List<TaskItem> GetTasksByStatus(TaskItemStatus status)
        {
            List<TaskItem> results = new List<TaskItem>();

            foreach (TaskItem task in _taskRepository.GetAll())
            {
                if (task.Status == status)
                {
                    results.Add(task);
                }
            }

            return results;
        }

        public bool UpdateTaskStatus(int taskId, TaskItemStatus newStatus)
        {
            TaskItem? task = _taskRepository.GetById(taskId);

            if (task == null)
            {
                return false;
            }

            task.Status = newStatus;
            _taskRepository.Save();
            return true;
        }

        public bool UpdateTaskDeadline(int taskId, DateTime newDeadline)
        {
            TaskItem? task = _taskRepository.GetById(taskId);

            if (task == null)
            {
                return false;
            }

            task.Deadline = newDeadline;
            _taskRepository.Save();
            return true;
        }


    }
}
