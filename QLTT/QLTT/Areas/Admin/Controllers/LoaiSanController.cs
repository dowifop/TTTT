using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace QLTT.Areas.Admin.Controllers
{
    public class LoaiSanController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: LoaiPhong
        public ActionResult Index()
        {
            return View(db.LoaiSans.ToList());
        }

        // GET: LoaiPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSan tblLoaiPhong = db.LoaiSans.Find(id);
            if (tblLoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiPhong);
        }

        // GET: LoaiPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "moTa,giaThue,anh")] LoaiSan loaiSan)
        {
            if (ModelState.IsValid)
            {
                if (loaiSan.anh == null)
                    loaiSan.anh = "[\"/Content/Home/hình/defaultSan.png\"]";
                db.LoaiSans.Add(loaiSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSan);
        }

        // GET: LoaiPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSan tblLoaiPhong = db.LoaiSans.Find(id);
            if (tblLoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(tblLoaiPhong);
        }

        // POST: LoaiPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Loai_San,mota,giaThue,anh")] LoaiSan loaiSan)
        {
            if (ModelState.IsValid)
            {
                if (loaiSan.anh == null)
                    loaiSan.anh = "[\"/Content/Home/hình/defaultSan.png\"]";
                db.Entry(loaiSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSan);
        }

        // GET: LoaiPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSan loaiSan = db.LoaiSans.Find(id);
            if (loaiSan == null)
            {
                return HttpNotFound();
            }
            return View(loaiSan);
        }

        // POST: LoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiSan loaiSan = db.LoaiSans.Find(id);
                db.LoaiSans.Remove(loaiSan);
                db.SaveChanges();
            }
            catch
            {

            }
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
