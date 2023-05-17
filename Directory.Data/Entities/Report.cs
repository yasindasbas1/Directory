using Directory.Core;

namespace Directory.Data.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string? Url { get; set; }
        public ReportStatuses Status { get; set; }

        ///Navigation Property

        public virtual List<ReportDetail> ReportDetail { get; set; }

    }
}
