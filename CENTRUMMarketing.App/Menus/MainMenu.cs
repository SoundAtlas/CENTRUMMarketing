using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly DashboardMenu _dashboardMenu;

        public MainMenu(
            CustomerMenu customerMenu,
            DashboardMenu dashboardMenu)
        {
            _customerMenu = customerMenu;
            _dashboardMenu = dashboardMenu;
        }

        public void ShowMainMenu()
        {
            string[] options =
            {
                "Customer Management",
                "Dashboard",
                "Exit"
            };

            bool running = true;

            while (running)
            {
                int? choice = UI.ConsoleHelpers.Navigation(
                    "CENTRUM MAIN MENU",
                    options);

                switch (choice)
                {
                    case 0:
                        _customerMenu.ShowCustomerMenu();
                        break;

                    case 1:
                        _dashboardMenu.ShowDashboard();
                        break;

                    case 2:
                    case null:
                        running = false;
                        break;
                }
            }
        }
    }
}