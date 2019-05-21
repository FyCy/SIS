using SIS.HTTP.Common;
using SIS.HTTP.Cookies.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, IHttpCookie> httpCookies;
        public HttpCookieCollection()
        {
            this.httpCookies = new Dictionary<string, IHttpCookie>();
        }

        public void AddCookie(IHttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));

            this.httpCookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.httpCookies.ContainsKey(key);
        }


        public IHttpCookie GetCookies(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return this.httpCookies[key];
        }

        public IEnumerator<IHttpCookie> GetEnumerator()
        {
            return this.httpCookies.Values.GetEnumerator();
        }

        public bool HasCookies()
        {
            return this.httpCookies.Count != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var cookie in this.httpCookies.Values)
            {
                sb.Append($"Set-Cookie: {cookie}").Append(GlobalConstants.HttpNewLine);
            }
            return sb.ToString();

        }
    }
}
