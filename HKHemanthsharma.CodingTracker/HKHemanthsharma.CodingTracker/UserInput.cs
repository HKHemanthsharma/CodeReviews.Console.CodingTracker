
using System.Globalization;


namespace HKHemanthsharma.CodingTracker
{
    public class UserInput
    {
        public UserInput() { }
        public static int MainMenu()
        {
            bool validinp = false;

            Console.WriteLine(@"Enter '1' to log coding time for Today:
Enter '2' to update coding time for a specific date:
Enter '3' to insert a specific Date and coding Time for that Date:
Enter '4' to Delete logs for specific Date:
Enter '5' to View your Coding stats:
Enter '0' to return to main menu:");
            int userInput = 0;
            validinp = int.TryParse(Console.ReadLine(), out userInput);
            while (!validinp || userInput < 0 || userInput > 5)
            {
                if (userInput == 0)
                {
                    break;
                }
                Console.WriteLine("Please choose the valid choice between 1 to 4:");
                validinp = int.TryParse(Console.ReadLine(), out userInput);
            }
            return userInput;
        }
        public static DateTime EnterStartTime()
        {
            Console.WriteLine("Enter the startTime for today: in hh:mm format");
            DateTime startTime;

            bool res = DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, DateTimeStyles.None, out startTime);
            while (!res)
            {
                Console.WriteLine("Invalid Time please give correct Time again:");

                res = DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, DateTimeStyles.None, out startTime);

            }
            return startTime;
        }
        public static DateTime EnterEndTime(DateTime startTime)
        {
            Console.WriteLine("Enter the endTime for today: in HH:MM format");
            DateTime endTime;
            bool futureproof = true;
            bool res = DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, DateTimeStyles.None, out endTime);
            if (res)
            {
                futureproof = FutureValidation(startTime, endTime);
            }
            while (!res || !futureproof)
            {
                Console.WriteLine("Invalid Time please give correct Time again and remember endTime can't be less than startTime:");
                res = DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, DateTimeStyles.None, out endTime);
                if (res)
                    futureproof = FutureValidation(startTime, endTime);
            }
            return endTime;
        }
        public static bool FutureValidation(DateTime startTime, DateTime endTime)
        {

            if (endTime < startTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static DateTime EnterDate()
        {
            Console.WriteLine("Enter the Date for which logs to be read/updated/Inserted/Deleted:(in MM/dd/yyyy format eg: (02/26/2025)");
            DateTime targetdate;
            bool res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);
            while (!res)
            {
                Console.WriteLine("Please enter the Date with correct 'MM/dd/yyyy format");
                res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);

            }
            return targetdate;
        }

        public static int codingreportchoice()
        {
            Console.WriteLine(@"Enter '1' to get last 'N' days report:
Enter '2' to get logs between any two dates:
Enter '3' to get logs for a specific date:
Enter '4' to get all the logs:");
            int userInput = 0;
            bool validinp = int.TryParse(Console.ReadLine(), out userInput);
            while (!validinp || userInput < 0 || userInput > 4)
            {
                Console.WriteLine("Please choose the valid choice between 1 to 4:");
                validinp = int.TryParse(Console.ReadLine(), out userInput);
            }
            return userInput;
        }
        public static int lastNDaysInput()
        {
            Console.WriteLine("please enter the number of days report you want to see from today: ");
            int totalDays;
            bool res = int.TryParse(Console.ReadLine(), out totalDays);
            if (!res)
            {
                Console.WriteLine("please  enter a valid number of days report you want to see from today: ");

                res = int.TryParse(Console.ReadLine(), out totalDays);

            }
            return totalDays;
        }
        public static int SnoSelect(int count)
        {
            bool res = int.TryParse(Console.ReadLine(), out int choice);
            while (!res || choice > count || choice <= 0)
            {
                Console.WriteLine($"Please enter the corect number to delete the log between 1 to {count} ");
                res = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }
        public static DateTime EnterStartDate()
        {
            Console.WriteLine("Enter the Start Date for which logs to be read:(in MM/dd/yyyy format eg: (02/26/2025)");
            DateTime targetdate;
            bool res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);
            while (!res)
            {
                Console.WriteLine("Please enter the Date with correct 'MM/dd/yyyy format");
                res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);

            }
            return targetdate;
        }
        public static DateTime EnterEndDate(DateTime startDate)
        {
            bool futureproof = false;
            Console.WriteLine("Enter the End Date for which logs to be read:(in MM/dd/yyyy format eg: (02/26/2025)");
            DateTime targetdate;
            bool res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);
            while (res)
            {
                futureproof = FutureProofDate(startDate, targetdate);
            }
            while (!res || !futureproof)
            {
                Console.WriteLine("Please enter the Date with correct 'MM/dd/yyyy format and it should not be less than startDate:");
                res = DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out targetdate);
                futureproof = FutureProofDate(startDate, targetdate);
            }
            return targetdate;
        }
        public static bool FutureProofDate(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
