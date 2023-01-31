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
using ReportBusiness.ReportCoverDay;

namespace ReportAPI.Controllers
{
    [Route("api/ReportCoverDay")]
    public class ReportCoverDayController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportCoverDayController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportCoverDay")]
        public IActionResult printReportCoverDay([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportCoverDayService();
                var Models = new ReportCoverDayViewModel();
                Models = JsonConvert.DeserializeObject<ReportCoverDayViewModel>(body.ToString());
                localFilePath = service.printReportCoverDay(Models, _hostingEnvironment.ContentRootPath);
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
                ReportCoverDayService _appService = new ReportCoverDayService();
                var Models = new ReportCoverDayViewModel();
                Models = JsonConvert.DeserializeObject<ReportCoverDayViewModel>(body.ToString());
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
