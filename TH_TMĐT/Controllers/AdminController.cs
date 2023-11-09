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
    public class AdminController : Controller
    {
        private TMDT_THEntities1 db = new TMDT_THEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewsManagement()
        {
            var baiBao = db.BaiBaos.Include(b => b.LoaiBaiBao).Include(b => b.TinhTrang);
            return View(baiBao.ToList());
        }
    }
}