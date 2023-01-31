using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportCheckStockOnHandPartial;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportCheckStockOnHandPartial")]
    public class ReportCheckStockOnHandPartialController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportCheckStockOnHandPartialController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportCheckStockOnHandPartial")]
        public IActionResult printReportKPI([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportCheckStockOnHandPartialService();
                var Models = new ReportCheckStockOnHandPartialViewModel();
                Models = JsonConvert.DeserializeObject<ReportCheckStockOnHandPartialViewModel>(body.ToString());
                localFilePath = service.printReportCheckStockOnHandPartial(Models, _hostingEnvironment.ContentRootPath);
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
                ReportCheckStockOnHandPartialService _appService = new ReportCheckStockOnHandPartialService();
                var Models = new ReportCheckStockOnHandPartialViewModel();
                Models = JsonConvert.DeserializeObject<ReportCheckStockOnHandPartialViewModel>(body.ToString());
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
