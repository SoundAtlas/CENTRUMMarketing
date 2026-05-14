namespace CENTRUMMarketing.App.UI
{
    public class DashboardPrinter
    {
        public void PrintDashboard(string title, string[] lines)
        {
            ConsoleHelpers.Headers(title);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("=======================================");
            ConsoleHelpers.Pause();
        }
    }
}
