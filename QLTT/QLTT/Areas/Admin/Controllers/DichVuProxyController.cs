using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Controllers
{
    public class DichVuProxyController : Controller
    {
        private readonly DichVuProtectionProxy _dichVuProtectionProxy;
        public DichVuProxyController(DichVuProtectionProxy dichVuProtectionProxy)
        {
            _dichVuProtectionProxy = dichVuProtectionProxy;
        }
        // GET: Admin/DichVuProxy
        public ActionResult Index()
        {
            return _dichVuProtectionProxy.Index();
        }
        public ActionResult Details(int? id)
        {
            return _dichVuProtectionProxy.Details(id);
        }
        public ActionResult Create()
        {
            return _dichVuProtectionProxy.Create();
        }
        public ActionResult CreateConfirmed(HttpPostedFileBase file, DichVu dichVu)
        {
            return _dichVuProtectionProxy.CreateConfirmed(file,dichVu);
        }
        public ActionResult Edit(int? id)
        {
            return _dichVuProtectionProxy.Edit(id);
        }
        public ActionResult EditConfirmed(HttpPostedFileBase file, DichVu dichVu)
        {
            return _dichVuProtectionProxy.EditConfirmed(file,dichVu);
        }
        public ActionResult Delete(int? id)
        {
            return _dichVuProtectionProxy.Delete(id);
        }
        public ActionResult DeleteConfirmed(int id)
        {
            return _dichVuProtectionProxy.DeleteConfirmed(id);
        }
    }
}