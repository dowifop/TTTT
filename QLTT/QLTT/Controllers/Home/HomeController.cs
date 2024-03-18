using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTT.Models;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace QLTT.Controllers
{
    public class HomeController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();

        public ActionResult Index()
        {
            //List<TinNhan> dstn = db.TinNhans.OrderByDescending(t => t.ngay_gui).Take(5).ToList();
            //ViewBag.Dstn = dstn;

            //// Truy vấn dữ liệu từ CSDL
            //List<TinNhan> tinNhans = db.TinNhans.ToList();

            //// Lọc bỏ các giá trị null
            //tinNhans = tinNhans.Where(tinNhan => tinNhan.danh_gia.HasValue && !string.IsNullOrEmpty(tinNhan.noi_dung)).ToList();

            //// Truyền dữ liệu vào ViewBag hoặc Model
            //ViewBag.TinNhans = tinNhans;
            return View();
        }

        //public double CalculateAverageRating(List<TinNhan> tinNhans)
        //{
        //    double averageRating = 0;
        //    if (tinNhans != null && tinNhans.Count > 0)
        //    {
        //        int totalRatings = 0;
        //        int ratedMessagesCount = 0;
        //        foreach (var tinNhan in tinNhans)
        //        {
        //            if (tinNhan != null && tinNhan.danh_gia.HasValue)
        //            {
        //                totalRatings += tinNhan.danh_gia.Value;
        //                ratedMessagesCount++;
        //            }
        //        }

        //        if (ratedMessagesCount > 0)
        //        {
        //            averageRating = (double)totalRatings / ratedMessagesCount;
        //        }
        //    }
        //    return averageRating;
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(string ho_ten, string mail, string noi_dung, int danh_gia)
        {
            if (noi_dung == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            KhachHang kh = (KhachHang)Session["KH"];
            if (kh == null)
            {
                if (ho_ten == null || mail == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (noi_dung.Length >= 500)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TinNhan tn = new TinNhan();
            if (kh == null)
            {
                tn.ho_ten = ho_ten;
                tn.mail = mail;
            }
            else
            {
                tn.ma_kh = kh.maKH;
            }

            tn.noi_dung = noi_dung;
            tn.ngay_gui = DateTime.Now;

            if (danh_gia >= 1 && danh_gia <= 5)
            {
                tn.danh_gia = danh_gia;
                try
                {
                    db.TinNhans.Add(tn);
                    db.SaveChanges();
                    ModelState.AddModelError("", "Gửi đánh giá thành công!");
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Đánh giá không hợp lệ! Vui lòng chọn từ 1 đến 5 sao.");
            }

            return View();
        }
        public ActionResult SMS(String ho_ten, String mail, String noi_dung)
        {
            if (noi_dung == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh == null)
            {
                if (ho_ten == null || mail == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (noi_dung.Length >= 500)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            TinNhan tn = new TinNhan();
            if (kh == null)
            {
                tn.ho_ten = ho_ten;
                tn.mail = mail;
            }
            else
            {
                tn.ma_kh = kh.maKH;
            }
            tn.noi_dung = noi_dung;
            try
            {
                db.TinNhans.Add(tn);
                db.SaveChanges();
                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            return View();
        }
        public ActionResult Event() {
            return View();

        }

        [HttpGet]
        public ActionResult Search()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
     
        public ActionResult Search(String NgayThue, int soGioThue)
        {
            List<San> li = new List<San>();
            if (NgayThue.Equals("") || soGioThue <= 0)
            {
                li = db.Sans.ToList();
            }
            else
            {
                Session["dsmaSan"] = null;
                Session["ngayThue"] = NgayThue;
                Session["soGioThue"] = soGioThue;

                DateTime.TryParseExact(NgayThue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedNgayThue);
                DateTime dateS = parsedNgayThue.AddHours(12);
                DateTime dateE = dateS.AddHours(soGioThue);
                var phieuThueSansInMemory = db.PhieuThueSans.ToList();

                var phieuThueSansFiltered = phieuThueSansInMemory
                                        .Where(m => (m.maTinhTrang == 1 || m.maTinhTrang == 2) && m.NgayThue.HasValue)
                                        .Select(m => new { m.maSan, m.soGioThue, NgayThue = m.NgayThue.Value })
                                        .ToList(); 

                li = db.Sans.AsEnumerable() 
                    .Where(t => !phieuThueSansFiltered.Any(m =>
                        m.NgayThue.AddHours(m.soGioThue ?? 0) > dateS && m.NgayThue < dateE && m.maSan == t.maSan)).ToList();

            }
            return View(li);
        }

        public ActionResult RentInfrastucture(string id)
        {
            try
            {
                if (Session["dsmaSan"] != null)
                {
                    List<int> ds = (List<int>)Session["dsmaSan"];
                    ds.Add(Int32.Parse(id));
                    Session["dsmaSan"] = ds;
                }
                else
                {
                    List<int> ds = new List<int>();
                    ds.Add(Int32.Parse(id));
                    Session["dsmaSan"] = ds;
                }

                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }

            var dstn = db.TinNhans.OrderByDescending(t => t.ngay_gui).Take(5).ToList();
            
            ViewBag.TinNhans = dstn;
            return View();
        }
        public ActionResult Cancel(string id)
        {
            try
            {
                List<int> ds;
                ds = (List<int>)Session["dsmaSan"];
                if (ds == null)
                    ds = new List<int>();
                ds.Remove(Int32.Parse(id));
                Session["dsmaSan"] = ds;
                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            return View();
        }
        public ActionResult Rent()
        {
            if (Session["KH"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            AutoCancelVoteRent();
            KhachHang kh = (KhachHang)Session["KH"];
            ViewBag.maKH = kh.maKH;
            ViewBag.hoTenKH = kh.hoTenKH;
            ViewBag.NgayDat = DateTime.Now;
            ViewBag.ngayThue = (String)Session["ngayThue"];
            if (Session["soGioThue"] != null)
            {
                ViewBag.soGioThue = Session["soGioThue"].ToString();
            }
            String sp = "";
            List<int> ds;
            ds = (List<int>)Session["dsmaSan"];
            if (ds == null)
                ds = new List<int>();
            ViewBag.maSan = JsonConvert.SerializeObject(ds);
            foreach (var item in ds)
            {
                San p = (San)db.Sans.Find(Int32.Parse(item.ToString()));
                sp += p.maSoSan.ToString() + ", ";
            }
            ViewBag.maSoSan = sp;
            var liP = db.PhieuThueSans.Where(u => u.maKH == kh.maKH && u.maTinhTrang == 1).ToList();
            return View(liP);
        }
        private void AutoCancelVoteRent()
        {
            var datenow = DateTime.Now;
            var tblPhieuDatSans = db.PhieuThueSans.Where(u => u.maTinhTrang == 1).Include(t => t.KhachHang).Include(t => t.San).Include(t => t.tinhTrangPT).ToList();
            foreach (var item in tblPhieuDatSans)
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
        public ActionResult Result(String maKH, String ngayThue, int soGioThue, String maSan)
        {
            if (maKH == null || ngayThue == null || maSan == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                PhieuThueSan tgd = new PhieuThueSan();
                List<int> ds = JsonConvert.DeserializeObject<List<int>>(maSan);
                tgd.maKH = maKH;
                tgd.maTinhTrang = 1;
                tgd.NgayDat = DateTime.Now;
                Console.WriteLine(ngayThue);
                tgd.NgayThue = DateTime.ParseExact(ngayThue, "d/M/yyyy", CultureInfo.InvariantCulture).AddHours(12);

                tgd.soGioThue = soGioThue;
                try
                {
                    for (int i = 0; i < ds.Count; i++)
                    {
                        tgd.maSan = ds[i];
                        db.PhieuThueSans.Add(tgd);
                        db.SaveChanges();
                        ViewBag.Result = "success";
                    }
                    ViewBag.NgayThue = tgd.NgayThue;
                    setNull();
                }
                catch
                {
                    ViewBag.Result = "error";
                }
            }
            return View();
        }

        public ActionResult CancelVoteRent()
        {
            setNull();
            return RedirectToAction("Rent", "Home");
        }
        private void setNull()
        {
            Session["ngayThue"] = null;
            Session["soGioThue"] = null;
            Session["maSan"] = null;
            Session["dsmaSan"] = null;
        }
        public ActionResult Slider(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            LoaiSan lp = db.LoaiSans.Find(id);
            return View(lp);
        }
        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult ChooseInfrastructure(string id)
        {           
            try
            {
                List<int> ds;
                ds = (List<int>)Session["dsmaSan"];
                if (ds == null)
                    ds = new List<int>();
                ds.Add(Int32.Parse(id));
                Session["dsmaSan"] = ds;
                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            return View();          
        }
    }
}
