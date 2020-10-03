using System.Web;
using System.Web.Mvc;

namespace wcf_Ticket_Servis_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

          
        }
    }
}
