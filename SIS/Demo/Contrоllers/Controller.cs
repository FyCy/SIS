using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Demo.Contrоllers
{
    public abstract class Controller
    {
        protected IHttpRequest HttpRequest { get; set; }
        private bool IsLoggedIn()
        {
            return this.HttpRequest.Session.ContainsParameters("username");
        }
        private string ParseTemplate(string viewContent)
        {
            string result = viewContent;
            if (this.IsLoggedIn())
            {
                result = viewContent.Replace("@Model.Message", $"{this.HttpRequest.Session.GetParameters("username")}");
            }
            else
            {
                result = viewContent.Replace("@Model.Message", "");
            }
            return result;
        }
        public IHttpResponse View([CallerMemberName] string view = null)

        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;
            string viewContent = File.ReadAllText("Views/" + controllerName + ".html");

            //this.ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(this.ParseTemplate(viewContent), HttpResponseStatusCode.Ok);


            htmlResult.Cookies.AddCookie(new HttpCookie("lang", "en"));

            return htmlResult;
        }

        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
