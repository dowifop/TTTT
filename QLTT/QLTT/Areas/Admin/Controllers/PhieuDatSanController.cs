using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QLTT.Areas.Admin.Models;
using  QLTT.Models;


namespace QLTT.Areas.Admin.Controllers.Admin
{
    public class PhieuDatSanController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: PhieuDatPhong
        public ActionResult Index()
        {
            AutoHuyPhieuDatSan();
            var PhieuThueSan = db.PhieuThueSans.Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT);
            return View(PhieuThueSan.ToList());
        }

        private void AutoHuyPhieuDatSan()
        {
            var datenow = DateTime.Now;
            var PhieuThueSan = db.PhieuThueSans.Where(u => u.maTinhTrang == 1).Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT).ToList();
            foreach (var item in PhieuThueSan)
            {
                System.Diagnostics.Debug.WriteLine((item.NgayThue - datenow).Value.Days);
                if ((item.NgayThue - datenow).Value.Days < 0)
                {
                    item.maTinhTrang = 3;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }


        public ActionResult List()
        {
            AutoHuyPhieuDatSan();
            var PhieuThueSan = db.PhieuThueSans/*.Where(t => t.maTinhTrang == 1 && t.NgayThue.Value.Month == DateTime.Now.Month && t.NgayThue.Value.Day == DateTime.Now.Day    && t.NgayThue.Value.Year == DateTime.Now.Year).Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT)*/;
            return View(PhieuThueSan.ToList());
        }

        // GET: PhieuDatPhong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            if (phieuThueSan == null)
            {
                return HttpNotFound();
            }
            return View(phieuThueSan);
        }

        // GET: PhieuDatPhong/Create

        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.select_maSan = id;
            }
            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "maKH");
            ViewBag.maSan = new SelectList(db.Sans.Where(u => u.maTinhTrang == 1), "maSan", "maSoSan");
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang");
            return View();
        }


        public ActionResult SelectSan(String dateE)
        {
            if (dateE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "maKH", "maKH");
            DateTime ngay_ra = (DateTime.Parse(dateE)).AddHours(12);
            ViewBag.ngay_ra = ngay_ra;
            var s = db.Sans.Where(t => !(db.PhieuThueSans.Where(m => (m.maTinhTrang == 1 || m.maTinhTrang == 2) && (m.NgayThue > DateTime.Now && m.NgayThue < ngay_ra))).Select(m => m.maSan).ToList().Contains(t.maSan) && t.maTinhTrang == 1);
            ViewBag.maSan = new SelectList(s, "maSan", "maSoSan");
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang");
            return View();
        }


        // POST: PhieuDatPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String radSelect, [Bind(Include = "maPT,maKH,soGioThue,NgayThue,NgayDat,maSan,maTinhTrang")] PhieuThueSan phieuThueSan, [Bind(Include = "hoTenKH,diaChiKH,emailKH,sdtKH")] KhachHangKH kh)
        {
            System.Diagnostics.Debug.WriteLine("SS :" + radSelect);
            if (radSelect.Equals("rad2"))
            {
                phieuThueSan.maKH = null;
                List<KhachHangKH> likh = new List<KhachHangKH>();
                likh.Add(kh);
                String ttkh = JsonConvert.SerializeObject(likh);
                phieuThueSan.thong_tin_khach_hang_thue = ttkh;
            }

            phieuThueSan.maTinhTrang = 1;
            phieuThueSan.NgayThue = DateTime.Now;
            phieuThueSan.NgayDat = DateTime.Now;
            db.PhieuThueSans.Add(phieuThueSan);
            db.SaveChanges();
            int ma = phieuThueSan.maPT;
            return RedirectToAction("Add", "HoaDon", new { id = ma });

            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "maKH", phieuThueSan.maKH);
            ViewBag.maSan = new SelectList(db.Sans, "maSan", "maSoSan", phieuThueSan.maSan);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", phieuThueSan.maTinhTrang);
            return View(phieuThueSan);
        }

        // GET: PhieuDatPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            if (phieuThueSan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "matKhau", phieuThueSan.maKH);
            ViewBag.maSan = new SelectList(db.Sans, "maSan", "maSoSan", phieuThueSan.maSan);
            ViewBag.NgayThue = new SelectList(db.PhieuThueSans, "maPT", "NgayThue", phieuThueSan.maPT);
            ViewBag.soGioThue = new SelectList(db.PhieuThueSans, "maPT", "soGioThue", phieuThueSan.maPT);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", phieuThueSan.maTinhTrang);
            return View(phieuThueSan);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maPT,maKH,soGioThue,NgayThue,NgayDat,maSan,maTinhTrang")] PhieuThueSan phieuThueSan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuThueSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "matKhau", phieuThueSan.maKH);
            ViewBag.maSan = new SelectList(db.Sans, "maSan", "maSoSan", phieuThueSan.maSan);
            ViewBag.NgayThue = new SelectList(db.PhieuThueSans, "maPT", "NgayThue", phieuThueSan.maPT);
            ViewBag.soGioThue = new SelectList(db.PhieuThueSans, "maPT", "soGioThue", phieuThueSan.maPT);
            ViewBag.maTinhTran = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", phieuThueSan.maTinhTrang);
            return View(phieuThueSan);
        }

        // GET: PhieuDatPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            if (phieuThueSan == null)
            {
                return HttpNotFound();
            }
            return View(phieuThueSan);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
                db.PhieuThueSans.Remove(phieuThueSan);
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
