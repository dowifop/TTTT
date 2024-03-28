using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Proxy
{
    public interface INhanVienController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult CreateConfirmed(NhanVien tblNhanVien);    
        ActionResult Edit(int? id);
        ActionResult EditConfirmed(NhanVien tblNhanVien);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
