namespace HKHemanthsharma.CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager dbmanager = new DatabaseManager();
            dbmanager.CreateDB(DatabaseManager.ConnectionString);
            dbmanager.TableExists();
            bool close = false;
            while (!close)
            {
                Console.Clear();
                int userChoice = UserInput.MainMenu();
                switch (userChoice)
                {
                    case 1:
                        dbmanager.InsertTodaylog();
                        break;
                    case 2:
                        dbmanager.UpdateLogForDate();
                        break;
                    case 3:
                        dbmanager.InsertLogForDate();
                        break;
                    case 4:
                        dbmanager.DeleteLogForDate();
                        break;
                    case 5:
                        dbmanager.ViewReports();
                        break;
                }
                Console.WriteLine("Press 0 to end the application or any other key to continue");
                if (Console.ReadLine() == "0")
                {
                    close = true;
                }
            }
            Console.ReadLine();
        }
    }
}
