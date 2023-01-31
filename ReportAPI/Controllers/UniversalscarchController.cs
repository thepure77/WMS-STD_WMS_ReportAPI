using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterDataAPI.Controllers
{
    [Route("api/Universalscarch")]
    public class UniversalscarchController : Controller
    {
        #region UniversalscarchController
        private readonly IHostingEnvironment _hostingEnvironment;
        public UniversalscarchController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Universalscarch
        [HttpPost("Universalscarch")]
        public IActionResult Universalscarch([FromBody] JObject body)
        {
            try
            {
                var service = new UniversalscarchService();
                var Models = new UniversalscarchViewModel();
                Models = JsonConvert.DeserializeObject<UniversalscarchViewModel>(body.ToString());
                var result = service.Universalscarch(Models);
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
