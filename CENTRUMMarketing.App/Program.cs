using CENTRUMMarketing.App.Menus;
using CENTRUMMarketing.Core.Enums;
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

            MainMenu mainMenu = new MainMenu(customerService, taskService);
            mainMenu.ShowMainMenu();

        }
    }
}




