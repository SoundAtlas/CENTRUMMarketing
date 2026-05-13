using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class DashboardMenu
    {
        private readonly DashboardService _dashboardService;
        private readonly DashboardPrinter _dashboardPrinter;

        public DashboardMenu(
            DashboardService dashboardService,
            DashboardPrinter dashboardPrinter)
            {
                _dashboardService = dashboardService;
                _dashboardPrinter = dashboardPrinter;
            }

        public void ShowDashboard()
        {
            Console.Clear();

            string[] dashboardLines =
            {
                $"Active customers:        {_dashboardService.GetActiveCustomersCount()}",
                $"Leads:                   {_dashboardService.GetLeadsCount()}",
                $"Tasks due this week:     {_dashboardService.GetTasksDueThisWeekCount()}",
                $"Waiting client tasks:    {_dashboardService.GetWaitingClientTasksCount()}",
                $"Completed tasks:         {_dashboardService.GetCompletedTasksCount()}"
               };

            _dashboardPrinter.PrintDashboard(
                "DASHBOARD",
                dashboardLines);

            Console.ReadKey();
        }
    }
}
