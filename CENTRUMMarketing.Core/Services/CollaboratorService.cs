using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Repositories;

namespace CENTRUMMarketing.Core.Services
{
    public class CollaboratorService
    {
        private readonly CollaboratorRepository _collaboratorRepository;

        public CollaboratorService(CollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }


        public Collaborator AddCollaborator(string name, string email, string phone)
        {
            int nextId = 1;

            foreach (var collaborator in _collaboratorRepository.GetAll())
            {
                if (collaborator.Id >= nextId)
                {
                    nextId = collaborator.Id + 1;
                }
            }

            Collaborator newCollaborator = new Collaborator(nextId, name, email, phone);

            _collaboratorRepository.Add(newCollaborator);

            return newCollaborator;
        }

        public List<Collaborator> GetAllCollaborators()
        {
            return _collaboratorRepository.GetAll();
        }

        public void SaveChanges()
        {
            _collaboratorRepository.Save();
        }
    }
}
