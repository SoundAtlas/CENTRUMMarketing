using CENTRUMMarketing.Core.Exceptions;
using CENTRUMMarketing.Core.Interfaces;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Repositories;

namespace CENTRUMMarketing.Core.Services
{
    public class CollaboratorService
    {
        private readonly CollaboratorRepository _collaboratorRepository;
        private readonly ITaskRepository _taskRepository;

        public CollaboratorService(
            CollaboratorRepository collaboratorRepository,
            ITaskRepository taskRepository)
        {
            _collaboratorRepository = collaboratorRepository;
            _taskRepository = taskRepository;
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

        public Collaborator? GetCollaboratorById(int collaboratorId)
        {
            return _collaboratorRepository.GetById(collaboratorId);
        }

        public void UpdateCollaborator(int collaboratorId, string name, string email, string phone)
        {
            Collaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);

            if (collaborator == null)
            {
                throw new EntityNotFoundException(
                    $"Collaborator with ID {collaboratorId} was not found.");
            }

            collaborator.Name = name;
            collaborator.Email = email;
            collaborator.Phone = phone;

            _collaboratorRepository.Save();
        }

        public void DeleteCollaborator(int collaboratorId)
        {
            Collaborator? collaborator = _collaboratorRepository.GetById(collaboratorId);

            if (collaborator == null)
            {
                throw new EntityNotFoundException(
                    $"Collaborator with ID {collaboratorId} was not found.");
            }

            foreach (TaskItem task in _taskRepository.GetAll())
            {
                if (task.CollaboratorIds.Contains(collaboratorId)) task.CollaboratorIds.Remove(collaboratorId);
            }

            _taskRepository.Save();

            bool deleted = _collaboratorRepository.Delete(collaboratorId);

            if (!deleted)
            {
                throw new EntityNotFoundException(
                    $"Collaborator with ID {collaboratorId} was not found.");
            }
        }

        public void SaveChanges()
        {
            _collaboratorRepository.Save();
        }
    }
}
