using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportLaborPerformance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ReportBusiness.ReportTraceability;

namespace ReportAPI.Controllers
{
    [Route("api/ReportTraceability")]
    public class ReportTraceabilityController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportTraceabilityController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportTraceability")]
        public IActionResult printReportTraceability([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportTraceabilityService();
                var Models = new ReportTraceabilityViewModel();
                Models = JsonConvert.DeserializeObject<ReportTraceabilityViewModel>(body.ToString());
                localFilePath = service.printReportTraceability(Models, _hostingEnvironment.ContentRootPath);
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
        public IActionResult ExportExcel([FromBody] JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                ReportTraceabilityService _appService = new ReportTraceabilityService();
                var Models = new ReportTraceabilityViewModel();
                Models = JsonConvert.DeserializeObject<ReportTraceabilityViewModel>(body.ToString());
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
