using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.CheckReplenishByOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportAPI.Controllers
{
    [Route("api/CheckReplenishByOrder")]
    public class CheckReplenishByOrderController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public CheckReplenishByOrderController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printCheckReplenishByOrder")]
        public IActionResult printReportPan([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new CheckReplenishByOrderService();
                var Models = new CheckReplenishByOrderViewModel();
                Models = JsonConvert.DeserializeObject<CheckReplenishByOrderViewModel>(body.ToString());
                localFilePath = service.printReportPan(Models, _hostingEnvironment.ContentRootPath);
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
                CheckReplenishByOrderService _appService = new CheckReplenishByOrderService();
                var Models = new CheckReplenishByOrderViewModel();
                Models = JsonConvert.DeserializeObject<CheckReplenishByOrderViewModel>(body.ToString());
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
