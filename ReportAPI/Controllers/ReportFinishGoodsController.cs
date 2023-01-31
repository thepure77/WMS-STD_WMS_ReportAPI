using System;
using System.Net;
using System.Net.Http;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportFinishGoods;
using ReportBusiness.ReportFinishGoodsService;

namespace ReportAPI.Controllers
{
    [Route("api/reportFinishGoods")]
    public class ReportFinishGoodsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportFinishGoodsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("reportFinishGoods")]
        public IActionResult reportFinishGoods([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportFinishGoodsService();
                var Models = new ReportFinishGoodsViewModel();
                Models = JsonConvert.DeserializeObject<ReportFinishGoodsViewModel>(body.ToString());
                localFilePath = service.printReportFinishGoods(Models, _hostingEnvironment.ContentRootPath);
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
                var service = new ReportFinishGoodsService();
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
                ReportFinishGoodsService _appService = new ReportFinishGoodsService();
                var Models = new ReportFinishGoodsViewModel();
                Models = JsonConvert.DeserializeObject<ReportFinishGoodsViewModel>(body.ToString());
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