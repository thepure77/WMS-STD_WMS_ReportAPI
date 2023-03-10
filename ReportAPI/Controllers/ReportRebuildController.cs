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
using ReportBusiness.ReportRebuild;
using System.Net;
using System.Net.Http;
using MasterDataBusiness.ViewModels;
using ReportBusiness.Report21;

namespace ReportAPI.Controllers
{
    [Route("api/ReportRebuild")]
    public class ReportRebuildController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportRebuildController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        //[HttpPost("ExportRebuild")]
        //public IActionResult ExportRebuild([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new ReportRebuildViewModel();
        //        Models = JsonConvert.DeserializeObject<ReportRebuildViewModel>(body.ToString());
        //        var result = service.ExportExcel(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}



        [HttpPost]
        [Route("ExportRebuild")]
        public IActionResult ExportRebuild([FromBody]JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                ReportRebuildService _appService = new ReportRebuildService();
                var Models = new ReportRebuildViewModel();
                Models = JsonConvert.DeserializeObject<ReportRebuildViewModel>(body.ToString());
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



        //[HttpPost]
        //[Route("ExportRebuild")]
        //public IActionResult ExportRebuild([FromBody]JObject body)
        //{
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    string StockMovementPath = "";
        //    try
        //    {
        //        Report21Service _appService = new Report21Service();
        //        var Models = new Report21ViewModel();
        //        Models = JsonConvert.DeserializeObject<Report21ViewModel>(body.ToString());
        //        StockMovementPath = _appService.ExportExcel(Models, _hostingEnvironment.ContentRootPath);

        //        if (!System.IO.File.Exists(StockMovementPath))
        //        {
        //            return NotFound();
        //        }
        //        return File(System.IO.File.ReadAllBytes(StockMovementPath), "application/octet-stream");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    finally
        //    {
        //        System.IO.File.Delete(StockMovementPath);
        //    }
        //}





        //[HttpPost("printReport21")]
        //public IActionResult printReport21([FromBody]JObject body)
        //{
        //    string localFilePath = "";
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new ReportRebuildViewModel();
        //        Models = JsonConvert.DeserializeObject<ReportRebuildViewModel>(body.ToString());
        //        localFilePath = service.printReport21(Models, _hostingEnvironment.ContentRootPath);
        //        if (!System.IO.File.Exists(localFilePath))
        //        {
        //            return NotFound();
        //        }
        //        return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
        //        //return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //    finally
        //    {
        //        //System.IO.File.Delete(localFilePath);
        //    }
        //}


        //[HttpPost("printReport2xx1")]
        //public IActionResult printReport2xx1([FromBody]JObject body)
        //{
        //    string localFilePath = "";
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new ReportRebuildViewModel();
        //        Models = JsonConvert.DeserializeObject<ReportRebuildViewModel>(body.ToString());
        //        var result = service.printReport2xx1(Models);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //    finally
        //    {
        //        //System.IO.File.Delete(localFilePath);
        //    }
        //}

        //[HttpPost("getLocationType")]
        //public IActionResult getLocationType([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new LocationTypeViewModel();
        //        Models = JsonConvert.DeserializeObject<LocationTypeViewModel>(body.ToString());
        //        var result = service.getLocationType(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("getYn")]
        //public IActionResult getYn([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new YnViewModel();
        //        Models = JsonConvert.DeserializeObject<YnViewModel>(body.ToString());
        //        var result = service.getYn(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("getBlock")]
        //public IActionResult getBlock([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new BlockViewModel();
        //        Models = JsonConvert.DeserializeObject<BlockViewModel>(body.ToString());
        //        var result = service.getBlock(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //#region autoSearchLocation
        //[HttpPost("autoSearchLocation")]
        //public IActionResult autoSearchLocation([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new ReportRebuildService();
        //        var Models = new ItemListViewModel();
        //        Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
        //        var result = service.autoSearchLocation(Models);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //#endregion
    }
}