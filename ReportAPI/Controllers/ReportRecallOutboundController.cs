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
using ReportBusiness.ReportRecallOutbound;

namespace ReportAPI.Controllers
{
    [Route("api/ReportRecallOutbound")]
    public class ReportRecallOutboundController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportRecallOutboundController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportRecall")]
        public IActionResult printReportRecall([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportRecallOutboundService();
                var Models = new ReportRecallOutboundRequestModel();
                Models = JsonConvert.DeserializeObject<ReportRecallOutboundRequestModel>(body.ToString());
                localFilePath = service.printReportRecall(Models, _hostingEnvironment.ContentRootPath);
                if (!System.IO.File.Exists(localFilePath))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
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
                ReportRecallOutboundService _appService = new ReportRecallOutboundService();
                var Models = new ReportRecallOutboundRequestModel();
                Models = JsonConvert.DeserializeObject<ReportRecallOutboundRequestModel>(body.ToString());
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