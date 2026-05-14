using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Exceptions;
using CENTRUMMarketing.Core.Interfaces;
using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICustomerRepository _customerRepository;
        private int _nextId;



        public TaskService(
           ITaskRepository taskRepository,
           ICustomerRepository customerRepository)
        {
            _taskRepository = taskRepository;
            _customerRepository = customerRepository;

            // Initialize _nextId based on existing tasks
            List<TaskItem> existingTasks = _taskRepository.GetAll();

            if (existingTasks.Count > 0)
            {
                _nextId = existingTasks.Max(t => t.Id) + 1;
            }
            else
            {
                _nextId = 1;
            }
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
                throw new EntityNotFoundException(
                    $"Customer with ID {customerId} not found.");
            }

            if (deadline < DateTime.Now)
            {
                throw new InvalidDeadlineException(
                    "Deadline cannot be in the past.");
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

        public void UpdateTaskStatus(int taskId, TaskItemStatus newStatus)
        {
            TaskItem? task = _taskRepository.GetById(taskId) ?? throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");

            task.Status = newStatus;
            _taskRepository.Save();
        }

        public void UpdateTaskDeadline(int taskId, DateTime newDeadline)
        {
            TaskItem? task = _taskRepository.GetById(taskId) ?? throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");

            task.Deadline = newDeadline;
            _taskRepository.Save();

        }

        public void AssignCollaboratorToTask(TaskItem task, int collaboratorId)
        {
            task.CollaboratorId = collaboratorId;
            _taskRepository.Save();
        }

        public void SaveChanges()
        {
            _taskRepository.Save();
        }

    }
}
