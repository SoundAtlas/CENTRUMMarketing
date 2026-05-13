using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class SearchMenu
    {
        private readonly CustomerService _customerService;
        private readonly CustomerPrinter _customerPrinter;
        private readonly TaskService _taskService;

        public SearchMenu(CustomerService customerService, TaskService taskService)
        {
            _customerService = customerService;
            _taskService = taskService;
            _customerPrinter = new CustomerPrinter();
        }

        public void ShowSearchMenu()
        {
            string[] options =
            {
                "Search Customer by Name",
                "View Tasks by Status",
                "View Archived Customers",
                "Exit"
            };

            bool running = true;

            while (running)
            {
                int? choice = ConsoleHelpers.Navigation("CENTRUM SEARCH MENU", options);

                switch (choice)
                {
                    case 0:
                        SearchCustomerByName();
                        break;

                    case 1:
                        ShowTasksByStatus();
                        break;

                    case 2:
                        ShowArchivedCustomers();
                        break;

                    case 3:
                    case null:
                        running = false;
                        break;
                }
            }
        }

        private void SearchCustomerByName()
        {
            Console.Clear();
            Console.WriteLine("SEARCH CUSTOMER BY NAME");
            Console.WriteLine();

            Console.Write("Enter full or partial customer name: ");
            string searchTerm = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine();
                Console.WriteLine("Search text cannot be empty.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Searching for customers matching: {searchTerm}");
            Console.WriteLine();

            var matchingCustomers = _customerService.SearchCustomersByName(searchTerm);

            _customerPrinter.PrintCustomers(matchingCustomers);

            Pause();
        }

        private void ShowTasksByStatus()
        {
            Console.Clear();
            Console.WriteLine("VIEW TASKS BY STATUS");
            Console.WriteLine();

            string[] statusOptions =
            {
                "To Do",
                "In Progress",
                "Waiting for Client",
                "Completed",
                "Back"
            };

            int? choice = ConsoleHelpers.Navigation("SELECT TASK STATUS", statusOptions);

            if (choice == null || choice == 4) return;

            TaskItemStatus selectedStatus;

            switch (choice)
            {
                case 0:
                    selectedStatus = TaskItemStatus.ToDo;
                    break;

                case 1:
                    selectedStatus = TaskItemStatus.InProgress;
                    break;

                case 2:
                    selectedStatus = TaskItemStatus.WaitingClient;
                    break;

                case 3:
                    selectedStatus = TaskItemStatus.Completed;
                    break;

                default:
                    return;
            }

            Console.Clear();
            Console.WriteLine($"TASKS WITH STATUS: {selectedStatus}");
            Console.WriteLine();

            var tasks = _taskService.GetTasksByStatus(selectedStatus);

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found with this status.");
            }
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id} | Title: {task.Title} | Deadline: {task.Deadline.ToShortDateString()} | Status: {task.Status}");
                }
            }

            Pause();
        }

        private void ShowArchivedCustomers()
        {
            Console.Clear();
            Console.WriteLine("ARCHIVED CUSTOMERS");
            Console.WriteLine();

            var archivedCustomers = _customerService.GetArchivedCustomers();

            if (archivedCustomers.Count == 0)
            {
                Console.WriteLine("No archived customers found.");
            }
            else
            {
                Console.WriteLine("Showing customers with status Dormant and no activity for 30+ days.");
                _customerPrinter.PrintCustomers(archivedCustomers);
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
