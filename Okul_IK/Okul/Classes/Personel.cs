using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Okul
{
    public class Personel : IPersonel, IKisiselBilgiler
    {
        int IPersonel.KimlikNo { get; set; }
        string IKisiselBilgiler.Ad { get; set; }
        string IKisiselBilgiler.Soyad { get; set; }
        DateTime IKisiselBilgiler.Dogumtarihi { get; set; }
        Enumlar.Departmanlar Departman { get; set; }
        Enumlar.Gorevler IPersonel.Gorevi { get; set; }
        DateTime Baslamatarihi { get; set; }

        //<----------------------------------------Personel ekleme kısmı----------------------------------------->
        public void PersonelEkle()
        {

            IPersonel personel = new Personel();                //İlgili alanlari kullaniciya girdirerek doldurdum.
            IKisiselBilgiler personel_bilgi = new Personel();

            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            personel.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (personel.KimlikNo.ToString().Length != 6) // Bu kısımda girilen ogrencino'nun 6 haneden fazla girilmemesini sağladım
            {
                    Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                    Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                    personel.KimlikNo = Convert.ToInt32(Console.ReadLine());   
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            personel_bilgi.Ad = Console.ReadLine();

            while (personel_bilgi.Ad == "") // Boş veri girilmemesi için ayarladım
            {
                Console.WriteLine("Isim alani bos birakilamaz.");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                personel_bilgi.Ad = Console.ReadLine();
            }
            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            personel_bilgi.Soyad = Console.ReadLine();

            while (personel_bilgi.Soyad == "") // Boş veri girilmemesi için ayarladım
            { 
                Console.WriteLine("Soyad alani bos birakilamaz.");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                personel_bilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            personel_bilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Departmanlarimiz: 
Idari Isler = 0 
Muhasebe = 1 
Ogrenci Isleri = 2 
Insan Kaynaklari = 3 
");

            Console.Write("Departmaninizin numarasini girin (Orn: 0) : ");
            string departman = Console.ReadLine();
            Departman = (Enumlar.Departmanlar)Convert.ToInt32(departman);

            while (Departman is < 0 or > (Enumlar.Departmanlar)3) //Burada sadece 0 ile 3 arasındaki sayıları girilmesini sağladım.
            {
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.WriteLine(@"Departmanlarimiz: 
Idari Isler = 0 
Muhasebe = 1 
Ogrenci Isleri = 2 
Insan Kaynaklari = 3 
");

                Console.Write("Departmaninizin numarasini girin (Orn: 0) : ");
                string departman_while = Console.ReadLine();
                Departman = (Enumlar.Departmanlar)Convert.ToInt32(departman_while);
            }
            
            Console.WriteLine(@"Gorevler: 
GenelSekreter = 2,
YoneticiSekreter = 3,
");
            Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
            string gorevler = Console.ReadLine();
            personel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(gorevler);

            while (personel.Gorevi is < (Enumlar.Gorevler)2 or > (Enumlar.Gorevler)3) //Burada sadece 2 ile 3 sayılarının girilmesini sağladım.
            {
                Console.WriteLine(@"Gorevler: 
GenelSekreter = 2,
YoneticiSekreter = 3,
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
                string gorevler_while = Console.ReadLine();
                personel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(gorevler_while);
            }            

            Console.Write("Ise baslama tarihinizi girin (Orn: 21/02/2021) : ");
            Baslamatarihi = Convert.ToDateTime(Console.ReadLine());

            //<---------------------------Json dosyasına kaydetme kısmı------------------------->

            JArray personelekle = new JArray(       //Bu kısımda girdigimiz verileri array'e kaydediyorum
                   new JObject(
                            new JProperty("KimlikNo", personel.KimlikNo),
                            new JProperty("Adi", personel_bilgi.Ad),
                            new JProperty("Soyadi", personel_bilgi.Soyad),
                            new JProperty("DogumTarihi", personel_bilgi.Dogumtarihi.ToShortDateString()),
                            new JProperty("Departmani", Departman.ToString()),
                            new JProperty("Gorevi", personel.Gorevi.ToString()),
                            new JProperty("BaslamaTarihi", Baslamatarihi.ToShortDateString())));

            // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

            File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json", personelekle.ToString());
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                personelekle.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nEkleme islemi basarili");
        }

        //<----------------------------------------Personel silme kısmı----------------------------------------->
        public void PersonelSil()
        {
            /*Bu kısımda olusturdugumuz dosyayı okutuyorum ve bunu bir array'e kaydediyorum daha sonra
             bu array'deki verisi olan elemanımızı secip siliyorum. Sonra bu işlemi json dosyamıza yazdırıyorum*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json");
            JArray personelsil = new JArray(json);
            JToken eleman = personelsil[0];
            personelsil.Remove(eleman);
            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                personelsil.WriteTo(yazdir);
            }
            Console.WriteLine("*\n*\n*\nSilme islemi basarili");
        }

        //<--------------------------------------Personel guncelleme kısmı----------------------------------------->
        public void PersonelGuncelle()
        {
            /*Bu kısımda olusturdugumuz json dosyasini okutup array'e kaydediyoruz. Daha sonra ekleme 
            kısmında yaptığımız gibi ilgili alanları kullanıcıya girdirttik ve okuttugumuz dosyaya geri yazdırdık*/

            var json = File.ReadAllText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json");
            JArray personel = new JArray(json);


            IPersonel yeni_personel = new Personel();
            IKisiselBilgiler yeni_personelbilgi = new Personel();
            
            Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
            yeni_personel.KimlikNo = Convert.ToInt32(Console.ReadLine());

            while (yeni_personel.KimlikNo.ToString().Length != 6)
            {
                Console.WriteLine("Giris basarisiz! Kimlik numarasi 6 haneli olmali");
                Console.Write("6 haneli kimlik numaranizi giriniz (Orn: 676881) : ");
                yeni_personel.KimlikNo = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Adinizi girin (Orn: Samet) : ");
            yeni_personelbilgi.Ad = Console.ReadLine();

            while (yeni_personelbilgi.Ad == "")
            {
                Console.WriteLine("Isim alani bos birakilamaz.");
                Console.Write("Adinizi girin (Orn: Samet) : ");
                yeni_personelbilgi.Ad = Console.ReadLine();
            }

            Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
            yeni_personelbilgi.Soyad = Console.ReadLine();

            while (yeni_personelbilgi.Soyad == "")
            {
                Console.WriteLine("Soyad alani bos birakilamaz.");
                Console.Write("Soyadinizi girin (Orn: Yilmaz) : ");
                yeni_personelbilgi.Soyad = Console.ReadLine();
            }

            Console.Write("Dogum tarihinizi girin (Orn: 12/12/2012) : ");
            yeni_personelbilgi.Dogumtarihi = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine(@"Departmanlarimiz: 
Idari Isler = 0 
Muhasebe = 1 
Ogrenci Isleri = 2 
Insan Kaynaklari = 3 
");
            Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
            Console.Write("Departmaninizin numarasini girin (Orn: 0) : ");
            string departman = Console.ReadLine();
            Departman = (Enumlar.Departmanlar)Convert.ToInt32(departman);

            while (Departman is < 0 or > (Enumlar.Departmanlar)3) //Burada sadece 0 ile 3 arasındaki sayıları girilmesini sağladım.
            {
                Console.WriteLine(@"Departmanlarimiz: 
Idari Isler = 0 
Muhasebe = 1 
Ogrenci Isleri = 2 
Insan Kaynaklari = 3 
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Departmaninizin numarasini girin (Orn: 0) : ");
                string departman_while = Console.ReadLine();
                Departman = (Enumlar.Departmanlar)Convert.ToInt32(departman_while);
            }


            Console.WriteLine(@"Gorevler: 
GenelSekreter = 2,
YoneticiSekreter = 3,
");
            Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
            string gorevler = Console.ReadLine();
            yeni_personel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(gorevler);

            while (yeni_personel.Gorevi is < (Enumlar.Gorevler)2 or > (Enumlar.Gorevler)3) //Burada sadece 2 ile 3 sayılarının girilmesini sağladım.
            {
                Console.WriteLine(@"Gorevler: 
GenelSekreter = 2,
YoneticiSekreter = 3,
");
                Console.WriteLine("Hata!!! Lutfen ekrandaki numaralardan birini giriniz.");
                Console.Write("Gorevinizin numarasini girin (Orn: 0) : ");
                string gorevler_while = Console.ReadLine();
                yeni_personel.Gorevi = (Enumlar.Gorevler)Convert.ToInt32(gorevler_while);
            }

            Console.Write("Ise baslama tarihinizi girin (Orn: 21/02/2021) : ");
            Baslamatarihi = Convert.ToDateTime(Console.ReadLine());

            JArray personelguncelle = new JArray(
                  new JObject(
                           new JProperty("KimlikNo", yeni_personel.KimlikNo),
                           new JProperty("Adi", yeni_personelbilgi.Ad),
                           new JProperty("Soyadi", yeni_personelbilgi.Soyad),
                           new JProperty("Dogumtarihi", yeni_personelbilgi.Dogumtarihi.ToShortDateString()),
                           new JProperty("Departman", Departman.ToString()),
                           new JProperty("Gorevi", yeni_personel.Gorevi.ToString()),
                           new JProperty("Baslamatarihi", Baslamatarihi.ToShortDateString())));

            JToken eleman = personel[0];
            eleman.Replace(personelguncelle[0]);   //Yukarıda okuttugum json dosyasının ilgili elemanını yeni array'imizdeki elemanla degistiriyorum. 

            using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\.Net Final Ödevi\json\Personel.json"))
            using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
            {
                personel.WriteTo(yazdir);        //Degistirdigimiz array'in elemanını json dosyamıza yazdırıyorum
            }
            Console.WriteLine("*\n*\n*\nGuncelleme islemi basarili");
        }
    }
}

