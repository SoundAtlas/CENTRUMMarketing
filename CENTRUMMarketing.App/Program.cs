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
            CollaboratorRepository collaboratorRepository = new CollaboratorRepository();

            CustomerService customerService = new CustomerService(customerRepository, taskRepository);
            TaskService taskService = new TaskService(taskRepository, customerRepository);
            CollaboratorService collaboratorService = new CollaboratorService(collaboratorRepository, taskRepository);

            DashboardService dashboardService = new DashboardService(
                customerRepository,
                taskRepository);

            DashboardPrinter dashboardPrinter = new DashboardPrinter();

            CustomerMenu customerMenu = new CustomerMenu(customerService);

            TaskMenu taskMenu = new TaskMenu(taskService, customerService, collaboratorService);
            CollaboratorMenu collaboratorMenu = new CollaboratorMenu(collaboratorService);

            DashboardMenu dashboardMenu = new DashboardMenu(
                dashboardService,
                dashboardPrinter);

            SearchMenu searchMenu = new SearchMenu(customerService, taskService);

            MainMenu mainMenu = new MainMenu(
                customerMenu,
                taskMenu,
                dashboardMenu,
                searchMenu,
                collaboratorMenu);

            mainMenu.ShowMainMenu();


        }
    }
}
