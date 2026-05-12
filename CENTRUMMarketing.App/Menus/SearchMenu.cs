using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;

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
                int? choice = ConsoleHelpers.Navigation(
                    "CENTRUM SEARCH MENU",
                    options);

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
                Console.WriteLine("\nSearch text cannot be empty. Press a key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nSearching for customers matching: {searchTerm}");

            // Call CustomerService
            // Include archived customers
            // Print matching customers with CustomerPrinter

            Console.WriteLine("\nSearch completed. Press a key to continue...");
            Console.ReadKey();
        }

        public void ShowTasksByStatus()
        {
            // do something related to looking up tasks by status


            // choose status from enum: ToDo, InProgress, WaitingClient, Completed
        }

        public void ShowArchivedCustomers()
        {
            // do something related to looking up archived customers

            // archived customers are those with status "Dormant" and no activity for 30+ days
        }
    }
}
