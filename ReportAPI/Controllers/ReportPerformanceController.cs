using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportPerformance;
using System.Net.Http;
using System.Net;

namespace ReportAPI.Controllers
{
    [Route("api/ReportPerformance")]
    public class ReportPerformanceController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportPerformanceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportPerformance")]
        public IActionResult printReportPerformance([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportPerformanceService();
                var Models = new ReportPerformanceViewModel();
                Models = JsonConvert.DeserializeObject<ReportPerformanceViewModel>(body.ToString());
                localFilePath = service.printReportPerformance(Models, _hostingEnvironment.ContentRootPath);
                if (!System.IO.File.Exists(localFilePath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally
            {
                System.IO.File.Delete(localFilePath);
            }
        }

        [HttpPost]
        [Route("ExportExcel")]
        public IActionResult ExportExcel([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                ReportPerformanceService _appService = new ReportPerformanceService();
                var Models = new ReportPerformanceViewModel();
                Models = JsonConvert.DeserializeObject<ReportPerformanceViewModel>(body.ToString());
                StockMovementPath = _appService.ExportExcel(Models, _hostingEnvironment.ContentRootPath);

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
