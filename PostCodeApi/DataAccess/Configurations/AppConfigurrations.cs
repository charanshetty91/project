using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class AppConfigurations : IAppConfigurations
    {
        public int MaxResultCount { get; set; }
    }
}
