using Directory.Core;

namespace Directory.Contact.Models
{
    public class ReportSummary
    {
        public int Id { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string? Url { get; set; }
        public ReportStatuses Status { get; set; }
    }



}
