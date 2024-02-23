using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using QLTT.Models;

namespace QLTT.Controllers.Home
{
    public class AccountController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }



        // GET: KhachHang/Create
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "maKH,matKhau,hoTenKH,sdtKH,diaChiKH,emailKH")] KhachHang kh)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(kh.maKH) ||
                    string.IsNullOrWhiteSpace(kh.matKhau) ||
                    string.IsNullOrWhiteSpace(kh.hoTenKH) ||
                    string.IsNullOrWhiteSpace(kh.sdtKH) ||
                    string.IsNullOrWhiteSpace(kh.diaChiKH) ||
                    string.IsNullOrWhiteSpace(kh.emailKH))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                    return View(kh);
                }
                if (db.KhachHangs.Any(k => k.maKH == kh.maKH))
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                    return View(kh);
                }

                if (kh.matKhau.Length < 6)
                {
                    ModelState.AddModelError("matKhau", "Mật khẩu phải chứa ít nhất 6 kí tự.");
                    return View(kh);
                }

                if (!kh.emailKH.EndsWith("@gmail.com"))
                {
                    ModelState.AddModelError("emailKH", "Email phải kết thúc bằng '@gmail.com'.");
                    return View(kh);
                }

                // kiểm tra số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số từ 0 -> 9
                if (!Regex.IsMatch(kh.sdtKH, "^0\\d{8,}$"))
                {
                    ModelState.AddModelError("sdtKH", "Số điện thoại phải bắt đầu bằng số 0 và có ít nhất 9 chữ số.");
                    return View(kh);
                }

                db.KhachHangs.Add(kh);
                db.SaveChanges();
                Session["KH"] = kh;
                return RedirectToAction("Login", "Account");
            }

            return View(kh);

        }




        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "Home");
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
        public ActionResult CaNhan([Bind(Include = "maKH,matKhau,hoTenKH,sdtKH,diaChiKH,emailKH")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = tblKhachHang;
                return RedirectToAction("Index", "Home");
            }
            return View(tblKhachHang);
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
                    return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult Ads()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Login", "Account");
        }
        public ActionResult XoaPhieuDatSan(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PhieuThueSan tblPhieuDatPhong = db.PhieuThueSans.Find(id);
            if (tblPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            if (tblPhieuDatPhong.maKH != kh.maKH)
                return RedirectToAction("Index", "Home");
            return View(tblPhieuDatPhong);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("XoaPhieuDatSan")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmXoaPhieuDatSan(int id)
        {
            PhieuThueSan tblPhieuDatPhong = db.PhieuThueSans.Find(id);
            tblPhieuDatPhong.maTinhTrang = 3;
            db.Entry(tblPhieuDatPhong).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Booksan", "Home");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SuaPhieuDatSan(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThueSan tblPhieuDatPhong = db.PhieuThueSans.Find(id);
            if (tblPhieuDatPhong == null)
            {
                return HttpNotFound();
            }
            if (tblPhieuDatPhong.maKH != kh.maKH)
                return RedirectToAction("Index", "Home");
            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "matKhau", tblPhieuDatPhong.maKH);
            ViewBag.maSan = new SelectList(db.Sans, "maSan", "maSoSan", tblPhieuDatPhong.maSan);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", tblPhieuDatPhong.maTinhTrang);
            return View(tblPhieuDatPhong);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaPhieuDatSan([Bind(Include = "maPT,maKH,NgayDat,NgayThue,soGioThue,maSoSan,maTinhTrang")] PhieuThueSan tblPhieuDatPhong)
        {
            if (ModelState.IsValid)
            {
                tblPhieuDatPhong.maTinhTrang = 1;
                db.Entry(tblPhieuDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BookSan", "Home");
            }
            ViewBag.maKH = new SelectList(db.KhachHangs, "maKH", "matKhau", tblPhieuDatPhong.maKH);
            ViewBag.maSan = new SelectList(db.Sans, "maSan", "maSoSan", tblPhieuDatPhong.maSan);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangPTs, "maTinhTrang", "tinhTrang", tblPhieuDatPhong.maTinhTrang);
            return RedirectToAction("BookSan", "Home");
        }
        public ActionResult HoaDon()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "Home");

            var dsHoaDon = db.HoaDonTS.Where(t => t.PhieuThueSan.maKH == kh.maKH).ToList();

            foreach (var hoaDon in dsHoaDon)
            {
                DateTime ngay_thue = (DateTime)hoaDon.PhieuThueSan.NgayThue;
                int so_gio_thue = 0;
                if (hoaDon.PhieuThueSan.soGioThue.HasValue)
                {
                    so_gio_thue = hoaDon.PhieuThueSan.soGioThue.Value;
                }

                DateTime ngay_bat_dau = ngay_thue;
                DateTime ngay_ket_thuc = ngay_thue.AddHours(so_gio_thue);

                double gia = (double)hoaDon.PhieuThueSan.San.LoaiSan1.giaThue;

                var so_gio = (ngay_ket_thuc - ngay_bat_dau).TotalHours;

                var tien_phong = so_gio * gia;
                hoaDon.tienSan = (int)tien_phong;


                double tong_tien_dich_vu = 0;
                List<double> list_tt = new List<double>();
                foreach (var dichVu in hoaDon.DichVuDaDats)
                {
                    double tien_dich_vu = (double)(dichVu.so_luong * dichVu.DichVu.gia);
                    tong_tien_dich_vu += tien_dich_vu;
                    list_tt.Add(tien_dich_vu);
                }

                hoaDon.tongTien = (int)(tien_phong + tong_tien_dich_vu);

            }

            return View(dsHoaDon);
        }


        public ActionResult PhieuDatPhong()
        {
            AutoHuyPhieuDatSan();
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "Home");

            var dsPDP = db.PhieuThueSans.Where(t => t.maKH == kh.maKH).ToList();
            return View(dsPDP);
        }
        private void AutoHuyPhieuDatSan()
        {
            var datenow = DateTime.Now;
            var tblPhieuDatPhongs = db.PhieuThueSans.Where(u => u.maTinhTrang == 1).Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT).ToList();
            foreach (var item in tblPhieuDatPhongs)
            {
                System.Diagnostics.Debug.WriteLine((item.NgayThue- datenow).Value.Days);
                if ((item.NgayThue - datenow).Value.Days < 0)
                {
                    item.maTinhTrang = 3;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public ActionResult ChiTietHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonT tblHoaDon = db.HoaDonTS.Find(id);
            if (tblHoaDon == null)
            {
                return HttpNotFound();
            }

            DateTime ngay_thue = (DateTime)tblHoaDon.PhieuThueSan.NgayThue;
            int so_gio_thue = tblHoaDon.PhieuThueSan.soGioThue ?? 0;  // giả định soGioThue là int?

            Double gia = (Double)tblHoaDon.PhieuThueSan.San.LoaiSan1.giaThue;

            var tien_phong = so_gio_thue  * gia;  // Chuyển số giờ thuê thành số ngày
            ViewBag.tien_phong = tien_phong;
            ViewBag.so_gio_thue = so_gio_thue;

            NhanVien nv = (NhanVien)Session["NV"];
            if (nv != null)
            {
                ViewBag.ho_ten = nv.hotenNV;
            }

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
            ViewBag.tong_tien = tien_phong + tongtiendv;
            return View(tblHoaDon);
        }

    }
}
