namespace CENTRUMMarketing.App.Menus
{
    public class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly TaskMenu _taskMenu;
        private readonly DashboardMenu _dashboardMenu;


        public MainMenu(
            CustomerMenu customerMenu,
            TaskMenu taskMenu,
            DashboardMenu dashboardMenu)
        {
            _customerMenu = customerMenu;
            _taskMenu = taskMenu;
            _dashboardMenu = dashboardMenu;
        }

        public void ShowMainMenu()
        {
            string[] options =
            {
                "Customer Management",
                "Task Management",
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
                        _taskMenu.ShowTaskMenu();
                        break;

                    case 2:
                        _dashboardMenu.ShowDashboard();
                        break;

                    case 3:
                    case null:
                        running = false;
                        break;
                }
            }
        }
    }
}