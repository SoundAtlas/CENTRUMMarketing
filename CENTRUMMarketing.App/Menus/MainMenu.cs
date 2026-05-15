using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly TaskMenu _taskMenu;
        private readonly DashboardMenu _dashboardMenu;
        private readonly SearchMenu _searchMenu;
        private readonly CollaboratorMenu _collaboratorMenu;
        private readonly DashboardService _dashboardService;

        public MainMenu(
            CustomerMenu customerMenu,
            TaskMenu taskMenu,
            DashboardMenu dashboardMenu,
            SearchMenu searchMenu,
            CollaboratorMenu collaboratorMenu,
            DashboardService dashboardService)
        {
            _customerMenu = customerMenu;
            _taskMenu = taskMenu;
            _dashboardMenu = dashboardMenu;
            _searchMenu = searchMenu;
            _collaboratorMenu = collaboratorMenu;
            _dashboardService = dashboardService;
        }

        public void ShowMainMenu()
        {
            string[] options =
            {
                "Customer Management",
                "Task Management",
                "Collaborator Management",
                "Dashboard",
                "Search and Filter",
                "Exit"
            };

            bool running = true;

            while (running)
            {
                int? choice = UI.ConsoleHelpers.Navigation(
                    GetMainMenuTitle(),
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
                        _collaboratorMenu.ShowCollaboratorMenu();
                        break;

                    case 3:
                        _dashboardMenu.ShowDashboard();
                        break;

                    case 4:
                        _searchMenu.ShowSearchMenu();
                        break;

                    case 5:
                    case null:
                        running = false;
                        break;
                }
            }
        }

        private string GetMainMenuTitle()
        {
            return
                $"CENTRUM MAIN MENU\n" +
                "\n" +
                "Mini dashboard\n" +
                "---------------------------------------\n" +
                $"Active customers:        {_dashboardService.GetActiveCustomersCount()}\n" +
                $"Leads:                   {_dashboardService.GetLeadsCount()}\n" +
                $"Tasks due this week:     {_dashboardService.GetTasksDueThisWeekCount()}\n" +
                $"Waiting on client:       {_dashboardService.GetWaitingClientTasksCount()}\n" +
                $"Ready for invoice:       {_dashboardService.GetInvoicingReadyCustomersCount()}";
        }
    }
}