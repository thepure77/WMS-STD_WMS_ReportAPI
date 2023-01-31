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
using ReportBusiness;
using ReportBusiness.Report15;
using ReportBusiness.Report18;
using ReportBusiness.ReportGoodsReceive;

namespace ReportAPI.Controllers
{
    [Route("api/Report15")]
    public class Report15Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report15Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("PrintReport15")]
        public IActionResult printReport15([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report15Service();
                var Models = new Report15ViewModel();
                Models = JsonConvert.DeserializeObject<Report15ViewModel>(body.ToString());
                localFilePath = service.printReport15(Models, _hostingEnvironment.ContentRootPath);
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
                var service = new Report15Service();
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

        #region autoSearchOwnerID
        [HttpPost("autoSearchMC")]
        public IActionResult autoSearchMC([FromBody]JObject body)
        {
            try
            {
                var service = new Report15Service();
                var Models = new Report15ViewModel();
                Models = JsonConvert.DeserializeObject<Report15ViewModel>(body.ToString());
                var result = service.autoSearchMC(Models);
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
                Report15Service _appService = new Report15Service();
                var Models = new Report15ViewModel();
                Models = JsonConvert.DeserializeObject<Report15ViewModel>(body.ToString());
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

        #region autoGenReport15
        [HttpGet("autoGenReport15")]
        public IActionResult autoGenReport15()
        {
            try
            {
                var service = new Report15Service();
                var result = service.autoGenReport15(_hostingEnvironment.ContentRootPath);
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