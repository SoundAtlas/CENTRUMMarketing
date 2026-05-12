using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class MainMenu
    {
        private CustomerService _customerService;
        private TaskService _taskService;
        private CustomerMenu _customerMenu;
        private TaskMenu _taskMenu;

        public MainMenu(CustomerService customerService, TaskService taskService)
        {
            _customerService = customerService;
            _taskService = taskService;
            _customerMenu = new CustomerMenu(_customerService);
            _taskMenu = new TaskMenu(_taskService, _customerService);
        }

        public void ShowMainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("=======================================");
                Console.WriteLine("          CENTRUM MAIN MENU");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Customer Management");
                Console.WriteLine("2. Task Management");
                Console.WriteLine("0. Exit");
                Console.WriteLine("=======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _customerMenu.ShowCustomerMenu();
                        break;

                    case "2":
                        _taskMenu.ShowTaskMenu();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}