using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Controllers
{
    public class NhanVienProxyController : Controller
    {
        private readonly NhanVienProtectionProxy _nhanVienProtectionProxy;
        public NhanVienProxyController(NhanVienProtectionProxy nhanVienProtectionProxy)
        {
            _nhanVienProtectionProxy = nhanVienProtectionProxy;
        }

        // GET: Admin/NhanVienProxy
        public ActionResult Index()
        {
            return _nhanVienProtectionProxy.Index();
        }
        public ActionResult Details(int id)
        {
            return _nhanVienProtectionProxy.Details(id);
        }
        public ActionResult Create()
        {
            return _nhanVienProtectionProxy.Create();
        }
        public ActionResult CreateConfirmed(NhanVien tblNhanVien)
        {
            return _nhanVienProtectionProxy.CreateConfirmed(tblNhanVien);
        }
        public ActionResult Edit(int id)
        {
            return _nhanVienProtectionProxy.Edit(id);
        }
        public ActionResult EditConfirmed(NhanVien tblNhanVien)
        {
            return _nhanVienProtectionProxy.EditConfirmed(tblNhanVien);
        }
        public ActionResult Delete(int id)
        {
            return _nhanVienProtectionProxy.Delete(id);
        }
        public ActionResult DeleteConfirmed(int id)
        {
            return _nhanVienProtectionProxy.DeleteConfirmed(id);
        }
    }
}