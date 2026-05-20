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
                "View Current Customers",
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

            string cvr = InputHelpers.ReadCvr("CVR: ");

            string email = InputHelpers.ReadEmail("Email: ");

            string phone = InputHelpers.ReadPhoneNumber("Phone number: ");

            // CustomerStatus
            Console.WriteLine("Choose customer status: ");
            Console.WriteLine("1. Lead");
            Console.WriteLine("2. Active");
            Console.WriteLine("3. Dormant");

            int statusChoice = InputHelpers.ReadInt("Choose status: ", 1, 3);

            CustomerStatus status = CustomerStatus.Active;

            switch (statusChoice)
            {
                case 1:
                    status = CustomerStatus.Lead;
                    break;
                case 2:
                    status = CustomerStatus.Active;
                    break;
                case 3:
                    status = CustomerStatus.Dormant;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;

            }

            try
            {
                Customer customer = _customerService.AddCustomer(
                    companyName,
                    contactPerson,
                    cvr,
                    email,
                    phone,
                    status);

                Console.WriteLine();
                Console.WriteLine($"Customer added: {customer.CompanyName}");

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            ConsoleHelpers.Pause();
        }

        private void ViewCustomersFlow()
        {

            List<Customer> customers = _customerService.GetAllCustomers();

            ConsoleHelpers.Headers("CURRENT CUSTOMERS");

            bool foundVisibleCustomers = false;

            foreach (Customer c in customers)
            {
                if (c.IsArchived())
                {
                    continue;
                }

                Console.WriteLine($"{c.Id}. {c.CompanyName} - {c.ContactPerson} - {c.Status} - Invoicing Ready: {(c.InvoicingReady ? "Yes" : "No")}");
                foundVisibleCustomers = true;
            }

            if (!foundVisibleCustomers)
            {
                Console.WriteLine("No customers to display.");
                ConsoleHelpers.Pause();
                return;
            }

            Console.WriteLine("=======================================");

            int id = InputHelpers.ReadInt("Enter customer ID to view details (0 to return): ", 0);

            if (id == 0)
            {
                return;
            }

            Customer? customer = _customerService.GetCustomerById(id);

            if (customer != null && !customer.IsArchived())
            {
                ShowCustomerDetailsFlow(customer);
            }
            else
            {
                Console.WriteLine("Customer not found");
                ConsoleHelpers.Pause();
            }

            Console.WriteLine("=======================================");

        }

        private void ShowCustomerDetailsFlow(Customer customer)
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
                Console.WriteLine("0. Return to previous menu");
                Console.WriteLine("=======================================");

                int choice = InputHelpers.ReadInt("Choose an option: ", 0, 4);

                switch (choice)
                {
                    case 1:
                        EditCustomerFlow(customer);
                        break;

                    case 2:
                        UpdateCustomerStatusFlow(customer);
                        break;

                    case 3:
                        customer.InvoicingReady = !customer.InvoicingReady;
                        _customerService.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine($"Invoicing ready set to: {(customer.InvoicingReady ? "Yes" : "No")}");
                        ConsoleHelpers.Pause();
                        break;

                    case 4:
                        if (DeleteCustomerFlow(customer)) inDetailsMenu = false;
                        break;

                    case 0:
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
            Console.WriteLine("0. Cancel");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose field to edit: ", 0, 5);

            switch (choice)
            {
                case 1:
                    customer.CompanyName = InputHelpers.ReadRequiredString("New company name: ");
                    break;

                case 2:
                    customer.ContactPerson = InputHelpers.ReadRequiredString("New contact person: ");
                    break;

                case 3:
                    customer.Cvr = InputHelpers.ReadCvr("New CVR: ");
                    break;

                case 4:
                    customer.Email = InputHelpers.ReadEmail("New email: ");
                    break;

                case 5:
                    customer.Phone = InputHelpers.ReadPhoneNumber("New phone number: ");
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
            Console.WriteLine($"Customer: {customer.CompanyName} has been updated.");
            ConsoleHelpers.Pause();
        }

        private void UpdateCustomerStatusFlow(Customer customer)
        {
            ConsoleHelpers.Headers($"UPDATE STATUS: {customer.CompanyName}");

            Console.WriteLine("1. Lead");
            Console.WriteLine("2. Active");
            Console.WriteLine("3. Dormant");
            Console.WriteLine("0. Cancel");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose new status: ", 0, 4);

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

            string[] confirmationOptions =
               {
                   "Yes, delete customer.",
                   "No, cancel."
               };

            int? confirmationChoice = ConsoleHelpers.Navigation(
                "DELETE CUSTOMER?",
                confirmationOptions);

            if (confirmationChoice == null || confirmationChoice == 1)
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
