using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Demo.Contrallers
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName] string view = null)

        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;
            string viewContent = File.ReadAllText("Views/"+controllerName  + ".html");


            HtmlResult html = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);

            html.Cookies.AddCookie(new HttpCookie("lang", "en"));
            html.Cookies.AddCookie(new HttpCookie("lang2", "en"));

            return html; //new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
        }
    }
}
