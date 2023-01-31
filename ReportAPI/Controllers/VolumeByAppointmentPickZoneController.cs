﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.VolumeByAppointmentPickZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportAPI.Controllers
{
    [Route("api/VolumeByAppointmentPickZone")]
    public class VolumeByAppointmentPickZoneController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public VolumeByAppointmentPickZoneController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("printVolumeByAppointmentPickZone")]
        public IActionResult printReportPan([FromBody] JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new VolumeByAppointmentPickZoneService();
                var Models = new VolumeByAppointmentPickZoneViewModel();
                Models = JsonConvert.DeserializeObject<VolumeByAppointmentPickZoneViewModel>(body.ToString());
                localFilePath = service.printReportAppointmentPickZone(Models, _hostingEnvironment.ContentRootPath);
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
                VolumeByAppointmentPickZoneService _appService = new VolumeByAppointmentPickZoneService();
                var Models = new VolumeByAppointmentPickZoneViewModel();
                Models = JsonConvert.DeserializeObject<VolumeByAppointmentPickZoneViewModel>(body.ToString());
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
