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
using ReportBusiness.ReportVendorWarehouse;

namespace ReportAPI.Controllers
{
    [Route("api/ReportVendorWarehouse")]
    public class ReportVendorWarehouseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportVendorWarehouseController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("PrintReportVendorWarehouse")]
        public IActionResult printReportVendorWarehouse([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportVendorWarehouseService();
                var Models = new ReportVendorWarehouseViewModel();
                Models = JsonConvert.DeserializeObject<ReportVendorWarehouseViewModel>(body.ToString());
                localFilePath = service.printReportVendorWarehouse(Models, _hostingEnvironment.ContentRootPath);
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
                var service = new ReportVendorWarehouseService();
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
                ReportVendorWarehouseService _appService = new ReportVendorWarehouseService();
                var Models = new ReportVendorWarehouseViewModel();
                Models = JsonConvert.DeserializeObject<ReportVendorWarehouseViewModel>(body.ToString());
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