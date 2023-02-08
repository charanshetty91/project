using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
   public class PostCodeData
    {
        public int status { get; set; }
        public Result result { get; set; }


    }

   public class Result
    {
        public string country { get; set; }
        public double latitude { get; set; }
        public string region { get; set; }
        public string parliamentary_constituency { get; set; }
        public string admin_district { get; set; }
       
    }
}
