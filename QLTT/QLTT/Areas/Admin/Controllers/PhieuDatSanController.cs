using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Composite;
using Newtonsoft.Json;
using QLTT.Areas.Admin.Models;
using QLTT.Models;


namespace QLTT.Areas.Admin.Controllers.Admin
{
    public class PhieuDatSanController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

       
        public ActionResult Index()
        {
            AutoCancelVote();
            var PhieuThueSan = db.PhieuThueSans.Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT);
            return View(PhieuThueSan.ToList());
        }

        private void AutoCancelVote()
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
            AutoCancelVote();
            var PhieuThueSan = db.PhieuThueSans/*.Where(t => t.maTinhTrang == 1 && t.NgayThue.Value.Month == DateTime.Now.Month && t.NgayThue.Value.Day == DateTime.Now.Day    && t.NgayThue.Value.Year == DateTime.Now.Year).Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT)*/;
            return View(PhieuThueSan.ToList());
        }

      
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
            ///return View(phieuThueSan);
            ///

            var bookingComposite = new BookingInfoComposite { maPT = phieuThueSan.maPT };

            // Thêm thông tin khách hàng
            bookingComposite.Add(new CustomerComponent(phieuThueSan.KhachHang));

            // Thêm thông tin sân
            bookingComposite.Add(new FieldComponent(phieuThueSan.San));

            // Bạn có thể thêm các component khác như dịch vụ, nhân viên...

            return View(bookingComposite);
        }

    



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


        public ActionResult SelectInfrastructure(String dateE)
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
            return RedirectToAction("Add", "Bill", new { id = ma });        
        }

       
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
