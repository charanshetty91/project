using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataAccess.CustomValidator;
using Newtonsoft.Json;

namespace DataAccess.Model
{
   public class LookupPostcodeRouteParameter
    {
        [NotNullOrWhiteSpaceValidator]
        public string PartialId { get; set; }

    }
}
