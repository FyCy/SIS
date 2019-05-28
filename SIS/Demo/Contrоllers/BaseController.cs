using Demo.Data;
using SIS.MvcFramework;

namespace Demo.Contrоllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new DemoDbContex();
          
        }
        protected DemoDbContex Db { get; }

       
    }
}
