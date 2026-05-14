using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class CollaboratorRepository : JsonRepository<Collaborator>
    {
        public CollaboratorRepository()
            : base(@"..\..\..\..\CENTRUMMarketing.Core\Data\collaborators.json")
        {
        }

        public Collaborator? GetById(int id)
        {
            foreach (Collaborator collaborator in GetAll())
            {
                if (collaborator.Id == id)
                {
                    return collaborator;
                }
            }

            return null;
        }

        public bool Delete(int id)
        {
            Collaborator? collaborator = GetById(id);

            if (collaborator == null) return false;
            
            return Remove(collaborator);
        }
    }
}
