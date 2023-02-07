using Amazon.Lambda.Core;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PostCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodeController : ControllerBase
    {


        private IPostCodeRepository _postCodeRepository;
        private ILambdaContext context;


        public PostCodeController(IPostCodeRepository postCodeRepository)
        {
            _postCodeRepository = postCodeRepository;

        }

        /// <summary>
        /// Lookup a postcode
        /// </summary>
        /// <param name="partialId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LookupPostcode/{partialId}")]
        public async Task<IActionResult> LookupPostcode([FromRoute] string partialId)
        {
            try
            {
                LambdaLogger.Log("LookupPostcode search started !!");
                var data = await _postCodeRepository.GetAllPostalCodeListById(partialId);
                if (data.result.Count > 0)
                {
                    LambdaLogger.Log($"query param name: {data}");
                    GetResponseHeader2Client();
                    return Ok(data.result);
                }
            }

            catch (Exception ex)
            {
                LambdaLogger.Log("Error in GetAllPostalCodeListById API");
                LambdaLogger.Log("Error details:" + ex.StackTrace);
              
            }
            return NotFound("no data found");
        }
        
        /// <summary>
        /// the “Autocomplete a postcode partial”
        /// </summary>
        /// <param name="partialId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAutoCompletePartialPostCode/{fullPostId}")]
        public async Task<ActionResult> AutocompletePostcodePartial([FromRoute] string fullPostId)
        {
            try
            {


                var data = await _postCodeRepository.GetPostCodeById(fullPostId);
                if (data.status == 200)
                {
                    PostCode postCode = new PostCode()
                    {
                        Country = data.result.country,
                        AdminDistrict = data.result.admin_district,
                        ParliamentaryConstituency = data.result.parliamentary_constituency,
                        Region = data.result.region,
                        Area = _postCodeRepository.GetArea(data.result.latitude)
                    };


                    GetResponseHeader2Client();
                    return Ok(postCode);
                }
                return NotFound($"No Data found with this Id : {fullPostId} ");
            }
            catch (Exception e)
            {
                return NotFound($"Error :Please contact administrator ");
            }
        }
        private void GetResponseHeader2Client()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,OPTIONS,PUT,DELETE");
        }
    }
}
