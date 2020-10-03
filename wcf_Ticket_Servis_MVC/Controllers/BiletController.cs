using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wcf_Ticket_Servis_MVC.Models;
using wcf_Ticket_Servis_MVC.ViewModel;

namespace wcf_Ticket_Servis_MVC.Controllers
{

    public class BiletController : Controller
    {
        // GET: Bilet

        

        public ActionResult Index()
        {
            var token = Session["Token"].ToString();
           
            
            BiletServicClient bi = new BiletServicClient();
            ViewBag.listeBiletPersonel = bi.BiletListeSirket(token);
            ViewBag.listeBiletMusteri = bi.BiletListeMusteri(token);
           

            return View();
        }
        [HttpGet]
        public ActionResult BiletEkle()
        {


            return View("BiletEkle");
        }

        [HttpPost]
        public ActionResult BiletEkle(BiletViewModel biletView,string token)
        {

            token = Session["Token"].ToString();

            BiletServicClient bsc = new BiletServicClient();

            bsc.BiletEkle(biletView.Bilet,token);


            return RedirectToAction("Index","Bilet");
        }

        public ActionResult BiletDelete(string id,string token)

        {
            token = Session["Token"].ToString();

            BiletServicClient bsc = new BiletServicClient();
            bsc.BiletDelete(bsc.BiletBul(id),token);

            return RedirectToAction("Index", "Bilet");
        }
        [HttpGet]
        public ActionResult BiletUpdate(string id)
        {
            BiletServicClient bsc = new BiletServicClient();
            BiletViewModel bvm = new BiletViewModel();
            bvm.Bilet = bsc.BiletBul(id);

           return View("BiletUpdate",bvm);
        }
      
        public ActionResult BiletUpdate(BiletViewModel biletView,string token)
        {
            token = Session["Token"].ToString();

            BiletServicClient bsc = new BiletServicClient();
            bsc.BiletUpdate(biletView.Bilet,token);


            return RedirectToAction("Index", "Bilet");

        }

    }
}