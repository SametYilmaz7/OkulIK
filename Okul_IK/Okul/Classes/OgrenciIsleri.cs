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
    public class OgrenciIsleri : IPersonel, IKisiselBilgiler
    {
        int IPersonel.KimlikNo { get; set; }
        string IKisiselBilgiler.Ad { get; set; }
        string IKisiselBilgiler.Soyad { get; set; }
        DateTime IKisiselBilgiler.Dogumtarihi { get; set; }
        Enumlar.Gorevler IPersonel.Gorevi { get; set; }

        //<----------------------------------------Ogrenci Isleri ekleme kısmı----------------------------------------->
        public void OgrenciIsleriEkle()
        {
            IPersonel Oipersonel = new OgrenciIsleri();
            IKisiselBilgiler Oipersonel_bilgi = new OgrenciIsleri();

            //İlgili alanlari kullaniciya girdirerek doldurdum.

            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            Oipersonel.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (Oipersonel.KimlikNo.ToString().Length != 6) // Bu kısımda girilen ogrencino'nun 6 haneden fazla girilmemesini sağladım 
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                Oipersonel.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            Oipersonel_bilgi.Ad = Console.ReadLine();

            while (Oipersonel_bilgi.Ad == "")  // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                Oipersonel_bilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            Oipersonel_bilgi.Soyad = Console.ReadLine();

            while (Oipersonel_bilgi.Soyad == "")   // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                Oipersonel_bilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            Oipersonel_bilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Islemler: 
Ogrenci Kayit = 4 
Ders Kayit = 5
Akademik Takvim = 6 
Yaz Okulu = 7
KimlikIslemleri = 8
");

            Console.Write("Islem numarasini girin (Orn: 6) : ");
            string ogrenciisleri = Console.ReadLine();
            Oipersonel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(ogrenciisleri);

            while (Oipersonel.Gorevi is < (Enumlar.Gorevler)4 or > (Enumlar.Gorevler)8) //Burada sadece 4 ile 8 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Islemler: 
Ogrenci Kayit = 4 
Ders Kayit = 5
Akademik Takvim = 6 
Yaz Okulu = 7
KimlikIslemleri = 8
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Islem numarasini girin (Orn: 6) : ");
                string _ogrenciisleri = Console.ReadLine();
                Oipersonel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_ogrenciisleri);
            }
            //<---------------------------Json dosyasına kaydetme kısmı------------------------->

            JArray Oipersonelekle = new JArray(      //Bu kısımda girdigimiz verileri array'e kaydediyorum
                   new JObject(
                            new JProperty("KimlikNo", Oipersonel.KimlikNo),
                            new JProperty("Adi", Oipersonel_bilgi.Ad),
                            new JProperty("Soyadi", Oipersonel_bilgi.Soyad),
                            new JProperty("DogumTarihi", Oipersonel_bilgi.Dogumtarihi.ToShortDateString()),
                            new JProperty("Gorevi", Oipersonel.Gorevi.ToString())));

            // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json", Oipersonelekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                Oipersonelekle.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nEkleme islemi basarili");
        }

        //<----------------------------------------Ogrenci Isleri silme kısmı----------------------------------------->
        public void OgrenciIsleriSil()
        {
            
            /*Bu kısımda olusturdugumuz dosyayı okutuyorum ve bunu bir array'e kaydediyorum daha sonra
             bu array'deki verisi olan elemanımızı secip siliyorum. Sonra bu işlemi json dosyamıza yazdırıyorum*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json");
            JArray Oipersonelsil = new JArray(json);
            JToken eleman = Oipersonelsil[0];
            Oipersonelsil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                Oipersonelsil.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nSilme islemi basarili");
        }

        //<--------------------------------------Ogrenci Isleri guncelleme kısmı----------------------------------------->
        public void OgrenciIsleriGuncelle()
        {
            /*Bu kısımda olusturdugumuz json dosyasini okutup array'e kaydediyoruz. Daha sonra ekleme 
            kısmında yaptığımız gibi ilgili alanları kullanıcıya girdirttik ve okuttugumuz dosyaya geri yazdırdık*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json");
            JArray Oipersonel = new JArray(json);

            IPersonel yeni_Oipersonel = new OgrenciIsleri();
            IKisiselBilgiler yeni_Oipersonelbilgi = new OgrenciIsleri();

            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            yeni_Oipersonel.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (yeni_Oipersonel.KimlikNo.ToString().Length != 6)
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                yeni_Oipersonel.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            yeni_Oipersonelbilgi.Ad = Console.ReadLine();

            while (yeni_Oipersonelbilgi.Ad == "")
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                yeni_Oipersonelbilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            yeni_Oipersonelbilgi.Soyad = Console.ReadLine();

            while (yeni_Oipersonelbilgi.Soyad == "")
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                yeni_Oipersonelbilgi.Soyad = Console.ReadLine();
            }
            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            yeni_Oipersonelbilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Islemler: 
Ogrenci Kayit = 4 
Ders Kayit = 5
Akademik Takvim = 6 
Yaz Okulu = 7
KimlikIslemleri = 8
");

            Console.Write("Isleminizin numarasini girin (Orn: 0) : ");
            string ogrenciisleri = Console.ReadLine();
            yeni_Oipersonel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(ogrenciisleri);

            while (yeni_Oipersonel.Gorevi is < (Enumlar.Gorevler)4 or > (Enumlar.Gorevler)8) //Burada sadece 4 ile 8 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Islemler: 
Ogrenci Kayit = 4 
Ders Kayit = 5
Akademik Takvim = 6 
Yaz Okulu = 7
KimlikIslemleri = 8
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Islem numarasini girin (Orn: 6) : ");
                string _ogrenciisleri = Console.ReadLine();
                yeni_Oipersonel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_ogrenciisleri);
            }

            JArray oipersonelguncelle = new JArray(
                  new JObject(
                           new JProperty("KimlikNo", yeni_Oipersonel.KimlikNo),
                           new JProperty("Adi", yeni_Oipersonelbilgi.Ad),
                           new JProperty("Soyadi", yeni_Oipersonelbilgi.Soyad),
                           new JProperty("Dogumtarihi", yeni_Oipersonelbilgi.Dogumtarihi.ToShortDateString()),
                           new JProperty("Gorevi", yeni_Oipersonel.Gorevi.ToString())));

            JToken eleman = Oipersonel[0];
            eleman.Replace(oipersonelguncelle[0]);  //Yukarıda okuttugum json dosyasının ilgili elemanını yeni array'imizdeki elemanla degistiriyorum.

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgrIsleriPersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                Oipersonel.WriteTo(yazdir);      //Degistirdigimiz array'in elemanını json dosyamıza yazdırıyorum
            }
            Console.WriteLine("*\n*\n*\nGuncelleme islemi basarili");
        }
    }
}
