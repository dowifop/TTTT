using QLTT.Areas.Admin.Controllers;
using QLTT.Areas.Admin.Factory;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Proxy
{
    public class DichVuProtectionProxy : IDichVuController
    {
        private readonly DichVuController _dichVuController;
        private readonly NhanVienFactory _nhanVienFactory;
        public DichVuProtectionProxy(DichVuController dichVuController, NhanVienFactory nhanVienFactory)
        {
            _dichVuController = dichVuController;
            _nhanVienFactory = nhanVienFactory;
        }
        public ActionResult Index()
        {
            return _dichVuController.Index();
        }
        public ActionResult Details(int? id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _dichVuController.Details(id);
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
                return _dichVuController.Create();
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult CreateConfirmed(HttpPostedFileBase file, DichVu dichvu)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _dichVuController.CreateConfirmed(file, dichvu);
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
                return _dichVuController.Edit(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult EditConfirmed(HttpPostedFileBase file, DichVu dichvu)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _dichVuController.EditConfirmed(file, dichvu);
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
                return _dichVuController.Delete(id);
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
                return _dichVuController.DeleteConfirmed(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

    }
}