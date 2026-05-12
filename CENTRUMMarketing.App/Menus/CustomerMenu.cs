using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class CustomerMenu
    {

        private CustomerService _customerService;

        public CustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public void ShowCustomerMenu()
        {
            bool inCustomerMenu = true;

            while (inCustomerMenu)
            {
                Console.Clear();

                Console.WriteLine("=======================================");
                Console.WriteLine("          CUSTOMER MANAGEMENT          ");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. View All Customers");
                Console.WriteLine("0. Return");
                Console.WriteLine("=======================================");
                Console.WriteLine("Choose an option: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        AddCustomerFlow();
                        break;
                    case "2":
                        ViewCustomersFlow();
                        break;
                    case "0":
                        inCustomerMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }

            }
        }


        private void AddCustomerFlow()
        {
            Console.Clear();

            Console.WriteLine("Company name: ");
            string companyName = Console.ReadLine();

            Console.WriteLine("Contact person: ");
            string contactPerson = Console.ReadLine();

            Console.WriteLine("CVR: ");
            string cvr = Console.ReadLine();

            Console.WriteLine("Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Phone number: ");
            string phone = Console.ReadLine();

            // CustomerStatus
            Console.WriteLine("Choose customer status: ");
            Console.WriteLine("1. Lead");
            Console.WriteLine("2. Active");
            Console.WriteLine("3. Inactive");
            Console.WriteLine("4. Dormant");

            string statusChoice = Console.ReadLine();
            CustomerStatus status = CustomerStatus.Active;

            switch (statusChoice)
            {
                case "1":
                    status = CustomerStatus.Lead;
                    break;
                case "2":
                    status = CustomerStatus.Active;
                    break;
                case "3":
                    status = CustomerStatus.Inactive;
                    break;
                case "4":
                    status = CustomerStatus.Dormant;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;

            }

            Customer customer = _customerService.AddCustomer(
                companyName,
                contactPerson,
                cvr,
                email,
                phone,
                status);

            Console.WriteLine();
            Console.WriteLine($"Customer added: {customer.CompanyName}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewCustomersFlow()
        {
            Console.Clear();

            List<Customer> customers = _customerService.GetAllCustomers();

            Console.WriteLine("=======================================");
            Console.WriteLine("             ALL CUSTOMERS             ");
            Console.WriteLine("=======================================");

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found");
            }
            else
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.Id}. {customer.CompanyName} - {customer.ContactPerson}, {customer.Phone} - {customer.Status} ");
                }
            }

            Console.WriteLine("=======================================");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}
