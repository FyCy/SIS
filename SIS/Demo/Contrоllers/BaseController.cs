using Demo.Data;
using Demo.Services;
using Demo.Services.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.IO;
namespace Demo.Contrоllers
{
    public abstract class BaseController
    {
        public BaseController()
        {
            this.Db = new DemoDbContex();
            this.UserCookieService = new UserCookieService();
        }
        protected DemoDbContex Db { get; }

        protected IUserCookieService UserCookieService { get; }
       

        protected string GetUser(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-WoW"))
            {
                return null;
            }
            var cookie = request.Cookies.GetCookies(".auth-WoW");

            var cookieContent = cookie.Value;
            var userName = this.UserCookieService.GetUsetData(cookieContent);

            return userName;
        }
        protected IHttpResponse View (string viewName)
        {
           var content =  File.ReadAllText("Views/" + viewName + ".html");

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        public IHttpResponse BadRequestError (string errorMassage)
        {
            return new HtmlResult($"<h1>{errorMassage}</h1>", HttpResponseStatusCode.BadRequest);
        }

        public IHttpResponse ServerError(string errorMassage)
        {
            return new HtmlResult($"<h1>{errorMassage}</h1>", HttpResponseStatusCode.InternalServerError);
        }
    }
}
