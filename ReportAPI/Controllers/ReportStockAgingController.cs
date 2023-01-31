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
using ReportBusiness.ReportGoodsReceive;
using ReportBusiness.ReportStockAging;

namespace ReportAPI.Controllers
{
    [Route("api/ReportStockAging")]
    public class ReportStockAgingController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportStockAgingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("PrintReportStockAging")]
        public IActionResult printReportStockAging([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportStockAgingService();
                var Models = new ReportStockAgingViewModel();
                Models = JsonConvert.DeserializeObject<ReportStockAgingViewModel>(body.ToString());
                localFilePath = service.printReportStockAging(Models, _hostingEnvironment.ContentRootPath);
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
                var service = new ReportStockAgingService();
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

        [HttpPost]
        [Route("ExportExcel")]
        public IActionResult ExportExcel([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string Path = "";
            try
            {
                ReportStockAgingService _appService = new ReportStockAgingService();
                var Models = new ReportStockAgingViewModel();
                Models = JsonConvert.DeserializeObject<ReportStockAgingViewModel>(body.ToString());
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