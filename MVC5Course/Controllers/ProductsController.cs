﻿using MVC5Course.Models;
using MVC5Course.Models.ViewModel;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            var data = repo.GetProduct列表頁所有資料(Active, showAll: false);

            return View(data); //等於 ViewData.Model =data;
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {

                repo.Add(product);
                repo.UnitOfWork.Commit();
                
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,FormCollection form)
        {
            var product = repo.Get單筆資料ByProductId(id);
            //範例程式會有問題,把不要欄位拿掉會用預設值更新
            //[Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product
            //if (ModelState.IsValid)
            if(TryUpdateModel<Product>(product))
            {
                //repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Get單筆資料ByProductId(id);
            //強迫關閉驗證
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult ProductsList(ProductVMQ model)
        {
            var data = repo.GetProduct列表頁所有資料(true);
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(model.q))
                {
                    data = data.Where(p => p.ProductName.Contains(model.q));
                }

                data = data.Where(p => p.Stock > model.Stock_S && p.Stock < model.Stock_E);

                ViewData.Model = data
                    .Select(p => new ProductVM()
                    {

                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock
                    });
            }
            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductVM data)
        {
            if (ModelState.IsValid)
            {
                // TODO: 儲存資料進資料庫
                TempData["CreateProduct_Result"] = " 商品新增成功 ";
                
                return RedirectToAction("ListProducts");
            }
            // 驗證失敗，繼續顯示原本的表單
            return View();
        }
    }
}