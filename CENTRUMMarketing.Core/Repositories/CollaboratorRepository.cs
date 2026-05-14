using CENTRUMMarketing.Core.Models;

namespace CENTRUMMarketing.Core.Repositories
{
    public class CollaboratorRepository : JsonRepository<Collaborator>
    {
        public CollaboratorRepository()
            : base(@"..\..\..\..\CENTRUMMarketing.Core\Data\collaborators.json")
        {
        }

    }
}
