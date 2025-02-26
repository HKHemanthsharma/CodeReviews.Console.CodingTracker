

using Dapper;
using HKHemanthsharma.CodingTracker.Models;
using Microsoft.Data.SqlClient;


namespace HKHemanthsharma.CodingTracker
{
    public class DatabaseManager
    {
        private static string _connectionString = "";
        public static string ConnectionString { get { return _connectionString; } }
        public DatabaseManager()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlexpress2022"].ConnectionString;
        }
        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void CreateDB(string connectionString)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "if DB_ID('CodingDB') is null begin create database CodingDB; end";

                    sqlConnection.Execute(query);
                    sqlConnection.Close();
                    Console.WriteLine("DB check done!!");
                    _connectionString = connectionString + ";Initial Catalog=" + " CodingDB";
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void TableExists()
        {
            try
            {
                string DbConnection = ConnectionString;
                using (SqlConnection sqlConnection = new SqlConnection(DbConnection))
                {
                    sqlConnection.Open();
                    string query = @"if object_id('codingtimelog') is null begin create table codingtimelog(
Coding_Id int identity(1,1) primary key,
DateofCoding Date not null,
StartTime time not null,
EndTime time not null,
Duration AS DATEDIFF(MINUTE, StartTime, EndTime)
);
end";
                    sqlConnection.Execute(query);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsertTodaylog()
        {
            try
            {
                DateTime startTime = UserInput.EnterStartTime();
                DateTime endTime = UserInput.EnterEndTime(startTime);
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "insert into codingtimelog(DateofCoding,StartTime,EndTime) values(@date,@starttime,@endtime)";
                    int row = conn.Execute(query, new { date = DateTime.Now.Date, startTime, endTime });
                    Console.WriteLine($"Successfully inserted {row} entry");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateLogForDate()
        {
            DateTime targetDate = UserInput.EnterDate();
            List<Codinglog> Codinglogs;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = @"select * from codingtimelog where DateofCoding=@date";
                Codinglogs = conn.Query<Codinglog>(query, new { date = targetDate }).ToList();
                string updatequery = "";
                int update_id = -1;

                if (Codinglogs.Count > 1)
                {
                    Console.WriteLine($"There are multiple logs for the {targetDate} date");
                    UserOutput.DisplayCodinglogListwithSerialNum(Codinglogs);
                    Console.WriteLine($"please enter the S.No of Codinglog to be modified : Makesure you choose between 1 to {Codinglogs.Count}");
                    int choice = UserInput.SnoSelect(Codinglogs.Count);
                    update_id = Codinglogs[choice].Coding_Id;

                }
                else
                {
                    if (Codinglogs.Count == 0)
                    {
                        Console.WriteLine("No logs found for this date!");
                        return;
                    }
                    else
                    {
                        update_id = Codinglogs[0].Coding_Id;
                    }
                }
                DateTime startTime = UserInput.EnterStartTime();
                DateTime endTime = UserInput.EnterEndTime(startTime);
                updatequery = @"update codingtimelog set StartTime=@starttime, EndTime=@endtime where Coding_Id=@updateid";
                int affected = conn.Execute(updatequery, new { starttime = startTime, endtime = endTime, updateid = update_id });
                Console.WriteLine($"sucessfully updated {affected} row");

            }
        }
        public void InsertLogForDate()
        {
            DateTime insertDate = UserInput.EnterDate();
            DateTime startTime = UserInput.EnterStartTime();
            DateTime endTime = UserInput.EnterEndTime(startTime);
            string insertQuery = @"insert into codingtimelog(DateofCoding,StartTime,EndTime) values(@insertdate,@starttime,@endtime)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                int affected = conn.Execute(insertQuery, new { insertdate = insertDate, starttime = startTime, endtime = endTime });
                Console.WriteLine($"sucessfully inserted {affected} row");
            }
        }
        public void DeleteLogForDate()
        {
            List<Codinglog> Codinglogs = new List<Codinglog>();
            DateTime targetDate = UserInput.EnterDate();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"select * from codingtimelog where DateofCoding=@date";
                    Codinglogs = conn.Query<Codinglog>(query, new { date = targetDate }).ToList();
                    string deletequery = "";
                    int delete_id = -1;

                    if (Codinglogs.Count > 1)
                    {
                        Console.WriteLine($"There are multiple logs for the {targetDate} date");
                        Console.WriteLine("Press 'y' or 'Y' to delete all logs: else press any other key");
                        if (Console.ReadLine().ToLower() != "y")
                        {
                            UserOutput.DisplayCodinglogListwithSerialNum(Codinglogs);
                            Console.WriteLine($"please enter the S.No of the Codinglog to be Deleted:  Makesure you choose between 1 to {Codinglogs.Count}");
                            int choice = UserInput.SnoSelect(Codinglogs.Count);
                            delete_id = Codinglogs[choice].Coding_Id;
                            deletequery = "delete from codingtimelog where Coding_Id=@deleteid";
                        }
                        else
                        {
                            deletequery = @"delete from codingtimelog where DateofCoding=@date";
                        }

                    }
                    else
                    {
                        if (Codinglogs.Count == 0)
                        {
                            Console.WriteLine("No logs found for this date!");
                            return;
                        }
                        else
                        {
                            delete_id = Codinglogs[0].Coding_Id;
                            deletequery = "delete from codingtimelog where Coding_Id=@deleteid";
                        }
                    }
                    if (deletequery.Contains("DateofCoding"))
                    {
                        int affected = conn.Execute(deletequery, new { date = targetDate });
                        Console.WriteLine($"sucessfully deleted {affected} rows");
                    }
                    else
                    {
                        int affected = conn.Execute(deletequery, new { deleteid = delete_id });
                        Console.WriteLine($"sucessfully deleted {affected} rows");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void ViewReports()
        {
            int reporttype = UserInput.CodingReportChoice();
            string Query = "";
            List<Codinglog> Codinglogs = new List<Codinglog>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (reporttype == 1)
                {
                    int totaldays = UserInput.LastNDaysInput();
                    Query = $"select * from codingtimelog where DateofCoding>=getdate()-{totaldays}";
                    Codinglogs = conn.Query<Codinglog>(Query).ToList();

                }
                else if (reporttype == 2)
                {
                    DateTime startDate = UserInput.EnterStartDate();
                    DateTime endDate = UserInput.EnterEndDate(startDate);
                    if (startDate > endDate)
                        (startDate, endDate) = (endDate, startDate);
                    Query = $"select * from codingtimelog where DateofCoding between @start and @end";
                    Codinglogs = conn.Query<Codinglog>(Query, new { start = startDate, end = endDate }).ToList();
                }
                else if (reporttype == 3)
                {
                    DateTime targetdate = UserInput.EnterDate();
                    Query = $"select * from codingtimelog where DateofCoding=@date";
                    Codinglogs = conn.Query<Codinglog>(Query, new { date = targetdate }).ToList();
                }
                else
                {
                    Query = "select * from codingtimelog order by Coding_Id";
                    Codinglogs = conn.Query<Codinglog>(Query).ToList();
                }
            }
            UserOutput.DisplayCodinglogList(Codinglogs);
        }
    }
}
