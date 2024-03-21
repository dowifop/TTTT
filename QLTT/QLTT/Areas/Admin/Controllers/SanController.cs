using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTT.Areas.Facade;
using QLTT.Facade;
using QLTT.Models;

namespace QLTT.Areas.Admin.Controllers.Admin
{
    /*    public class SanController : Controller
        {
            private QlyTheThaoEntities db = new QlyTheThaoEntities();

            public ActionResult Index()
            {
                var Sans = db.Sans.Where(t => t.maTinhTrang < 5).Include(t => t.LoaiSan).Include(t => t.TinhTrangSan);
                return View(Sans.ToList());
            }

            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                San san = db.Sans.Find(id);
                if (san == null)
                {
                    return HttpNotFound();
                }
                return View(san);
            }

            public ActionResult Create()
            {
                ViewBag.Loai_San = new SelectList(db.LoaiSans, "Loai_San", "moTa");

                ViewBag.maTinhTrang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota");
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "maSan,maSoSan,Loai_San,maTinhTrang")] San san)
            {
                if (ModelState.IsValid)
                {
                    db.Sans.Add(san);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Loai_San = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.Loai_San);
                ViewBag.maTinhTrang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", san.maTinhTrang);
                return View(san);
            }

            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                San san = db.Sans.Find(id);
                if (san == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Loai_San = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.Loai_San);
                ViewBag.maTinhTrang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", san.maTinhTrang);
                return View(san);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "maSan,maSoSan,Loai_San,maTinhTrang")] San san)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(san).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.loai_san = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.Loai_San);
                ViewBag.maTinhTrang = new SelectList(db.TinhTrangSans, "maTinhTrang", "mota", san.maTinhTrang);
                return View(san);
            }
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                San san = db.Sans.Find(id);
                if (san == null)
                {
                    return HttpNotFound();
                }
                return View(san);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                try
                {
                    San san = db.Sans.Find(id);
                    san.maTinhTrang = 5;
                    db.Entry(san).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {

                }
                return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }*/

    public class SanController : Controller
    {
        private ISanFacade sanFacade;

        public SanController()
        {
            this.sanFacade = new SanFacade(new QlyTheThaoEntities());
        }

        public ActionResult Index()
        {
            var sans = sanFacade.GetAllSans();
            return View(sans);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San san = sanFacade.GetSanById(id.Value);
            if (san == null)
            {
                return HttpNotFound();
            }
            return View(san);
        }

        public ActionResult Create()
        {
            ViewBag.Loai_San = new SelectList(sanFacade.GetLoaiSans(), "Loai_San", "moTa");
            ViewBag.maTinhTrang = new SelectList(sanFacade.GetTinhTrangSans(), "maTinhTrang", "mota");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSan,maSoSan,Loai_San,maTinhTrang")] San san)
        {
            if (ModelState.IsValid)
            {
                sanFacade.AddSan(san);
                return RedirectToAction("Index");
            }

            ViewBag.Loai_San = new SelectList(sanFacade.GetLoaiSans(), "Loai_San", "moTa", san.Loai_San);
            ViewBag.maTinhTrang = new SelectList(sanFacade.GetTinhTrangSans(), "maTinhTrang", "mota", san.maTinhTrang);
            return View(san);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San san = sanFacade.GetSanById(id.Value);
            if (san == null)
            {
                return HttpNotFound();
            }
            ViewBag.Loai_San = new SelectList(sanFacade.GetLoaiSans(), "Loai_San", "moTa", san.Loai_San);
            ViewBag.maTinhTrang = new SelectList(sanFacade.GetTinhTrangSans(), "maTinhTrang", "mota", san.maTinhTrang);
            return View(san);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSan,maSoSan,Loai_San,maTinhTrang")] San san)
        {
            if (ModelState.IsValid)
            {
                sanFacade.UpdateSan(san);
                return RedirectToAction("Index");
            }
            ViewBag.Loai_San = new SelectList(sanFacade.GetLoaiSans(), "Loai_San", "moTa", san.Loai_San);
            ViewBag.maTinhTrang = new SelectList(sanFacade.GetTinhTrangSans(), "maTinhTrang", "mota", san.maTinhTrang);
            return View(san);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            San san = sanFacade.GetSanById(id.Value);
            if (san == null)
            {
                return HttpNotFound();
            }
            return View(san);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sanFacade.DeleteSan(id);
            return RedirectToAction("Index");
        }
    }
}
