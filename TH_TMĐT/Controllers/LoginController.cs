using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TH_TMĐT.Models;

namespace TH_TMĐT.Controllers
{
    public class LoginController : Controller
    {
        TMDT_THEntities1 db = new TMDT_THEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TaiKhoan objUser)
        {
            if (ModelState.IsValid)
            {
                //if (model.IsValid(model))
                //{
                    var user = db.TaiKhoans.Where(u => u.TenDN.Equals(objUser.TenDN) && u.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                    if (user != null)
                    {
                        // Kiểm tra MaQuyen để chia controller cho từng loại người dùng
                        if (user.MaQuyen == "CB")
                        {
                            // Người dùng có quyền CB (Admin)
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (user.MaQuyen == "BT")
                        {
                            // Người dùng có quyền BT (Staff)
                            return RedirectToAction("Index", "Staff");
                        }
                        else if (user.MaQuyen == "KH")
                        {
                            // Người dùng có quyền KH (Customer)
                            return RedirectToAction("Index", "Customer");
                        }
                    }
                    else
                    {
                        // Đăng nhập không thành công
                        ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                //}

            }
            return View(objUser);
        }

    }
}