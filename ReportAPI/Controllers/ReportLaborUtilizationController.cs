using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportLaborIncentiveScheme;
using ReportBusiness.ReportLaborUtilization;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportLaborUtilization")]
    public class ReportLaborUtilizationController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public ReportLaborUtilizationController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportLaborUtilization")]
        public IActionResult printReportKPI([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportLaborUtilizationService();
                var Models = new ReportLaborUtilizationViewModel();
                Models = JsonConvert.DeserializeObject<ReportLaborUtilizationViewModel>(body.ToString());
                localFilePath = service.printReportLaborUtilization(Models, _hostingEnvironment.ContentRootPath);
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
                ReportLaborIncentiveSchemeService _appService = new ReportLaborIncentiveSchemeService();
                var Models = new ReportLaborIncentiveSchemeViewModel();
                Models = JsonConvert.DeserializeObject<ReportLaborIncentiveSchemeViewModel>(body.ToString());
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
