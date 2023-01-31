using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportGoodsReceive;
using ReportBusiness.ReportRetrun;

namespace ReportAPI.Controllers
{
    [Route("api/ReportRetrun")]
    public class ReportRetrunController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportRetrunController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportRetrun")]
        public IActionResult printReport3([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportRetrunService();
                var Models = JsonConvert.DeserializeObject<ReportRetrunViewModel>(body.ToString());
                localFilePath = service.printReportRetrun(Models, _hostingEnvironment.ContentRootPath);
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
            string Path = "";
            try
            {
                ReportRetrunService _appService = new ReportRetrunService();
                var Models = new ReportRetrunViewModel();
                Models = JsonConvert.DeserializeObject<ReportRetrunViewModel>(body.ToString());
                Path = _appService.ExportExcel(Models, _hostingEnvironment.ContentRootPath);

                if (!System.IO.File.Exists(Path))
                {
                    return NotFound();
                }
                return File(System.IO.File.ReadAllBytes(Path), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                System.IO.File.Delete(Path);
            }
        }
    }
}