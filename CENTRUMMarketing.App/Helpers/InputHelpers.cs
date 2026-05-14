namespace CENTRUMMarketing.App.Helpers
{
    public class InputHelpers
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                bool success = int.TryParse(Console.ReadLine(), out int result);

                if (success)
                {
                    return result;
                }

                Console.WriteLine("Invalid input. Please enter a valid whole number.");
            }
        }

        public static DateTime ReadDate(string message)
        {
            while (true)
            {
                Console.Write(message);

                bool success = DateTime.TryParse(Console.ReadLine(), out DateTime date);

                if (success)
                {
                    return date;
                }

                Console.WriteLine("Invalid input. Please enter a valid date (e.g., 2026-12-31).");
            }
        }

        public static string ReadRequiredString(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Input cannot be empty.");
            }
        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
