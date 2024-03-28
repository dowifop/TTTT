using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Controllers
{
    public class LoaiSanProxyController : Controller,ILoaiSanController
    {
        private readonly LoaiSanProtectionProxy _loaiSanProtectionProxy;
        public LoaiSanProxyController(LoaiSanProtectionProxy loaiSanProtectionProxy)
        {
            _loaiSanProtectionProxy = loaiSanProtectionProxy;
        }
        // GET: Admin/LoaiSanProxy
        public ActionResult Index()
        {
            return _loaiSanProtectionProxy.Index();
        }
        public ActionResult Details(int? id)
        {
            return _loaiSanProtectionProxy.Details(id);
        }
        public ActionResult Create()
        {
            return _loaiSanProtectionProxy.Create();
        }
        public ActionResult CreateConfirmed(LoaiSan loaiSan)
        {
            return _loaiSanProtectionProxy.CreateConfirmed(loaiSan);
        }
        public ActionResult Edit(int? id)
        {
            return _loaiSanProtectionProxy.Edit(id);
        }
        public ActionResult EditConfirmed(LoaiSan loaiSan)
        {
            return _loaiSanProtectionProxy.EditConfirmed(loaiSan);
        }
        public ActionResult Delete(int? id)
        {
            return _loaiSanProtectionProxy.Delete(id);
        }
        public ActionResult DeleteConfirmed(int id)
        {
            return _loaiSanProtectionProxy.DeleteConfirmed(id);
        }
         
    }
}