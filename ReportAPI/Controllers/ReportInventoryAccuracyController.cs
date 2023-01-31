using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportInventoryAccuracy;

namespace ReportAPI.Controllers
{
    [Route("api/ReportInventoryAccuracy")]
    public class ReportInventoryAccuracyController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportInventoryAccuracyController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportInventoryAccuracy")]
        public IActionResult printReport9([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportInventoryAccuracyService();
                var Models = new ReportInventoryAccuracyViewModel();
                Models = JsonConvert.DeserializeObject<ReportInventoryAccuracyViewModel>(body.ToString());
                localFilePath = service.printReportInventoryAccuracy(Models, _hostingEnvironment.ContentRootPath);
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
                ReportInventoryAccuracyService _appService = new ReportInventoryAccuracyService();
                var Models = new ReportInventoryAccuracyViewModel();
                Models = JsonConvert.DeserializeObject<ReportInventoryAccuracyViewModel>(body.ToString());
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
