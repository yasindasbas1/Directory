using Directory.Report.Services;
using Formes.XunitTest.Data;
using System.Collections.Generic;
using Directory.Contact.Models;
using Directory.Contact.Services;
using Xunit;

namespace Directory.xUnitTest
{
    public class DirectoryContactServiceTest : IClassFixture<DbContactTestDataFixture>
    {
        private ContactService _contactService;

        DbContactTestDataFixture _fixture;

        public DirectoryContactServiceTest(DbContactTestDataFixture dbTestDataFixture)
        {
            _fixture = dbTestDataFixture;
            _contactService = new ContactService(_fixture.Context);
        }

        [Theory]
        [InlineData("Emre", "Doðan", "Erser")]
        public async void AddPerson_InsertPerson_True(string name, string surname, string company)
        {
            var vPerson = new ContactInfo
            {
                Name = name,
                Surname = surname,
                Company = company,
            };

            var vResult = await _contactService.AddContact(vPerson);
            Assert.True(vResult.Success);
        }
        
    }
}