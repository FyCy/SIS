using SIS.HTTP.Common;
using SIS.HTTP.Cookies.Contracts;
using System;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie : IHttpCookie
    {
        private const int HttpCookieDefaulthExpirationDays = 3;

        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key , string value , int expires = HttpCookieDefaulthExpirationDays , string path = HttpCookieDefaultPath) :this(key,value,true,expires,path)
        {
            this.IsNew = IsNew;
        }

        public HttpCookie(string key, string value, bool Isnew,int expires = HttpCookieDefaulthExpirationDays, string path = HttpCookieDefaultPath)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            this.Key = key;
            this.Value = value;
            this.IsNew = true;
            this.Path = path;
            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public string Key { get; }
        public string Value { get; }
        public DateTime Expires { get;  set; }
        public string Path { get; set; }
        public bool IsNew { get;  }
        public bool HttpOnly { get; set; } = true;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{this.Key}={this.Value}; Expires={this.Expires:R}");

            if (this.HttpOnly)
            {
                sb.Append("; HttpOnly");
            }

            sb.Append($"; Path={this.Path}");

            return sb.ToString();
        }

        public void Delete()
        {
            this.Expires = DateTime.UtcNow.AddDays(-1);
        }
    }
}
