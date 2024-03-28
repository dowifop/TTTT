using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Controllers
{
    public class KhachHangProxyController : Controller
    {
        private readonly KhachHangProtectionProxy _khachHangProtectionProxy;
        public KhachHangProxyController(KhachHangProtectionProxy khachHangProtectionProxy) 
        {
            _khachHangProtectionProxy = khachHangProtectionProxy;
        }
        // GET: Admin/KhachHangProxy
        public ActionResult Index()
        {
            return _khachHangProtectionProxy.Index();
        }
        public ActionResult Details(string id)
        {
            return _khachHangProtectionProxy.Details(id);
        }
        public ActionResult Add()
        {
            return _khachHangProtectionProxy.Add();
        }
        public ActionResult AddConfirm(KhachHang tblKhachHang)
        {
            return _khachHangProtectionProxy.AddConfirmed(tblKhachHang);
        }
        public ActionResult Edit(string id) 
        {
            return _khachHangProtectionProxy.Edit(id);
        }
        public ActionResult EditConFirm(KhachHang tblKhachHang)
        {
            return _khachHangProtectionProxy.EditConfirmed(tblKhachHang);
        }
        public ActionResult Delete(string id)
        {
            return _khachHangProtectionProxy.Delete(id);
        }
        public ActionResult DeleteConfirm(string id)
        {
            return _khachHangProtectionProxy.DeleteConfirmed(id);
        }
    }
}