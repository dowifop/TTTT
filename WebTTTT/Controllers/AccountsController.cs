using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebTTTT.Models;

namespace WebTTTT.Controllers
{
    public class AccountsController : Controller

    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "taiKhoan,matKhau,hoTenKH,sdtKH,diaChiKH,emailKH")] KhachHang kh)
        {
            

            
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(kh.taiKhoan) ||
                    string.IsNullOrWhiteSpace(kh.matKhau) ||
                    string.IsNullOrWhiteSpace(kh.hoTenKH) ||
                    string.IsNullOrWhiteSpace(kh.sdtKH) ||
                    string.IsNullOrWhiteSpace(kh.diaChiKH) ||
                    string.IsNullOrWhiteSpace(kh.emailKH))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                    return View(kh);
                }
                if (db.KhachHangs.Any(k => k.taiKhoan == kh.taiKhoan))
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
                return RedirectToAction("Login", "Accounts");
            }

            return View(kh);
          
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(KhachHang kh)
        {
            var check = db.KhachHangs.Where(s => s.taiKhoan == kh.taiKhoan
                && s.matKhau == kh.matKhau).FirstOrDefault();
            if (check == null) //login sai thông tin
            {
                ViewBag.ErrorInfo = "Sai thông tin";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["taiKhoan"] = kh.taiKhoan;
                Session["matKhau"] = kh.matKhau;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}