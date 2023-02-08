using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using Newtonsoft.Json;

namespace DataAccess.Repository
{
   public class PostCodeRepository : IPostCodeRepository
   {
        /// <summary>
        /// This method is used to get partial post code results.
        /// </summary>
        /// <param name="partialId">partial Id</param>
        /// <returns></returns>
        public async Task<PostCodeList> GetAllPostalCodeListById(string partialId)
       {
           var client = new HttpClient();
           var apiLink = "https://api.postcodes.io/postcodes/";
           client.BaseAddress = new Uri(apiLink);
           var result = await client.GetAsync($"{partialId}/autocomplete");
           var responseData = result.Content.ReadAsStringAsync().Result;

           PostCodeList postCodeList = JsonConvert.DeserializeObject<PostCodeList>(responseData);
           return postCodeList;
       }

        /// <summary>
        /// This actions is used to get valid Post code details 
        /// </summary>
        /// <param name="fullId">full Id</param>
        /// <returns></returns>
        public async Task<PostCodeData> GetPostCodeById(string fullId)
       {

           var client = new HttpClient();
           var apiLink = "https://api.postcodes.io/postcodes/";
           client.BaseAddress = new Uri(apiLink);
           var result = await client.GetAsync(fullId);
           var responseData = result.Content.ReadAsStringAsync().Result;

           PostCodeData postCodeData = JsonConvert.DeserializeObject<PostCodeData>(responseData);
           return postCodeData;
       }

        /// <summary>
        /// This action used to get area details based on latitude
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <returns></returns>
        public string GetArea(double latitude)
       {
           if (latitude < 52.229466) return "South";
           if (latitude >= 53.27169) return "North";
           if (52.229466 <= latitude && latitude < 53.27169) return "Midlands";
           return "";
       }
   }
}
