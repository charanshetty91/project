using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DataAccess.Model
{
    public class PostCodeList
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "result")]
        public List<string> Result { get; set; }
    }

    public class PartialCodeResult
    {
      
        public string Result { get; set; }

        public string  Area { get; set; }
    }
}
