using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.LogGiExport;
using ReportBusiness.ReportLaborPerformance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportAPI.Controllers
{
    [Route("api/LogGiExport")]
    public class LogGiExportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LogGiExportController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        
        [HttpPost]
        [Route("Exportgi")]
        public IActionResult ExportExcel([FromBody] JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                LogGiExportService _appService = new LogGiExportService();
                var Models = new LogGiExportViewModel();
                Models = JsonConvert.DeserializeObject<LogGiExportViewModel>(body.ToString());
                StockMovementPath = _appService.Exportgi(Models, _hostingEnvironment.ContentRootPath);

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