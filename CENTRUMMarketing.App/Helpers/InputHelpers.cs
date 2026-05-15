namespace CENTRUMMarketing.App.Helpers
{
    public class InputHelpers
    {
        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                }

                Console.WriteLine($"Invalid input. Please enter a valid whole number between {min} and {max}.");
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

        public static string ReadEmail(string message)
        {
            while (true)
            {

                string email = ReadRequiredString(message);

                if (email.Contains("@") && email.Contains("."))
                {
                    return email;
                }

                Console.WriteLine("Invalid email. Email must contain @ and .");
            }
        }

        public static string ReadCvr(string message)
        {
            while (true)
            {
                string cvr = ReadRequiredString(message);

                if (cvr.Length == 8 && cvr.All(char.IsDigit))
                {
                    return cvr;
                }

                Console.WriteLine("CVR must contain exactly 8 digits.");
            }
        }

        public static string ReadPhoneNumber(string prompt)
        {
            while (true)
            {
                string phone = ReadRequiredString(prompt);

                if (phone.All(char.IsDigit) && phone.Length >= 8)
                {
                    return phone;
                }

                Console.WriteLine("Phone number must contain at least 8 digits.");
            }
        }

    }
}
