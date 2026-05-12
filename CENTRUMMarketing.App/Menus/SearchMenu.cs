using CENTRUMMarketing.App.UI;
using TaskStatus = CENTRUMMarketing.Core.Enums.TaskStatus;

namespace CENTRUMMarketing.App.Menus
{
    public class SearchMenu
    {
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
            Console.WriteLine("SEARCH CUSTOMER BY NAME\n\n");

            Console.Write("Enter full or partial customer name: ");
            string searchTerm = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("\nSearch text cannot be empty.");
                Pause();
                return;
            }

            Console.WriteLine($"\nSearching for customers matching: {searchTerm}");

            // TODO:
            // Call CustomerService
            // Include archived customers
            // Print matching customers with CustomerPrinter

            Pause();
        }

        public void ShowTasksByStatus()
        {
            Console.Clear();
            Console.WriteLine("VIEW TASKS BY STATUS\n\n");

            string[] statusOptions =
            {
                "To Do",
                "In Progress",
                "Waiting for Client",
                "Completed",
                "Back"
            };

            int? choice = ConsoleHelpers.Navigation("SELECT TASK STATUS", statusOptions);

            if (choice == null || choice == 4)
                return;

            TaskStatus selectedStatus;

            switch (choice)
            {
                case 0:
                    selectedStatus = TaskStatus.ToDo;
                    break;

                case 1:
                    selectedStatus = TaskStatus.InProgress;
                    break;

                case 2:
                    selectedStatus = TaskStatus.WaitingClient;
                    break;

                case 3:
                    selectedStatus = TaskStatus.Completed;
                    break;

                default:
                    return;
            }

            Console.Clear();
            Console.WriteLine($"TASKS WITH STATUS: {selectedStatus}\n\n");

            // TODO:
            // Call TaskService to get tasks with selectedStatus
            // Get tasks matching selectedStatus
            // Print matching tasks with TaskPrinter

            Pause();
        }

        public void ShowArchivedCustomers()
        {
            Console.Clear();
            Console.WriteLine("ARCHIVED CUSTOMERS\n\n");

            Console.WriteLine("Showing customers with status Dormant and no activity for 30+ days.");

            // TODO:
            // Call ArchiveService to get archived customers
            //      - Status is Dormant
            //      - LastActivityDate is 30+ days ago
            // Print archived customers with CustomerPrinter

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
