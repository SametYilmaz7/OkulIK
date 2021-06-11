using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Okul
{
     public class Enumlar
    {
        
        public enum Bolumler
        {
            BilgisayarProgramciligi = 0,
            MobilProgramlama = 1,
            MakineMuhendisligi = 2,
            BilgisayarMuhendisligi = 3,
            ElektrikMuhendisligi = 4,
            InsaatMuhendisligi = 5,
            Felsefe = 6 
            
        }

        public enum Departmanlar
        {
            IdariIsler = 0,
            Muhasebe = 1,
            OgrenciIsleri = 2,
            InsanKaynaklari = 3

        }

        public enum Gorevler
        {   
            //Idari Personel Gorevleri
            Mudur = 0,
            MudurYardimcisi = 1,
            GenelSekreter = 2,
            YoneticiSekreter = 3,
            //Ogrenci Isleri Gorevleri
            OgrenciKayit = 4,
            DersKayit = 5,
            AkademikTakvim = 6,
            YazOkulu = 7,
            KimlikIslemleri = 8,
            //Ogretim Gorevlisi Gorevleri
            BilgisayarOgretmeni = 9,
            MakineOgretmeni = 10,
            ElektrikOgretmeni = 11,
            InsaatOgretmeni = 12,
            FelsefeOgretmeni = 13,
        }
        
    }
}
