using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Okul
{
    public class Idaripersonel : IPersonel, IKisiselBilgiler
    {
        int IPersonel.KimlikNo { get; set; } 
        string IKisiselBilgiler.Ad { get; set; }
        string IKisiselBilgiler.Soyad { get; set; }
        DateTime IKisiselBilgiler.Dogumtarihi { get; set; }
        Enumlar.Gorevler IPersonel.Gorevi { get; set; }

        //<----------------------------------------Idari Personel ekleme kısmı----------------------------------------->

        public void IdariPersonelEkle()
        {
            IPersonel idare = new Idaripersonel();
            IKisiselBilgiler idare_bilgiler = new Idaripersonel();

            //İlgili alanlari kullaniciya girdirerek doldurdum.

            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 545221) : ");   // Bu kısımda girilen ogrencino'nun 6 haneden fazla girilmemesini sağladım 
            idare.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (idare.KimlikNo.ToString().Length != 6)
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 545221) : ");
                idare.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            idare_bilgiler.Ad = Console.ReadLine();

            while (idare_bilgiler.Ad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Isim alani bos birakilamaz.");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                idare_bilgiler.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            idare_bilgiler.Soyad = Console.ReadLine();

            while (idare_bilgiler.Soyad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                idare_bilgiler.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            idare_bilgiler.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Gorevler: 
Mudur = 0 
Mudur Yardimcisi = 1 
Genel Sekreter = 2 
Yonetici Sekreter = 3 
");

            Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
            string Gorev = Console.ReadLine();
            idare.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(Gorev);

            while (idare.Gorevi is < 0 or > (Enumlar.Gorevler)3) //Burada sadece 0 ile 3 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Gorevler: 
Mudur = 0 
Mudur Yardimcisi = 1 
Genel Sekreter = 2 
Yonetici Sekreter = 3 
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
                string _Gorev = Console.ReadLine();
                idare.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_Gorev);
            }
            //<---------------------------Json dosyasına kaydetme kısmı------------------------->

            JArray idareekle = new JArray( //Bu kısımda girdigimiz verileri array'e kaydediyorum
                   new JObject(
                            new JProperty("KimlikNo", idare.KimlikNo),
                            new JProperty("Adi", idare_bilgiler.Ad),
                            new JProperty("Soyadi", idare_bilgiler.Soyad),
                            new JProperty("DogumTarihi", idare_bilgiler.Dogumtarihi.ToShortDateString()),
                            new JProperty("Gorevi", idare.Gorevi.ToString())));

            // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json", idareekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                idareekle.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nEkleme islemi basarili");
        }

        //<----------------------------------------Idari Personel silme kısmı----------------------------------------->
        public void IdariPersonelSil()
        {
            /*Bu kısımda olusturdugumuz dosyayı okutuyorum ve bunu bir array'e kaydediyorum daha sonra
            bu array'deki verisi olan elemanımızı secip siliyorum. Sonra bu işlemi json dosyamıza yazdırıyorum*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json");
            JArray idaripersonelsil = new JArray(json);
            JToken eleman = idaripersonelsil[0];
            idaripersonelsil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                idaripersonelsil.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nSilme islemi basarili");
        }

        //<--------------------------------------Ogrenci guncelleme kısmı----------------------------------------->
        public void IdariPersonelGuncelle()
        {
            /*Bu kısımda olusturdugumuz json dosyasini okutup array'e kaydediyoruz. Daha sonra ekleme 
            kısmında yaptığımız gibi ilgili alanları kullanıcıya girdirttik ve okuttugumuz dosyaya geri yazdırdık*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json");
            JArray Idaripersonel = new JArray(json);

            IPersonel yeni_idare = new Idaripersonel();
            IKisiselBilgiler yeni_idarebilgiler = new Idaripersonel();
            
            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 545221) : ");
            yeni_idare.KimlikNo = Convert.ToInt32(Console.ReadLine());
            
            while (yeni_idare.KimlikNo.ToString().Length != 6)
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 545221) : ");
                yeni_idare.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            yeni_idarebilgiler.Ad = Console.ReadLine();

            while (yeni_idarebilgiler.Ad == "")
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                yeni_idarebilgiler.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            yeni_idarebilgiler.Soyad = Console.ReadLine();

            while (yeni_idarebilgiler.Soyad == "")
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                yeni_idarebilgiler.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            yeni_idarebilgiler.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Gorevler: 
Mudur = 0 
Mudur Yardimcisi = 1 
Genel Sekreter = 2 
Yonetici Sekreter = 3 
");
            Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
            string Gorev = Console.ReadLine();
            yeni_idare.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(Gorev);

            while (yeni_idare.Gorevi is < 0 or > (Enumlar.Gorevler)3) //Burada sadece 0 ile 3 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Gorevler: 
Mudur = 0 
Mudur Yardimcisi = 1 
Genel Sekreter = 2 
Yonetici Sekreter = 3 
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
                string _Gorev = Console.ReadLine();
                yeni_idare.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_Gorev);
            }

            JArray idaripersonelguncelle = new JArray(
                  new JObject(
                           new JProperty("KimlikNo", yeni_idare.KimlikNo),
                           new JProperty("Adi", yeni_idarebilgiler.Ad),
                           new JProperty("Soyadi", yeni_idarebilgiler.Soyad),
                           new JProperty("Dogumtarihi", yeni_idarebilgiler.Dogumtarihi.ToShortDateString()),
                           new JProperty("Gorevi", yeni_idare.Gorevi.ToString())));

            JToken eleman = Idaripersonel[0];
            eleman.Replace(idaripersonelguncelle[0]);  //Yukarıda okuttugum json dosyasının ilgili elemanını yeni array'imizdeki elemanla degistiriyorum. 

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Idaripersonel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                Idaripersonel.WriteTo(yazdir);   //Degistirdigimiz array'in elemanını json dosyamıza yazdırıyorum
            }
            Console.WriteLine("Guncelleme islemi basarili");
        }
    }
}
