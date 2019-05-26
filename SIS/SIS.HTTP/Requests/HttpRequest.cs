using SIS.HTTP.Common;
using SIS.HTTP.Contracts;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Exeptions;
using SIS.HTTP.Headers;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SIS.HTTP.Requests

{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(
            requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }
        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpCookieCollection Cookies { get; }
        public IHttpSession Session { get; set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3 ||
                requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment
                )
            {
                return false;
            }
            return true;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

            return true;
        }

        private bool HasQueryString()
        {

            return this.Url.Split('?').Length > 1;
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }
        private void ParseRequestMethod(string[] requestLineParams)
        {
            HttpRequestMethod method;
            bool parseResult = HttpRequestMethod.TryParse(requestLineParams[0], true, out method);
            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLineParams[0]));
            }
            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLineParams)
        {
            this.Url = requestLineParams[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            plainHeaders.Select(plainHeader => plainHeader.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries))
            .ToList().ForEach(headerKeyValuePair => this.Headers.AddHeader
            (new HttpHeader(headerKeyValuePair[0], headerKeyValuePair[1])));
        }

        private void ParseRequestQueryParameters()
        {

            if (this.HasQueryString())
            {
                if (!this.Url.Contains('?'))
                {
                    return;
                }
                string queryString = this.Url.Split('?', '#')[1];
                if (string.IsNullOrWhiteSpace(queryString))
                {
                    return;
                }
                string[] queryParams = queryString.Split('&');
                //.ForEach(parsedQueryParameterKeyValuePair =>
                //this.QueryData.Add(parsedQueryParameterKeyValuePair[0], parsedQueryParameterKeyValuePair[1]));

                if (!IsValidRequestQueryString(queryString, queryParams))
                {
                    throw new BadRequestException();
                }

               queryParams.Select(plainQueryParameter => plainQueryParameter.Split('='))
              .ToList()
              .ForEach(parsedQueryParameterKeyValuePair =>
              this.QueryData.Add(parsedQueryParameterKeyValuePair[0], parsedQueryParameterKeyValuePair[1]));
              ////  this.Url.Split('?', '#')[1]
              ////.Split('&')
              //.Select(plainQueryParameter => plainQueryParameter.Split('='))
              //.ToList()
              //.ForEach(parsedQueryParameterKeyValuePair =>
              //this.QueryData.Add(parsedQueryParameterKeyValuePair[0], parsedQueryParameterKeyValuePair[1]));
            }
        }

        private void ParseRequestFormDataParamaters(string requestBody)
        {
            //TODO Parse Multiple Parameters 
            if (!string.IsNullOrEmpty(requestBody))
            {
                requestBody
                .Split('&')
               .Select(plainQueryParameter => plainQueryParameter.Split('='))
               .ToList()
               .ForEach(parsedQueryParameterKeyValuePair =>
               this.FormData.Add(parsedQueryParameterKeyValuePair[0], parsedQueryParameterKeyValuePair[1]));
            }
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParamaters(requestBody);
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader(HttpHeader.Cookie))
            {
                string value = this.Headers.GetHeader(HttpHeader.Cookie).Value;

                string[] unparsedCookie = value.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in unparsedCookie)
                {
                    string[] cookieKVP = cookie.Split(new[] { '=' }, 2);

                    HttpCookie httpCookie = new HttpCookie(cookieKVP[0], cookieKVP[1], false);

                    this.Cookies.AddCookie(httpCookie);
                }

            }
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestString = requestString.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLineParams = splitRequestString[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLineParams);
            this.ParseRequestUrl(requestLineParams);
            this.ParseRequestPath();
            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestString).ToArray());
            this.ParseCookies();
            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);


        }
    }
}
