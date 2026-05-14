using CENTRUMMarketing.App.Helpers;
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
                    case null:
                        inCollaboratorMenu = false;
                        break;
                }
            }
        }

        private void AddCollaboratorFlow()
        {
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("            ADD COLLABORATOR           ");
            Console.WriteLine("=======================================");

            string name = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's name: ");

            string email = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's email: ");

            string phoneNumber = Helpers.InputHelpers.ReadRequiredString("Enter collaborator's phone number: ");

            Collaborator collaborator = _collaboratorService.AddCollaborator(name, email, phoneNumber);

            Console.WriteLine();
            Console.WriteLine($"Collaborator added with ID: {collaborator.Id}");
            InputHelpers.Pause();
        }

        private void ViewAllCollaboratorsFlow()
        {
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("          ALL COLLABORATORS            ");
            Console.WriteLine("=======================================");
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

            InputHelpers.Pause();
        }


    }
}
