using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportBusiness.ConfigModel;
using ReportBusiness.ReportReceiving;

namespace ReportAPI.Controllers
{
    [Route("api/DropdownReport")]
    //[ApiController]
    public class DropdownReportController : Controller
    {
        [HttpPost("dropdownDocumentType")]
        public IActionResult dropdownDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new DropdownService();
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.dropdownDocumentType(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("dropdownLocationType")]
        public IActionResult dropdownLocationType([FromBody]JObject body)
        {
            try
            {
                var service = new DropdownService();
                var Models = new LocationTypeViewModel();
                Models = JsonConvert.DeserializeObject<LocationTypeViewModel>(body.ToString());
                var result = service.dropdownLocationType(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #region autoUser
        [HttpPost("autoUser")]
        public IActionResult autoUser([FromBody]JObject body)
        {
            try
            {
                var service = new DropdownService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region businessUnit
        [HttpPost("dropdownBusinessUnit")]
        public IActionResult dropdownBusinessUnit([FromBody] JObject body)
        {
            try
            {
                var service = new DropdownService();
                var Models = new BusinessUnitViewModel();
                Models = JsonConvert.DeserializeObject<BusinessUnitViewModel>(body.ToString());
                var result = service.dropdownBusinessUnit(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoUser
        [HttpPost("autoVendor")]
        public IActionResult autoVendor([FromBody]JObject body)
        {
            try
            {
                var service = new DropdownService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVendor(Models);
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