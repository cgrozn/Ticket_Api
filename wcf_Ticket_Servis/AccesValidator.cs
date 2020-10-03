using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using wcf_Ticket_Servis.Entity;

namespace wcf_Ticket_Servis
{
    public class AccesValidator:ServiceAuthorizationManager
    {


        TicketApp2020Entities db = new TicketApp2020Entities();
        //TicketEntities db = new TicketEntities();
  

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
          

        // Authorization header alınır, Base64 string çözümlenir
        var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (authHeader != null && authHeader != string.Empty)
            {
                var svcCredentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');
                var user = new
                {
                    Name = svcCredentials[0],
                    Password = svcCredentials[1]
                };
                var bul = db.TblPersonel.FirstOrDefault(x => x.Ad == user.Name && x.Password == user.Password);

                if ( bul!=null)
                {
                    // başarılı
                    return true;
                }
                else
                {
                    // Authorization header sağlanamadı
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"wcf_Ticket_Servis\"");
                    // HTTP status 401 fırlatılır
                    throw new WebFaultException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                // Authorization header sağlanamadı
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"wcf_Ticket_Servis\"");
                // HTTP status 401 fırlatılır
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
