using QLTT.Areas.Admin.Proxy;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLTT.Areas.Admin.Factory
{
    public interface INhanVienFactory
    {
        NhanVien CreateNhanVien();
    }
}