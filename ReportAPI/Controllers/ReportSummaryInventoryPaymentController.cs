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
using ReportBusiness.ReportSummaryInventoryPayment;
using MasterDataBusiness.ViewModels;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportSummaryInventoryPayment")]
    public class ReportSummaryInventoryPaymentController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportSummaryInventoryPaymentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportSummaryInventoryPayment")]
        public IActionResult printReportSummaryInventoryPayment([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportSummaryInventoryPaymentService();
                var Models = new ReportSummaryInventoryPaymentViewModel();
                Models = JsonConvert.DeserializeObject<ReportSummaryInventoryPaymentViewModel>(body.ToString());
                localFilePath = service.printReportSummaryInventoryPayment(Models, _hostingEnvironment.ContentRootPath);
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
            string Path = "";
            try
            {
                ReportSummaryInventoryPaymentService _appService = new ReportSummaryInventoryPaymentService();
                var Models = new ReportSummaryInventoryPaymentViewModel();
                Models = JsonConvert.DeserializeObject<ReportSummaryInventoryPaymentViewModel>(body.ToString());
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

        #region autoSearchOwnerID
        [HttpPost("autoSearchOwnerID")]
        public IActionResult autoSearchOwnerID([FromBody]JObject body)
        {
            try
            {
                var service = new ReportSummaryInventoryPaymentService();
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

    }
}