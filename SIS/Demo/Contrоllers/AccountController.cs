using Demo.Models;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Routing;
using SIS.MvcFramework.Services;
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

        [HttpGet("/register")]
        public IHttpResponse Register ()
        {
            return this.View("Register");
        }

        [HttpGet("/login")]
        public IHttpResponse Login()
        {
            return this.View("Login");
        }


        [HttpPostAttribute("/login")]
        public IHttpResponse DoLogin()
        {
            var userName = this.Request.FormData["username"].ToString().Trim();
            var password = this.Request.FormData["password"].ToString();
            var hashedPassword = this.hashService.Hash(password);

            var user =  this.Db.Users.FirstOrDefault(userFromDb => userFromDb.Username == userName 
                && userFromDb.Password == hashedPassword);

            if (user == null)
            {
                return BadRequestError("Ivalid username or password!");
            }

            var response = this.RedirectTo("/hello");
            var cookie = this.UserCookieService.GetUserCookie(user.Username);
            response.Cookies.AddCookie(new HttpCookie(".auth-WoW" , cookie , 7));

            return response;
        }

        [HttpPostAttribute("/register")]
        public  IHttpResponse DoRegister ()
        {
            var userName =this.Request.FormData["username"].ToString();
            var password = this.Request.FormData["password"].ToString();
            var confrimPassword = this.Request.FormData["confirmPassword"].ToString();

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
            PrepareHtmlResult("Register" , HttpResponseStatusCode.Ok);
            return Response;
        }

        [HttpGet("/logout")]
        public IHttpResponse Logout ()
        {
            if (!this.Request.Cookies.ContainsCookie(".auth-WoW"))
            {
                return this.RedirectTo("/");
            }
            var cookie = this.Request.Cookies.GetCookies(".auth-WoW");

            cookie.Delete();
            this.Response.Cookies.AddCookie(cookie);
            return this.RedirectTo("/");
        }
    }
}
