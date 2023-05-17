namespace Directory.Contact.Models
{
    public class ContactSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<ContactInformationSummary> ContactInformationSummaries { get; set; }
    }
}
