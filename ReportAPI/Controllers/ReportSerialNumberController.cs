using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ReportBusiness.ReportSerialNumber;

namespace ReportAPI.Controllers
{
    [Route("api/ReportSerialNumber")]
    public class ReportSerialNumberController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportSerialNumberController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportSerialNumber")]
        public IActionResult printReportSerialNumber([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportSerialNumberService();
                var Models = new ReportSerialNumberRequestModel();
                Models = JsonConvert.DeserializeObject<ReportSerialNumberRequestModel>(body.ToString());
                localFilePath = service.ReportSerialNumber(Models, _hostingEnvironment.ContentRootPath);
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
                ReportSerialNumberService _appService = new ReportSerialNumberService();
                var Models = new ReportSerialNumberRequestModel();
                Models = JsonConvert.DeserializeObject<ReportSerialNumberRequestModel>(body.ToString());
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
