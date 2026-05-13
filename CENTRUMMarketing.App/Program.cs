using CENTRUMMarketing.App.Menus;
using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Repositories;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            CustomerService customerService = new CustomerService();
            TaskService taskService = new TaskService(customerService);

            customerService.AddCustomer("Nordic Design", "Mikkel Sørensen", "123456", "BLABLA@GMAIL.COM", "88888888", CustomerStatus.Lead);
            customerService.AddCustomer("Nordic Design Studio", "Mikkel Hansen", "654321", "BLABLABLA@GMAIL.COM", "99999999", CustomerStatus.Active);

            CustomerRepository customerRepository = new CustomerRepository();
            TaskRepository taskRepository = new TaskRepository();

            DashboardService dashboardService = new DashboardService(
                customerRepository,
                taskRepository);

            DashboardPrinter dashboardPrinter = new DashboardPrinter();

            DashboardMenu dashboardMenu = new DashboardMenu(
                dashboardService,
                dashboardPrinter);

            CustomerMenu customerMenu = new CustomerMenu(customerService);

            TaskMenu taskMenu = new TaskMenu(taskService, customerService);

            MainMenu mainMenu = new MainMenu(
                customerMenu,
                taskMenu,
                dashboardMenu);

            mainMenu.ShowMainMenu();
        }
    }
}




