using CENTRUMMarketing.App.UI;
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
            string[] options =
            {
                "Customer Management",
                "Exit"
            };

            bool running = true;

            while (running)
            {
                int? choice = ConsoleHelpers.Navigation(
                    "CENTRUM MAIN MENU",
                    options);

                switch (choice)
                {
                    case 0:
                        _customerMenu.ShowCustomerMenu();
                        break;

                    case 1:
                    case null:
                        running = false;
                        break;
                }
            }
        }
    }
}