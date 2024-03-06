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
    public class KhachHangController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHang/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "maKH,matKhau,hoTenKH,diaChiKH,sdtKH,emailKH")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                Session["KH"] = khachHang;
                return RedirectToAction("BookRoom", "TrangChu");
            }

            return View(khachHang);
        }

        public ActionResult Add()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "maKH,matKhau,hoTenKH,diaChiKH,sdtKH,emailKH")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHangs.Find(tblKhachHang.maKH) == null)
                {
                    db.KhachHangs.Add(tblKhachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "KhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

            return View(tblKhachHang);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang tblKhachHang = db.KhachHangs.Find(id);
            if (tblKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tblKhachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maKH,matKhau,hoTenKH,diaChiKH,sdtKH,emailKH")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblKhachHang);
        }


        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                kh = (KhachHang)Session["KH"];
            }
            return View(kh);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "maKH,matKhau,hoTenKH,diaChiKH,sdtKH,emailKH")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = tblKhachHang;
                return RedirectToAction("Index", "TrangChu");
            }
            return View(tblKhachHang);
        }

        // GET: KhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang tblKhachHang = db.KhachHangs.Find(id);
            if (tblKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tblKhachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                KhachHang tblKhachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(tblKhachHang);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.KhachHangs.Where(a => a.maKH.Equals(objUser.maKH) && a.matKhau.Equals(objUser.matKhau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["KH"] = obj;
                    return RedirectToAction("BookRoom", "TrangChu");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh != null)
                return RedirectToAction("BookRoom", "TrangChu");
            return View();
        }


        public ActionResult SuaPhieuThueSan(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            if (phieuThueSan == null)
            {
                return HttpNotFound();
            }
            if (phieuThueSan.maKH != kh.maKH)
                return RedirectToAction("Index", "Home");
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "maKH", "matKhau", phieuThueSan.maKH);
            ViewBag.ma_San = new SelectList(db.Sans, "maSan", "maSoSan", phieuThueSan.maSan);
            ViewBag.ma_tinh_trang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", phieuThueSan.maTinhTrang);
            return View(phieuThueSan);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaPhieuThueSan([Bind(Include = "maPT,maKH,NgayThue,soGioThue,maSan,maTinhTrang")] PhieuThueSan phieuThueSan)
        {
            if (ModelState.IsValid)
            {
                phieuThueSan.maTinhTrang = 1;
                db.Entry(phieuThueSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BookRoom", "TrangChu");
            }
            ViewBag.ma_kh = new SelectList(db.KhachHangs, "maKH", "matKhau", phieuThueSan.maKH);
            ViewBag.ma_San = new SelectList(db.Sans, "maSan", "soSan", phieuThueSan.maSan);
            ViewBag.ma_tinh_trang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", phieuThueSan.maTinhTrang);
            return RedirectToAction("BookRoom", "TrangChu");
        }

        // GET: PhieuDatPhong/Delete/5
        public ActionResult XoaPhieuThueSan(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            if (phieuThueSan == null)
            {
                return HttpNotFound();
            }
            if (phieuThueSan.maKH != kh.maKH)
                return RedirectToAction("Index", "TrangChu");
            return View(phieuThueSan);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("XoaPhieuThueSan")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmXoaPhieuThueSan(int id)
        {
            PhieuThueSan phieuThueSan = db.PhieuThueSans.Find(id);
            phieuThueSan.maTinhTrang = 3;
            db.Entry(phieuThueSan).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BookRoom", "TrangChu");
        }

        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Login", "KhachHang");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult HoaDon()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "TrangChu");

            var dsHoaDon = db.HoaDonTS.Where(t => t.PhieuThueSan.maKH == kh.maKH).ToList();
            return View(dsHoaDon);
        }
        public ActionResult ChiTietHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonT hoaDon = db.HoaDonTS.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            var tien_san = (hoaDon.PhieuThueSan.soGioThue * hoaDon.PhieuThueSan.San.LoaiSan1.giaThue);
            ViewBag.tien_san = tien_san;

            ViewBag.time_now = DateTime.Now.ToString();

            List<DichVuDaDat> dsdv = db.DichVuDaDats.Where(u => u.maHDTS == id).ToList();
            ViewBag.list_dv = dsdv;
            double tongtiendv = 0;
            List<double> tt = new List<double>();
            foreach (var item in dsdv)
            {
                double t = (double)(item.so_luong * item.DichVu.gia);
                tongtiendv += t;
                tt.Add(t);
            }
            ViewBag.list_tt = tt;
            ViewBag.tien_dich_vu = tongtiendv;
            ViewBag.tong_tien = tien_san + tongtiendv;
            return View(hoaDon);
        }
    }
}