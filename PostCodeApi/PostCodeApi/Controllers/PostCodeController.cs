using Amazon.Lambda.Core;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataAccess.Constants;
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
        [Route("LookupPostcode/{PartialId:minlength(1)}/{MaxResultCount:int:min(0)}")]
        public async Task<IActionResult> LookupPostcode([FromRoute] LookupPostcodeRouteParameter parms)
        {
            try
            {
                LambdaLogger.Log("LookupPostcode search started !!");
                var data = await _postCodeRepository.GetAllPostalCodeListById(parms);
                if (data == null)
                {
                    LambdaLogger.Log($"no data for current search");
                    return NotFound(Constants.NoDataMessage);
                }
                LambdaLogger.Log($"search data found!!");
                return Ok(data);
            }

            catch (Exception ex)
            {
                LambdaLogger.Log("Error in LookupPostcode API");
                LambdaLogger.Log("Error details:" + ex.StackTrace);
                return NotFound(Constants.ExceptionErrorMessage);
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
                    return NotFound(Constants.NoDataMessage);
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                LambdaLogger.Log("Error in AutocompletePostcodePartial API");
                LambdaLogger.Log("Error Details:" + ex.StackTrace);
                return NotFound(Constants.ExceptionErrorMessage);
            }
        }
    }
}
