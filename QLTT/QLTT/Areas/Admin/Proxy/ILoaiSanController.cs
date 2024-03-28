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
    public interface ILoaiSanController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult CreateConfirmed(LoaiSan loaiSan);
        ActionResult Edit(int? id);
        ActionResult EditConfirmed(LoaiSan loaiSan);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
