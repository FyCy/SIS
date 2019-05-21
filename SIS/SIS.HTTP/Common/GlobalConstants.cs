using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Common
{
    public class GlobalConstants
    {
        public const string HttpOneProtocolFragment = "HTTP/1.1";


        public const string HostHeaderKey = "Host";

        public const string HttpNewLine = "\r\n";

        public const string UnsupportedHttpMethodExceptionMessage = "The Http method - {0} is not supported.";
    }
}
