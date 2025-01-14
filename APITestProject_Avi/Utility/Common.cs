using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject_Avi.Utility
{
    public static class Common
    {
        private const string _baseUrl = "http://localhost:8080/onlinewallet/";

        public static string BaseUrl
        {
            get { return _baseUrl; }
        }
    }
}
