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
using ReportBusiness.Report3;
using ReportBusiness.ReportGoodsReceive;

namespace ReportAPI.Controllers
{
    [Route("api/Report3")]
    public class Report3Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport3")]
        public IActionResult printReport3([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report3Service();
                var Models = new Report3ViewModel();
                Models = JsonConvert.DeserializeObject<Report3ViewModel>(body.ToString());
                localFilePath = service.printReport3(Models, _hostingEnvironment.ContentRootPath);
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
                Report3Service _appService = new Report3Service();
                var Models = new Report3ViewModel();
                Models = JsonConvert.DeserializeObject<Report3ViewModel>(body.ToString());
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


        #region autoSearchTag
        [HttpPost("autoSearchTag")]
        public IActionResult autoSearchTag([FromBody]JObject body)
        {
            try
            {
                var service = new Report3Service();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSearchTag(Models);
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