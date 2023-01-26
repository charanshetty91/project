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

        public async Task<PostCodeData> GetPostCodeById(string postCodeId)
        {

            var client = new HttpClient();
            var apiLink = "https://api.postcodes.io/postcodes/";
            client.BaseAddress = new Uri(apiLink);
            var result = await client.GetAsync(postCodeId);
            var responseData = result.Content.ReadAsStringAsync().Result;

            PostCodeData postCodeData = JsonConvert.DeserializeObject<PostCodeData>(responseData);
            return postCodeData;
        }

        public string GetArea(double latitude)
        {
            if (latitude < 52.229466) return "South";
            if (latitude >= 53.27169) return "North";
            if (52.229466 <= latitude && latitude < 53.27169) return "Midlands";
            return "";
        }
    }
}
