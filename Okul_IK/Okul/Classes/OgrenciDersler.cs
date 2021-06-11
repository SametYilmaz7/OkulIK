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
    public class OgrenciDersler : IDersNo, IOgrenciNo
    {
        int IDersNo.DersNo { get; set; }
        int IOgrenciNo.OgrenciNo { get; set; }

        //<---------------------------Ogrenci Ders Ekleme Kısmı---------------------------->
        public void OgrenciDersEkle()
        {
            IDersNo ders_no = new OgrenciDersler();
            IOgrenciNo ogrenci_no = new OgrenciDersler();
            
            //Burada hazır veri kullandım.

            ders_no.DersNo = 124;
            ogrenci_no.OgrenciNo = 6590;

            JArray ogrencidersekle = new JArray(  //Verileri jsonda array'e kaydettim
                  new JObject(
                           new JProperty("DersNo", ders_no.DersNo),
                           new JProperty("OgrenciNo", ogrenci_no.OgrenciNo)));

            //Json dosyasi olusturup olusturdugum dosyaya arraydeki verileri kaydediyorum.

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrenciDersler.json", ogrencidersekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrenciDersler.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogrencidersekle.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nDers Ekleme islemi basarili.");
        }

        //<---------------------------Ogrenci Ders Silme Kısmı---------------------------->
        public void OgrenciDersSil()
        {
            //Json dosyasini okutup yeni bir array'e kaydediyorum daha sonra bu array'in ilgili elemanını silip tekrardan dosyaya yazdırıyorum.
            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrenciDersler.json");
            JArray ogrenciders_sil = new JArray(json);
            JToken eleman = ogrenciders_sil[0];
            ogrenciders_sil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrenciDersler.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogrenciders_sil.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nDers Silme islemi basarili.");
        }
    }
}
