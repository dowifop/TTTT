using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTTTT.Models;

namespace WebTTTT.Controllers
{
    public class TrangChuController : Controller
    {
        private QlyTheThaoEntities db = new QlyTheThaoEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
            WebTTTT.Models.QlyTheThaoEntities db = new WebTTTT.Models.QlyTheThaoEntities();
            return View();
        }
    }
}