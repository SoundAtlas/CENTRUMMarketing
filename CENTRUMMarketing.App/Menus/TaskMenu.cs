using CENTRUMMarketing.App.Helpers;
using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Exceptions;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class TaskMenu
    {
        private TaskService _taskService;
        private CustomerService _customerService;
        private CollaboratorService _collaboratorService;

        public TaskMenu(TaskService taskService, CustomerService customerService, CollaboratorService collaboratorService)
        {
            _taskService = taskService;
            _customerService = customerService;
            _collaboratorService = collaboratorService;
        }

        public void ShowTaskMenu()
        {
            bool inTaskMenu = true;

            while (inTaskMenu)
            {
                ConsoleHelpers.Headers("TASK MANAGEMENT");

                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. View Tasks By Customer");
                Console.WriteLine("0. Back");
                Console.WriteLine("=======================================");

                int choice = InputHelpers.ReadInt("Choose an option: ");

                switch (choice)
                {
                    case 1:
                        AddTaskFlow();
                        break;

                    case 2:
                        ViewAllTasksFlow();
                        break;

                    case 3:
                        ViewTasksByCustomerFlow();
                        break;

                    case 0:
                        inTaskMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }


        private void AddTaskFlow()
        {
            ConsoleHelpers.Headers("ADD TASK");

            int customerId = InputHelpers.ReadInt("Customer ID: ");

            string title = InputHelpers.ReadRequiredString("Task title: ");

            string description = InputHelpers.ReadRequiredString("Task description: ");

            DateTime deadline = InputHelpers.ReadDate("Deadline (yyyy-mm-dd): ");

            Console.WriteLine("Choose task status: ");
            Console.WriteLine("1. To Do");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Waiting Client");
            Console.WriteLine("4. Completed");

            int statusChoice = InputHelpers.ReadInt("Choice: ");

            TaskItemStatus status = TaskItemStatus.ToDo;

            switch (statusChoice)
            {
                case 1:
                    status = TaskItemStatus.ToDo;
                    break;
                case 2:
                    status = TaskItemStatus.InProgress;
                    break;
                case 3:
                    status = TaskItemStatus.WaitingClient;
                    break;
                case 4:
                    status = TaskItemStatus.Completed;
                    break;
                default:
                    Console.WriteLine("Invalid status choice.");
                    ConsoleHelpers.Pause();
                    return;
            }

            try
            {
                TaskItem? task = _taskService.AddTask(
                    customerId,
                    title,
                    description,
                    deadline,
                    status);

                Customer? customer = _customerService.GetCustomerById(customerId);

                Console.WriteLine();
                Console.WriteLine("Task created successfully.");
                Console.WriteLine($"Task: {task.Title}");
                Console.WriteLine($"Customer: {customer?.CompanyName}");
                Console.WriteLine($"Deadline: {task.Deadline.ToShortDateString()}");
                Console.WriteLine($"Status: {task.Status}");

            }

            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            catch (InvalidDeadlineException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            ConsoleHelpers.Pause();

        }

        private void ViewAllTasksFlow()
        {

            List<TaskItem> tasks = _taskService.GetAllTasks();

            ConsoleHelpers.Headers("ALL TASKS");


            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (TaskItem t in tasks)
                {
                    Customer? customer = _customerService.GetCustomerById(t.CustomerId);

                    string customerName = "Unknown Customer";

                    if (customer != null)
                    {
                        customerName = customer.CompanyName;
                    }

                    Console.WriteLine($"ID: {t.Id} | Customer: {customerName} | Title: {t.Title} | Status: {t.Status} | Deadline: {t.Deadline.ToShortDateString()}");
                }
            }
            Console.WriteLine("=======================================");

            int id = InputHelpers.ReadInt("Enter task ID to view details (0 to return): ");

            if (id == 0)
            {
                return;
            }

            TaskItem? task = _taskService.GetTaskById(id);

            if (task != null)
            {
                ShowTaskDetails(task);
            }

            else
            {
                Console.WriteLine("Task not found.");
                ConsoleHelpers.Pause();
            }

        }

        private void ViewTasksByCustomerFlow()
        {
            ConsoleHelpers.Headers("VIEW TASKS BY CUSTOMER");

            int customerId = InputHelpers.ReadInt("Enter Customer ID: ");

            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                ConsoleHelpers.Pause();
                return;
            }

            List<TaskItem> tasks = _taskService.GetTasksByCustomerId(customerId);

            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.WriteLine($"  TASKS FOR {customer.CompanyName}");
            Console.WriteLine("=======================================");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found for this customer.");
                ConsoleHelpers.Pause();
                return;
            }
            else
            {
                foreach (TaskItem task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title} - {task.Deadline.ToShortDateString()} - {task.Status}");
                }
            }

            Console.WriteLine("=======================================");

            int taskId = InputHelpers.ReadInt("Enter task ID to view details (0 to return): ");

            if (taskId == 0)
            {
                return;
            }

            TaskItem? selectedTask = _taskService.GetTaskById(taskId);

            if (selectedTask == null || selectedTask.CustomerId != customerId)
            {
                Console.WriteLine("Task not found for this customer.");
                ConsoleHelpers.Pause();
                return;
            }

            ShowTaskDetails(selectedTask);

        }


        private void ShowTaskDetails(TaskItem task)
        {
            bool inDetailsMenu = true;

            while (inDetailsMenu)
            {

                Customer? customer = _customerService.GetCustomerById(task.CustomerId);

                string customerName = "Unknown customer";

                if (customer != null)
                {
                    customerName = customer.CompanyName;
                }

                ConsoleHelpers.Headers("TASK DETAILS");
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Customer: {customerName}");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Deadline: {task.Deadline.ToShortDateString()}");
                Console.WriteLine($"Status: {task.Status}");

                Console.Write("Collaborators: ");

                bool hasCollaborators = false;

                foreach (Collaborator collaborator in _collaboratorService.GetAllCollaborators())
                {
                    if (task.CollaboratorIds.Contains(collaborator.Id))
                    {
                        Console.WriteLine($"{collaborator.Name}");
                        hasCollaborators = true;
                    }
                }

                if (!hasCollaborators)
                    Console.WriteLine("None");

                Console.WriteLine("=======================================");
                Console.WriteLine("1. Edit Task");
                Console.WriteLine("2. Update Status");
                Console.WriteLine("3. Update Deadline");
                Console.WriteLine("4. Assign Collaborator");
                Console.WriteLine("5. Remove Collaborator");
                Console.WriteLine("6. Delete Task");
                Console.WriteLine("0. Back");
                Console.WriteLine("=======================================");

                int choice = InputHelpers.ReadInt("Choose an option: ");

                switch (choice)
                {
                    case 1:
                        EditTaskFlow(task);
                        break;

                    case 2:
                        UpdateTaskItemStatusFlow(task);
                        break;

                    case 3:
                        UpdateTaskDeadlineFlow(task);
                        break;

                    case 4:
                        AssignCollaboratorFlow(task);
                        break;

                    case 5:
                        RemoveCollaboratorFlow(task);
                        break;

                    case 6:
                        if (DeleteTaskFlow(task))
                        {
                            inDetailsMenu = false;
                        }
                        break;

                    case 0:
                        inDetailsMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void EditTaskFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("EDIT TASK");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose field to edit: ");

            switch (choice)
            {
                case 1:
                    task.Title = InputHelpers.ReadRequiredString("Enter new title: ");
                    break;

                case 2:
                    task.Description = InputHelpers.ReadRequiredString("Enter new description: ");
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            _taskService.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("Task updated successfully.");
            ConsoleHelpers.Pause();
        }

        private void UpdateTaskItemStatusFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("UPDATE TASK STATUS");
            Console.WriteLine("1. To Do");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Waiting Client");
            Console.WriteLine("4. Completed");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose new status: ");

            TaskItemStatus newStatus;

            switch (choice)
            {
                case 1:
                    newStatus = TaskItemStatus.ToDo;
                    break;

                case 2:
                    newStatus = TaskItemStatus.InProgress;
                    break;

                case 3:
                    newStatus = TaskItemStatus.WaitingClient;
                    break;

                case 4:
                    newStatus = TaskItemStatus.Completed;
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            try
            {
                _taskService.UpdateTaskStatus(task.Id, newStatus);
                Console.WriteLine($"Task status updated to: {newStatus}");
            }

            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            ConsoleHelpers.Pause();
        }

        private void UpdateTaskDeadlineFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("UPDATE TASK DEADLINE");
            Console.WriteLine($"Current deadline: {task.Deadline:d}");
            Console.WriteLine("=======================================");

            DateTime newDeadline = InputHelpers.ReadDate("Enter new deadline (yyyy-mm-dd): ");

            try
            {
                _taskService.UpdateTaskDeadline(task.Id, newDeadline);
                Console.WriteLine($"Task deadline updated to: {newDeadline:d}");
            }

            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            catch (InvalidDeadlineException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            ConsoleHelpers.Pause();

        }

        private bool DeleteTaskFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("DELETE TASK");

            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Title: {task.Title}");
            Console.WriteLine($"Deadline: {task.Deadline:d}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine("=======================================");
            Console.WriteLine("This action cannot be undone.");
            Console.WriteLine();

            string confirmation = InputHelpers.ReadRequiredString("Type DELETE to confirm: ");

            if (!confirmation.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Deletion cancelled.");
                ConsoleHelpers.Pause();
                return false;
            }

            try
            {
                _taskService.DeleteTask(task.Id);

                Console.WriteLine();
                Console.WriteLine("Task deleted successfully.");
                ConsoleHelpers.Pause();

                return true;
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                ConsoleHelpers.Pause();

                return false;
            }
        }

        private void AssignCollaboratorFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("ASSIGN COLLABORATOR");

            List<Collaborator> collaborators = _collaboratorService.GetAllCollaborators();

            if (collaborators.Count == 0)
            {
                Console.WriteLine("No collaborators found.");
                ConsoleHelpers.Pause();
                return;
            }

            bool availableCollaborators = false;

            foreach (Collaborator c in collaborators)
            {
                if (!task.CollaboratorIds.Contains(c.Id))
                {
                    Console.WriteLine($"{c.Id}. {c.Name}");
                    availableCollaborators = true;
                }
            }

            if (!availableCollaborators)
            {
                Console.WriteLine("All collaborators are already assigned to this task.");
                ConsoleHelpers.Pause();
                return;
            }

            Console.WriteLine();

            int collaboratorId = InputHelpers.ReadInt("Enter collaborator ID to assign: ");

            Collaborator? collaborator = null;

            foreach (Collaborator c in collaborators)
            {
                if (c.Id == collaboratorId)
                {
                    collaborator = c;
                    break;
                }
            }

            if (collaborator == null)
            {
                Console.WriteLine("Collaborator not found.");
                ConsoleHelpers.Pause();
                return;
            }

            _taskService.AssignCollaboratorToTask(task, collaboratorId);

            Console.WriteLine($"Assigned {collaborator.Name} to {task.Title}");
            ConsoleHelpers.Pause();

        }

        private void RemoveCollaboratorFlow(TaskItem task)
        {
            ConsoleHelpers.Headers("REMOVE COLLABORATOR");

            if (task.CollaboratorIds.Count == 0)
            {
                Console.WriteLine("No collaborators assigned to this task.");
                ConsoleHelpers.Pause();
                return;
            }

            List<Collaborator> assignedCollaborators = _collaboratorService.GetAllCollaborators();

            foreach (Collaborator c in assignedCollaborators)
            {
                if (task.CollaboratorIds.Contains(c.Id))
                {
                    Console.WriteLine($"{c.Id}. {c.Name}");
                }
            }

            Console.WriteLine();

            int collaboratorId = InputHelpers.ReadInt("Enter collaborator ID to remove: ");

            if (!task.CollaboratorIds.Contains(collaboratorId))
            {
                Console.WriteLine("That collaborator is not assigned to this task.");
                ConsoleHelpers.Pause();
                return;
            }

            task.CollaboratorIds.Remove(collaboratorId);

            _taskService.SaveChanges();

            Console.WriteLine();
            Console.WriteLine("Collaborator removed from task.");

            ConsoleHelpers.Pause();
        }
    }
}
