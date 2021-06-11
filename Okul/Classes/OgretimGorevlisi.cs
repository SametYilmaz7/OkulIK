using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Okul
{
    public class OgretimGorevlisi : IPersonel, IKisiselBilgiler
    {
        int IPersonel.KimlikNo { get; set; }
        string IKisiselBilgiler.Ad { get; set; }
        string IKisiselBilgiler.Soyad { get; set; }
        DateTime IKisiselBilgiler.Dogumtarihi { get; set; }
        Enumlar.Gorevler IPersonel.Gorevi { get; set; }

        //<----------------------------------------Ogretim Gorevlisi ekleme kısmı----------------------------------------->
        public void OgretimGorevlisiEkle()
        {
            IPersonel ogretimgorevlisi = new OgretimGorevlisi();           
            IKisiselBilgiler ogretimgorevlisi_bilgi = new OgretimGorevlisi();

            //İlgili alanlari kullaniciya girdirerek doldurdum.

            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            ogretimgorevlisi.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (ogretimgorevlisi.KimlikNo.ToString().Length != 6)  // Bu kısımda girilen ogrencino'nun 6 haneden fazla girilmemesini sağladım 
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                ogretimgorevlisi.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            ogretimgorevlisi_bilgi.Ad = Console.ReadLine();

            while (ogretimgorevlisi_bilgi.Ad == "")   // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                ogretimgorevlisi_bilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            ogretimgorevlisi_bilgi.Soyad = Console.ReadLine();

            while (ogretimgorevlisi_bilgi.Soyad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                ogretimgorevlisi_bilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            ogretimgorevlisi_bilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Ogretmenler: 
Bilgisayar Ogretmeni = 9
Makine Ogretmeni = 10
Elektrik Ogretmeni = 11
Insaat Ogretmeni = 12
Felsefe Ogretmeni = 13
");

            Console.Write("Ogretmen numarasini girin (Orn: 11) : ");
            string _ogretimgorevlisi = Console.ReadLine();
            ogretimgorevlisi.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_ogretimgorevlisi);

            while (ogretimgorevlisi.Gorevi is < (Enumlar.Gorevler)9 or > (Enumlar.Gorevler)13) //Burada sadece 9 ile 13 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Ogretmenler: 
Bilgisayar Ogretmeni = 9
Makine Ogretmeni = 10
Elektrik Ogretmeni = 11
Insaat Ogretmeni = 12
Felsefe Ogretmeni = 13
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Ogretmen numarasini girin (Orn: 11) : ");
                string ogretimgorevlisi_while = Console.ReadLine();
                ogretimgorevlisi.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(ogretimgorevlisi_while);
            }

            //<---------------------------Json dosyasına kaydetme kısmı------------------------->

            JArray OgretimGorevlisiekle = new JArray(        //Bu kısımda girdigimiz verileri array'e kaydediyorum
                       new JObject(
                                new JProperty("KimlikNo", ogretimgorevlisi.KimlikNo),
                                new JProperty("Adi", ogretimgorevlisi_bilgi.Ad),
                                new JProperty("Soyadi", ogretimgorevlisi_bilgi.Soyad),
                                new JProperty("DogumTarihi", ogretimgorevlisi_bilgi.Dogumtarihi.ToShortDateString()),
                                new JProperty("Gorevi", ogretimgorevlisi.Gorevi.ToString())));

            // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json", OgretimGorevlisiekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                OgretimGorevlisiekle.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nEkleme islemi basarili");
        }

        //<----------------------------------------Ogretim Gorevlisi silme kısmı----------------------------------------->
        public void OgretimGorevlisiSil()
        {
            /*Bu kısımda olusturdugumuz dosyayı okutuyorum ve bunu bir array'e kaydediyorum daha sonra
             bu array'deki verisi olan elemanımızı secip siliyorum. Sonra bu işlemi json dosyamıza yazdırıyorum*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json");
            JArray ogretimgorevlisil = new JArray(json);
            JToken eleman = ogretimgorevlisil[0];
            ogretimgorevlisil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogretimgorevlisil.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nSilme islemi basarili");
        }

        //<--------------------------------------Ogretim Gorevlisi guncelleme kısmı----------------------------------------->
        public void OgretimGorevlisiGuncelle()
        {
            /*Bu kısımda olusturdugumuz json dosyasini okutup array'e kaydediyoruz. Daha sonra ekleme 
            kısmında yaptığımız gibi ilgili alanları kullanıcıya girdirttik ve okuttugumuz dosyaya geri yazdırdık*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json");
            JArray ogretimgorevlisi = new JArray(json);

            IPersonel yeni_ogretimgorevlisi = new OgretimGorevlisi();
            IKisiselBilgiler yeni_ogretimgorevlisibilgi = new OgretimGorevlisi();

           
            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            yeni_ogretimgorevlisi.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (yeni_ogretimgorevlisi.KimlikNo.ToString().Length != 6)
            { 
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                yeni_ogretimgorevlisi.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            yeni_ogretimgorevlisibilgi.Ad = Console.ReadLine();

            while (yeni_ogretimgorevlisibilgi.Ad == "")
            {
                Console.WriteLine("Isim alani bos birakilamaz");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                yeni_ogretimgorevlisibilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            yeni_ogretimgorevlisibilgi.Soyad = Console.ReadLine();

            while (yeni_ogretimgorevlisibilgi.Soyad == "")
            {
                Console.WriteLine("Soyad alani bos birakilamaz");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                yeni_ogretimgorevlisibilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            yeni_ogretimgorevlisibilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Ogretmenler: 
Bilgisayar Ogretmeni = 9
Makine Ogretmeni = 10
Elektrik Ogretmeni = 11
Insaat Ogretmeni = 12
Felsefe Ogretmeni = 13
");

            Console.Write("Ogretmen numarasini girin (Orn: 11) : ");
            string _ogretimgorevlisi = Console.ReadLine();
            yeni_ogretimgorevlisi.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(_ogretimgorevlisi);

            while (yeni_ogretimgorevlisi.Gorevi is < (Enumlar.Gorevler)9 or > (Enumlar.Gorevler)13) //Burada sadece 9 ile 13 arasındaki sayıların girilmesini sağladım.
            {
                Console.WriteLine(@"Ogretmenler: 
Bilgisayar Ogretmeni = 9
Makine Ogretmeni = 10
Elektrik Ogretmeni = 11
Insaat Ogretmeni = 12
Felsefe Ogretmeni = 13
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Ogretmen numarasini girin (Orn: 11) : ");
                string ogretimgorevlisi_while = Console.ReadLine();
                yeni_ogretimgorevlisi.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(ogretimgorevlisi_while);
            }
            JArray OgretimGorevlisiguncelle = new JArray(
                       new JObject(
                                new JProperty("KimlikNo", yeni_ogretimgorevlisi.KimlikNo),
                                new JProperty("Adi", yeni_ogretimgorevlisibilgi.Ad),
                                new JProperty("Soyadi", yeni_ogretimgorevlisibilgi.Soyad),
                                new JProperty("DogumTarihi", yeni_ogretimgorevlisibilgi.Dogumtarihi.ToShortDateString()),
                                new JProperty("Gorevi", yeni_ogretimgorevlisi.Gorevi.ToString())));

            JToken eleman = ogretimgorevlisi[0];
            eleman.Replace(OgretimGorevlisiguncelle[0]);  //Yukarıda okuttugum json dosyasının ilgili elemanını yeni array'imizdeki elemanla degistiriyorum. 

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\OgretimGorevlisi.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                ogretimgorevlisi.WriteTo(yazdir);    //Degistirdigimiz array'in elemanını json dosyamıza yazdırıyorum
            }
            Console.WriteLine("*\n*\n*\nGuncelleme islemi basarili");
        }
    }
}
