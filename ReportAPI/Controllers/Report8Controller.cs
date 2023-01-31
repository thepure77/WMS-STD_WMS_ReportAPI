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
using ReportBusiness.Report8;
using ReportBusiness.ReportGoodsReceive;

namespace ReportAPI.Controllers
{
    [Route("api/Report8")]
    public class Report8Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report8Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport8")]
        public IActionResult printReport8([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report8Service();
                var Models = new Report8ViewModelV2();
                Models = JsonConvert.DeserializeObject<Report8ViewModelV2>(body.ToString());
                localFilePath = service.printReport8(Models, _hostingEnvironment.ContentRootPath);
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

        #region autoSearchTagNo
        [HttpPost("autoSearchTagNo")]
        public IActionResult autoSearchTagNo([FromBody]JObject body)
        {
            try
            {
                var service = new Report8Service();
                var Models = new Report8ViewModel();
                Models = JsonConvert.DeserializeObject<Report8ViewModel>(body.ToString());
                var result = service.autoSearchTagNo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchLocationID
        [HttpPost("autoSearchLocationID")]
        public IActionResult autoSearchLocationID([FromBody]JObject body)
        {
            try
            {
                var service = new Report8Service();
                var Models = new Report8ViewModel();
                Models = JsonConvert.DeserializeObject<Report8ViewModel>(body.ToString());
                var result = service.autoSearchLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchOwnerID
        [HttpPost("autoSearchOwnerID")]
        public IActionResult autoSearchOwnerID([FromBody]JObject body)
        {
            try
            {
                var service = new Report8Service();
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
            string StockMovementPath = "";
            try
            {
                Report8Service _appService = new Report8Service();
                var Models = new Report8ViewModelV2();
                Models = JsonConvert.DeserializeObject<Report8ViewModelV2>(body.ToString());
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

        #region autoSearchSloc
        [HttpPost("autoSearchSloc")]
        public IActionResult autoSearchSloc([FromBody]JObject body)
        {
            try
            {
                var service = new Report8Service();
                var Models = new Report8ViewModel();
                Models = JsonConvert.DeserializeObject<Report8ViewModel>(body.ToString());
                var result = service.autoSearchSloc(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoCreateBy
        [HttpPost("autoCreateBy")]
        public IActionResult autoCreateBy([FromBody]JObject body)
        {
            try
            {
                var service = new Report8Service();
                var Models = new Report8ViewModel();
                Models = JsonConvert.DeserializeObject<Report8ViewModel>(body.ToString());
                var result = service.autoCreateBy(Models);
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