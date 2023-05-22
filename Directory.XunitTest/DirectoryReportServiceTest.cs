using Directory.Contact.Models;
using Directory.Contact.Services;
using Directory.Report.Services;
using Formes.XunitTest.Data;
using System.Collections.Generic;
using Xunit;

namespace Directory.XunitTest
{
    public class DirectoryReportServiceTest: IClassFixture<ReportTestDbFixture>
    {
        private ReportService _reportService;
        private ReportPublisherService _reportPublisherService;

        ReportTestDbFixture _fixture;

        public DirectoryReportServiceTest(ReportTestDbFixture dbTestDataFixture)
        {
            _fixture = dbTestDataFixture;
            _reportService = new ReportService(_fixture.Context);
            _reportPublisherService = new ReportPublisherService(_fixture.Context);
        }

        [Fact]
        public async void CreateReport_ReturnSuccess_True()
        {
            var vResult = await _reportPublisherService.CreateReport();
            Assert.True(vResult.Success);
        }

        [Theory]
        [InlineData("9999")]
        public async void MakeReport_ReturnSuccess_False(string message)
        {
            var vResult = _reportService.MakeReport(message);
            Assert.False(vResult.Success);
        }

        [Theory]
        [InlineData("3000")]
        public async void MakeReport_ReturnErrorMessage_True(string message)
        {
            var vResult = _reportService.MakeReport(message);
            Assert.True(vResult.Message == "Rapor durumu uygun değil");
        }
    }
}
