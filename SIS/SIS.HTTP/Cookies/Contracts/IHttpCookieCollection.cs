using System.Collections.Generic;

namespace SIS.HTTP.Cookies.Contracts
{
    public interface IHttpCookieCollection : IEnumerable<IHttpCookie>
    {
        void AddCookie(IHttpCookie cookie);

        bool ContainsCookie(string key);

        IHttpCookie GetCookies(string key);

        bool HasCookies();
    }
}
