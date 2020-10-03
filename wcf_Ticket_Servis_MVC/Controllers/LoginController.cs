using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using wcf_Ticket_Servis_MVC.Entity;
using System.Data.Entity;
using wcf_Ticket_Servis_MVC.Models;
using wcf_Ticket_Servis_MVC.ViewModel;


namespace wcf_Ticket_Servis_MVC.Controllers
{

    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }


        TicketApp18 db = new TicketApp18();
        TicketApp2020Entities2 db2 = new TicketApp2020Entities2();

        [AllowAnonymous]

        [HttpPost]
        public ActionResult Login(Login login)
                   {

          
            LoginServisClient log = new LoginServisClient();


          
            var gir   = log.Login(login);

            //var bul = db.TblPersonel.FirstOrDefault(x => x.Ad == login.Ad&& x.Password == login.Password);
            var bul = db2.TblPersonel.FirstOrDefault(x => x.Ad == login.Ad && x.Password == login.Password);


            if (gir==true)

            {
             
              Session["Token"] = bul.TokenTut;
                string aa = bul.TokenTut;

                FormsAuthentication.SetAuthCookie(login.Ad.ToString(),false);

                return RedirectToAction("Index", "Home");

            }

            else
            {
                
                    return RedirectToAction("Index");
                
            }

        }
       
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult KayitOl(Personel personel)
        {
            LoginServisClient log = new LoginServisClient();
            log.KayitOl(personel);

            return RedirectToAction("Index", "Login");
        }

    }
}