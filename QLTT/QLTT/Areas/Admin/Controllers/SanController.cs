using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTT.Models;

namespace QLTT.Areas.Admin.Controllers.Admin
{
    public class SanController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: Phong
        public ActionResult Index()
        {
            var Sans = db.Sans.Where(t => t.maTinhTrang < 5).Include(t => t.LoaiSan1).Include(t => t.TinhTrangSan);
            return View(Sans.ToList());
        }

        // GET: Phong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San san = db.Sans.Find(id);
            if (san == null)
            {
                return HttpNotFound();
            }
            return View(san);
        }

        // GET: Phong/Create
        public ActionResult Create()
        {
            ViewBag.loai_san = new SelectList(db.Sans, "LoaiSan", "moTa");
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota");
            return View();
        }

        // POST: Phong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSan,maSoSan,LoaiSan,maTinhTrang")] San san)
        {
            if (ModelState.IsValid)
            {
                db.Sans.Add(san);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.loai_san = new SelectList(db.LoaiSans, "LoaiSan", "moTa", san.LoaiSan1);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", san.maTinhTrang);
            return View(san);
        }

        // GET: Phong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San tblPhong = db.Sans.Find(id);
            if (tblPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.loai_San = new SelectList(db.LoaiSans, "LoaiSan", "moTa", tblPhong.LoaiSan1);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", tblPhong.maTinhTrang);
            return View(tblPhong);
        }

        // POST: Phong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSan,maSoSan,LoaiSan,maTinhTrang")] San tblPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.loai_san = new SelectList(db.LoaiSans, "LoaiSan", "moTa", tblPhong.LoaiSan1);
            ViewBag.ma_tinh_trang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", tblPhong.maTinhTrang);
            return View(tblPhong);
        }

        // GET: Phong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San san = db.Sans.Find(id);
            if (san == null)
            {
                return HttpNotFound();
            }
            return View(san);
        }

        // POST: Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                San san = db.Sans.Find(id);
                san.maTinhTrang = 5;
                db.Entry(san).State = EntityState.Modified;
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
