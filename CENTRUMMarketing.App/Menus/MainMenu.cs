using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class MainMenu
    {
        private CustomerService _customerService;
        private CustomerMenu _customerMenu;

        public MainMenu(CustomerService customerService)
        {
            _customerService = customerService;
            _customerMenu = new CustomerMenu(_customerService);
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
                Console.WriteLine("0. Exit");
                Console.WriteLine("=======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _customerMenu.ShowCustomerMenu();
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