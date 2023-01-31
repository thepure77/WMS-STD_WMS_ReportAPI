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
using ReportBusiness;
using ReportBusiness.Report16;
using ReportBusiness.Report16;
using ReportBusiness.Report18;
using ReportBusiness.ReportGoodsReceive;

namespace ReportAPI.Controllers
{
    [Route("api/Report16")]
    public class Report16Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report16Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("PrintReport16")]
        public IActionResult printReport16([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report16Service();
                var Models = new Report16ViewModel();
                Models = JsonConvert.DeserializeObject<Report16ViewModel>(body.ToString());
                localFilePath = service.printReport16(Models, _hostingEnvironment.ContentRootPath);
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
                Report16Service _appService = new Report16Service();
                var Models = new Report16ViewModel();
                Models = JsonConvert.DeserializeObject<Report16ViewModel>(body.ToString());
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