using CENTRUMMarketing.App.Helpers;
using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class SearchMenu
    {
        private readonly CollaboratorService _collaboratorService;
        private readonly CustomerService _customerService;
        private readonly CustomerPrinter _customerPrinter;
        private readonly TaskService _taskService;

        public SearchMenu(
            CustomerService customerService,
            TaskService taskService,
            CollaboratorService collaboratorService)
        {
            _customerService = customerService;
            _taskService = taskService;
            _collaboratorService = collaboratorService;
            _customerPrinter = new CustomerPrinter();
        }

        public void ShowSearchMenu()
        {
            string[] options =
            {
                "Search Customer by Name",
                "Search Customer by Status",
                "View Tasks by Status",
                "View Tasks by Collaborator",
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
                        ShowCustomersByStatus();
                        break;

                    case 2:
                        ShowTasksByStatus();
                        break;

                    case 3:
                        ShowTasksByCollaborator();
                        break;

                    case 4:
                        ShowArchivedCustomers();
                        break;

                    case 5:
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

            string searchTerm = InputHelpers.ReadRequiredString("Enter full or partial customer name: ");

            Console.WriteLine();
            Console.WriteLine($"Searching for customers matching: {searchTerm}");
            Console.WriteLine();

            var matchingCustomers = _customerService.SearchCustomersByName(searchTerm);

            _customerPrinter.PrintCustomers(matchingCustomers);

            Pause();
        }

        private void ShowCustomersByStatus()
        {
            Console.Clear();
            Console.WriteLine("FILTER CUSTOMERS BY STATUS");
            Console.WriteLine();

            string[] statusOptions =
            {
                "Lead",
                "Active",
                "Dormant",
                "Back"
            };

            int? choice = ConsoleHelpers.Navigation("SELECT CUSTOMER STATUS", statusOptions);
            
            if (choice == null || choice == 3) return;
            
            CustomerStatus selectedStatus;

            switch (choice)
            {
                case 0:
                    selectedStatus = CustomerStatus.Lead;
                    break;

                case 1:
                    selectedStatus = CustomerStatus.Active;
                    break;

                case 2:
                    selectedStatus = CustomerStatus.Dormant;
                    break;

                default:
                    return;
            }

            Console.Clear();
            Console.WriteLine($"CUSTOMERS WITH STATUS: {selectedStatus}");
            Console.WriteLine();

            var customers = _customerService.GetCustomersByStatus(selectedStatus);
            
            if (customers.Count == 0) Console.WriteLine("No customers found with this status.");
            else _customerPrinter.PrintCustomers(customers);

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

        private void ShowTasksByCollaborator()
        {
            Console.Clear();
            Console.WriteLine("VIEW TASKS BY COLLABORATOR");
            Console.WriteLine();

            var collaborators = _collaboratorService.GetAllCollaborators();

            if (collaborators.Count == 0)
            {
                Console.WriteLine("No collaborators found.");
                Pause();
                return;
            }
            string[] collaboratorOptions = new string[collaborators.Count + 1];

            for (int i = 0; i < collaborators.Count; i++)
            {
                var collaborator = collaborators[i];
                collaboratorOptions[i] = $"{collaborator.Name} ({collaborator.Email})";
            }

            collaboratorOptions[collaboratorOptions.Length - 1] = "Back";

            int? choice = ConsoleHelpers.Navigation("SELECT COLLABORATOR", collaboratorOptions);

            if (choice == null || choice == collaboratorOptions.Length - 1) return;

            var selectedCollaborator = collaborators[choice.Value];

            Console.Clear();
            Console.WriteLine($"TASKS ASSIGNED TO: {selectedCollaborator.Name}");
            Console.WriteLine();

            var tasks = _taskService.GetTasksByCollaboratorId(selectedCollaborator.Id);

            if (tasks.Count == 0) Console.WriteLine("No tasks found for this collaborator.");
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id} | Title: {task.Title} | Deadline: {task.Deadline:yyyy/MM/dd} | Status: {task.Status}");
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
