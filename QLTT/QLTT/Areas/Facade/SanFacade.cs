using QLTT.Areas.Facade;
using QLTT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLTT.Facade
{
    public class SanFacade : ISanFacade
    {
        private QlyTheThaoEntities _db;

        public SanFacade(QlyTheThaoEntities db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<San> GetAllSans()
        {
            return _db.Sans.Where(s => s.maTinhTrang < 5).ToList();
        }

        public San GetSanById(int id)
        {
            return _db.Sans.Find(id);
        }

        public void AddSan(San san)
        {
            _db.Sans.Add(san);
            _db.SaveChanges();
        }

        public void UpdateSan(San san)
        {
            _db.Entry(san).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteSan(int id)
        {
            var san = GetSanById(id);
            if (san != null)
            {
                _db.Sans.Remove(san);
                _db.SaveChanges();
            }
        }

        public IEnumerable<LoaiSan> GetLoaiSans()
        {
            return _db.LoaiSans.ToList();
        }

        public IEnumerable<TinhTrangSan> GetTinhTrangSans()
        {
            return _db.TinhTrangSans.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void RemoveSan(San san)
        {
            throw new NotImplementedException();
        }
    }
}
public class SanController : Controller
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

        ViewBag.Loai_San = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.LoaiSan);
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
        ViewBag.Loai_San = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.LoaiSan);
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
        ViewBag.loai_san = new SelectList(db.LoaiSans, "Loai_San", "moTa", san.LoaiSan);
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
    ////adưujaldưkaldkưl
}