using Directory.Report.Services;
using Formes.XunitTest.Data;
using System.Collections.Generic;
using Directory.Contact.Models;
using Directory.Contact.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Directory.Data;
using Directory.Data.Entities;

namespace Directory.xUnitTest
{
    public class DirectoryContactServiceTest : IClassFixture<ContactTestDbFixture>
    {
        private ContactService _contactService;

        ContactTestDbFixture _fixture;

        public DirectoryContactServiceTest(ContactTestDbFixture dbTestDataFixture)
        {
            _fixture = dbTestDataFixture;
            _contactService = new ContactService(_fixture.Context);
        }

        [Theory]
        [InlineData("Emre", "Doðan", "Erser")]
        public async void AddContact_InsertContact_True(string name, string surname, string company)
        {
            var vContact = new ContactInfo
            {
                Name = name,
                Surname = surname,
                Company = company,
            };

            var vResult = await _contactService.AddContact(vContact);
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

        [Theory]
        [InlineData(100)]
        public async void GetContactSummaryById_ReturnContactSummary_NotNull(int id)
        {
            var vResult = await _contactService.GetContactSummaryById(id);
            Assert.NotNull(vResult.Payload);
        }

        [Theory]
        [InlineData(100)]
        public async void DeleteContact_ReturnSuccess_True(int id)
        {
            var vResult = await _contactService.DeleteContact(id);
            Assert.True(vResult.Success);
        }

        [Theory]
        [InlineData(9999)]
        public async void DeleteContact_ReturnSuccess_False(int id)
        {
            var vResult = await _contactService.DeleteContact(id);
            Assert.False(vResult.Success);
        }


        [Fact]
        public async void GetReportSummary_ReturnReportSummary_True()
        {
            var vResult = await _contactService.ReportSummary();
            Assert.True(vResult.Success);
        }

        [Fact]
        public async void GetReportSummary_ReturnReportSummary_IsType()
        {
            var vResult = await _contactService.ReportSummary();
            Assert.IsType<List<ReportSummary>>(vResult.Payload);
        }

        [Theory]
        [InlineData(100, "Ýstanbul", "02554655", "mail@gmail.com")]
        public async void AddContactInformation_InsertContact_True(int contactId, string location, string telephone, string mail)
        {
            var vContactInformation = new ContactInfo()
            {
                Id = contactId,
                ContactInformationInfo = new ContactInformationInfo()
                {
                    Location = location,
                    Telephone = telephone,
                    Mail = mail
                }
 
            };

            var vResult = await _contactService.AddContactInformation(vContactInformation);
            Assert.True(vResult.Success);
        }

        [Theory]
        [InlineData(30)]
        public async void RemoveContactInformation_ReturnSuccess_True(int id)
        {
            var vResult = await _contactService.RemoveContactInformation(id);
            Assert.True(vResult.Success);
        }

        [Theory]
        [InlineData(9999)]
        public async void RemoveContactInformation_ReturnSuccess_False(int id)
        {
            var vResult = await _contactService.RemoveContactInformation(id);
            Assert.False(vResult.Success);
        }

        [Theory]
        [InlineData(9999)]
        public async void ReportDetailSummaryById_ReturnCount_False(int id)
        {
            var vResult = await _contactService.ReportDetailSummaryById(id);
            Assert.False(vResult.Payload.Count > 0);
        }

        [Theory]
        [InlineData(3000)]
        public async void ReportDetailSummaryById_ReturnReportDetailSummary_NotNull(int id)
        {
            var vResult = await _contactService.ReportDetailSummaryById(id);
            Assert.NotNull(vResult.Payload);
        }

        [Theory]
        [InlineData(3000)]
        public async void ReportDetailSummaryById_ReturnReportDetailSummary_IsType(int id)
        {
            var vResult = await _contactService.ReportDetailSummaryById(id);
            Assert.IsType<List<ReportDetailSummary>>(vResult.Payload);
        }

    }
}