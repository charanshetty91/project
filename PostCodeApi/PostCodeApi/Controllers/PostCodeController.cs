using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Logging;

namespace PostCodeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodeController : ControllerBase
    {


          private IPostCodeRepository _postCodeRepository;
       

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
                var data = await _postCodeRepository.GetAllPostalCodeListById(partialId);
                return Ok(data.result);
            }
            catch (Exception e)
            {
                //context.Logger.Log("Execution of API :LookupPostcode : error detail ");
                //context.Logger.Log(e.StackTrace);
                throw;
            }
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
                return Ok(postCode);
            }
            return NotFound($"No Data found with this Id : {fullPostId} ");
        }
    }
}
