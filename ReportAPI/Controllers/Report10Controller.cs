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
using ReportBusiness.Report10;
using System.Net.Http;
using System.Net;

namespace ReportAPI.Controllers
{
    [Route("api/Report10")]
    public class Report10Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report10Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport10")]
        public IActionResult printReport10([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report10Service();
                var Models = new Report10ViewModel();
                Models = JsonConvert.DeserializeObject<Report10ViewModel>(body.ToString());
                localFilePath = service.printReport10(Models, _hostingEnvironment.ContentRootPath);
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

        #region autoSearchAisle
        [HttpPost("autoSearchAisle")]
        public IActionResult autoSearchAisle([FromBody]JObject body)
        {
            try
            {
                var service = new Report10Service();
                var Models = new Report10ViewModel();
                Models = JsonConvert.DeserializeObject<Report10ViewModel>(body.ToString());
                var result = service.autoSearchAisle(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchlayer
        [HttpPost("autoSearchlayer")]
        public IActionResult autoSearchlayer([FromBody]JObject body)
        {
            try
            {
                var service = new Report10Service();
                var Models = new Report10ViewModel();
                Models = JsonConvert.DeserializeObject<Report10ViewModel>(body.ToString());
                var result = service.autoSearchlayer(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        [HttpPost]
        [Route("ExportExcel")]
        public IActionResult ExportExcel([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                Report10Service _appService = new Report10Service();
                var Models = new Report10ViewModel();
                Models = JsonConvert.DeserializeObject<Report10ViewModel>(body.ToString());
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