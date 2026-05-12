namespace CENTRUMMarketing.App.UI
{
    public class DashboardPrinter
    {
        public void PrintDashboard(string title, string[] lines)
        {
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine($"              {title}");
            Console.WriteLine("=======================================");

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("=======================================");
            Console.WriteLine("Press any key to return...");
        }
    }
}
