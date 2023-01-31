using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportLaborIncentiveScheme;
using ReportBusiness.ReportLaborUtilization;
using ReportBusiness.ReportShipping;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportShipping")]
    public class ReportShippingController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public ReportShippingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportShipping")]
        public IActionResult printReportKPI([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportShippingService();
                var Models = new ReportShippingViewModel();
                Models = JsonConvert.DeserializeObject<ReportShippingViewModel>(body.ToString());
                localFilePath = service.printReportShipping(Models, _hostingEnvironment.ContentRootPath);
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
                ReportShippingService _appService = new ReportShippingService();
                var Models = new ReportShippingViewModel();
                Models = JsonConvert.DeserializeObject<ReportShippingViewModel>(body.ToString());
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
