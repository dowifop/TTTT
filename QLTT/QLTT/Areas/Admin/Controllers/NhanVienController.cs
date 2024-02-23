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
    public class NhanVienController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            var tblNhanViens = db.NhanViens.Include(t => t.ChucVu);
            return View(tblNhanViens.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien tblNhanVien = db.NhanViens.Find(id);
            if (tblNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(tblNhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            ViewBag.maCV = new SelectList(db.ChucVus, "maCV", "tenCV");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maNV,hoTenNV,emailNV,sdtNV,diaChiNV,dobNV,taiKhoan,matKhau,maPH,maCV,maSK")] NhanVien tblNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(tblNhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maCV = new SelectList(db.ChucVus, "maCV", "tenCV", tblNhanVien.maCV);
            return View(tblNhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien tblNhanVien = db.NhanViens.Find(id);
            if (tblNhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.maCV= new SelectList(db.ChucVus, "maCV", "tenCV", tblNhanVien.maCV);
            // Truy vấn danh sách các mục từ bảng PhongBan và truyền vào ViewBag.maPH
            ViewBag.maPH = new SelectList(db.PhongBans, "maPH", "tenPH", tblNhanVien.maPH);


            return View(tblNhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maNV,hoTenNV,emailNV,sdtNV,diaChiNV,dobNV,taiKhoan,matKhau,maPH,maCV,maSK")] NhanVien tblNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maCV = new SelectList(db.ChucVus, "maCV", "tenCV", tblNhanVien.maCV);
            // Truy vấn danh sách các mục từ bảng PhongBan và truyền vào ViewBag.maPH
            ViewBag.maPH = new SelectList(db.PhongBans, "maPH", "tenPH", tblNhanVien.maPH);


            return View(tblNhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien tblNhanVien = db.NhanViens.Find(id);
            if (tblNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(tblNhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                NhanVien tblNhanVien = db.NhanViens.Find(id);
                db.NhanViens.Remove(tblNhanVien);
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
