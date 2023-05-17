namespace Directory.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public bool Deleted { get; set; }


        ///Navigation Property
        public virtual List<ContactInformation>? ContactInformations { get; set; }
    }
}