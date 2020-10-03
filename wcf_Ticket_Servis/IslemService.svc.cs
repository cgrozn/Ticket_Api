using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using wcf_Ticket_Servis.Model;
//using wcf_Ticket_Servis.Entity;
using System.Data.Entity;
using System.IdentityModel.Tokens;
using System.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using System.Data.SqlClient;
using wcf_Ticket_Servis.Entity;

namespace wcf_Ticket_Servis
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IslemService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select IslemService.svc or IslemService.svc.cs at the Solution Explorer and start debugging.
    public class IslemService : IIslemService
    {
        
        TicketApp2020Entities db = new TicketApp2020Entities();
        //TicketEntitiesDB db = new TicketEntitiesDB();
      //TicketEntities db = new TicketEntities();

        private string secureToken;

        public string tokentut;

        public ResponseUserData Kontrol()

        {
            HataDenetimi hata = new HataDenetimi();
            
            ResponseUserData ret = new ResponseUserData();
            //TokenUser
            //ID
            //UserName
            //FirmaID
            //IsAdmin r
            
            


            string tmpToken = HttpContext.Current.Request.Headers["Token"];

            if (tmpToken != null && tmpToken != "")
            {
                var handler = new JwtSecurityTokenHandler();


                var token = handler.ReadJwtToken(tmpToken);

                ret.FirmaID = Convert.ToInt32(token.Payload["FirmaID"]);
                ret.UserID = Convert.ToInt32(token.Payload["ID"]);
                ret.IsAdmin = Convert.ToBoolean(token.Payload["IsAdmin"]);
                ret.TipNo = Convert.ToInt32(token.Payload["TipNo"]);

            
                return ret;
            }
            else
            {
                return null;
            }

        }
        

      //  public ResponseUserData Login(UserLoginData data)
        public ResponseUserData Login(Personel data)

        {

            //  private string secureToken;

           // var bul = db.TblPersonel.FirstOrDefault(x => x.Ad == data.Username && x.Password == data.Password);
            var bul = db.TblPersonel.FirstOrDefault(x => x.Ad == data.Ad && x.Password == data.Password);

           


           // if(bul.ID)

           

            if (bul != null)
            {
               


               // secureToken = GetJwt(data.Ad, bul.ID, bul.FirmaID.Value, bul.IsAdmin.Value, bul.TipNo.Value);
                secureToken = GetJwt(data.Ad, bul.ID, bul.FirmaID.Value, bul.IsAdmin.Value, bul.TipNo.Value);
               
                tokentut = secureToken;

                TblPersonel bil = db.TblPersonel.Single(pe => pe.ID == bul.ID);
              if (bil != null)
                {
                    bil.TokenTut = tokentut;
                   db.TblPersonel.Add(bil);
                    db.Entry(bil).State = EntityState.Modified;
                   db.SaveChanges();
               }
               




                var response = new ResponseUserData
                {

                    UserID = bul.ID,
                  //  UserName = data.Username,
                   UserName = data.Ad,
                    Authenticated = true,
                    Token = secureToken,
                    IsAdmin = bul.IsAdmin.Value,
                    FirmaID = bul.FirmaID.Value,
                    TipNo = bul.TipNo.Value



                };

                return response;

            }

            else
            {
                OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return null;
            }

        }
     
        private byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+');
            s = s.Replace('_', '/');
            switch (s.Length % 4)
            {
                case 0: break;
                case 2: s += "=="; break;
                case 3: s += "="; break;
                default:
                    throw new System.Exception(
                "Illegal base64url string!");
            }
            return Convert.FromBase64String(s);
        }

        private long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        //  public string GetJwt(string user, string pass)
        public string GetJwt(string prmUserName, int prmID, int prmFirmaID, bool prmIsAdmin, int prmTipNo)
        {
            byte[] secretKey = Base64UrlDecode("hi");
            DateTime issued = DateTime.Now;


            var User = new Dictionary<string, object>()
                    {


                        {"UserName", prmUserName},

                        { "ID", prmID },
                  {"FirmaID", prmFirmaID},
                {"IsAdmin",prmIsAdmin},
                {"TipNo",prmTipNo},

                         {"iat", ToUnixTime(issued).ToString()}
                    };

            string token = JWT.Encode(User, secretKey, JwsAlgorithm.HS256);

            return token;
        }

        public bool FirmaEkle(Firma firma)
        {
            ResponseUserData tmp = Kontrol();
            if (tmp != null==tmp.IsAdmin==true)
            {
                try
                   
                {
                   
                    TblFirma fir = new TblFirma();
                    fir.Ad = firma.Ad;
                    fir.Adres = firma.Adres;
                    fir.Telefon = firma.Telefon;
                    fir.Telefon2 = firma.Telefon2;
                    fir.VergiDaire = firma.VergiDaire;
                    fir.VergiNumara = firma.VergiNumara;
                    fir.WebSite = firma.WebSite;
                    fir.FirmaID = tmp.FirmaID;
                    fir.Mail = firma.Mail;
                    fir.TipNo = 2;
                    fir.IsSilindi = false;
                    db.TblFirma.Add(fir);
                    db.SaveChanges();
                    return true;

                }
                catch
                {
                    return false;
                }
                

            }

            else
            {
                return false;
            }
        }

        public Firma FirmaBul(string id)
        {
            int nid = Convert.ToInt32(id);

            return db.TblFirma.Where(p => p.ID == nid).Select(pe => new Firma
            {

                ID = pe.ID,
                Ad = pe.Ad,
                Adres = pe.Adres,
                Telefon = pe.Telefon,
                Telefon2 = pe.Telefon2,
                Mail = pe.Mail,
                VergiNumara = pe.VergiNumara,
                VergiDaire = pe.VergiDaire,
                WebSite = pe.WebSite
            }).First();
        }

        public bool FirmaUpdate(Firma firma)
        { ResponseUserData tmp = Kontrol();
            if (tmp != null == tmp.IsAdmin == true)
            {

                try
                {
                    int id = Convert.ToInt32(firma.ID);
                    TblFirma fir = db.TblFirma.Single(pe => pe.ID == id);

                    fir.Ad = firma.Ad;
                    fir.Adres = firma.Adres;
                    fir.Telefon = firma.Telefon;
                    fir.Telefon2 = firma.Telefon2;
                    fir.VergiDaire = firma.VergiDaire;
                    fir.VergiNumara = firma.VergiNumara;
                    fir.WebSite = firma.WebSite;
                    fir.Mail = firma.Mail;
                    db.TblFirma.Add(fir);
                    db.Entry(fir).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;

                }
                catch
                {
                    return false;
                }

               
            }
            else
            {
                return false;
            }

        }

        public bool FirmaDelete(Firma firma)
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {

                try
                {
                    int id = Convert.ToInt32(firma.ID);
                    TblFirma fir = db.TblFirma.Single(pe => pe.ID == id);
                    fir.IsSilindi = true;
                    db.TblFirma.Add(fir);
                    db.Entry(fir).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;


                }
            }
            else
            {
                return false;
            }

           
        }




        public List<Firma> FirmaMusteri()
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {
                var bul = (from firma in db.TblFirma
                           where tmp.FirmaID == firma.FirmaID &&firma.IsSilindi==false
                           select new Firma
                           {
                               ID = firma.ID,
                               Ad=firma.Ad,
                               Adres=firma.Adres,
                               Telefon=firma.Telefon,
                               Telefon2=firma.Telefon2,
                               VergiDaire=firma.VergiDaire,
                               VergiNumara=firma.VergiNumara,
                               WebSite=firma.WebSite,
                               Mail=firma.Mail,
                               FirmaID=firma.FirmaID.Value
                               
                               
                           }).ToList();
                return bul;
            }
            else
            {
                return new List<Firma>();
            }
            //return db.TblFirma.Where(p => p.IsSilindi == false).Select(pe => new Firma
            //{
            //    ID = pe.ID,
            //    Ad = pe.Ad,
            //    Adres = pe.Adres,
            //    Telefon = pe.Telefon,
            //    Telefon2 = pe.Telefon2,
            //    VergiDaire = pe.VergiDaire,
            //    VergiNumara = pe.VergiNumara,
            //    WebSite = pe.WebSite,
            //    Mail = pe.Mail
            //     TipNo = pe.TipNo.Value



            //}).ToList();

        }

        public bool PersonelEkle(Personel personel)
        {

            ResponseUserData tmp = Kontrol();
            if (tmp != null)
            {
                try
                {
                    TblPersonel per = new TblPersonel();

                    per.Ad = personel.Ad;
                    per.Soyad = personel.Soyad;
                    per.Departman = personel.Departman;
                    per.Email = personel.Email;
                    per.Telefon = personel.Telefon;
                    per.Telefon2 = personel.Telefon2;
                    per.Adres = personel.Adres;
                    per.WebSite = personel.WebSite;
                    per.Password = personel.Password;
                    per.FirmaID = tmp.FirmaID;
                    per.TipNo = tmp.TipNo;
                    per.IsSilindi = false;
                    per.IsAdmin = false;
                    db.TblPersonel.Add(per);
                    db.SaveChanges();
                    return true;

                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

          

        }

        public bool PersonelUpdate(Personel personel)
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {
                try
                {
                    int id = Convert.ToInt32(personel.ID);
                    TblPersonel per = db.TblPersonel.Single(pe => pe.ID == id);

                    per.Ad = personel.Ad;
                    per.Soyad = personel.Soyad;
                    per.Departman = personel.Departman;
                    per.Email = personel.Email;
                    
                    per.Telefon = personel.Telefon;
                    per.Telefon2 = personel.Telefon2;

                    per.Adres = personel.Adres;
                    per.WebSite = personel.WebSite;

                   
                    db.TblPersonel.Add(per);
                  db.Entry(per).State = EntityState.Modified;
                    //  db.TblFirma.Add(fir);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
      
        }

        public bool PersonelDelete(Personel personel)
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {
                try
                {
                    int id = Convert.ToInt32(personel.ID);
                    TblPersonel per = db.TblPersonel.Single(pe => pe.ID == id);

                    per.IsSilindi = true;
                    db.TblPersonel.Add(per);
                   db.Entry(per).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;


                }
            }
            else
            {
                return false;
            }
        }

        public Personel PersonelBul(string id)
        {
            int nid = Convert.ToInt32(id);

            return db.TblPersonel.Where(p => p.ID == nid).Select(pe => new Personel
            {
                ID = pe.ID,
                Ad = pe.Ad,
                Soyad = pe.Soyad,
                Departman = pe.Departman,
                Email = pe.Email,
                Telefon = pe.Telefon,
                Telefon2 = pe.Telefon2,
                Adres = pe.Adres,
                WebSite = pe.WebSite,
                FirmaID = pe.FirmaID.Value

            }).First();
        }




        public List<Personel> PersonelListeSirket()
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {

                var bul = (from personel in db.TblPersonel
                           join firma in db.TblFirma on personel.FirmaID equals firma.ID
                           where tmp.FirmaID == firma.ID &&personel.IsSilindi==false
                           select new Personel {
                               ID=personel.ID,
                               Ad=personel.Ad,
                               Soyad=personel.Soyad,
                               Departman=personel.Departman,
                               Email=personel.Email,
                               Telefon=personel.Telefon,
                               Telefon2=personel.Telefon2,
                               Adres=personel.Adres,
                               WebSite=personel.WebSite,
                               FirmaID=personel.FirmaID.Value


                           }).ToList();

                return bul;

            }
            else
            {
                return null;
            }
            //return db.TblPersonel.Where(pe => pe.TipNo == 6 && pe.IsSilindi == false && tmp.FirmaID == pe.FirmaID).Select(pe => new Personel
            //{
            //    ID = pe.ID,
            //    Ad = pe.Ad,
            //    Soyad = pe.Soyad,
            //    Departman = pe.Departman,
            //    Email = pe.Email,
            //    Telefon = pe.Telefon,
            //    Telefon2 = pe.Telefon2,
            //    Adres = pe.Adres,
            //    WebSite = pe.WebSite,
            //    FirmaID = pe.FirmaID.Value


            //}).ToList();

        }

        public List<Personel> PersonelListeMusteri()
        {

            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            {

                var bul = (from personel in db.TblPersonel
                           join firma in db.TblFirma on personel.FirmaID equals firma.ID
                           where tmp.FirmaID == firma.ID &&personel.IsSilindi==false
                           select new Personel
                           {
                               ID = personel.ID,
                               Ad = personel.Ad,
                               Soyad = personel.Soyad,
                               Departman = personel.Departman,
                               Email = personel.Email,
                               Telefon = personel.Telefon,
                               Telefon2 = personel.Telefon2,
                               Adres = personel.Adres,
                               WebSite = personel.WebSite,
                               FirmaID = personel.FirmaID.Value



                           }).ToList();

                return bul;

            }
            else
            {
                return null;
            }
            //return db.TblPersonel.Where(pe => pe.TipNo == 7 && pe.IsSilindi == false).Select(pe => new Personel
            //{
            //    ID = pe.ID,

            //    Ad = pe.Ad,
            //    Soyad = pe.Soyad,
            //    Departman = pe.Departman,
            //    Email = pe.Email,
            //    Telefon = pe.Telefon,
            //    Telefon2 = pe.Telefon2,
            //    Adres = pe.Adres,
            //    WebSite = pe.WebSite

            //}).ToList();
        }


        public bool DosyaUpdate(Dosya dosya)
        {
            try
            {
                int id = Convert.ToInt32(dosya.ID);
                TblDosya dos = db.TblDosya.Single(pe => pe.ID == id);

                dos.Ad = dosya.Ad;
                dos.OlusTarih = Convert.ToDateTime(dosya.OlusTarihi);
                dos.Path = dosya.Path;
                dos.Sira = Convert.ToInt32(dosya.Sira);
                dos.TipNo = Convert.ToInt32(dosya.TipID);

                db.TblDosya.Add(dos);
              db.Entry(dos).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DosyaEkle(Dosya dosya)
        {
            try
            {
                TblDosya dos = new TblDosya();

                dos.Ad = dosya.Ad;
                dos.OlusTarih = Convert.ToDateTime(dosya.OlusTarihi);
                dos.Path = dosya.Path;
                dos.Sira = Convert.ToInt32(dosya.Sira);
              //  dos.TipID = Convert.ToInt32(dosya.TipID);
                db.TblDosya.Add(dos);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }

        public bool DosyaDelete(Dosya dosya)
        {
            try
            {
                int id = Convert.ToInt32(dosya.ID);
                TblDosya dos = db.TblDosya.Single(pe => pe.ID == id);
                db.TblDosya.Remove(dos);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;


            }

        }

        public Dosya DosyaBul(string id)
        {
            int nid = Convert.ToInt32(id);

            return db.TblDosya.Where(dosy => dosy.ID == nid).Select(dosya => new Dosya
            {
                ID = dosya.ID,
                Ad = dosya.Ad,
                OlusTarihi = dosya.OlusTarih.ToString(),
                Path = dosya.Path,
                Sira = dosya.Sira.ToString(),
              //  TipID = dosya.TipID.ToString()


            }).First();
        }

        public List<Dosya> DosyaListe()
        {
            return db.TblDosya.Where(pe => pe.TipNo == 2 || pe.IsSilindi == false).Select(pe => new Dosya
            {
                ID = pe.ID,
                Ad = pe.Ad,
                OlusTarihi = pe.OlusTarih.ToString(),
                Path = pe.Path,
                Sira = pe.Sira.ToString(),
               // TipID = pe.TipID.ToString()

            }).ToList();
        }





        public List<Bilet> BiletSirket()
        {

            ResponseUserData tmp = Kontrol();
            if (tmp != null)
            {
                var bul = (from bilet in db.TblBilet
                           join personel in db.TblPersonel on bilet.PersonelID equals personel.ID
                           join firma in db.TblFirma on personel.FirmaID equals firma.ID
                           where firma.ID == personel.FirmaID && bilet.IsSilindi == false && bilet.TipNo == tmp.TipNo
                           select new Bilet
                           {

                               ID = bilet.ID,
                               Baslik = bilet.Baslik,
                               Konu = bilet.Konu,
                               Notlar = bilet.Notlar,
                               PersonelID = bilet.PersonelID.Value
                          //     OlusTarih = bilet.OlusTarih.Value.Date
           
                          //    TipNo=bilet.TipNo.Value

                           }).ToList();

                return bul;
            }
            else
            {
                return null;
            }




                //return bul;
                //TblFirma fir = new TblFirma();
                //return db.TblBilet.Where(p => p.TipNo == 6 && p.IsSilindi == false && p.PersonelID == fir.FirmaID).Select(pe => new Bilet
                //return db.TblBilet.Where(p => p.TipNo == 6 && p.IsSilindi == false).Select(pe => new Bilet
                //  return db.TblBilet.Where(p => p.TipNo == 6 && p.IsSilindi == false).Select(pe => new Bilet
                //  {
                //      ID = pe.ID,
                //      Baslik = pe.Baslik,
                //      Konu = pe.Konu,
                //      Notlar = pe.Notlar,
                //      PersonelID = pe.PersonelID.Value
                //       OlusTarih = pe.OlusTarih.Value.ToString()
                //       OlusTarih = Convert.ToDateTime(pe.OlusTarih)
                //    BitisTarihi = Convert.ToDateTime(pe.BitisTarihi)

                //  }).ToList();
            }

        public List<Bilet> BiletMusteri()
        {

            ResponseUserData tmp = Kontrol();
            if (tmp != null)
            {
            

                var bul = (from bilet in db.TblBilet
                           join personel in db.TblPersonel on bilet.PersonelID equals personel.ID
                           join firma in db.TblFirma on personel.FirmaID equals firma.ID


                           where tmp.FirmaID== firma.FirmaID && bilet.IsSilindi == false&&bilet.TipNo==7
                           select new Bilet
                           {

                               ID = bilet.ID,
                               Baslik = bilet.Baslik,
                               Konu = bilet.Konu,
                               Notlar = bilet.Notlar,
                               PersonelID = bilet.PersonelID.Value
                             ///  OlusTarih = bilet.OlusTarih.Value

                               //  TipNo=bilet.TipNo.Value

                           }).ToList();
                return bul;
            }

            else
            {
                return null;
            }

        }
    

        public bool BiletEkle(Bilet bilet)
        {

            ResponseUserData tmp = Kontrol();


            if (tmp != null)
            {

                //   var bul=db.TblPersonel.Where(p=>p.TipNo==tmp.TipNo)
                TblBilet bil = new TblBilet();

                bil.Baslik = bilet.Baslik;
                bil.Konu = bilet.Konu;
                bil.Notlar = bilet.Notlar;
                bil.PersonelID = tmp.UserID;
                bil.IsSilindi = false;
                bil.TipNo = tmp.TipNo;
                bil.OlusTarih = DateTime.Now;
                db.TblBilet.Add(bil);
                db.SaveChanges();
                return true;


            }

            return false;




        }

        public Bilet BiletBul(string id)
        {
            int nid = Convert.ToInt32(id);


            if (id != null)
            {
                var bul = (from bilet in db.TblBilet
                           where bilet.ID == nid
                           select new Bilet
                           {
                               ID = bilet.ID,
                               Baslik = bilet.Baslik,
                               Konu = bilet.Konu,
                               Notlar = bilet.Notlar,
                               PersonelID = bilet.PersonelID.Value,
                               TipNo = bilet.TipNo.Value
                            //   OlusTarih = bilet.OlusTarih.Value.Date

                           }).First();
                return bul;
            }
            else
            {
                return null;
            }
           
                //return db.TblBilet.Where(bi => bi.ID == nid).Select(bilet => new Bilet
                //{

                //    ID = bilet.ID,
                //    Baslik = bilet.Baslik,
                //    Konu = bilet.Konu,
                //    Notlar = bilet.Notlar,
                //    PersonelID=bilet.PersonelID.Value,
                //    OlusTarih = bilet.OlusTarih.Value.ToString()

                //    //  convert(OlusTarih)=Convert.ToString(bilet.OlusTarih.ToString())
                //    // OlusTarih=(bilet.OlusTarih.Value.Date.t
                //}).First();

               
            
                 }

        public bool BiletDelete(Bilet bilet)
        {
            ResponseUserData tmp = Kontrol();

            if (tmp != null)
            //if (tmp != null&&tmp.IsAdmin==true)
            {
                try
                {
                    int id = Convert.ToInt32(bilet.ID);
                    //  TblBilet bil = db.TblBilet.Single(pe => pe.ID == id);

                    var sil = (from x in db.TblBilet where x.ID == id select x).FirstOrDefault();

                    sil.IsSilindi = true;


                    db.TblBilet.Add(sil);
                   db.Entry(sil).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;


                }

            }
            else
            {
                return false;
            }

        }

        public bool BiletUpdate(Bilet bilet)
        {
            ResponseUserData tmp = Kontrol();
            int id = Convert.ToInt32(bilet.ID);

            if (tmp != null)
            {
                //var bak = (from bil in db.TblBilet
                //           join personel in db.TblPersonel on bil.PersonelID equals personel.ID
                //           where bil.PersonelID == personel.ID
                //           select bil).First();


                //if (bak != null)
                //{
                try
                {
                    TblBilet bil = db.TblBilet.Single(pe => pe.ID == id);

                    bil.Baslik = bilet.Baslik;
                    bil.Konu = bilet.Konu;
                    bil.Notlar = bilet.Notlar;
                    bil.PersonelID = bilet.PersonelID;
                   
                    db.TblBilet.Add(bil);
                    db.Entry(bil).State = EntityState.Modified;
                    db.SaveChanges();

                    //  return true;

                    return true;

                }
                catch
                {
                    return false;
                }
                //}

                //return true;
            }



            else
            {
                return false;

            }


        }

        public List<Bilet> BiletGetir()
        {
           
            return null;
        }

        public bool KayitOl(Personel personel)
        {
            TblPersonel per = new TblPersonel();
            try
            {
                per.Email = personel.Email;
                per.Password = personel.Password;
                per.Telefon = personel.Telefon;
                per.Ad = personel.Ad;
                per.Soyad = personel.Soyad;
                per.TipNo = 7;
                per.FirmaID = null;
                per.IsAdmin = true;
                per.DurumNo = 1;

                db.TblPersonel.Add(per);
                db.SaveChanges();

              //    per.WebSite=per



                return true;
            }
           
                catch
                {
                    return false;
                }
        }




    }
}

    


