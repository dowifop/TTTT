using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Areas.Admin.Proxy
{
    public interface IKhachHangController
    {
        ActionResult Index();
        ActionResult Details(string id);
        ActionResult Add();
        ActionResult AddConfirmed(KhachHang tblKhachHang);
        ActionResult Edit(string id);
        ActionResult EditConfirmed(KhachHang tblKhachHang);
        ActionResult Delete(string id);
        ActionResult DeleteConfirmed(string id);
    }
}
