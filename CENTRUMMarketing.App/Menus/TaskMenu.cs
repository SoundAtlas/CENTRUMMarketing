using CENTRUMMarketing.App.Helpers;
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

        public TaskMenu(TaskService taskService, CustomerService customerService)
        {
            _taskService = taskService;
            _customerService = customerService;
        }

        public void ShowTaskMenu()
        {
            bool inTaskMenu = true;

            while (inTaskMenu)
            {
                Console.Clear();

                Console.WriteLine("=======================================");
                Console.WriteLine("             TASK MANAGEMENT           ");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View All Tasks");
                Console.WriteLine("3. View Tasks By Customer");
                Console.WriteLine("4. Update Task Status");
                Console.WriteLine("0. Back");
                Console.WriteLine("=======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTaskFlow();
                        break;

                    case "2":
                        ViewAllTasksFlow();
                        break;

                    case "3":
                        ViewTasksByCustomerFlow();
                        break;

                    case "0":
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
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("               ADD TASK                ");
            Console.WriteLine("=======================================");

            int customerId = InputHelpers.ReadInt("Customer ID: ");

            string title = InputHelpers.ReadRequiredString("Task title: ");

            string description = InputHelpers.ReadRequiredString("Task description: ");

            DateTime deadline = InputHelpers.ReadDate("Deadline (yyyy-mm-dd): ");

            Console.WriteLine("Choose task status: ");
            Console.WriteLine("1. To Do");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Waiting Client");
            Console.WriteLine("4. Completed");
            Console.Write("Choice: ");

            string statusChoice = Console.ReadLine();

            TaskItemStatus status = TaskItemStatus.ToDo;

            switch (statusChoice)
            {
                case "1":
                    status = TaskItemStatus.ToDo;
                    break;
                case "2":
                    status = TaskItemStatus.InProgress;
                    break;
                case "3":
                    status = TaskItemStatus.WaitingClient;
                    break;
                case "4":
                    status = TaskItemStatus.Completed;
                    break;
                default:
                    Console.WriteLine("Invalid status choice. Defaulting to 'To Do'.");
                    break;
            }

            try
            {
                TaskItem? task = _taskService.AddTask(
                    customerId,
                    title,
                    description,
                    deadline,
                    status);

                Console.WriteLine();
                Console.WriteLine("Task created successfully.");

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

            Console.Write("Press any key to continue...");
            Console.ReadKey();

        }

        private void ViewAllTasksFlow()
        {
            Console.Clear();
            List<TaskItem> tasks = _taskService.GetAllTasks();

            Console.WriteLine("=======================================");
            Console.WriteLine("               ALL TASKS               ");
            Console.WriteLine("=======================================");


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
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }

        }

        private void ViewTasksByCustomerFlow()
        {
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("         VIEW TASKS BY CUSTOMER        ");
            Console.WriteLine("=======================================");

            int customerId = InputHelpers.ReadInt("Enter Customer ID: ");

            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
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
            }
            else
            {
                foreach (TaskItem task in tasks)
                {
                    Console.WriteLine($"{task.Id}. {task.Title} - {task.Deadline.ToShortDateString()} - {task.Status}");
                }
            }

            Console.WriteLine("=======================================");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }


        private void ShowTaskDetails(TaskItem task)
        {
            bool inDetailsMenu = true;

            while (inDetailsMenu)
            {
                Console.Clear();

                Customer? customer = _customerService.GetCustomerById(task.CustomerId);

                string customerName = "Unknown customer";

                if (customer != null)
                {
                    customerName = customer.CompanyName;
                }

                Console.WriteLine("=======================================");
                Console.WriteLine("              TASK DETAILS");
                Console.WriteLine("=======================================");
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Customer: {customerName}");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Deadline: {task.Deadline.ToShortDateString()}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Edit Task");
                Console.WriteLine("2. Update Status");
                Console.WriteLine("3. Update Deadline");
                Console.WriteLine("0. Back");
                Console.WriteLine("=======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EditTaskFlow(task);
                        break;

                    case "2":
                        UpdateTaskItemStatusFlow(task);
                        break;

                    case "3":
                        UpdateTaskDeadlineFlow(task);
                        break;

                    case "0":
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
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("              EDIT TASK                ");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Description");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");
            Console.Write("Choose field to edit: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    task.Title = InputHelpers.ReadRequiredString("Enter new title: ");
                    break;

                case "2":
                    task.Description = InputHelpers.ReadRequiredString("Enter new description: ");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            Console.WriteLine();
            Console.WriteLine("Task updated successfully.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private void UpdateTaskItemStatusFlow(TaskItem task)
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("        UPDATE TASK STATUS         ");
            Console.WriteLine("=======================================");
            Console.WriteLine("1. To Do");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Waiting Client");
            Console.WriteLine("4. Completed");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");
            Console.Write("Choose new status: ");

            string choice = Console.ReadLine();

            TaskItemStatus newStatus;

            switch (choice)
            {
                case "1":
                    newStatus = TaskItemStatus.ToDo;
                    break;

                case "2":
                    newStatus = TaskItemStatus.InProgress;
                    break;

                case "3":
                    newStatus = TaskItemStatus.WaitingClient;
                    break;

                case "4":
                    newStatus = TaskItemStatus.Completed;
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            try
            {
                _taskService.UpdateTaskStatus(task.Id, newStatus);
                Console.WriteLine($"Task status updated to: {task.Status}");
            }

            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private void UpdateTaskDeadlineFlow(TaskItem task)
        {
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("         UPDATE TASK DEADLINE          ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Current deadline: {task.Deadline.ToShortDateString()}");
            Console.WriteLine("=======================================");

            DateTime newDeadline = InputHelpers.ReadDate("Enter new deadline (yyyy-mm-dd): ");

            try
            {
                _taskService.UpdateTaskDeadline(task.Id, newDeadline);
                Console.WriteLine($"Task deadline updated to: {task.Deadline.ToShortDateString()}");
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

            Console.Write("Press any key to continue...");
            Console.ReadKey();

        }
    }
}


