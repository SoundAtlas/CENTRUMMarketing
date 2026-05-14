using CENTRUMMarketing.App.UI;
using CENTRUMMarketing.Core.Exceptions;
using CENTRUMMarketing.Core.Models;
using CENTRUMMarketing.Core.Services;

namespace CENTRUMMarketing.App.Menus
{
    public class CollaboratorMenu
    {
        private readonly CollaboratorService _collaboratorService;

        public CollaboratorMenu(CollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        public void ShowCollaboratorMenu()
        {
            bool inCollaboratorMenu = true;

            while (inCollaboratorMenu)
            {
                string[] options =
                {
                    "Add Collaborator",
                    "View All Collaborators",
                    "Delete Collaborator",
                    "Back"
                };

                int? choice = UI.ConsoleHelpers.Navigation(
                    "COLLABORATOR MANAGEMENT",
                    options);

                switch (choice)
                {
                    case 0:
                        AddCollaboratorFlow();
                        break;
                    case 1:
                        ViewAllCollaboratorsFlow();
                        break;

                    case 2:
                        DeleteCollaboratorFlow();
                        break;

                    case 3:
                    case null:
                        inCollaboratorMenu = false;
                        break;
                }
            }
        }

        private void AddCollaboratorFlow()
        {
            ConsoleHelpers.Headers("ADD NEW COLLABORATOR");

            string name = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's name: ");

            string email = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's email: ");

            string phoneNumber = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's phone number: ");

            Collaborator collaborator = _collaboratorService.AddCollaborator(name, email, phoneNumber);

            Console.WriteLine();
            Console.WriteLine($"Collaborator added with ID: {collaborator.Id}");
            ConsoleHelpers.Pause();
        }

        private void ViewAllCollaboratorsFlow()
        {
            ConsoleHelpers.Headers("ALL COLLABORATORS");

            List<Collaborator> collaborators = _collaboratorService.GetAllCollaborators();

            if (collaborators.Count == 0)
            {
                Console.WriteLine("No collaborators found.");
            }
            else
            {
                foreach (var collaborator in collaborators)
                {
                    Console.WriteLine($"ID: {collaborator.Id} | Name: {collaborator.Name} | Email: {collaborator.Email} | Phone: {collaborator.Phone}");
                }
            }

            ConsoleHelpers.Pause();
        }

        private void DeleteCollaboratorFlow()
        {
            ConsoleHelpers.Headers("DELETE COLLABORATOR");

            List<Collaborator> collaborators = _collaboratorService.GetAllCollaborators();

            if (collaborators.Count == 0)
            {
                Console.WriteLine("No collaborators found.");
                ConsoleHelpers.Pause();
                return;
            }

            foreach (Collaborator collaborator in collaborators)
            {
                Console.WriteLine($"ID: {collaborator.Id} | Name: {collaborator.Name} | Email: {collaborator.Email} | Phone: {collaborator.Phone}");
            }

            Console.WriteLine("=======================================");

            int collaboratorId = Helpers.InputHelpers.ReadInt("Enter collaborator ID to delete (0 to cancel): ");

            if (collaboratorId == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Deletion cancelled.");
                ConsoleHelpers.Pause();
                return;
            }

            Collaborator? collaboratorToDelete = null;

            foreach (Collaborator collaborator in collaborators)
            {
                if (collaborator.Id == collaboratorId)
                {
                    collaboratorToDelete = collaborator;
                    break;
                }
            }

            if (collaboratorToDelete == null)
            {
                Console.WriteLine();
                Console.WriteLine("Collaborator not found.");
                ConsoleHelpers.Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"You are about to delete: {collaboratorToDelete.Name}");
            Console.WriteLine("This will also remove this collaborator from assigned tasks.");
            Console.WriteLine("This action cannot be undone.");
            Console.WriteLine();

            string confirmation = Helpers.InputHelpers.ReadRequiredString("Type DELETE to confirm: ");

            if (!confirmation.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Deletion cancelled.");
                ConsoleHelpers.Pause();
                return;
            }

            try
            {
                _collaboratorService.DeleteCollaborator(collaboratorId);

                Console.WriteLine();
                Console.WriteLine("Collaborator deleted successfully.");
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

            ConsoleHelpers.Pause();
        }
    }
}
