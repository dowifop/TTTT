using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLTT.Areas.Admin.Models;
using QLTT.Models;

namespace QLTT.Areas.Admin.Controllers.Admin
{
    public class HoaDonController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        // GET: HoaDon
        public ActionResult Index()
        {
            var HoaDonTS = db.HoaDonTS.Where(t => t.maTinhTrang == 2).Include(t => t.NhanVien).Include(t => t.PhieuThueSan)
                .Include(t => t.tinhTrangHD);
            double tong = 0;
            foreach (var item in HoaDonTS.ToList())
            {
                if (item.maTinhTrang == 2)
                {
                    tong += (double)item.tongTien;
                }
            }
            ViewBag.tong_tien = String.Format("{0:0,0.00}", tong);
            return View(HoaDonTS.ToList());
        }

        [HttpPost]
        public ActionResult Index(String beginDate)
        {
            System.Diagnostics.Debug.WriteLine("your message here " + beginDate);
            List<HoaDonT> dshd = new List<HoaDonT>();
            String query = "select * from HoaDonTS where maTinhTrang=2 ";
            if (!beginDate.Equals(""))
                query += " and ngayThueSan >= '" + beginDate + "'";
            //if (!endDate.Equals(""))
            //    query += " and ngay_tra_phong <= '" + endDate + "'";

            dshd = db.HoaDonTS.SqlQuery(query).ToList();
            double tong = 0;
            foreach (var item in dshd)
            {
                if (item.maTinhTrang == 2)
                {
                    tong += (double)item.tongTien;
                }
            }
            ViewBag.tongTien = tong.ToString("C");
            return View(dshd);
        }
        //public ActionResult Index(String beginDate)
        //{
        //    System.Diagnostics.Debug.WriteLine("your message here " + beginDate);
        //    List<HoaDonT> dshd = new List<HoaDonT>();

        //    string connString = db.Database.Connection.ConnectionString;  // replace with your actual connection string
        //    StringBuilder query = new StringBuilder("SELECT * FROM HoaDonTS WHERE maTinhTrang=@maTinhTrang");

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@maTinhTrang", 2);

        //            if (!String.IsNullOrEmpty(beginDate))
        //            {
        //                query.Append(" AND NgayThue >= @beginDate");
        //                cmd.Parameters.AddWithValue("@beginDate", beginDate);
        //            }

        //            cmd.CommandText = query.ToString();
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var item = new HoaDonT
        //                    {
        //                        //populate the HoaDonT object here, e.g.,
        //                        maTinhTrang = reader.GetInt32(reader.GetOrdinal("maTinhTrang")),
        //                        tongTien = reader.GetInt32(reader.GetOrdinal("tongTien")),
        //                        //you need to do this for all properties of HoaDonT
        //                    };
        //                    dshd.Add(item);
        //                }
        //            }
        //        }
        //    }

        //    double tong = 0;
        //    foreach (var item in dshd)
        //    {
        //        if (item.maTinhTrang == 2)
        //        {
        //            tong += (double)item.tongTien;
        //        }
        //    }
        //    ViewBag.tongTien = tong.ToString("C");
        //    return View(dshd);
        //}
        // GET: HoaDon/Details/5
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

