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
using ReportBusiness.Report9;
using MasterDataBusiness.ViewModels;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/Report9")]
    public class Report9Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report9Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport9")]
        public IActionResult printReport9([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report9Service();
                var Models = new Report9ViewModel();
                Models = JsonConvert.DeserializeObject<Report9ViewModel>(body.ToString());
                localFilePath = service.printReport9(Models, _hostingEnvironment.ContentRootPath);
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


        #region autoSearchOwnerID
        [HttpPost("autoSearchOwnerID")]
        public IActionResult autoSearchOwnerID([FromBody]JObject body)
        {
            try
            {
                var service = new Report9Service();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        [HttpPost("getUserGroupMenu")]
        public IActionResult getUserGroupMenu([FromBody]JObject body)
        {
            try
            {
                var service = new Report9Service();
                var Models = new ConfigUserGroupMenuViewModel();
                Models = JsonConvert.DeserializeObject<ConfigUserGroupMenuViewModel>(body.ToString());
                var result = service.getUserGroupMenu(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getZone")]
        public IActionResult getZone([FromBody]JObject body)
        {
            try
            {
                var service = new Report9Service();
                var Models = new ZoneViewModel();
                Models = JsonConvert.DeserializeObject<ZoneViewModel>(body.ToString());
                var result = service.getZone(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                Report9Service _appService = new Report9Service();
                var Models = new Report9ViewModel();
                Models = JsonConvert.DeserializeObject<Report9ViewModel>(body.ToString());
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