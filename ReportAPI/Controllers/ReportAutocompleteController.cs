using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using DataAccess;
using ReportBusiness.ReportAutocomplete;
using System.Net.Http;
using System.Net;

namespace ReportAPI.Controllers
{
    [Route("api/ReportAutocomplete")]
    public class ReportAutocompleteController : Controller
    {

        #region autoGINo
        [HttpPost("autoGINo")]
        public IActionResult autoGINo([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoGINo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoPlanGINo
        [HttpPost("autoPlanGINo")]
        public IActionResult autoPlanGINo([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoPlanGINo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoProductType
        [HttpPost("autoProductType")]
        public IActionResult autoProductType([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoProductType(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoBinCardYear
        [HttpPost("autoBinCardYear")]
        public IActionResult autoBinCardYear([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoBinCardYear(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoWarehouse
        [HttpPost("autoWarehouse")]
        public IActionResult autoWarehouse([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoWarehouse(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwner
        [HttpPost("autoOwner")]
        public IActionResult autoOwner([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoOwner(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwnerBOM
        [HttpPost("autoOwnerBOM")]
        public IActionResult autoOwnerBOM([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoOwnerBOM(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwner
        [HttpPost("autoOwnerID")]
        public IActionResult autoOwnerID([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoOwnerID(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoMC
        [HttpPost("autoMC")]
        public IActionResult autoMC([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoMC(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoProductIdStockOnCartonFlow
        [HttpPost("AutoProductIdStockOnCartonFlow")]
        public IActionResult AutoProductIdStockOnCartonFlow([FromBody]JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.AutoProductIdStockOnCartonFlow(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoShipTo
        [HttpPost("AutoShipTo")]
        public IActionResult AutoShipTo([FromBody] JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.AutoProductIdStockOnCartonFlow(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoSearchVendor
        [HttpPost("autoSearchVendor")]
        public IActionResult autoSearchVendor([FromBody] JObject body)
        {
            try
            {
                var service = new ReportAutocompleteService();
                var Models = new ReportAutocompleteViewModel();
                Models = JsonConvert.DeserializeObject<ReportAutocompleteViewModel>(body.ToString());
                var result = service.autoSearchVendor(Models);
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