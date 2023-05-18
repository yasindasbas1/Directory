namespace Directory.Contact.Models
{
    public class ReportDetailSummary
    {

        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int TelephoneCount { get; set; }
    }
}
