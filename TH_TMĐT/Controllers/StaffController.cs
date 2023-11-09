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
    public class StaffController : Controller
    {
        private TMDT_THEntities1 db = new TMDT_THEntities1();
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ComposeNews() {
            ViewBag.MaLoai = new SelectList(db.LoaiBaiBaos, "MaLoai", "TenLoai");
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComposeNews(BaiBao baiBao)
        {
            if (ModelState.IsValid)
            {
                baiBao.NgayDang = DateTime.Now;
                baiBao.NgaySua = DateTime.Now;
                baiBao.MaTT = "TT2";
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
            return View(baiBao);
        }

        public ActionResult NewsManagement()
        {
            var baiBao = db.BaiBaos.Include(b => b.LoaiBaiBao).Include(b => b.TinhTrang);
            return View(baiBao.ToList());
        }
    }
}