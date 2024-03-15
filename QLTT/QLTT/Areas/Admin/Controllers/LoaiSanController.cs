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
      
        public ActionResult Index()
        {
            return View(db.LoaiSans.ToList());
        }

      
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
        public ActionResult Create()
        {
            return View();
        }    
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

        [HttpPost, ActionName("DeleteService")]
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
