using Directory.Report.Services;
using Formes.XunitTest.Data;
using System.Collections.Generic;
using Directory.Contact.Models;
using Directory.Contact.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Directory.Data;

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

        [Fact]
        public async void GetContactSummary_ReturnContactSummary_Type()
        {
            var vResult = await _contactService.GetContactSummary();
            Assert.IsType<List<ContactSummary>>(vResult.Payload);
        }

        [Theory]
        [InlineData(9999)]
        public async void GetContactSummaryById_ReturnContactSummary_True(int id)
        {
            var vResult = await _contactService.GetContactSummaryById(id);
            Assert.Null(vResult.Payload);
        }

        [Fact]
        public async void RequestReport_ReturnSucces_True()
        {
            var vResult = await _contactService.RequestReport();
            Assert.True(vResult.Success);
        }

        [Fact]
        public async void GetReportSummary_ReturnReportSummary_True()
        {
            var vResult = await _contactService.ReportSummary();
            Assert.True(vResult.Success);
        }

    }
}