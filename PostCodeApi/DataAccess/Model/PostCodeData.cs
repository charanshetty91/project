using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataAccess.Model
{
    public class PostCodeData
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public Result Result { get; set; }


    }

    public class Result
    {
        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "Region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "parliamentary_constituency")]
        public string ParliamentaryConstituency { get; set; }

        [JsonProperty(PropertyName = "admin_district")]
        public string AdminDistrict { get; set; }

    }
}
