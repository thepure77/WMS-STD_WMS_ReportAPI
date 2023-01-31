using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.Labor.NonStandardTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportAPI.Controllers
{
    [Route("api/NonStandardizedTransactions")]
    public class NonStandardizedTransactionsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public NonStandardizedTransactionsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            string localFilePath = "";
            try
            {
                var service = new NonStandardTransactionsService();
                var Models = new SearchDetailModel();
                Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
