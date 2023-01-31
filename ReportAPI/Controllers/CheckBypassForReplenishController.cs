using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{

    [Route("api/checkBypassForReplenish")]
    public class CheckBypassForReplenishController : Controller
    {
        [HttpPost("filtercheckBypassForReplenish")]
        public IActionResult filtercheckBypassForReplenish([FromBody]JObject body)
        {
            try
            {
                var service = new CheckBypassForReplenishService();
                var Models = new CheckBypassForReplenishViewModel();
                Models = JsonConvert.DeserializeObject<CheckBypassForReplenishViewModel>(body.ToString());
                var result = service.filtercheckBypassForReplenish(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Export")]
        public IActionResult ExportCheckBypassForReplenish([FromBody]JObject body)
        {
            try
            {
                var service = new CheckBypassForReplenishService();
                var Models = new CheckBypassForReplenishExportViewModel();
                Models = JsonConvert.DeserializeObject<CheckBypassForReplenishExportViewModel>(body.ToString());
                var result = service.ExportCheckBypassForReplenish(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
