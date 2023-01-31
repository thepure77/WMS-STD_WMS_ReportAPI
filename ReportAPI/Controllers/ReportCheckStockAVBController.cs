﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportCheckStockAVB;
using System;
using System.Net;
using System.Net.Http;

namespace ReportAPI.Controllers
{
    [Route("api/ReportCheckStockAVB")]
    public class ReportCheckStockAVBController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ReportCheckStockAVBController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReportCheckStockAVB")]
        public IActionResult printReportKPI([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new ReportCheckStockAVBService();
                var Models = new ReportCheckStockAVBViewModel();
                Models = JsonConvert.DeserializeObject<ReportCheckStockAVBViewModel>(body.ToString());
                localFilePath = service.printReportCheckStockAVB(Models, _hostingEnvironment.ContentRootPath);
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
        public IActionResult ExportExcel([FromBody] JObject body)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string StockMovementPath = "";
            try
            {
                ReportCheckStockAVBService _appService = new ReportCheckStockAVBService();
                var Models = new ReportCheckStockAVBViewModel();
                Models = JsonConvert.DeserializeObject<ReportCheckStockAVBViewModel>(body.ToString());
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
