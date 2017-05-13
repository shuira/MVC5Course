using MVC5Course.Models;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        

        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();

            var data = all
                .Where(p => p.Is刪除 != true && p.Active == true && p.ProductName.Contains("Black"))
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

            //foreach (var item in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}

            //db.OrderLine.RemoveRange(product.OrderLine);
            try
            {
                product.Is刪除 = true;
            }
            catch(DbEntityValidationException ex)
            {
                throw ex;
            }
            

            //db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0",id).FirstOrDefault ();
            return View(data);
        }
    }
}