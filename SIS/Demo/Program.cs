using Demo.Contrоllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            //GET
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest
                             => new HomeController().Home(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/register", httpRequest
                             => new AccountController().Register(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", httpRequest
                             => new AccountController().Login(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/hello", httpRequest
                             => new HomeController().GetUsername(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout", httpRequest
                             => new AccountController().Logout(httpRequest));
            //POST
            serverRoutingTable.Add(HttpRequestMethod.Post, "/register", httpRequest
                           => new AccountController().DoRegister(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/login", httpRequest
                           => new AccountController().DoLogin(httpRequest));


            Server server = new Server(2356, serverRoutingTable);

            server.Run();

        }
    }
}
