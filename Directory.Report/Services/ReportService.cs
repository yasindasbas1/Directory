using Directory.Core;
using Directory.Data;
using Directory.Data.Entities;
using Directory.Report.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using Serilog;

namespace Directory.Report.Services
{
    public class ReportService
    {
        private readonly ReportContextDb _db;
        public int _reportId;
        private string _fileUrl;
        private List<ReportDetail> _reportDetails;
        private readonly HttpClient httpClient;

        public ReportService(ReportContextDb db)
        {
            _db = db;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:32001");
        }

        public Result MakeReport(string message)
        {
            try
            {
                _reportId = Convert.ToInt32(message);

                var vUpdateResult = UpdateReportStatus();

                if (vUpdateResult.Failed)
                    return Result.PrepareFailure(vUpdateResult.Message);

                var vReportResult = GetPersonReport().Result;

                if (vReportResult.Failed)
                    return Result.PrepareFailure(vReportResult.Message);

                WriteExcel();

                var vCreateReportDetail = CreateReportDetail().Result;

                if (vCreateReportDetail.Failed)
                    return Result.PrepareFailure("Rapor kaydetme veritabanı işlemi başarısız");


                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService MakeReport error");
                return Result.PrepareFailure("Rapor hazırlama başarısız");
            }
        }

        public Result UpdateReportStatus()
        {
            try
            {
                var vReport = new Data.Entities.Report();
                vReport = _db.Reports
                    .Where(r => r.Id == _reportId)
                    .Select(report => new Data.Entities.Report()
                    {
                        Id = report.Id,
                        Status = report.Status
                    })
                    .FirstOrDefault();

                if (vReport == null)
                    return Result.PrepareFailure("Rapor talep kaydı bulunamadı");

                if (vReport.Status != ReportStatuses.Waiting)
                    return Result.PrepareFailure("Rapor durumu uygun değil");

                _db.Reports.AttachRange(vReport);

                vReport.Status = ReportStatuses.Preparing;
                _db.SaveChanges();

                _db.Entry(vReport).State = EntityState.Detached;

                return Result.PrepareSuccess();
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService UpdateReportStatus error");
                return Result.PrepareFailure("Rapor talep durumu güncellenemedi");
            }
        }

        private async Task<Result> GetPersonReport()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Contact/ContactInformationSummary");
                response.EnsureSuccessStatusCode();

                var vResult = JsonConvert.DeserializeObject<Result<List<ContactInformationSummary>>>(response.Content.ReadAsStringAsync().Result);

                if (vResult.Failed)
                    return Result.PrepareFailure(vResult.Message);

                _reportDetails = vResult.Payload
                    .GroupBy(info => info.Location)
                    .Select(info => new ReportDetail()
                    {
                        Location = info.Key,
                        TelephoneCount = info.Count(c => c.Telephone != null),
                        ContactCount = info.Count()
                    })
                    .ToList();

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService GetPerson Kişi bilgileri alınamadı");
                return Result.PrepareFailure("Contact servisinden veri alınamadı");
            }
        }

        private void WriteExcel()
        {
            try
            {
                DateTime now = DateTime.Now;
                string shortDate = now.ToString("dd.MM.yyyy");
                string fileName = shortDate + "-" + _reportId + ".xlsx";
                string filePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), fileName);

                var package = new ExcelPackage();
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.Add("Report");

                worksheet.Cells[1, 1].Value = "Location";
                worksheet.Cells[1, 2].Value = "TelephoneCount";
                worksheet.Cells[1, 3].Value = "PersonCount";

                for (int i = 0; i < _reportDetails.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = _reportDetails[i].Location;
                    worksheet.Cells[i + 2, 2].Value = _reportDetails[i].TelephoneCount;
                    worksheet.Cells[i + 2, 3].Value = _reportDetails[i].ContactCount;
                }

                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);

                //Dosya yolu alınır
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                _fileUrl = Path.Combine(currentDirectory, fileName);
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService WriteExcel Excel Oluşturulamadı");
            }
        }

        private async Task<Result> CreateReportDetail()
        {
            try
            {
                Data.Entities.Report? vReport = new Data.Entities.Report();
                vReport = await _db.Reports
                    .Where(report => report.Id == _reportId)
                    .Select(report => new Data.Entities.Report()
                    {
                        Id = report.Id,
                        RequestTime = report.RequestTime,
                        CompletedTime = report.CompletedTime,
                        Status = report.Status,
                        Url = report.Url
                    })
                    .FirstOrDefaultAsync();

                if (vReport == null)
                    return Result.PrepareFailure("Rapor kaydı bulunamadı");

                if (vReport.Status == ReportStatuses.Complated)
                    return Result.PrepareFailure("Tamamlanmış rapor isteğine rapor detayı eklenemez");

                _db.Reports.AttachRange(vReport);
                _db.Entry(vReport).State = EntityState.Modified;

                vReport.Status = ReportStatuses.Complated;
                vReport.CompletedTime = DateTime.Now;
                vReport.Url = _fileUrl;

                _db.ReportDetails.AddRange(_reportDetails.Select(detail => new ReportDetail()
                {
                    ReportId = _reportId,
                    Location = detail.Location,
                    TelephoneCount = detail.TelephoneCount,
                    ContactCount = detail.ContactCount
                }).ToList());

                await _db.SaveChangesAsync();

                return Result.PrepareSuccess();
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService CreateReportDetail error");
                return Result.PrepareFailure("Rapor detay eklenemedi");
            }
        }

    }
}
