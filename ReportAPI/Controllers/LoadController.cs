using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.Load;
using System;

namespace ReportAPI.Controllers
{
    [Route("api/TruckLoad")]
    public class LoadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public LoadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        #region printOutTracePicking
        [HttpPost("printOutTracePicking")]
        public IActionResult printOutTracePicking([FromBody]JObject body)
        {
            try
            {
                var service = new LoadService();
                var Models = new Trace_picking();
                Models = JsonConvert.DeserializeObject<Trace_picking>(body.ToString());
                var result = service.printOutTracePicking(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region printOutTraceLoading
        [HttpPost("printOutTraceLoading")]
        public IActionResult printOutTraceLoading([FromBody]JObject body)
        {
            try
            {
                var service = new LoadService();
                var Models = new Trace_loading();
                Models = JsonConvert.DeserializeObject<Trace_loading>(body.ToString());
                var result = service.printOutTraceLoading(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        [HttpPost("ExcelOutTraceLoading")]
        public IActionResult ExcelOutTraceLoading([FromBody] JObject body)
        {
            try
            {
                var service = new LoadService();
                var Models = new Trace_loading();
                Models = JsonConvert.DeserializeObject<Trace_loading>(body.ToString());
                var result = service.ExcelOutTraceLoading(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ExcelOutTracePick")]
        public IActionResult ExcelOutTracePick([FromBody] JObject body)
        {
            try
            {
                var service = new LoadService();
                var Models = new Trace_picking();
                Models = JsonConvert.DeserializeObject<Trace_picking>(body.ToString());
                var result = service.ExcelOutPick(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region printOutTraceTransferReplenish
        [HttpPost("printOutTraceTransferReplenish")]
        public IActionResult printOutTraceTransferReplenish([FromBody] JObject body)
        {
            try
            {
                var service = new LoadService();
                var Models = new TraceTransferModel();
                Models = JsonConvert.DeserializeObject<TraceTransferModel>(body.ToString());
                var result = service.printOutTraceTransferReplenish(Models);

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
