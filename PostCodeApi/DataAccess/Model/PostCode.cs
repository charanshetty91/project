using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class PostCode
    {
        public string country { get; set; }
        public string region { get; set; }
        public string adminDistrict { get; set; }
        public string parliamentaryConstituency { get; set; }
        public string area { get; set; }
    }
}
