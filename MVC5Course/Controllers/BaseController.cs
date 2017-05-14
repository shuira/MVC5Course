using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    /// <summary>
    /// 抽出共用BaseController , 宣告成abstract不能被執行
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        //找不到網址導頁功能

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
        //}

        [LocalOnly]
        public ActionResult Debug()
        {
            return Content("Hello");
        }
    }
}