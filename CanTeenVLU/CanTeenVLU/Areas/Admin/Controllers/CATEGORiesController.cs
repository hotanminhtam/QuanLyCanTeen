﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CanTeenVLU.Areas.Admin.Middleware;
using CanTeenVLU.Models;
using System.Transactions;
using System.Data.SqlClient;

namespace CanTeenVLU.Areas.Admin.Controllers
{
    [LoginVerification]
    public class CATEGORiesController : Controller
    {
        private QUANLYCANTEENEntities db = new QUANLYCANTEENEntities();

        // GET: Admin/CATEGORies
        public ActionResult Index()
        {
            return View(db.CATEGORies.ToList());
        }

        // GET: Admin/CATEGORies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // GET: Admin/CATEGORies/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "Images");
        }

        // POST: Admin/CATEGORies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATEGORY model, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.CATEGORies.Add(model);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        img.SaveAs(path + model.ID);

                        scope.Complete();
                        return RedirectToAction("Index");

                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
            }

            return View(model);
        }

        private const string PICTURE_PATH = "~/Images";

        // GET: Admin/CATEGORies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // POST: Admin/CATEGORies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CATEGORY_CODE,CATEGORY_NAME,IMAGE_URL,STATUS")] CATEGORY cATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATEGORY);
        }

        // GET: Admin/CATEGORies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // POST: Admin/FOODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            db.CATEGORies.Remove(cATEGORY);
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
