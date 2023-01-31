using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportRecall_Inbound;

namespace ReportAPI.Controllers
{
    [Route("api/ReportRecallInbound")]
    public class ReportRecallInboundController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportRecallInboundController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("printReportRecallInbound")]
        public IActionResult printReportRecall([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportRecall_InboundService();
                var Models = new ReportRecall_InboundRequestModel();
                Models = JsonConvert.DeserializeObject<ReportRecall_InboundRequestModel>(body.ToString());
                localFilePath = service.printReportRecallInbound(Models, _hostingEnvironment.ContentRootPath);
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
                ReportRecall_InboundService _appService = new ReportRecall_InboundService();
                var Models = new ReportRecall_InboundRequestModel();
                Models = JsonConvert.DeserializeObject<ReportRecall_InboundRequestModel>(body.ToString());
                StockMovementPath = _appService.ExportExcelRecallInbound(Models, _hostingEnvironment.ContentRootPath);

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