using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;

namespace Demo.Contrоllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Home (IHttpRequest request)
        {
            return this.View("Home");
        }

        public IHttpResponse GetUsername (IHttpRequest request)
        {
            return new HtmlResult($"<h1> Hello {this.GetUser(request)}", HttpResponseStatusCode.Ok);
        }
    }
}
