using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;


namespace Okul
{
    public class Ogrenci : IOgrenciNo, IKisiselBilgiler
    {
        int IOgrenciNo.OgrenciNo { get; set; }
        string IKisiselBilgiler.Ad { get; set; }
        string IKisiselBilgiler.Soyad { get; set; }
        DateTime IKisiselBilgiler.Dogumtarihi { get; set; }
        Enumlar.Bolumler Bolum { get; set; }
        int Sinif { get; set; }
        double NotOrtalama { get; set; }
        
        //<----------------------------------------Ogrenci ekleme kısmı----------------------------------------->
        public void OgrenciEkle()
        {
            IOgrenciNo ogrenci = new Ogrenci();                 
            IKisiselBilgiler ogrenci_bilgi = new Ogrenci();

            //İlgili alanlari kullaniciya girdirerek doldurdum.

            Console.Write("4 haneli ogrenci numaranızı giriniz (Orn: 1265) : ");
            ogrenci.OgrenciNo = Convert.ToInt32(Console.ReadLine());
            
            while (ogrenci.OgrenciNo.ToString().Length != 4)  // Bu kısımda girilen ogrencino'nun 4 haneden fazla girilmemesini sağladım 
            {
                Console.WriteLine("Giris basarisiz! Numaraniz 4 haneli olmali");    
                Console.Write("4 haneli ogrenci numaranızı giriniz (Orn: 1265) : ");
                ogrenci.OgrenciNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            ogrenci_bilgi.Ad = Console.ReadLine();

            while (ogrenci_bilgi.Ad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                ogrenci_bilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            ogrenci_bilgi.Soyad = Console.ReadLine();

            while (ogrenci_bilgi.Soyad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                ogrenci_bilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 27/11/2001) : ");
            ogrenci_bilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Bolumlerimiz: 
Bilgisayar Programciligi = 0 
MobilProgramlama = 1 
MakineMuhendisligi = 2 
BilgisayarMuhendisligi = 3 
ElektrikMuhendisligi = 4 
InsaatMuhendisligi = 5 
Felsefe = 6");

            Console.Write("Bolumunuzun numarasini girin (Orn: 3) : ");
            string _Bolum = Console.ReadLine();
            Bolum = (Enumlar.Bolumler)Convert.ToInt32(_Bolum);

            while (Bolum is < 0 or > (Enumlar.Bolumler)6) //Burada sadece 0 ile 6 arasındaki sayıları girilmesini sağladım.
            {
                Console.WriteLine(@"Bolumlerimiz: 
Bilgisayar Programciligi = 0 
MobilProgramlama = 1 
MakineMuhendisligi = 2 
BilgisayarMuhendisligi = 3 
ElektrikMuhendisligi = 4 
InsaatMuhendisligi = 5 
Felsefe = 6");
                Console.WriteLine("Hatali numara girdiniz!! lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Bolumunuzun numarasini girin (Orn: 3) : ");
                string Bolum_while= Console.ReadLine();
                Bolum = (Enumlar.Bolumler)Convert.ToInt32(Bolum_while);
            }

            Console.Write("Kacinci sinif oldugunuzu girin (Orn: 3) : ");
            Sinif = Convert.ToInt32(Console.ReadLine());

            while (Sinif < 1 || Sinif  > 4) // Sınıf verisinin 1 ile 4 arasındaki sayılardan başka sayı girilmemesini sağladım
            {
                Console.WriteLine("Hatali giris!! Sinif 1 ile 4 arasinda olmali.");
                Console.Write("Kacinci sinif oldugunuzu girin (Orn: 3) : ");
                Sinif = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Not ortalaminizi giriniz (Orn: 3,74) : ");
            NotOrtalama = Convert.ToDouble(Console.ReadLine());

            while (NotOrtalama !<= 0 || NotOrtalama !>= 4) // NotOrtalama verisinin 0 ile 4 arasındaki sayılardan başka sayı girilmemesini sağladım.
            {
                Console.WriteLine("Hatali giris!!! Not ortalamasi 0 ile 4 arasinda olmali");
                Console.Write("Not ortalaminizi giriniz (Orn: 3,74) : ");
                NotOrtalama = Convert.ToDouble(Console.ReadLine());
            }

            //<---------------------------Json dosyasına kaydetme kısmı------------------------->

            JArray ogrenciekle = new JArray(   //Bu kısımda girdigimiz verileri array'e kaydediyorum
                   new JObject(
                            new JProperty("OgrenciNo", ogrenci.OgrenciNo),
                            new JProperty("Adi", ogrenci_bilgi.Ad),
                            new JProperty("Soyadi", ogrenci_bilgi.Soyad),
                            new JProperty("Dogumtarihi", ogrenci_bilgi.Dogumtarihi.ToShortDateString()),
                            new JProperty("Bolumu", Bolum.ToString()),
                            new JProperty("Sinifi", Sinif),
                            new JProperty("Not Ortalama", NotOrtalama)));
                       
            // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json", ogrenciekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogrenciekle.WriteTo(yazdir);
            }

            Console.WriteLine("*\n*\n*\nEkleme islemi basarili.");
        }

        //<----------------------------------------Ogrenci silme kısmı----------------------------------------->
        public void OgrenciSil()
        {
            /*Bu kısımda olusturdugumuz dosyayı okutuyorum ve bunu bir array'e kaydediyorum daha sonra
             bu array'deki verisi olan elemanımızı secip siliyorum. Sonra bu işlemi json dosyamıza yazdırıyorum*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json");            
            JArray ogrenci = new JArray(json);
            JToken eleman = ogrenci[0];
            ogrenci.Remove(eleman);           
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogrenci.WriteTo(yazdir);
            }

            Console.WriteLine("*\n*\n*\nSilme islemi basarili");
        }
        //<--------------------------------------Ogrenci guncelleme kısmı----------------------------------------->
        public void OgrenciGuncelle()
        {
            /*Bu kısımda olusturdugumuz json dosyasini okutup array'e kaydediyoruz. Daha sonra ekleme 
            kısmında yaptığımız gibi ilgili alanları kullanıcıya girdirttik ve okuttugumuz dosyaya geri yazdırdık*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json");
            JArray ogrenci = new JArray(json);

            IOgrenciNo yeni_ogrenci = new Ogrenci();
            IKisiselBilgiler yeni_ogrencibilgi = new Ogrenci();

            Console.Write("4 haneli ogrenci numaranızı giriniz (Orn: 1265) : ");
            yeni_ogrenci.OgrenciNo = Convert.ToInt32(Console.ReadLine());

            while (yeni_ogrenci.OgrenciNo.ToString().Length != 4)
            {
                Console.WriteLine("Giris basarisiz! Numaraniz 4 haneli olmali");
                Console.Write("4 haneli ogrenci numaranızı giriniz (Orn: 1265) : ");
                yeni_ogrenci.OgrenciNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            yeni_ogrencibilgi.Ad = Console.ReadLine();

            while (yeni_ogrencibilgi.Ad == "")
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                yeni_ogrencibilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            yeni_ogrencibilgi.Soyad = Console.ReadLine();

            while (yeni_ogrencibilgi.Soyad == "")
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                yeni_ogrencibilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 27/11/2001) : ");
            yeni_ogrencibilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Bolumlerimiz: 
Bilgisayar Programciligi = 0 
MobilProgramlama = 1 
MakineMuhendisligi = 2 
BilgisayarMuhendisligi = 3 
ElektrikMuhendisligi = 4 
InsaatMuhendisligi = 5 
Tıp = 6 
Felsefe = 7");

            Console.Write("Bolumunuzun numarasini girin (Orn: 3) : ");
            string _Bolum = Console.ReadLine();
            Bolum = (Enumlar.Bolumler)Convert.ToInt32(_Bolum);

            Console.Write("Kacinci sinif oldugunuzu girin (Orn: 3) : ");
            Sinif = Convert.ToInt32(Console.ReadLine());

            while (Sinif! <= 1 || Sinif! >= 4)
            {
                Console.WriteLine("Hatali giris!! Sinif 1 ile 4 arasinda olmali.");
                Console.Write("Kacinci sinif oldugunuzu girin (Orn: 3) : ");
                Sinif = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Not ortalaminizi giriniz (Orn: 3,74) : ");
            NotOrtalama = Convert.ToDouble(Console.ReadLine());
            
            while (NotOrtalama! <= 0 || NotOrtalama! >= 4)
            {
                Console.WriteLine("Hatali giris!!! Not ortalamasi 0 ile 4 arasinda olmali");
                Console.Write("Not ortalaminizi giriniz (Orn: 3,74) : ");
                NotOrtalama = Convert.ToDouble(Console.ReadLine());
            }

            JArray ogrenciguncelle = new JArray(
                  new JObject(
                           new JProperty("OgrenciNo", yeni_ogrenci.OgrenciNo),
                           new JProperty("Adi", yeni_ogrencibilgi.Ad),
                           new JProperty("Soyadi", yeni_ogrencibilgi.Soyad),
                           new JProperty("Dogumtarihi", yeni_ogrencibilgi.Dogumtarihi.ToShortDateString()),
                           new JProperty("Bolumu", Bolum.ToString()),
                           new JProperty("Sinifi", Sinif),
                           new JProperty("Not Ortalama", NotOrtalama)));

            JToken eleman = ogrenci[0];
            eleman.Replace(ogrenciguncelle[0]); //Yukarıda okuttugum json dosyasının ilgili elemanını yeni array'imizdeki elemanla degistiriyorum. 

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Ogrenci.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogrenci.WriteTo(yazdir);  //Degistirdigimiz array'in elemanını json dosyamıza yazdırıyorum
            }

            Console.WriteLine("*\n*\n*\nGuncelleme islemi basarili");
        }
    }
}

