using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ReportWrongLocation;

namespace ReportAPI.Controllers
{
    [Route("api/WrongLocation")]
    public class ReportWrongLocationController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportWrongLocationController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("filterViewWrongLocation")]
        public IActionResult filterViewWrongLocation([FromBody]JObject body)
        {
            try
            {
                var service = new ReportWrongLocationService();
                var Models = new ReportWrongLocationModel();
                Models = JsonConvert.DeserializeObject<ReportWrongLocationModel>(body.ToString());
                var result = service.filterViewWrongLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}