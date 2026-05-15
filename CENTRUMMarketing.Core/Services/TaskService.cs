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

            customer.LastActivityDate = DateTime.Now;

            _taskRepository.Save();
            _customerRepository.Save();

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

        public List<TaskItem> GetTasksByCollaboratorId(int collaboratorId)
        {
            List<TaskItem> results = new List<TaskItem>();

            foreach (TaskItem task in _taskRepository.GetAll())
            {
                if (task.CollaboratorIds.Contains(collaboratorId))
                {
                    results.Add(task);
                }
            }

            return results;
        }

        public List<TaskItem> GetTasksWithUpcomingDeadlines(int daysAhead)
        {
            List<TaskItem> results = new List<TaskItem>();

            DateTime today = DateTime.Today;
            DateTime maxDeadline = today.AddDays(daysAhead);

            foreach (TaskItem task in _taskRepository.GetAll())
            {
                bool isUpcoming = task.Deadline.Date >= today && task.Deadline.Date <= maxDeadline;
                bool isNotCompleted = task.Status != TaskItemStatus.Completed;

                if (isUpcoming && isNotCompleted) results.Add(task);
            }

            results.Sort((task1, task2) => task1.Deadline.CompareTo(task2.Deadline));

            return results;
        }

        public void DeleteTask(int taskId)
        {
            TaskItem? task = _taskRepository.GetById(taskId);
            
            if (task == null)

            {
                throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");
            }

            bool deleted = _taskRepository.Delete(taskId);

            if (!deleted)
            {
                throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");
            }

            Customer? customer = _customerRepository.GetById(task.CustomerId);

            if (customer != null)
            {
                customer.Tasks.RemoveAll(t => t.Id == taskId);
                customer.LastActivityDate = DateTime.Now;
                _customerRepository.Save();
            }
        }

        public void UpdateTaskStatus(int taskId, TaskItemStatus newStatus)
        {
            TaskItem? task = _taskRepository.GetById(taskId) ?? throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");

            task.Status = newStatus;

            _taskRepository.Save();
            UpdateCustomerLastActivityDate(task.CustomerId);
        }

        public void UpdateTaskDeadline(int taskId, DateTime newDeadline)
        {
            TaskItem? task = _taskRepository.GetById(taskId) ?? throw new EntityNotFoundException(
                    $"Task with ID {taskId} was not found.");

            if (newDeadline < DateTime.Now)
            {
                throw new InvalidDeadlineException(
                    "Deadline cannot be in the past.");
            }

            task.Deadline = newDeadline;

            _taskRepository.Save();
            UpdateCustomerLastActivityDate(task.CustomerId);
        }

        public void AssignCollaboratorToTask(TaskItem task, int collaboratorId)
        {
            if (task.CollaboratorIds.Contains(collaboratorId))
            {
                return;
            }

            task.CollaboratorIds.Add(collaboratorId);
            _taskRepository.Save();
        }

        public void SaveChanges()
        {
            _taskRepository.Save();
        }

        private void UpdateCustomerLastActivityDate(int customerId)
        {
            Customer? customer = _customerRepository.GetById(customerId);

            if (customer == null)
            {
                throw new EntityNotFoundException(
                    $"Customer with ID {customerId} not found.");
            }

            customer.LastActivityDate = DateTime.Now;
            _customerRepository.Save();
        }
    }
}
