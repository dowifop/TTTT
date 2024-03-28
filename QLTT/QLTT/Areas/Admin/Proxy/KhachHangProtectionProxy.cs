using Microsoft.ApplicationInsights.Extensibility.Implementation;
using QLTT.Areas.Admin.Controllers;
using QLTT.Areas.Admin.Factory;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QLTT.Areas.Admin.Proxy
{
    public class KhachHangProtectionProxy : IKhachHangController
    {
        private readonly KhachHangController _controller;
        private readonly NhanVienFactory _nhanVienFactory;

        public KhachHangProtectionProxy(KhachHangController controller, NhanVienFactory nhanVienFactory)
        {
            _controller = controller;
            _nhanVienFactory = nhanVienFactory;
        }

        // Proxy method for Index
        public ActionResult Index()
        {
            return _controller.Index();
        }
        // Proxy method for Details
        public ActionResult Details(string id)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.Details(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Add()
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.Add();
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult AddConfirmed(KhachHang tblKhachHang)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.AddConfirmed(tblKhachHang);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Edit(string id)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.Edit(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult EditConfirmed(KhachHang tblKhachHang)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.EditConfirmed(tblKhachHang);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult Delete(string id)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.Delete(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien currentUser = _nhanVienFactory.CreateNhanVien();
            if (currentUser != null && currentUser.maCV == 1)
            {
                return _controller.DeleteConfirmed(id);
            }
            else
            {
                throw new UnauthorizedAccessException("Bạn không đủ quyền truy cập trang này.");
            }
        }
    }
}