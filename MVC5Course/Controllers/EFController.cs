using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
       private FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();
            
            var data = all
                .Where(p => p.Active == true && p.ProductName.Contains("Black"))
                .OrderByDescending(p => p.ProductId);
            return View(data);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
               {
                db.Product.Add(product);
                db.SaveChanges();
               }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
                        return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {

            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;
                item.Price = product.Price;
                item.Stock = product.Stock;
                item.Active = product.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            


            foreach (var item in product.OrderLine.ToList())
            {
                db.OrderLine.Remove(item);
            }

            //db.OrderLine.RemoveRange(product.OrderLine);

            db.Product.Remove(product);
            db.SaveChanges();
            
           return RedirectToAction("Index");
        }

        
    }
}