using Directory.Report.Services;
using Microsoft.AspNetCore.Mvc;

namespace Directory.Report.Controllers
{
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportPublisherService _reportPublisherService;

        public ReportController(ReportPublisherService reportPublisherService)
        {
            _reportPublisherService = reportPublisherService;
        }

        [HttpGet("CreateReport")]
        public async Task<IActionResult> CreateReport()
        {
            var vResult = await _reportPublisherService.CreateReport();
            return Ok(vResult);
        }

    }
}
