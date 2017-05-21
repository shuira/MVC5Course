﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;

namespace MVC5Course.Controllers
{
    public class Clinets1Controller : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Clinets1
        //public ActionResult Index()
        //{
        //    var ratings = (from p in db.Client
        //                    select p.CreditRating)
        //                    .Distinct()
        //                   .OrderBy(p => p).ToList();
            
        //    ViewBag.CreditRatingFilter = new SelectList(ratings);
            
        //    var lastNames = (from p in db.Client
        //                   select p.LastName)
        //                   .Distinct()
        //                   .OrderBy(p => p).ToList();
            
        //    ViewBag.LastNameFilter = new SelectList(lastNames);
        //    var client = db.Client.Include(c => c.Occupation);
        //    return View(client.Take(10));
        //}
        
        public ActionResult Index(int CreditRatingFilter = -1,string LastNameFilter = "", int pageNo = 1)
        {
            var ratings = (from p in db.Client
                           where p.CreditRating < 10
                           select p.CreditRating)
                           .Distinct()
                           .OrderBy(p => p).ToList();

            ViewBag.CreditRatingFilter = new SelectList(ratings);

            var lastNames = (from p in db.Client
                             select p.LastName)
                           .Distinct()
                           .OrderBy(p => p).ToList();

            ViewBag.LastNameFilter = new SelectList(lastNames);

            var client = db.Client.AsQueryable();

            if (CreditRatingFilter >= 0)
            {
                client = client.Where(p => p.CreditRating == CreditRatingFilter);
            }
            if (!String.IsNullOrEmpty(LastNameFilter))
            {
                client = client.Where(p => p.LastName == LastNameFilter);
            }

            ViewData.Model = client
                  .OrderByDescending(p => p.ClientId)
                  .ToPagedList(pageNo, 10);

            return View();
        }

       
        // GET: Clinets1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clinets1/Create
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clinets1/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Client.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clinets1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.CreditRating = new SelectList(items);

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clinets1/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clinets1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clinets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Client.Find(id);
            db.Client.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
