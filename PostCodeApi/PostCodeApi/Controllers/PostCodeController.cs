using Amazon.Lambda.Core;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace PostCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PostCodeController : ControllerBase
    {
        private readonly IPostCodeRepository _postCodeRepository;

        public PostCodeController(IPostCodeRepository postCodeRepository)
        {
            _postCodeRepository = postCodeRepository;

        }

        /// <summary>
        /// Lookup a postcode
        /// </summary>
        /// <param name="partialId">partial Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("LookupPostcode/{PartialId:minlength(1)}/{MaxResultCount:int}")]
        public async Task<IActionResult> LookupPostcode([FromRoute] LookupPostcodeRouteParameter parms)
        {
            try
            {
                LambdaLogger.Log("LookupPostcode search started !!");
                var data = await _postCodeRepository.GetAllPostalCodeListById(parms.PartialId);
                int count = 0;
                if (data.result == null)
                {
                    LambdaLogger.Log($"no data for current search");
                    return NotFound("no data found");
                }
                if (data.result.Count > 0)
                {
                    LambdaLogger.Log($"search data found!!");
                    count = data.result.Count > parms.MaxResultCount ? parms.MaxResultCount : data.result.Count;
                    return Ok(data.result.GetRange(0, count));
                }
                return NotFound("no data found");
            }

            catch (Exception ex)
            {
                LambdaLogger.Log("Error in LookupPostcode API");
                LambdaLogger.Log("Error details:" + ex.StackTrace);
                return NotFound($"Error :Please contact administrator ");
            }
        }

        /// <summary>
        /// the “Autocomplete a postcode partial”
        /// </summary>
        /// <param name="fullPostId">full PostCode Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAutoCompletePartialPostCode/{FullPostId:minlength(1)}")]
        public async Task<ActionResult> AutocompletePostcodePartial([FromRoute] AutocompletePostcodePartialRouteParameter parms)
        {
            try
            {
                var data = await _postCodeRepository.GetPostCodeById(parms.FullPostId);
                if (data == null)
                {
                    return NotFound($"No Data found with this code ");
                }
                return Ok(data);
               
            }
            catch (Exception ex)
            {
                LambdaLogger.Log("Error in AutocompletePostcodePartial API");
                LambdaLogger.Log("Error Details:" + ex.StackTrace);
                return NotFound($"Error :Please contact administrator ");
            }
        }
    }
}
