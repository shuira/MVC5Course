using System.Web.Mvc;
using System.Linq;
namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [SharedViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult SuccessRedirect()
         {
             //Response.Write("<script>alert('建立成功!'); location.href='/';</script>");
             //return "<script>alert('建立成功!'); location.href='/';</script>";
             //return Content("<script>alert('建立成功!'); location.href='/';</script>");
             return PartialView("SuccessRedirect", "/");
         }

        [SharedViewBag(MyProperty = "")]
        public ActionResult PartialAbout()
        {
            //ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GetFile()
         {
             return File(Server.MapPath("~/Content/123.jpg"), "image/jpg", "NewName.jpg");
         }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            
                       return Json(db.Product.Take(5),
            JsonRequestBehavior.AllowGet);
        }

}
}