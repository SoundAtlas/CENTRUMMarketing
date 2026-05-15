using CENTRUMMarketing.App.Helpers;
using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Exceptions;
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
            string[] options =
            {
                "Add Customer",
                "View All Customers",
                "Return"
            };

            bool inCustomerMenu = true;

            while (inCustomerMenu)
            {
                int? choice = ConsoleHelpers.Navigation("CUSTOMER MANAGEMENT", options);

                switch (choice)
                {
                    case 0:
                        AddCustomerFlow();
                        break;

                    case 1:
                        ViewCustomersFlow();
                        break;

                    case 2:
                    case null:
                        inCustomerMenu = false;
                        break;
                }
            }
        }


        private void AddCustomerFlow()
        {
            ConsoleHelpers.Headers("ADD NEW CUSTOMER");

            string companyName = InputHelpers.ReadRequiredString("Company name: ");

            string contactPerson = InputHelpers.ReadRequiredString("Contact person: ");

            string cvr = InputHelpers.ReadRequiredString("CVR: ");

            string email = InputHelpers.ReadRequiredString("Email: ");

            string phone = InputHelpers.ReadRequiredString("Phone number: ");

            // CustomerStatus
            Console.WriteLine("Choose customer status: ");
            Console.WriteLine("1. Lead");
            Console.WriteLine("2. Active");
            Console.WriteLine("3. Dormant");

            string? statusChoice = Console.ReadLine();

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
            ConsoleHelpers.Pause();
        }

        private void ViewCustomersFlow()
        {


            List<Customer> customers = _customerService.GetAllCustomers();

            ConsoleHelpers.Headers("ALL CUSTOMERS");

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found");
            }
            else
            {
                foreach (var c in customers)
                {
                    Console.WriteLine($"{c.Id}. {c.CompanyName} - {c.ContactPerson} - {c.Status} - Invoicing Ready: {(c.InvoicingReady ? "Yes" : "No")}");
                }
            }

            Console.WriteLine("=======================================");

            int id = InputHelpers.ReadInt("Enter customer ID to view details (0 to return): ");

            if (id == 0)
            {
                return;
            }

            Customer? customer = _customerService.GetCustomerById(id);

            if (customer != null)
            {
                ShowCustomerDetails(customer);
            }
            else
            {
                Console.WriteLine("Customer not found");
                ConsoleHelpers.Pause();
            }

            Console.WriteLine("=======================================");

        }

        private void ShowCustomerDetails(Customer customer)
        {

            bool inDetailsMenu = true;

            while (inDetailsMenu)
            {
                ConsoleHelpers.Headers($"Customer: {customer.CompanyName}");

                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"Company Name: {customer.CompanyName}");
                Console.WriteLine($"Contact Person: {customer.ContactPerson}");
                Console.WriteLine($"CVR: {customer.Cvr}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.Phone}");
                Console.WriteLine($"Status: {customer.Status}");
                Console.WriteLine($"Invoicing ready: {(customer.InvoicingReady ? "Yes" : "No")}");
                Console.WriteLine("=======================================");
                Console.WriteLine("1. Edit Customer");
                Console.WriteLine("2. Update Status");
                Console.WriteLine("3. Toggle Invoicing Ready");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("0. Back");
                Console.WriteLine("=======================================");

                string choice = InputHelpers.ReadRequiredString("Choose an option: ");

                switch (choice)
                {
                    case "1":
                        EditCustomerFlow(customer);
                        break;

                    case "2":
                        UpdateCustomerStatusFlow(customer);
                        break;

                    case "3":
                        customer.InvoicingReady = !customer.InvoicingReady;
                        _customerService.SaveChanges();
                        Console.WriteLine();
                        break;

                    case "4":
                        if (DeleteCustomerFlow(customer)) inDetailsMenu = false;
                        break;

                    case "0":
                        inDetailsMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void EditCustomerFlow(Customer customer)
        {
            ConsoleHelpers.Headers($"EDIT CUSTOMER: {customer.CompanyName}");

            Console.WriteLine("1. Company Name");
            Console.WriteLine("2. Contact Person");
            Console.WriteLine("3. CVR");
            Console.WriteLine("4. Email");
            Console.WriteLine("5. Phone");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");

            string choice = InputHelpers.ReadRequiredString("Choose field to edit: ");

            switch (choice)
            {
                case "1":
                    customer.CompanyName = InputHelpers.ReadRequiredString("New company name: ");
                    break;

                case "2":
                    customer.ContactPerson = InputHelpers.ReadRequiredString("New contact person: ");
                    break;

                case "3":
                    customer.Cvr = InputHelpers.ReadRequiredString("New CVR: ");
                    break;

                case "4":
                    customer.Email = InputHelpers.ReadRequiredString("New email: ");
                    break;

                case "5":
                    customer.Phone = InputHelpers.ReadRequiredString("New phone number: ");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            _customerService.SaveChanges();

            Console.WriteLine();
            Console.WriteLine($"Customer: {customer.CompanyName} has been updated.");
            ConsoleHelpers.Pause();
        }

        private void UpdateCustomerStatusFlow(Customer customer)
        {
            ConsoleHelpers.Headers($"UPDATE STATUS: {customer.CompanyName}");

            Console.WriteLine("1. Lead");
            Console.WriteLine("2. Active");
            Console.WriteLine("3. Dormant");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose new status: ");

            switch (choice)
            {
                case 1:
                    customer.Status = CustomerStatus.Lead;
                    break;

                case 2:
                    customer.Status = CustomerStatus.Active;
                    break;

                case 3:
                    customer.Status = CustomerStatus.Dormant;
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadKey();
                    return;
            }

            _customerService.SaveChanges();

            Console.WriteLine();
            Console.WriteLine($"Status updated to: {customer.Status}");
            ConsoleHelpers.Pause();
        }

        private bool DeleteCustomerFlow(Customer customer)
        {
            ConsoleHelpers.Headers("DELETE CUSTOMER");

            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine($"Company Name: {customer.CompanyName}");
            Console.WriteLine($"Contact Person: {customer.ContactPerson}");
            Console.WriteLine($"CVR: {customer.Cvr}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.Phone}");
            Console.WriteLine($"Status: {customer.Status}");
            Console.WriteLine("=======================================");
            Console.WriteLine("This action cannot be undone.");
            Console.WriteLine("Customers with connected tasks cannot be deleted.");
            Console.WriteLine();

            string confirmation = InputHelpers.ReadRequiredString("Type DELETE to confirm deletion: ");

            if (!confirmation.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Deletion cancelled.");
                ConsoleHelpers.Pause();

                return false;
            }

            try
            {
                _customerService.DeleteCustomer(customer.Id);

                Console.WriteLine();
                Console.WriteLine("Customer deleted successfully.");
                ConsoleHelpers.Pause();

                return true;
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                ConsoleHelpers.Pause();

                return false;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                ConsoleHelpers.Pause();

                return false;
            }
        }
    }
}
