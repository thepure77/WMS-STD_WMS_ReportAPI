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
using ReportBusiness.ReportSummaryMaterialsStock;
using MasterDataBusiness.ViewModels;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportSummaryMaterialsStock")]
    public class ReportSummaryMaterialsStockController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportSummaryMaterialsStockController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportSummaryMaterialsStock")]
        public IActionResult printReportSummaryMaterialsStock([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportSummaryMaterialsStockService();
                var Models = new ReportSummaryMaterialsStockViewModel();
                Models = JsonConvert.DeserializeObject<ReportSummaryMaterialsStockViewModel>(body.ToString());
                localFilePath = service.printReportSummaryMaterialsStock(Models, _hostingEnvironment.ContentRootPath);
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
                var service = new ReportSummaryMaterialsStockService();
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
                ReportSummaryMaterialsStockService _appService = new ReportSummaryMaterialsStockService();
                var Models = new ReportSummaryMaterialsStockViewModel();
                Models = JsonConvert.DeserializeObject<ReportSummaryMaterialsStockViewModel>(body.ToString());
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