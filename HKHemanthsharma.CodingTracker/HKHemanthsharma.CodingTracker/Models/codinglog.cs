

namespace HKHemanthsharma.CodingTracker.Models
{
    public class codinglog
    {
        public int Coding_Id { get; set; }
        public DateTime DateofCoding { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Duration { get; set; }

    }
}
