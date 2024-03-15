using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTT.Models;

namespace QLTT.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        // GET: Admin
        QlyTheThaoEntities db = new QlyTheThaoEntities();
        public ActionResult Index()
        {
            int so_san_trong = 0, so_san_sd = 0, so_san_don = 0;
            var listSans = db.Sans.Where(t => t.maTinhTrang < 5).ToList();
            foreach (var item in listSans)
            {
                if (item.maTinhTrang == 1)
                    so_san_trong++;
                else if (item.maTinhTrang == 2)
                    so_san_sd++;
                else
                    so_san_don++;
            }
            ViewBag.so_san_trong = so_san_trong;
            ViewBag.so_san_sd = so_san_sd;
            ViewBag.so_san_don = so_san_don;
            return View(listSans);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NhanVien objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.NhanViens.Where(a => a.taiKhoan.Equals(objUser.taiKhoan) && a.matKhau.Equals(objUser.matKhau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["NV"] = obj;
                    return RedirectToAction("Index", "KhachHang");
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
            if (Session["NV"] != null)
                return RedirectToAction("Index", "ThongKe");
            return View();
        }
        public ActionResult Logout()
        {
            Session["NV"] = null;
            return RedirectToAction("Login","Index");
        }


        public ActionResult ChooseRent()
        {
            return View();
        }
        public ActionResult ListOfActive()
        {
            var list = db.HoaDonTS.Where(u => u.maTinhTrang == 1).Include(t => t.NhanVien).Include(t => t.PhieuThueSan).Include(t => t.tinhTrangHD);
            return View(list.ToList());
        }
        public ActionResult ListOfServiceCalled()
        {
            var list = db.HoaDonTS.Where(u => u.maTinhTrang == 1).Include(t => t.NhanVien).Include(t => t.PhieuThueSan).Include(t => t.tinhTrangHD);
            return View(list.ToList());
        }
        public ActionResult Return(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        public ActionResult FindBill(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ma_hd = db.HoaDonTS.Where(u => u.PhieuThueSan.maSan == id && u.maTinhTrang == 1).First().maHDTS;
            return RedirectToAction("Pay", "Bill", new { id = ma_hd });
        }
        public ActionResult FindHdById2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ma_hd = db.HoaDonTS.Where(u => u.PhieuThueSan.maSan == id && u.maTinhTrang == 1).First().maHDTS;
            return RedirectToAction("CallService", "Bill", new { id = ma_hd });
        }
        public ActionResult CleanInfrastructure(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           San p = db.Sans.Where(u => u.maSan == id).First();
            p.maTinhTrang = 1;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Index");
        }
        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Employee()
        {
            NhanVien nv = (NhanVien)Session["NV"];
            if (nv != null)
            {
                nv = db.NhanViens.Find(nv.maNV);
                ViewBag.maCV = new SelectList(db.ChucVus, "maCV", "tenCV", nv.maCV);
                return View(nv);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Employee([Bind(Include = "maNV,hoTenNV,emailNV,sdtNV,diaChiNV,dobNV,taiKhoan,matKhau,maPH,maCV,maSK")] NhanVien tblNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maCV = new SelectList(db.ChucVus, "maCV", "tenCV", tblNhanVien.maCV);
            return View(tblNhanVien);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string code = "";
                    List<String> dsImg = new List<string>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        String filename = Path.Combine(Server.MapPath("~/Content/Images/Phong/"), fname);
                        file.SaveAs(filename);
                        dsImg.Add("/Content/Images/Phong/" + fname);
                    }
                    // Returns message that successfully uploaded
                    code = Newtonsoft.Json.JsonConvert.SerializeObject(dsImg);
                    return Json(code);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}