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
using ReportBusiness.Report5;
using ReportBusiness.Report6;
using ReportBusiness;
using System.Net.Http;
using System.Net;
using MasterDataBusiness.ViewModels;

namespace ReportAPI.Controllers
{
    [Route("api/Report6")]
    public class Report6Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report6Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport6")]
        public IActionResult printReport6([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report6Service();
                var Models = new Report6ViewModel();
                Models = JsonConvert.DeserializeObject<Report6ViewModel>(body.ToString());
                localFilePath = service.printReport6(Models, _hostingEnvironment.ContentRootPath);
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
                Report6Service _appService = new Report6Service();
                var Models = new Report6ViewModel();
                Models = JsonConvert.DeserializeObject<Report6ViewModel>(body.ToString());
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
                var service = new Report6Service();
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

        #region autoSearchUser
        [HttpPost("autoSearchUserPick")]
        public IActionResult autoSearchUserPick([FromBody]JObject body)
        {
            try
            {
                var service = new Report6Service();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchUserPick(Models);
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