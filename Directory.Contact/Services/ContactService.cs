using Directory.Core;
using Directory.Data;
using Directory.Directory.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Directory.Contact.Services
{
    public class ContactService
    {
        private readonly Context _db;

        public ContactService(Context db)
        {
            _db = db;
        }

        public async Task<Result<List<ContactSummary>>> GetContactSummary()
        {
            try
            {
                var vContacts = await _db.Contacts
                    .Include(contact => contact.ContactInformations)
                    .Select(contact => new ContactSummary()
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        Company = contact.Company,
                    })
                    .ToListAsync();

                return Result<List<ContactSummary>>.PrepareSuccess(vContacts);
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "COntactService GetContactSummary error");
                return Result<List<ContactSummary>>.PrepareFailure("Kişiler verisi alınamadı");
            }
        }

        public async Task<Result<ContactSummary>> GetContactSummaryById(int contactId)
        {
            try
            {
                var vContact = await _db.Contacts
                    .Include(contact => contact.ContactInformations)
                    .Where(contact => contact.Id == contactId)
                    .Select(contact => new ContactSummary()
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        Surname = contact.Surname,
                        Company = contact.Company,
                        ContactInformationSummaries = contact.ContactInformations
                            .Select(info => new ContactInformationSummary()
                            {
                                Id = info.Id,
                                Telephone = info.Telephone,
                                Mail = info.Mail,
                                Location = info.Location
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (vContact == null)
                    return Result<ContactSummary>.PrepareFailure("Kayıt bulunamadı");

                return Result<ContactSummary>.PrepareSuccess(vContact);

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ContactService GetContactSummaryById error");
                return Result<ContactSummary>.PrepareFailure("Kişi verisi alınamadı");
            }
        }
    }
}
