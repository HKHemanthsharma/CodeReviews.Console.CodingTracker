
using HKHemanthsharma.CodingTracker.Models;
using Spectre.Console;

namespace HKHemanthsharma.CodingTracker
{
    public class UserOutput
    {
        public static void DisplayCodinglogList(List<codinglog> logs)
        {
            var table = new Table();
            var properties = typeof(codinglog).GetProperties();
            foreach (var property in properties)
            {
                property.Name.ToString();
                table.AddColumn(new TableColumn(property.Name));
            }
            foreach (codinglog log in logs)
            {

                table.AddRow(log.Coding_Id.ToString(), log.DateofCoding.ToString(), log.StartTime.ToString(), log.EndTime.ToString(), log.Duration.ToString());
            }
            table.Border = TableBorder.Ascii2;
            AnsiConsole.Write(table);

        }
        public static void DisplayCodinglogListwithSerialNum(List<codinglog> logs)
        {
            var table = new Table();
            table.AddColumn("S.no");
            var properties = typeof(codinglog).GetProperties();
            foreach (var property in properties)
            {
                property.Name.ToString();
                table.AddColumn(new TableColumn(property.Name));
            }
            int count = 1;
            foreach (codinglog log in logs)
            {

                table.AddRow(count.ToString(), log.Coding_Id.ToString(), log.DateofCoding.ToString(), log.StartTime.ToString(), log.EndTime.ToString(), log.Duration.ToString());
                count++;
            }
            table.Border = TableBorder.Ascii2;
            AnsiConsole.Write(table);

        }
    }
}
