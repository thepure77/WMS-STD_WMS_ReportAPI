using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportLaborIncentiveScheme;
using ReportBusiness.ReportLaborUtilization;
using ReportBusiness.ReportPick;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportPick")]
    public class ReportPickController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public ReportPickController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportPick")]
        public IActionResult printReportKPI([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportPickService();
                var Models = new ReportPickViewModel();
                Models = JsonConvert.DeserializeObject<ReportPickViewModel>(body.ToString());
                localFilePath = service.printReportPick(Models, _hostingEnvironment.ContentRootPath);
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
                ReportPickService _appService = new ReportPickService();
                var Models = new ReportPickViewModel();
                Models = JsonConvert.DeserializeObject<ReportPickViewModel>(body.ToString());
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
