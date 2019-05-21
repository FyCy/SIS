using System;

namespace SIS.HTTP.Cookies.Contracts
{
    public interface IHttpCookie
    {
        string Key  { get;  }

        string Value { get; }

        DateTime Expires { get; set; }

        string Path { get; set; }

        bool IsNew { get; }

        bool HttpOnly { get; set; }

        void Delete();

    }
}
