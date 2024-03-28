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
    public class LoaiSanProtectionProxy : ILoaiSanController
    {
        private readonly LoaiSanController _loaiSancontroller;
        private readonly NhanVienFactory _nhanVienFactory;
        public LoaiSanProtectionProxy(LoaiSanController loaiSancontroller, NhanVienFactory nhanVienFactory)
        {
            _loaiSancontroller = loaiSancontroller;
            _nhanVienFactory = nhanVienFactory;
        }
        public ActionResult Index()
        {
            return _loaiSancontroller.Index();
        }
        public ActionResult Create()
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if(userCurrent != null && userCurrent.maCV == 1)
            {
                return _loaiSancontroller.Create();
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Details(int? id)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _loaiSancontroller.Details(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult CreateConfirmed(LoaiSan loaiSan)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _loaiSancontroller.CreateConfirmed(loaiSan);
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
                return _loaiSancontroller.Edit(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult EditConfirmed(LoaiSan loaiSan)
        {
            NhanVien userCurrent = _nhanVienFactory.CreateNhanVien();
            if (userCurrent != null && userCurrent.maCV == 1)
            {
                return _loaiSancontroller.EditConfirmed(loaiSan);
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
                return _loaiSancontroller.Delete(id);
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
                return _loaiSancontroller.DeleteConfirmed(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }     
    }
}