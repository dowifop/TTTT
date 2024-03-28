using QLTT.Areas.Admin.Controllers;
using QLTT.Areas.Admin.Controllers.Admin;
using QLTT.Areas.Admin.Factory;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Proxy
{
    public class SanProtectionProxy : ISanController
    {
        private readonly SanController _sanController;
        private readonly NhanVienFactory _nhanVienFactory;
        public SanProtectionProxy(SanController sanController, NhanVienFactory nhanVienFactory)
        {
            _sanController = sanController;
            _nhanVienFactory = nhanVienFactory;
        }
        public ActionResult Index()
        {
            return _sanController.Index();
        }
        public ActionResult Details(int? id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.Details(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Create()
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.Create();
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult CreateConfirmed(San san)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.CreateConfirmed(san);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult Delete(int? id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.Delete(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Edit(int? id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.Details(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult EditConfirmed(San san)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.EditConfirmed(san);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _sanController.DeleteConfirmed(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }       
    }
}