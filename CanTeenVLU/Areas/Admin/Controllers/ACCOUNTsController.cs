using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CanTeenVLU.Areas.Admin.Middleware;
using CanTeenVLU.Models;

namespace CanTeenVLU.Areas.Admin.Controllers
{
    [LoginVerification]
    public class ACCOUNTsController : Controller
    {
        private QUANLYCANTEENEntities db = new QUANLYCANTEENEntities();

        // GET: Admin/ACCOUNTs
        public ActionResult Index()
        {
            return View(db.ACCOUNTs.ToList());
        }

        // GET: Admin/ACCOUNTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ACCOUNTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EMAIL,PASSWORD,FULL_NAME,STATUS,ROLE")] ACCOUNT aCCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.ACCOUNTs.Add(aCCOUNT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // POST: Admin/ACCOUNTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EMAIL,PASSWORD,FULL_NAME,STATUS,ROLE")] ACCOUNT aCCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCOUNT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCCOUNT);
        }

        // GET: Admin/ACCOUNTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // POST: Admin/ACCOUNTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACCOUNT aCCOUNT = db.ACCOUNTs.Find(id);
            db.ACCOUNTs.Remove(aCCOUNT);
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
