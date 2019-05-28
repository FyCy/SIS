using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Routing;

namespace Demo.Contrоllers
{
    public class HomeController : BaseController
    {
        [HttpGet("/")]
        public IHttpResponse Home ()
        {
            return this.View("Home");
        }
        [HttpGet("/hello")]

        public IHttpResponse GetUsername ()
        {
             PrepareHtmlResult($"<h1> Hello {this.User}", HttpResponseStatusCode.Ok);
            return this.Response;
        }
    }
}
