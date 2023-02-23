using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.CustomValidator;

namespace DataAccess.Model
{
    public class AutocompletePostcodePartialRouteParameter
    {
        [NotNullOrWhiteSpaceValidator]
        public string FullPostId { get; set; }
    }
}
