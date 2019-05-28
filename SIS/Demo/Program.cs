using SIS.MvcFramework;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var mvcApplication = new StarUp();
            WebHost.Start(mvcApplication);
        }
    }
}
