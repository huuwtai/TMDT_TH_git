using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TH_TMĐT.Models;

namespace TH_TMĐT.Controllers
{
    public class KhachHangController : Controller
    {
        private TMDT_THEntities1 db = new TMDT_THEntities1();

        public ActionResult Login()
        {
            return View();
        }

        // GET: KhachHang
        public ActionResult Index()
        {
            var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.TaiKhoan);
            return View(khachHangs.ToList());
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHang/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoai", "TenLoai");
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,MaLoaiKH,HoTen,NgaySinh,DiaChi,NgayThamGia,MaTK")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoai", "TenLoai", khachHang.MaLoaiKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", khachHang.MaTK);
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoai", "TenLoai", khachHang.MaLoaiKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", khachHang.MaTK);
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,MaLoaiKH,HoTen,NgaySinh,DiaChi,NgayThamGia,MaTK")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoai", "TenLoai", khachHang.MaLoaiKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", khachHang.MaTK);
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
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
