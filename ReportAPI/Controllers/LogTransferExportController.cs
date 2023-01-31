using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.LogTransferExport;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/LogTransferExport")]
    public class LogTransferExportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LogTransferExportController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("ExportTransfer")]
        public IActionResult ExportTransfer([FromBody] JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                LogTransferExportService _appService = new LogTransferExportService();
                var Models = new LogTransferExportViewModel();
                Models = JsonConvert.DeserializeObject<LogTransferExportViewModel>(body.ToString());
                StockMovementPath = _appService.ExportTransfer(Models, _hostingEnvironment.ContentRootPath);

                if (!System.IO.File.Exists(StockMovementPath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(StockMovementPath), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                System.IO.File.Delete(StockMovementPath);
            }
        }
    }
}
