﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        // GET: Form
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = db.Product.Find(id);
            return View();
        }
        [HttpPost]
        //上動作'Edit' 的目前要求，在下列動作方法之間模稜兩可: (因為少[HttpPost])
        //Model Binding 優先權最高 > Model
        //延遲驗證模式
        public ActionResult Edit(int id,FormCollection form)
        {
            var product = db.Product.Find(id);
            if (TryUpdateModel(product, includeProperties:new string[]{ "ProductName"}))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}