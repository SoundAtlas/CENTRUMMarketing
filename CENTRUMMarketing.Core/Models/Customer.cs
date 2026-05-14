using CENTRUMMarketing.Core.Enums;

namespace CENTRUMMarketing.Core.Models
{
    public class Customer : BaseEntity
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Cvr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerStatus Status { get; set; }
        public bool InvoicingReady { get; set; }
        public DateTime LastActivityDate { get; set; }
        public List<TaskItem> Tasks { get; set; }


        public Customer(
            int id,
            string companyName,
            string contactPerson,
            string cvr,
            string email,
            string phone,
            CustomerStatus status)
            : base(id)
        {

            CompanyName = companyName;
            ContactPerson = contactPerson;
            Cvr = cvr;
            Email = email;
            Phone = phone;
            Status = status;

            InvoicingReady = false;
            LastActivityDate = DateTime.Now;
            Tasks = new List<TaskItem>();
        }


        public bool IsArchived()
        {
            return Status == CustomerStatus.Dormant && LastActivityDate <= DateTime.Now.AddDays(-30);
        }

    }
}
