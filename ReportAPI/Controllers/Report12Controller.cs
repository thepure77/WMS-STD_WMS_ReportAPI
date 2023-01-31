﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using DataAccess;
using ReportBusiness.Report12;
using System.Net.Http;
using System.Net;

namespace ReportAPI.Controllers
{
    [Route("api/Report12")]
    public class Report12Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Report12Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("printReport12")]
        public IActionResult printReport12([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new Report12Service();
                var Models = new Report12ViewModel();
                Models = JsonConvert.DeserializeObject<Report12ViewModel>(body.ToString());
                localFilePath = service.printReport12(Models, _hostingEnvironment.ContentRootPath);
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
                Report12Service _appService = new Report12Service();
                var Models = new Report12ViewModel();
                Models = JsonConvert.DeserializeObject<Report12ViewModel>(body.ToString());
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