        // GET: HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.maNV = new SelectList(db.NhanViens, "maNV", "hotenNV");
            ViewBag.maPT = new SelectList(db.PhieuThueSans, "maPT", "maKH");
            ViewBag.maTinhTrang = new SelectList(db.tinhTrangHDs, "maTinhTrang", "mo_ta");
            return View();
        }

        // POST: HoaDon/Create
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
        // GET: HoaDon/Edit/5
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

        // POST: HoaDon/Edit/5
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

        // GET: HoaDon/Delete/5
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

        // POST: HoaDon/Delete/5
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
                List<KhachHangKH> likh;
                PhieuThueSan pt = db.PhieuThueSans.Find(Int32.Parse(maPT));
                //if (pt.thong_tin_khach_hang_thue == null)
                //{
                //    likh = new List<KhachHangKH>();
                //    likh.Add(new KhachHangKH("", ""));
                //}
                //else
                //{
                //    likh = JsonConvert.DeserializeObject<List<KhachHangKH>>(pt.thong_tin_khach_hang_thue);
                //}
                //if (!hotenKH1.Equals(""))
                //    likh.Add(new KhachHangKH(hotenKH1, EmailKH1));
                //if (!hotenKH2.Equals(""))
                //    likh.Add(new KhachHangKH(hotenKH2, EmailKH2));
                //if (!hotenKH3.Equals(""))
                //    likh.Add(new KhachHangKH(hotenKH3, EmailKH3));
                //if (!hotenKH4.Equals(""))
                //    likh.Add(new KhachHangKH(hotenKH4, EmailKH4));
                //pt.thong_tin_khach_hang_thue = JsonConvert.SerializeObject(likh);
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
        public ActionResult ThanhToan(int? id)
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

            //var songay = (dateE - dateS).TotalDays;
            //if (dateS > ngay_vao)
            //    songay++;
            //if (ngay_ra > dateE)
            //    songay++;
            //var ti_le_phu_thu = tblHoaDon.tblPhieuDatPhong.tblPhong.tblLoaiPhong.ti_le_phu_thu;
            //var so_ngay_phu_thu = Math.Abs(Math.Ceiling((ngay_ra - ngay_du_kien).TotalDays));

            //System.Diagnostics.Debug.WriteLine("So ngay: " + so_ngay_phu_thu);
            //System.Diagnostics.Debug.WriteLine("Gia: " + gia);
            //System.Diagnostics.Debug.WriteLine("Ti le: :" + ti_le_phu_thu);

            //var phuthu = so_ngay_phu_thu * gia * ti_le_phu_thu / 100;
            //ViewBag.phu_thu = phuthu;

            //System.Diagnostics.Debug.WriteLine("Phu thu:" + phuthu);

            //ViewBag.so_ngay_phu_thu = so_ngay_phu_thu;
            var tienSan = soGioThue * gia;
            ViewBag.tienSan = tienSan;
            ViewBag.soGioThue = soGioThue;

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
            ViewBag.tong_tien = tienSan + tongtiendv;
            return View(tblHoaDon);
        }


        public ActionResult GoiDichVu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiDichVuModel mod = new GoiDichVuModel();
            mod.dsDichVu = db.DichVus.Where(t => t.ton_kho > 0).ToList();
            mod.dsDvDaDat = db.DichVuDaDats.Where(u => u.maHDTS == id).ToList();
            ViewBag.ma_hd = id;
            ViewBag.so_phong = db.HoaDonTS.Find(id).PhieuThueSan.San.maSoSan;
            return View(mod);
        }
        public ActionResult XacNhanGoiDichVu(String ma_hd, String ma_dv, String so_luong)
        {
            if (ma_hd == null || ma_dv == null || so_luong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int mahd = Int32.Parse(ma_hd);
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
                    dv.maHDTS = Int32.Parse(ma_hd);
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
            return RedirectToAction("GoiDichVu", "HoaDon", new { id = ma_hd });
        }
        public ActionResult SuaDichVu(String ma_hd, String edit_id, String edit_so_luong)
        {
            if (ma_hd == null || edit_id == null || edit_so_luong == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVuDaDat dsdv = db.DichVuDaDats.Find(Int32.Parse(edit_id));
            int sol = Int32.Parse(edit_so_luong);
            DichVu dv = db.DichVus.Find(dsdv.ma_dv);
            int del = (int)(sol - dsdv.so_luong);
            if (del > dv.ton_kho)
            {
                return RedirectToAction("GoiDichVu", "HoaDon", new { id = ma_hd });
            }
            else
            {
                dsdv.so_luong = sol;
                dv.ton_kho -= del;
                db.Entry(dsdv).State = EntityState.Modified;
                db.Entry(dv).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("GoiDichVu", "HoaDon", new { id = ma_hd });
        }
        public ActionResult XoaDichVu(String ma_hd, String del_id)
        {
            DichVuDaDat d = db.DichVuDaDats.Find(Int32.Parse(del_id));
            db.DichVuDaDats.Remove(d);
            db.SaveChanges();
            return RedirectToAction("GoiDichVu", "HoaDon", new { id = ma_hd });
        }


        /// <summary>
        /// ///////////////////

        /// <returns></returns>
        /// 

        public ActionResult XacNhanThanhToan(String ma_hd, String tien_san, String tien_dich_vu, String tong_tien)
        {
            if (ma_hd == null || tien_san == null || tien_dich_vu == null || tong_tien == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                HoaDonT hd = db.HoaDonTS.Find(Int32.Parse(ma_hd));
                NhanVien nv = (NhanVien)Session["NV"];
                if (nv != null)
                    hd.maNV = nv.maNV;
                hd.tienSan = int.Parse(tien_san);
                hd.tiendichvu = int.Parse(tien_dich_vu);
                hd.tongTien = int.Parse(tong_tien);
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
            ViewBag.ma_hd = ma_hd;
            return View();
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

            DateTime ngay_thue = (DateTime)tblHoaDon.ngayThueSan;
            int soGioThue = tblHoaDon.PhieuThueSan.soGioThue ?? 0;
            DateTime dateS = new DateTime(ngay_thue.Year, ngay_thue.Month, ngay_thue.Day, 12, 0, 0);
            Double gia = (Double)tblHoaDon.PhieuThueSan.San.LoaiSan1.giaThue;

            //var songay = (dateE - dateS).TotalDays;
            //if (dateS > ngay_vao)
            //    songay++;
            //if (ngay_ra > dateE)
            //    songay++;

            //var ti_le_phu_thu = tblHoaDon.tblPhieuDatPhong.tblPhong.tblLoaiPhong.ti_le_phu_thu;
            //var so_ngay_phu_thu = Math.Abs(Math.Ceiling((ngay_ra - ngay_du_kien).TotalDays));

            //System.Diagnostics.Debug.WriteLine("So ngay: " + so_ngay_phu_thu);
            //System.Diagnostics.Debug.WriteLine("Gia: " + gia);
            //System.Diagnostics.Debug.WriteLine("Ti le: " + ti_le_phu_thu);

            //var phuthu = so_ngay_phu_thu * gia * ti_le_phu_thu / 100;
            //ViewBag.phu_thu = phuthu;

            //System.Diagnostics.Debug.WriteLine("Phu thu: " + phuthu);

            //ViewBag.so_ngay_phu_thu = so_ngay_phu_thu;
            var tien_san = soGioThue * gia;
            ViewBag.tien_san = tien_san;
            ViewBag.soGioThue = soGioThue;

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
                double t = (double)(item.so_luong * item.DichVu.gia.Value);
                tongtiendv += t;
                tt.Add(t);
            }

            ViewBag.list_tt = tt;
            ViewBag.tien_dich_vu = tongtiendv;
            ViewBag.tong_tien = tien_san + tongtiendv;

            return View(tblHoaDon);
        }



        public ActionResult GiaHanSan(int? id)
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
        public ActionResult ResultGiaHan(String ma_pts, int soGioThue)
        {
            if (ma_pts == null || soGioThue == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                PhieuThueSan pdp = db.PhieuThueSans.Find(Int32.Parse(ma_pts));
                pdp.soGioThue = soGioThue;
                ViewBag.result = "success";
                ViewBag.ngay_ra = soGioThue;
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



  

    

