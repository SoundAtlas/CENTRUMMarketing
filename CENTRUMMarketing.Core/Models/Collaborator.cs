namespace CENTRUMMarketing.Core.Models
{
    public class Collaborator : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public Collaborator(
            int id, string name, string email, string phone) : base(id)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

    }
}
