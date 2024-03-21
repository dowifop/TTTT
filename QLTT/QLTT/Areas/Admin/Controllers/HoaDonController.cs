using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QLTT.Areas.Admin.Models;
using QLTT.Models;
using Test;

namespace QLTT.Areas.Admin.Controllers.Admin
{
    public class HoaDonController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: Bill
        public ActionResult Index()
        {
            var HoaDonTS = db.HoaDonTS.Where(t => t.maTinhTrang == 2).Include(t => t.NhanVien).Include(t => t.PhieuThueSan)
                .Include(t => t.tinhTrangHD);
            double tong = HoaDonTS.Sum(item => item.tongTien) ?? 0;
            ViewBag.tongTien = String.Format("{0:0,0.00}", tong);
            return View(HoaDonTS.ToList());
        }

        [HttpPost]
        public ActionResult Index(DateTime? beginDate, DateTime? endDate)
        {
            IQueryable<HoaDonT> query = db.HoaDonTS.Where(t => t.maTinhTrang == 2);

            if (beginDate.HasValue)
            {
                query = query.Where(t => t.ngayThueSan >= beginDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.ngayThueSan <= endDate);
            }

            var dshd = query.Include(t => t.NhanVien).Include(t => t.PhieuThueSan)
                .Include(t => t.tinhTrangHD).ToList();

            int tong = dshd.Sum(item => item.tongTien) ?? 0;
            ViewBag.tongTien = tong.ToString("C");
            return View(dshd);
        }
        // GET: Bill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonT hoaDonT = db.HoaDonTS.Find(id);
            if (hoaDonT == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonT);
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            ViewBag.maNV = new SelectList(db.NhanViens, "maNV", "hotenNV");
            ViewBag.maPT = new SelectList(db.PhieuThueSans, "maPT", "maKH");
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangHDs, "maTinhTrang", "mo_ta");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maHD,maPT,ngayThueSan,maTinhTrang")] HoaDonT hoaDonT)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonTS.Add(hoaDonT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maNV = new SelectList(db.NhanViens, "maNV", "hotenNV", hoaDonT.maNV);
            ViewBag.maPT = new SelectList(db.PhieuThueSans, "maPT", "maKH", hoaDonT.maPT);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangHDs, "maTinhTrang", "mo_ta", hoaDonT.maTinhTrang);
            return View(hoaDonT);
        }
        public ActionResult Add(int? id)
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
        // GET: Bill/EditService/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonT hoaDonT = db.HoaDonTS.Find(id);
            if (hoaDonT == null)
            {
                return HttpNotFound();
            }
            ViewBag.maNV = new SelectList(db.NhanViens, "maNV", "hotenNV", hoaDonT.maNV);
            ViewBag.maPT = new SelectList(db.PhieuThueSans, "maPT", "maKH", hoaDonT.maPT);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangHDs, "maTinhTrang", "mo_ta", hoaDonT.maTinhTrang);
            return View(hoaDonT);
        }

        // POST: Bill/EditService/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maHD,maNV,maPT,ngayThueSan,maTinhTrang,tienSan,tiendichvu,tongTien")] HoaDonT hoaDonT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maNV = new SelectList(db.NhanViens, "maNV", "hotenNV", hoaDonT.maNV);
            ViewBag.maPT = new SelectList(db.PhieuThueSans, "maPT", "maKH", hoaDonT.maPT);
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangHDs, "maTinhTrang", "mo_ta", hoaDonT.maTinhTrang);
            return View(hoaDonT);
        }

        // GET: Bill/DeleteService/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonT hoaDonT = db.HoaDonTS.Find(id);
            if (hoaDonT == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonT);
        }

        // POST: Bill/DeleteService/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonT hoaDonT = db.HoaDonTS.Find(id);
            db.HoaDonTS.Remove(hoaDonT);
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
        public ActionResult Result(String maPT, String hotenKH1, String hotenKH2, String hotenKH3, String hotenKH4, String EmailKH1, String EmailKH2, String EmailKH3, String EmailKH4)
        {
            if (maPT == null)
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {
                PhieuThueSan pt = db.PhieuThueSans.Find(Int32.Parse(maPT));             
                db.Entry(pt).State = EntityState.Modified;
                db.SaveChanges();

                HoaDonT hd = new HoaDonT();
                hd.maPT = Int32.Parse(maPT);
                hd.maTinhTrang = 1;
                try
                {
                    db.HoaDonTS.Add(hd);
                    PhieuThueSan tgd = db.PhieuThueSans.Find(Int32.Parse(maPT));
                    if (tgd == null)
                    {
                        return HttpNotFound();
                    }
                    San s = db.Sans.Find(tgd.maSan);
                    if (s == null)
                    {
                        return HttpNotFound();
                    }
                    tgd.maTinhTrang = 2;
                    db.Entry(tgd).State = EntityState.Modified;
                    s.maTinhTrang = 2;
                    db.Entry(s).State = EntityState.Modified;
                    ViewBag.soGioThue = tgd.soGioThue;
                    db.SaveChanges();
                    ViewBag.Result = "success";
                }
                catch
                {
                    ViewBag.Result = "error";
                }
            }
            return View();
        }
        public ActionResult Pay(int? id)
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
            DateTime ngayThue = (DateTime)tblHoaDon.PhieuThueSan.NgayThue;
            int soGioThue = tblHoaDon.PhieuThueSan.soGioThue ?? 0;
            DateTime dateS = new DateTime(ngayThue.Year, ngayThue.Month, ngayThue.Day, 12, 0, 0);
            int gia = (int)tblHoaDon.PhieuThueSan.San.LoaiSan1.giaThue;

            // Tạo component cơ bản cho tiền sân
            ITinhTien tienSan = new TienSan(soGioThue, gia);

            // Áp dụng VAT cho tiền sân
            tienSan = new VATDecorator(tienSan);

            // Lấy và hiển thị tiền sân sau VAT
            var tienSanSauVAT = tienSan.TinhTien();
            ViewBag.tienSan = tienSanSauVAT;
            ViewBag.soGioThue = soGioThue;

            NhanVien nv = (NhanVien)Session["NV"];
            if (nv != null)
            {
                ViewBag.hotenNV = nv.hotenNV;
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
            // Tạo component cơ bản cho tiền dịch vụ
            ITinhTien tienDichVu = new TienDichVu(tongtiendv);

            ViewBag.list_tt = tt;
            ViewBag.tiendichvu = tongtiendv;
            double tongTien = tienSanSauVAT + tienDichVu.TinhTien();
            ViewBag.tongTien = tongTien;
            return View(tblHoaDon);
        }
        public ActionResult CallService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiDichVuModel mod = new GoiDichVuModel();
            mod.dsDichVu = db.DichVus.Where(t => t.ton_kho > 0).ToList();
            mod.dsDvDaDat = db.DichVuDaDats.Where(u => u.maHDTS == id).ToList();
            ViewBag.maHDTS = id;
            ViewBag.maSoSan = db.HoaDonTS.Find(id).PhieuThueSan.San.maSoSan;
            return View(mod);
        }
        public ActionResult ConfirmService(String maHDTS, String ma_dv, String so_luong)
        {
            if (maHDTS == null || ma_dv == null || so_luong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int mahd = Int32.Parse(maHDTS);
            int madv = Int32.Parse(ma_dv);
            int sol = Int32.Parse(so_luong);
            var ds = db.DichVuDaDats.Where(t => t.maHDTS == mahd).ToList();

            try
            {
                bool check = false;
                foreach (var item in ds)
                {
                    if (item.ma_dv == madv)
                    {
                        item.so_luong += sol;
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    DichVuDaDat dv = new DichVuDaDat();
                    dv.maHDTS = Int32.Parse(maHDTS);
                    dv.ma_dv = Int32.Parse(ma_dv);
                    dv.so_luong = Int32.Parse(so_luong);
                    db.DichVuDaDats.Add(dv);
                }
                DichVu dichvu = db.DichVus.Find(madv);
                dichvu.ton_kho -= sol;
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("CallService", "HoaDon", new { id = maHDTS });
        }
        public ActionResult EditService(String maHDTS, String edit_id, String edit_so_luong)
        {
            if (maHDTS == null || edit_id == null || edit_so_luong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVuDaDat dsdv = db.DichVuDaDats.Find(Int32.Parse(edit_id));
            int sol = Int32.Parse(edit_so_luong);
            DichVu dv = db.DichVus.Find(dsdv.ma_dv);
            int del = (int)(sol - dsdv.so_luong);
            if (del > dv.ton_kho)
            {
                return RedirectToAction("CallService", "HoaDon", new { id = maHDTS });
            }
            else
            {
                dsdv.so_luong = sol;
                dv.ton_kho -= del;
                db.Entry(dsdv).State = EntityState.Modified;
                db.Entry(dv).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("CallService", "HoaDon", new { id = maHDTS });
        }
        public ActionResult DeleteService(String maHDTS, String del_id)
        {
            DichVuDaDat d = db.DichVuDaDats.Find(Int32.Parse(del_id));
            db.DichVuDaDats.Remove(d);
            db.SaveChanges();
            return RedirectToAction("CallService", "HoaDon", new { id = maHDTS });
        }
        public ActionResult ConfirmPayment(String maHDTS, String tienSan, String tiendichvu, String tongTien)
        {
            if (maHDTS == null || tienSan == null || tiendichvu == null || tongTien == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                HoaDonT hd = db.HoaDonTS.Find(Int32.Parse(maHDTS));
                NhanVien nv = (NhanVien)Session["NV"];
                if (nv != null)
                    hd.maNV = nv.maNV;
                hd.tienSan = int.Parse(tienSan);
                hd.tiendichvu = int.Parse(tiendichvu);
                hd.tongTien = int.Parse(tongTien);
                hd.maTinhTrang = 2;
                hd.ngayThueSan = DateTime.Now;
                db.Entry(hd).State = EntityState.Modified;

                San p = db.Sans.Find(hd.PhieuThueSan.maSan);
                p.maTinhTrang = 3;
                PhieuThueSan pd = db.PhieuThueSans.Find(hd.PhieuThueSan.maPT);
                pd.maTinhTrang = 4;
                db.Entry(p).State = EntityState.Modified;
                db.Entry(pd).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            ViewBag.maHDTS = maHDTS;
            return View();
        }
        public ActionResult DetailBill(int? id)
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

            DateTime ngay_thue = (DateTime)tblHoaDon.ngayThueSan;
            int soGioThue = tblHoaDon.PhieuThueSan.soGioThue ?? 0;
            DateTime dateS = new DateTime(ngay_thue.Year, ngay_thue.Month, ngay_thue.Day, 12, 0, 0);
            int gia = (int)tblHoaDon.PhieuThueSan.San.LoaiSan1.giaThue;
            var tienSan = soGioThue * gia;
            ViewBag.tienSan = tienSan;
            ViewBag.soGioThue = soGioThue;

            NhanVien nv = (NhanVien)Session["NV"];
            if (nv != null)
            {
                ViewBag.hotenNV = nv.hotenNV;
            }

            List<DichVuDaDat> dsdv = db.DichVuDaDats.Where(u => u.maHDTS == id).ToList();
            ViewBag.list_dv = dsdv;

            double tongtiendv = 0;
            List<double> tt = new List<double>();
            foreach (var item in dsdv)
            {
                double t = (double)(item.so_luong * item.DichVu.gia.Value);
                tongtiendv += t;
                tt.Add(t);
            }

            ViewBag.list_tt = tt;
            ViewBag.tiendichvu = tongtiendv;
            ViewBag.tongTien = tienSan + tongtiendv;

            return View(tblHoaDon);
        }



        public ActionResult Extend(int? id)
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
            PhieuThueSan pdp = db.PhieuThueSans.Find(tblHoaDon.maPT);
            String dt = null;
            try
            {
                int? d = db.PhieuThueSans.Where(t => t.maTinhTrang == 1 && t.maSan == pdp.San.maSan).Select(t => t.soGioThue).OrderBy(t => t).First();
                dt = d.ToString();
            }
            catch
            {

            }
            ViewBag.dateMax = dt;
            return View(pdp);
        }
        public ActionResult ResultExtend(String ma_pts, int soGioThue)
        {
            if (ma_pts == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                PhieuThueSan pdp = db.PhieuThueSans.Find(Int32.Parse(ma_pts));
                pdp.soGioThue = soGioThue;
                ViewBag.result = "success";
                ViewBag.soGioThue = soGioThue;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.result = "error: " + e;
            }
            return View();
        }
    }
}



  

    

