
using HKHemanthsharma.CodingTracker.Models;
using Spectre.Console;

namespace HKHemanthsharma.CodingTracker
{
    public class UserOutput
    {
        public static void DisplayCodinglogList(List<Codinglog> logs)
        {
            var table = new Table();
            var properties = typeof(Codinglog).GetProperties();
            foreach (var property in properties)
            {
                table.AddColumn(new TableColumn(property.Name));
            }
            foreach (Codinglog log in logs)
            {

                table.AddRow(log.Coding_Id.ToString(), log.DateofCoding.ToString(), log.StartTime.ToString(), log.EndTime.ToString(), log.Duration.ToString());
            }
            table.Border = TableBorder.Ascii2;
            AnsiConsole.Write(table);

        }
        public static void DisplayCodinglogListwithSerialNum(List<Codinglog> logs)
        {
            var table = new Table();
            table.AddColumn("S.no");
            var properties = typeof(Codinglog).GetProperties();
            foreach (var property in properties)
            {
                property.Name.ToString();
                table.AddColumn(new TableColumn(property.Name));
            }
            int count = 1;
            foreach (Codinglog log in logs)
            {

                table.AddRow(count.ToString(), log.Coding_Id.ToString(), log.DateofCoding.ToString(), log.StartTime.ToString(), log.EndTime.ToString(), log.Duration.ToString());
                count++;
            }
            table.Border = TableBorder.Ascii2;
            AnsiConsole.Write(table);

        }
    }
}
