using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Controllers
{
    public class SanProxyController : Controller,ISanController
    {
        private readonly SanProtectionProxy _sanProtectionProxy;
        public SanProxyController(SanProtectionProxy sanProtectionProxy)
        {
            _sanProtectionProxy = sanProtectionProxy;
        }
        // GET: Admin/SanProxy
        public ActionResult Index()
        {
            return _sanProtectionProxy.Index();
        }
        public ActionResult Details(int? id)
        {
            throw new NotImplementedException();
        }
        public ActionResult Create()
        {
            return _sanProtectionProxy.Create();
        }
        public ActionResult CreateConfirmed(San san)
        {
            return _sanProtectionProxy.CreateConfirmed(san);
        }
        public ActionResult Edit(int? id)
        {
            return _sanProtectionProxy.Edit(id);
        }
        public ActionResult EditConfirmed(San san)
        {
            return _sanProtectionProxy.EditConfirmed(san);
        }    
        public ActionResult Delete(int? id)
        {
            return _sanProtectionProxy.Delete(id);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            return (_sanProtectionProxy.DeleteConfirmed(id));
        }

             
    }
}