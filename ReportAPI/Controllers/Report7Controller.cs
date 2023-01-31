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
using ReportBusiness.Report7;
using System.Net.Http;
using System.Net;
using MasterDataBusiness.ViewModels;

namespace ReportAPI.Controllers
{
    [Route("api/Report7")]
    public class Report7Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report7Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport7")]
        public IActionResult printReport7([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report7Service();
                var Models = new Report7ViewModel();
                Models = JsonConvert.DeserializeObject<Report7ViewModel>(body.ToString());
                localFilePath = service.printReport7(Models, _hostingEnvironment.ContentRootPath);
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
                Report7Service _appService = new Report7Service();
                var Models = new Report7ViewModel();
                Models = JsonConvert.DeserializeObject<Report7ViewModel>(body.ToString());
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

        #region autoSearchUser
        [HttpPost("autoSearchUser")]
        public IActionResult autoSearchUser([FromBody]JObject body)
        {
            try
            {
                var service = new Report7Service();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}