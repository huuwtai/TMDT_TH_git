using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using TH_TMĐT.Models;

namespace TH_TMĐT.Bussiness
{
    public class BaiBaoBusiness
    {
        TMDT_THEntities1 db = new TMDT_THEntities1();
        public List<BaiBao> getNewsAll()
        {
            List<BaiBao> listNews = db.BaiBaos.ToList();
            return listNews;
        }
        public BaiBao getNewsByID(string news)
        {
            BaiBao listnews = db.BaiBaos.Where(s => s.MaBao == news).FirstOrDefault();
            return listnews;
        }

      
    }
}