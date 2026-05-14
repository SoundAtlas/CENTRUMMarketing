using CENTRUMMarketing.App.Menus;
using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Repositories;
using CENTRUMMarketing.Core.Services;


namespace CENTRUMMarketing.App
{
    public class Program
    {
        static void Main(string[] args)
        {

            CustomerRepository customerRepository = new CustomerRepository();
            TaskRepository taskRepository = new TaskRepository();

            CustomerService customerService = new CustomerService(customerRepository);
            TaskService taskService = new TaskService(taskRepository, customerRepository);

            DashboardService dashboardService = new DashboardService(
                customerRepository,
                taskRepository);

            DashboardPrinter dashboardPrinter = new DashboardPrinter();

            CustomerMenu customerMenu = new CustomerMenu(customerService);

            TaskMenu taskMenu = new TaskMenu(taskService, customerService);

            DashboardMenu dashboardMenu = new DashboardMenu(
                dashboardService,
                dashboardPrinter);

            SearchMenu searchMenu = new SearchMenu(customerService, taskService);

            MainMenu mainMenu = new MainMenu(
                customerMenu,
                taskMenu,
                dashboardMenu,
                searchMenu);

            mainMenu.ShowMainMenu();


        }
    }
}




