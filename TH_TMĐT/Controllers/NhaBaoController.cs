using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TH_TMĐT.Models;

namespace TH_TMĐT.Controllers
{
    public class NhaBaoController : Controller
    {
        private TMDT_THEntities1 db = new TMDT_THEntities1();

        // GET: NhaBao
        public ActionResult Index()
        {
            var baiBao = db.BaiBaos.Include(b => b.LoaiBaiBao).Include(b => b.TinhTrang);
            return View(baiBao.ToList());
        }

        // GET: NhaBao/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            return View(baiBao);
        }

        // GET: NhaBao/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiBaiBaos, "MaLoai", "TenLoai");
            ViewBag.MaTT = new SelectList(db.TinhTrangs, "MaTT", "TenTinhTrang");
            return View();
        }

        // POST: NhaBao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( BaiBao baiBao)
        {
            if (ModelState.IsValid)
            {
        
                baiBao.NgayDang = DateTime.Now;
                baiBao.NgaySua = DateTime.Now;
                var file = Request.Files["Image"];
                if (file != null && file.ContentLength > 0)
                {
                    // Lưu hình ảnh vào thư mục trên máy chủ hoặc lưu vào cơ sở dữ liệu
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Public/images/post"), fileName);
                    file.SaveAs(path);

                    // Lưu đường dẫn hình ảnh vào đối tượng BaiBao
                    baiBao.HinhAnh = "~/Public/images/post" + fileName;
                }
                db.BaiBaos.Add(baiBao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiBaiBaos, "MaLoai", "TenLoai", baiBao.MaLoai);
            ViewBag.MaTT = new SelectList(db.TinhTrangs, "MaTT", "TenTinhTrang", baiBao.MaTT);
            return View(baiBao);
        }

        // GET: NhaBao/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiBaiBaos, "MaLoai", "TenLoai", baiBao.MaLoai);
            ViewBag.MaTT = new SelectList(db.TinhTrangs, "MaTT", "TenTinhTrang", baiBao.MaTT);
            return View(baiBao);
        }

        // POST: NhaBao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BaiBao baiBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiBaiBaos, "MaLoai", "TenLoai", baiBao.MaLoai);
            ViewBag.MaTT = new SelectList(db.TinhTrangs, "MaTT", "TenTinhTrang", baiBao.MaTT);
            return View(baiBao);
        }

        // GET: NhaBao/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            return View(baiBao);
        }

        // POST: NhaBao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BaiBao baiBao = db.BaiBaos.Find(id);
            db.BaiBaos.Remove(baiBao);
            db.SaveChanges();
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
    }
}
