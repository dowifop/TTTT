using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Factory
{
    public class NhanVienFactory : INhanVienFactory
    {
        public NhanVien CreateNhanVien()
        {
            // Giả định 'NV' là key của đối tượng nhân viên trong session
            NhanVien nhanVien = HttpContext.Current.Session["NV"] as NhanVien;
            if (nhanVien == null)
            {
                return null; 
            }
            return nhanVien;
        }
    }
}