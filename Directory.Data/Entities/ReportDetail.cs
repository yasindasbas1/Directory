namespace Directory.Data.Entities
{
    public class ReportDetail
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int TelephoneCount { get; set; }

    }
}
