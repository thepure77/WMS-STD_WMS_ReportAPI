using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportBusiness.ViewStockOnCartonFlow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReportAPI.Controllers
{
    [Route("api/ViewStockCartonFlow")]
    public class ViewStockOnCartonFlowController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ViewStockOnCartonFlowController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("filterViewSt")]
        public IActionResult filterView([FromBody]JObject body)
        {
            try
            {
                var service = new ViewStockOnCartonFlowService();
                var Models = new ViewStockOnCartonFlowModel();
                Models = JsonConvert.DeserializeObject<ViewStockOnCartonFlowModel>(body.ToString());
                var result = service.filterview(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("ExportViewSt")]
        public IActionResult ExportThanawat([FromBody] JObject body)
        {
            try
            {
                var service = new ViewStockOnCartonFlowService();
                var Models = new ViewStockOnCartonFlowModel();
                Models = JsonConvert.DeserializeObject<ViewStockOnCartonFlowModel>(body.ToString());
                var result = service.ExportThanawat(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("getLocationTypeFlowCa")]
        public IActionResult getLocationTypeFlowCa([FromBody] JObject body)
        {
            try
            {
                var service = new ViewStockOnCartonFlowService();
                var Models = new LocationTypeFlowCaViewModel();
                Models = JsonConvert.DeserializeObject<LocationTypeFlowCaViewModel>(body.ToString());
                var result = service.getLocationTypeFlowCa(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}