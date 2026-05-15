using CENTRUMMarketing.App.Helpers;
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
                    "Edit Collaborator",
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
                        EditCollaboratorFlow();
                        break;

                    case 3:
                        DeleteCollaboratorFlow();
                        break;

                    case 4:
                    case null:
                        inCollaboratorMenu = false;
                        break;
                }
            }
        }

        private void AddCollaboratorFlow()
        {
            ConsoleHelpers.Headers("ADD NEW COLLABORATOR");

            string name = InputHelpers.ReadRequiredString("Enter collaborator's name: ");

            string email = InputHelpers.ReadEmail("Enter collaborator's email: ");

            string phoneNumber = InputHelpers.ReadPhoneNumber("Enter collaborator's phone number: ");

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

        private void EditCollaboratorFlow()
        {
            ConsoleHelpers.Headers("EDIT COLLABORATOR");

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

            int collaboratorId = InputHelpers.ReadInt("Enter collaborator ID to edit (0 to cancel): ", 0);

            Collaborator? c = _collaboratorService.GetCollaboratorById(collaboratorId);

            if (c == null)
            {
                Console.WriteLine("Collaborator not found.");
                ConsoleHelpers.Pause();
                return;
            }

            if (collaboratorId == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Edit cancelled.");
                ConsoleHelpers.Pause();
                return;
            }

            Collaborator? collaboratorToEdit = _collaboratorService.GetCollaboratorById(collaboratorId);

            if (collaboratorToEdit == null)
            {
                Console.WriteLine();
                Console.WriteLine("Collaborator not found.");
                ConsoleHelpers.Pause();
                return;
            }

            Console.WriteLine($"1. Name: {collaboratorToEdit.Name}");
            Console.WriteLine($"2. Email: {collaboratorToEdit.Email}");
            Console.WriteLine($"3. Phone: {collaboratorToEdit.Phone}");
            Console.WriteLine("0. Back");
            Console.WriteLine("=======================================");

            int choice = InputHelpers.ReadInt("Choose field to edit: ", 0, 3);

            string name = collaboratorToEdit.Name;
            string email = collaboratorToEdit.Email;
            string phone = collaboratorToEdit.Phone;

            switch (choice)
            {
                case 1:
                    name = InputHelpers.ReadRequiredString("Enter new name: ");
                    break;

                case 2:
                    email = InputHelpers.ReadEmail("Enter new email: ");
                    break;

                case 3:
                    phone = InputHelpers.ReadRequiredString("Enter new phone number: ");
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    ConsoleHelpers.Pause();
                    return;
            }

            try
            {
                _collaboratorService.UpdateCollaborator(
                    collaboratorId,
                    name,
                    email,
                    phone);

                Console.WriteLine();
                Console.WriteLine("Collaborator updated successfully.");
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
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

            int collaboratorId = InputHelpers.ReadInt("Enter collaborator ID to delete (0 to cancel): ", 1);

            Collaborator? c = _collaboratorService.GetCollaboratorById(collaboratorId);

            if (c == null)
            {
                Console.WriteLine("Collaborator not found.");
                ConsoleHelpers.Pause();
                return;
            }

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

            string confirmation = InputHelpers.ReadRequiredString("Type DELETE to confirm: ");

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
