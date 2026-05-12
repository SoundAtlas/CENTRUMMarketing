namespace CENTRUMMarketing.Core.Models
{
    public class Collaborator
    {
        public int CollaboratorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public Collaborator(
            int collaboratorId, string name, string email, string phone, string role)
        {
            CollaboratorId = collaboratorId;
            Name = name;
            Email = email;
            Phone = phone;
            Role = role;
        }

    }
}
