using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class PostCode
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string AdminDistrict { get; set; }
        public string ParliamentaryConstituency { get; set; }
        public string Area { get; set; }
    }
}
