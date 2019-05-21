using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Contrallers
{
    public class HomeController :BaseController
    {
        public IHttpResponse Home(IHttpRequest request)
        {
            return this.View();
        }
    }
}
