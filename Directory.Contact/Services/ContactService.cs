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
                var vPersons = await _db.Contacts
                    .Include(person => person.ContactInformations)
                    .Select(person => new ContactSummary()
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Surname = person.Surname,
                        Company = person.Company,
                    })
                    .ToListAsync();

                return Result<List<ContactSummary>>.PrepareSuccess(vPersons);
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "PersonService GetPersonSummary error");
                return Result<List<ContactSummary>>.PrepareFailure("Kişiler verisi alınamadı");
            }
        }
    }
}
