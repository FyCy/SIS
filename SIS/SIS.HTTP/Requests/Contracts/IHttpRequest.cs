using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using System.Collections.Generic;

namespace SIS.HTTP.Requests
{
    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        IHttpCookieCollection Cookies { get; }
        Dictionary<string, object> FormData { get; }

        Dictionary<string , object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

    }
}
