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
using System.Transactions;

namespace CanTeenVLU.Areas.Admin.Controllers
{
    [LoginVerification]
    public class FOODsController : Controller
    {
        private QUANLYCANTEENEntities db = new QUANLYCANTEENEntities();

        // GET: Admin/FOODs
        public ActionResult Index()
        {
            var fOODs = db.FOODs.Include(f => f.CATEGORY);
            return View(fOODs.ToList());
        }

        // GET: Admin/FOODs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            return View(fOOD);
        }

        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }

        // GET: Admin/FOODs/Create
        public ActionResult Create()
        {
            //ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE");
            return View();
        }

        // POST: Admin/FOODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FOOD model, HttpPostedFileBase img)
        {
            ValidateProduct(model);
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.FOODs.Add(model);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        img.SaveAs(path + model.ID);

                        scope.Complete();
                    return RedirectToAction("Index");

                    }
                }
                else ModelState.AddModelError("", "Hình ảnh không được tìm thấy");
            }

            //ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE", model.CATEGORY_ID);
            return View(model);
        }

        private const string PICTURE_PATH = "~/Images/";
        private void ValidateProduct(FOOD product)
        {
            if (product.PRICE < 0)
            {
                ModelState.AddModelError("Giá", "Giá của sản phẩm phải lớn hơn 0");
            }
        }

        // GET: Admin/FOODs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE", fOOD.CATEGORY_ID);
            return View(fOOD);
        }

        // POST: Admin/FOODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FOOD_CODE,FOOD_NAME,CATEGORY_ID,DESCRIPTION,PRICE,IMAGE_URL,STATUS")] FOOD fOOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fOOD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE", fOOD.CATEGORY_ID);
            return View(fOOD);
        }

        // GET: Admin/FOODs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            return View(fOOD);
        }

        // POST: Admin/FOODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FOOD fOOD = db.FOODs.Find(id);
            db.FOODs.Remove(fOOD);
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
