using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using wcf_Ticket_Servis.Model;


namespace wcf_Ticket_Servis
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIslemService" in both code and config file together.
    [ServiceContract]
    public interface IIslemService
    {
        
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate ="Login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)] 
        //ResponseUserData Login(UserLoginData data);
        ResponseUserData Login(Personel data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Kontrol", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResponseUserData Kontrol();

      

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "BiletBul/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Bilet BiletBul(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "BiletSirket", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Bilet> BiletSirket();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "BiletMusteri", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Bilet> BiletMusteri();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "BiletEkle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BiletEkle(Bilet bilet);

        
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "BiletDelete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
         bool BiletDelete(Bilet bilet);
    
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "BiletUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BiletUpdate(Bilet bilet);


        

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "FirmaMusteri", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Firma> FirmaMusteri();


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate ="FirmaBul/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
         Firma FirmaBul(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate ="FirmaUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool FirmaUpdate(Firma firma);
        [OperationContract]

        [WebInvoke(Method = "DELETE",UriTemplate ="FirmaDelete",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool FirmaDelete(Firma firma);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "KayitOl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool KayitOl(Personel personel);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate ="FirmaEkle",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool FirmaEkle(Firma firma);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PersonelListeSirket", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Personel> PersonelListeSirket();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PersonelListeMusteri", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Personel> PersonelListeMusteri();


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PersonelEkle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool PersonelEkle(Personel personel);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PersonelUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool PersonelUpdate(Personel personel);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "PersonelDelete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool PersonelDelete(Personel personel);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PersonelBul/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Personel PersonelBul(string id);

      

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "DosyaUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DosyaUpdate(Dosya dosya);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DosyaEkle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DosyaEkle(Dosya dosya);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "DosyaDelete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool DosyaDelete(Dosya dosya);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DosyaBul/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Dosya DosyaBul(string id);
        
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "DosyaListe", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Dosya> DosyaListe();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "BiletGetir", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Bilet> BiletGetir();




    }
}
