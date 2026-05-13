using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.App.UI
{
    public class CustomerPrinter
    {
        public void PrintCustomers(List<Customer> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            Console.WriteLine("Matching customers: ");
            Console.WriteLine();

            foreach (Customer customer in customers)
            {
                PrintCustomers(customer);
                Console.WriteLine("----------------------------------------");
            }
        }

        public void PrintCustomers(Customer customer)
        {
            int taskCount = customer.Tasks == null ? 0 : customer.Tasks.Count;

            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine($"Company name: {customer.CompanyName}");
            Console.WriteLine($"Contact person: {customer.ContactPerson}");
            Console.WriteLine($"CVR: {customer.Cvr}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.Phone}");
            Console.WriteLine($"Status: {customer.Status}");
            Console.WriteLine($"Invoicing ready: {FormatBool(customer.InvoicingReady)}");
            Console.WriteLine($"Last activity date: {customer.LastActivityDate:dd-MM-yyyy}");
            Console.WriteLine($"Tasks connected: {taskCount}");
        }

        private string FormatBool(bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}
