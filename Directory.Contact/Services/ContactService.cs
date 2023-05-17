using Directory.Contact.Models;
using Directory.Core;
using Directory.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using Directory.Data.Entities;

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
    }
}
