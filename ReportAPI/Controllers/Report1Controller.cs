using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using DataAccess;
using ReportBusiness.Report1;
//using ReportBusiness.Report1;
using System.Net.Http;
using System.Net;
using ReportBusiness.ConfigModel;

namespace ReportAPI.Controllers
{
    [Route("api/Report1")]
    public class Report1Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report1Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport1")]
        public IActionResult printReport1([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report1Service();
                var Models = new Report1ViewModel_V2();
                Models = JsonConvert.DeserializeObject<Report1ViewModel_V2>(body.ToString());
                localFilePath = service.printReport1(Models, _hostingEnvironment.ContentRootPath);
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
                Report1Service _appService = new Report1Service();
                var Models = new Report1ViewModel_V2();
                Models = JsonConvert.DeserializeObject<Report1ViewModel_V2>(body.ToString());
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