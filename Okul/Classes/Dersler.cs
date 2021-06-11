using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okul
{
    public class Dersler : IDersler, IDersNo
    {
        int IDersNo.DersNo { get; set; }
        string IDersler.DersAdi { get; set; }
        string IDersler.Aciklama { get; set; }
        string IDersler.OgretimGorevlisi { get; set; }

        //<-------------------------Dersler Ekleme Kısmı--------------------------->
        public void DersEkle()
        {
            IDersNo ders_no = new Dersler();
            IDersler dersler = new Dersler();

            //Burada hazır veri kullandım.

            ders_no.DersNo = 124;
            dersler.DersAdi = ".Net Programlama";
            dersler.Aciklama = "Nesne tabanlı programlamanın temelini atarak c# dilinin temellerini ogretmeyi amaclar.";
            dersler.OgretimGorevlisi = "Emrah Saricicek";

            JArray dersekle = new JArray(       //Verileri jsonda array'e kaydettim
                   new JObject(
                            new JProperty("DersNo", ders_no.DersNo),
                            new JProperty("DersAdi", dersler.DersAdi),
                            new JProperty("Aciklama", dersler.Aciklama),
                            new JProperty("OgretimGorevlisi", dersler.OgretimGorevlisi)));

            //Json dosyasi olusturup olusturdugum dosyaya arraydeki verileri kaydediyorum.

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json", dersekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                dersekle.WriteTo(yazdir);
            }
        }

        //<---------------------------Ogrenci Ders Silme Kısmı---------------------------->

        public void DersSil()
        {
            //Json dosyasini okutup yeni bir array'e kaydediyorum daha sonra bu array'in ilgili elemanını silip tekrardan dosyaya yazdırıyorum.

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json");
            JArray ders_sil = new JArray(json);
            JToken eleman = ders_sil[0];
            ders_sil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ders_sil.WriteTo(yazdir);
            }
        }
        //<---------------------------Ogrenci Ders Guncelleme Kısmı---------------------------->
        public void DersGuncelle()
        {
            //Olusturdugumuz json dosyasını bir array'e kaydediyorum
            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json");
            JArray dersler = new JArray(json);

            IDersNo yeni_dersno = new Dersler();
            IDersler yeni_dersler = new Dersler();

            //Burada hazır veri kullandım.

            yeni_dersno.DersNo = 102;
            yeni_dersler.DersAdi = "Inkılap Tarihi";
            yeni_dersler.Aciklama = "Atatürk inkılaplarını ve ulkemizin tarihini ogretmeyi amaclar";
            yeni_dersler.OgretimGorevlisi = "Mesut Mutlu";

            JArray derslerguncelle = new JArray(    //Verileri jsonda array'e kaydettim
                       new JObject(
                            new JProperty("DersNo", yeni_dersno.DersNo),
                            new JProperty("DersAdi", yeni_dersler.DersAdi),
                            new JProperty("Aciklama", yeni_dersler.Aciklama),
                            new JProperty("OgretimGorevlisi", yeni_dersler.OgretimGorevlisi)));


            JToken eleman = dersler[0];
            eleman.Replace(derslerguncelle[0]); //Yeni verileri okutup array'e atadıgımız json dosyasındaki arrayle degistiriyorum

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Dersler.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                dersler.WriteTo(yazdir);  //Bu islemi dosyaya yazdırıyorum
            }
        }
    }
}
