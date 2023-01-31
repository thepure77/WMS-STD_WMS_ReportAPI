using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.LogCancelExport;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
namespace ReportAPI.Controllers
{
    [Route("api/LogCancelExport")]
    public class LogCancelExportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LogCancelExportController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("ExportCancel")]
        public IActionResult ExportCancel([FromBody] JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                LogCancelExportService _appService = new LogCancelExportService();
                var Models = new LogCancelExportViewModel();
                Models = JsonConvert.DeserializeObject<LogCancelExportViewModel>(body.ToString());
                StockMovementPath = _appService.ExportCancel(Models, _hostingEnvironment.ContentRootPath);

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
