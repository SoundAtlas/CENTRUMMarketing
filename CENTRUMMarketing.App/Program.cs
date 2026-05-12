using CENTRUMMarketing.Core.Enums;
using CENTRUMMarketing.Core.Models;
namespace CENTRUMMarketing.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer(
                1, "Nordic Design", "Mikkel Sørensen", "123456", "BLABLA@GMAIL.COM", "88888888", CustomerStatus.Lead);

            Console.WriteLine(customer.Id);
            Console.WriteLine(customer.CompanyName);
            Console.WriteLine(customer.ContactPerson);
            Console.WriteLine(customer.Cvr);
            Console.WriteLine(customer.Phone);
            Console.WriteLine(customer.Email);
            Console.WriteLine(customer.Status);
        }
    }
}
