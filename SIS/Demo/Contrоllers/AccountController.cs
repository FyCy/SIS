using Demo.Models;
using Demo.Services;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Linq;

namespace Demo.Contrоllers
{
    public class AccountController : BaseController
    {
        private HashService hashService;
        public AccountController()
        {
            this.hashService = new HashService();
        }
        public IHttpResponse Register (IHttpRequest request)
        {
            return this.View("Register");
        }
        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }
        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var hashedPassword = this.hashService.Hash(password);

            var user =  this.Db.Users.FirstOrDefault(userFromDb => userFromDb.Username == userName 
                && userFromDb.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Ivalid username or password!");
            }

            var response = new RedirectResult("/");
            var cookie = this.UserCookieService.GetUserCookie(user.Username);
            response.Cookies.AddCookie(new HttpCookie(".auth-WoW" , cookie , 7));

            return response;
        }
        public  IHttpResponse DoRegister (IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();
            var confrimPassword = request.FormData["confirmPassword"].ToString();

            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 4)
            {
                return this.BadRequestError("Please provide valid username with length 4 or more!");
            }
            if (this.Db.Users.Any(x=>x.Username == userName))
            {
                return this.BadRequestError("User with the same username already exist!");
            }
            if (password.Length < 4)
            {
                return this.BadRequestError("Password must be 4 or more characters!");
            }
            if (password != confrimPassword)
            {
                return this.BadRequestError("Passwords must match!");
            }
            var hashedPassword = this.hashService.Hash(password);

            var user = new User
            {
                Name = userName,
                Username = userName,
                Password = hashedPassword
            };

            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e )
            {

                return ServerError(e.Message);
            }         
            return new HtmlResult("Register" , HttpResponseStatusCode.Ok);
        }

        public IHttpResponse Logout (IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-WoW"))
            {
                return new RedirectResult("/");
            }
            var cookie = request.Cookies.GetCookies(".auth-WoW");

            cookie.Delete();
            var response = new RedirectResult("/");
            response.Cookies.AddCookie(cookie);
            return response;
        }
    }
}
