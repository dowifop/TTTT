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
    public interface IDichVuController
    {
        ActionResult Index();
        ActionResult Details(int? id);
        ActionResult Create();
        ActionResult CreateConfirmed(HttpPostedFileBase file,DichVu dichvu);
        ActionResult Edit(int? id);
        ActionResult EditConfirmed(HttpPostedFileBase file,DichVu dichvu);
        ActionResult Delete(int? id);
        ActionResult DeleteConfirmed(int id);
    }
}
