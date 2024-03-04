using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Controllers
{
    public class BangGiaController : Controller
    {
        // GET: BangGia
        QLTT.Models.QlyTheThaoEntities db = new Models.QlyTheThaoEntities();
      
        public ActionResult GiaSan()
        {
            return View(db.LoaiSans.ToList());
        }
        public ActionResult GiaDichVu()
        {
            return View(db.DichVus.ToList());
        }
    }
}