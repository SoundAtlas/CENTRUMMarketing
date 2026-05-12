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

            customerService.AddCustomer("Nordic Design", "Mikkel Sørensen", "123456", "BLABLA@GMAIL.COM", "88888888", CustomerStatus.Lead);
            customerService.AddCustomer("Nordic Design Studio", "Mikkel Hansen", "654321", "BLABLABLA@GMAIL.COM", "99999999", CustomerStatus.Active);

            CustomerMenu customerMenu = new CustomerMenu(customerService);
            MainMenu mainMenu = new MainMenu(customerService);

            bool running = true;

            while (running)
            {
                Console.Clear();
                mainMenu.ShowMainMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        customerMenu.ShowCustomerMenu();
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


