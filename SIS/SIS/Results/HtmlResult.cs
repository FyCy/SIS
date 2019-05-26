using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    public class HtmlResult : HttpResponse
    {
        public HtmlResult (string content , HttpResponseStatusCode responseStasusCode) : base(responseStasusCode)
        {
            this.Headers.AddHeader(new HttpHeader("Content-Type", "text/html; charset=8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

    }
}
