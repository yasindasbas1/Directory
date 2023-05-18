using Directory.Contact.Models;
using Directory.Core;
using Directory.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Directory.Data.Entities;
using Newtonsoft.Json;

namespace Directory.Contact.Services
{
    public class ContactService
    {
        private readonly Context _db;
        private readonly HttpClient httpClient;

        public ContactService(Context db)
        {
            _db = db;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:32002");
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

        public async Task<Result> AddContact(ContactInfo contactInfo)
        {
            try
            {
                _db.Contacts.Add(new Data.Entities.Contact()
                {
                    Name = contactInfo.Name,
                    Surname = contactInfo.Surname,
                    Company = contactInfo.Company,
                });

                await _db.SaveChangesAsync();

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ContactService AddContact error");
                return Result.PrepareFailure("Kişi verisi eklenemedi");
            }
        }

        public async Task<Result> DeleteContact(int contactId)
        {
            try
            {
                var vContact = await _db.Contacts
                    .Where(contact => contact.Id == contactId)
                    .Select(contact => new Data.Entities.Contact()
                    {
                        Id = contact.Id,
                        Deleted = contact.Deleted
                    })
                    .FirstOrDefaultAsync();

                if (vContact == null)
                    return Result.PrepareFailure("Kayıt bulunamadı");

                _db.Contacts.Attach(vContact);

                vContact.Deleted = true;

                await _db.SaveChangesAsync();

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ContactService DeleteContact error");
                return Result.PrepareFailure("Kişi verisi silinemedi");
            }
        }

        public async Task<Result> AddContactInformation(ContactInfo contactInfo)
        {
            try
            {
                var vContact = await _db.Contacts
                    .Where(contact => contact.Id == contactInfo.Id)
                    .Select(contact => new Data.Entities.Contact()
                    {
                        Id = contact.Id,
                    })
                    .FirstOrDefaultAsync();

                if (vContact == null)
                    Result.PrepareFailure("Kişi kaydı bulunamadı");


                _db.ContactInformations.Add(new ContactInformation()
                {
                    ContactId = contactInfo.Id,
                    Location = contactInfo.ContactInformationInfo.Location,
                    Telephone = contactInfo.ContactInformationInfo.Telephone,
                    Mail = contactInfo.ContactInformationInfo.Mail,
                    Deleted = false
                });

                await _db.SaveChangesAsync();

                return Result.PrepareSuccess();
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ContactService AddContactInformation error");
                return Result.PrepareFailure("Kişiye iletişim bilgisi eklenemedi");
            }
        }

        public async Task<Result> RemoveContactInformation(int contactInformationId)
        {
            try
            {
                var vContactInformation = await _db.ContactInformations
                    .Where(info => info.Id == contactInformationId)
                    .Select(info => new ContactInformation()
                    {
                        Id = info.Id,
                        Deleted = info.Deleted
                    })
                    .FirstOrDefaultAsync();

                if (vContactInformation == null)
                    return Result.PrepareFailure("Kayıt bulunamadı");

                _db.ContactInformations.Attach(vContactInformation);

                vContactInformation.Deleted = true;

                await _db.SaveChangesAsync();

                return Result.PrepareSuccess();
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ContactService RemoveContactInformation error");
                return Result.PrepareFailure("Kişiye ait iletişim bilgisi kaldırılamadı.");
            }
        }

        public async Task<Result> RequestReport()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Report/CreateReport");
                response.EnsureSuccessStatusCode();

                var vResult = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);
                if (vResult.Failed)
                    return Result.PrepareFailure(vResult.Message);

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "Contact RequestReport error");
                return Result.PrepareFailure("Rapor talep edilemedi");
            }
        }
    }
}
