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
    public class NhanVienProtectionProxy : INhanVienController
    {
        private readonly NhanVienController _nhanVienController;
        private readonly NhanVienFactory _nhanVienFactory;
        public NhanVienProtectionProxy(NhanVienController nhanVienController, NhanVienFactory nhanVienFactory)
        {
            _nhanVienController = nhanVienController;
            _nhanVienFactory = nhanVienFactory;
        }
        public ActionResult Index()
        {
            return _nhanVienController.Index();
        }
        public ActionResult Create()
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.Create();
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult CreateConfirmed(NhanVien tblNhanVien)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.CreateConfirmed(tblNhanVien);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult Delete(int? id)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.Delete(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.DeleteConfirmed(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult Details(int? id)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.Details(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult Edit(int? id)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.Edit(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        public ActionResult EditConfirmed(NhanVien tblNhanVien)
        {
            NhanVien nhanvien = _nhanVienFactory.CreateNhanVien();
            if (nhanvien != null && nhanvien.maCV == 1)
            {
                return _nhanVienController.EditConfirmed(tblNhanVien);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }

        
    }
}