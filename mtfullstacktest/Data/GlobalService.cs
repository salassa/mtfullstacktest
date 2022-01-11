using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtfullstacktest.Data
{
    public class GlobalService
    {
        private readonly IConfiguration _iconfigurationurl;
        public GlobalService(IConfiguration iconfigurationurl)
        {
            _iconfigurationurl = iconfigurationurl;
        }

        public string GetURLApi()
        {
            return _iconfigurationurl.GetValue<string>("ClientAppSettings:BaseApiUrl");
        }
        public string GetBaseURL()
        {
            return _iconfigurationurl.GetValue<string>("ClientAppSettings:BaseUrl");
        }
    }
}
