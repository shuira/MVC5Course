using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HelloController : BaseController
    {
        // GET: Hello
        public ActionResult Index()
        {
            return View();
        }
    }
}