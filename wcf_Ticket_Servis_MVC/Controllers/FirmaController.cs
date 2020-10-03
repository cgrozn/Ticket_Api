using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wcf_Ticket_Servis_MVC.Models;
using wcf_Ticket_Servis_MVC.ViewModel;

namespace wcf_Ticket_Servis_MVC.Controllers
{

    public class FirmaController : Controller
    {
        public string token;
        // GET: Firma
        public ActionResult Index()
        {
           token = Session["Token"].ToString();

            FirmaServisClient fir = new FirmaServisClient();
            ViewBag.listeFirmaSirket = fir.FirmaListeSirket(token);
            ViewBag.listeFirmaMusteri = fir.FirmaListeMusteri(token);
            return View();
        }

        [HttpGet]
        public ActionResult FirmaEkle()
        {
            return View("FirmaEkle");
        }


        [HttpPost]
        public ActionResult FirmaEkle(FirmaViewModel firmaView,string token)
        {
            token = Session["Token"].ToString();
            FirmaServisClient fir = new FirmaServisClient();

            fir.FirmaEkle(firmaView.firma,token);

            return RedirectToAction("Index", "Firma");
        }

    
        public ActionResult FirmaDelete(string id,string token)
        {
            token = Session["Token"].ToString();

            FirmaServisClient fsc = new FirmaServisClient();

             fsc.FirmaDelete(fsc.FirmaBul(id),token);

            return RedirectToAction("Index","Firma");
        }

        [HttpGet]
        public ActionResult FirmaUpdate(string id)
        {
           
            FirmaServisClient fsc = new FirmaServisClient();
            FirmaViewModel fvm = new FirmaViewModel();
            fvm.firma = fsc.FirmaBul(id);



            return View("FirmaUpdate", fvm);

           
        }
        [HttpPost]
        public ActionResult FirmaUpdate(FirmaViewModel firmaView,string token)
        {
            token = Session["Token"].ToString();

            FirmaServisClient fsc = new FirmaServisClient();
            fsc.FirmaUpdate(firmaView.firma,token);
            return RedirectToAction("Index");
        }


    }
}