namespace Directory.Contact.Models
{
    public class ContactInformationSummary
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }
        public bool Deleted { get; set; }
    }
}
