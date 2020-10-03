using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wcf_Ticket_Servis_MVC.Models;
using wcf_Ticket_Servis_MVC.ViewModel;

namespace wcf_Ticket_Servis_MVC.Controllers
{

    public class PersonelController : Controller
    {

        public string token;

        // GET: Personel
        public ActionResult Index()

        {

            token = Session["Token"].ToString();

            PersonelServisClient psc = new PersonelServisClient();
            ViewBag.listePersonelListeSirket = psc.PersonelListeSirket(token);
            return View();
        }


        public ActionResult PersonelListe()
        {
           // token = Session["Token"].ToString();

        //    PersonelServisClient psc = new PersonelServisClient();
        //    ViewBag.listePersonelListeSirket = psc.PersonelListeSirket(token);

            return View();
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View("PersonelEkle");
        }

      [HttpPost]
        public ActionResult PersonelEkle(PersonelViewModel personelView,string token)
        {
            token = Session["Token"].ToString();

            PersonelServisClient psc = new PersonelServisClient();

            psc.PersonelEkle(personelView.Personel,token);

            return RedirectToAction("Index", "Personel");


        }

        public ActionResult PersonelDelete(string id,string token)
        {
            token = Session["Token"].ToString();

            PersonelServisClient psc = new PersonelServisClient();

            psc.PersonelDelete(psc.PersonelBul(id), token);

            return RedirectToAction("Index", "Personel");
        }
        [HttpGet]
        public ActionResult PersonelUpdate(string id)
        {

            PersonelServisClient psc = new PersonelServisClient();

            PersonelViewModel pvm = new PersonelViewModel();
            pvm.Personel = psc.PersonelBul(id);
               
            return View("PersonelUpdate",pvm);


        }

       [HttpPost]
        public ActionResult PersonelUpdate(PersonelViewModel personelView,string token)
        {
            token = Session["Token"].ToString();
            PersonelServisClient psc = new PersonelServisClient();
            psc.PersonelUpdate(personelView.Personel, token);

            return RedirectToAction("Index", "Personel");

          
        }
       
    }
}