using System.Net;
using System.Web.Mvc;
using QLTT.Areas.Facade;
using QLTT.Facade;
using QLTT.Models;

namespace QLTT.Areas.Admin.Controllers.Admin
{
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
