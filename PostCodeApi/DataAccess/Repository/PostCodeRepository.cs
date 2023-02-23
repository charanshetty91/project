using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Helper;
using DataAccess.Model;
using Newtonsoft.Json;
using ConstantsData = DataAccess.Constants.Constants;
namespace DataAccess.Repository
{
    public class PostCodeRepository : IPostCodeRepository
    {
        private readonly IMapper _mapper;
        private readonly IAreaFinder _areaFinder;

        public PostCodeRepository(IMapper mapper, IAreaFinder areaFinder)
        {
            _mapper = mapper;
            _areaFinder = areaFinder;
        }

        /// <summary>
        /// This method is used to get partial post code results.
        /// </summary>
        /// <param name="partialId">partial Id</param>
        /// <returns></returns>
        public async Task<PostCodeList> GetAllPostalCodeListById(string partialId)
        {
            var requestUri = $"{partialId}/autocomplete";
            var responseData = await ResponseData(requestUri);
            PostCodeList postCodeList = JsonConvert.DeserializeObject<PostCodeList>(responseData);
            return postCodeList;
        }

        /// <summary>
        /// This actions is used to get valid Post code details 
        /// </summary>
        /// <param name="fullId">full Id</param>
        /// <returns></returns>
        public async Task<PostCode> GetPostCodeById(string fullId)
        {
            var responseData = await ResponseData(fullId);
            PostCode resultData = null;
            PostCodeData postCodeData = JsonConvert.DeserializeObject<PostCodeData>(responseData);

            if (postCodeData != null && postCodeData.Result != null)
            {
                resultData = _mapper.Map<PostCode>(postCodeData.Result);
                resultData.Area = _areaFinder.GetArea(postCodeData.Result.Latitude);
            }

            return resultData;
        }
        private static async Task<string> ResponseData(string requestUri)
        {
            var client =new HttpClient
            {
                BaseAddress = new Uri(ConstantsData.ApiLink)
            };
            var result = await client.GetAsync(requestUri);
            var responseData = result.Content.ReadAsStringAsync().Result;
            return responseData;
        }

       
    }
}
