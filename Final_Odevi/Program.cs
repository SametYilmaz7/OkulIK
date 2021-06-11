using System;
using Okul;


namespace Final_Odevi
{
    class Program
    {
        static void Main(string[] args)
        {
            int secim, personel_secim;
            string ogrenci_secim, _personelsecim, idaripersonel_secim, oipersonel_secim, ogretimpersonel_secim;
            string ders_ekle, ders_guncelle;

        baslangic:

            Console.WriteLine("*\n*\n*\n*****************Kayit Sistemine Hosgeldiniz*****************");
            Console.WriteLine("Ogrenci Kayıt icin '1' Personel icin '2' yaziniz. ");
            Console.WriteLine("Cikis yapmak istiyorsaniz '3' yaziniz.");
            secim = Convert.ToInt32(Console.ReadLine());

            if (secim == 1 || secim == 2 || secim == 3) // Baslangictaki sorguda sadece 1 ya da 2 girilmesini sagladim
            {
                switch (secim)
                {
                    //Ogrenci kayit islemleri
                    case 1:
                        Console.WriteLine("****Ogrenci Kayit Sistemine Hosgeldiniz****");
                        Console.WriteLine("Ogrenci eklemek icin 'ekle', \nsilmek icin 'sil', \nkayitli bir veriyi guncellemek icin ise 'guncelle' yaziniz.");
                        ogrenci_secim = Console.ReadLine();

                        Ogrenci ogrenci = new();
                        Dersler dersler = new();
                        OgrenciDersler ogrenciders = new();

                        if (ogrenci_secim == "ekle")
                        {
                            ogrenci.OgrenciEkle();
                            dersler.DersEkle();
                            ogrenciders.OgrenciDersEkle();
                            Console.WriteLine("*\n*\nOgrencinin kayit ve ders ekleme islemleri basariyla tamamlanmistir.");
                        }

                        else if (ogrenci_secim == "sil")
                        {
                            ogrenci.OgrenciSil();
                            dersler.DersSil();
                            ogrenciders.OgrenciDersSil();
                            Console.WriteLine("Sistemimizde sectiginiz dersler kaldirilmistir.");
                        }

                        else if (ogrenci_secim == "guncelle")
                        {
                            ogrenci.OgrenciGuncelle();
                            dersler.DersGuncelle();
                            Console.WriteLine("Ogrenci ve ders guncellemesi basariyla tamamlanmistir.");
                        }
                        else Console.WriteLine("Yanlis komut girdiniz... \nProgram sonlandirildi.");

                        goto baslangic; // Programın bitmeyip tekrar etmesi icin goto komutunu kullandım


                    //Personel kayit islemleri 

                    case 2:
                        Console.WriteLine("****Personel Kayit Sistemine Hosgeldiniz****");
                        Console.WriteLine("Hangi personel icin kayit yaptirmak istediginizi seciniz.");
                        Console.WriteLine("Personel = 1 \nIdari Personel = 2 \nOgrenci Isleri Personel = 3 \nOgretim Gorevlisi Personel = 4");
                        personel_secim = Convert.ToInt32(Console.ReadLine());

                        while (personel_secim < 1 || personel_secim > 4) //Sadece sorguda istenilen sayilarin girilmesi icin ayarladim
                        {
                            Console.WriteLine("Hata!! Lutfen ekranda yazan sayilardan birini giriniz.");
                            Console.WriteLine("Personel = 1 \nIdari Personel = 2 \nOgrenci Isleri Personel = 3 \nOgretim Gorevlisi Personel = 4");
                            personel_secim = Convert.ToInt32(Console.ReadLine());
                        }
                        switch (personel_secim)
                        {
                            //Personel islemleri
                            case 1:
                                Console.WriteLine("Personel kayit islemini sectiniz.");
                                Console.WriteLine("Personel eklemek icin 'ekle', \nsilmek icin 'sil', \nkayitli bir veriyi guncellemek icin ise 'guncelle' yaziniz.");
                                _personelsecim = Console.ReadLine();

                                Personel personel = new();

                                if (_personelsecim == "ekle") personel.PersonelEkle();
                                else if (_personelsecim == "sil") personel.PersonelSil();
                                else if (_personelsecim == "guncelle") personel.PersonelGuncelle();
                                else Console.WriteLine("Yanlis komut girdiniz... \nProgram sonlandirildi.");
                                goto baslangic; // Programın bitmeyip tekrar etmesi icin goto komutunu kullandım

                            //Idari Personel islemleri
                            case 2:
                                Console.WriteLine("Idari Personel kayit islemini sectiniz.");
                                Console.WriteLine("Idari Personel eklemek icin 'ekle', \nsilmek icin 'sil', \nkayitli bir veriyi guncellemek icin ise 'guncelle' yaziniz.");
                                idaripersonel_secim = Console.ReadLine();

                                Idaripersonel idaripersonel = new();

                                if (idaripersonel_secim == "ekle") idaripersonel.IdariPersonelEkle();
                                else if (idaripersonel_secim == "sil") idaripersonel.IdariPersonelSil();
                                else if (idaripersonel_secim == "guncelle") idaripersonel.IdariPersonelGuncelle();
                                else Console.WriteLine("Yanlis komut girdiniz... \nProgram sonlandirildi.");
                                goto baslangic; // Programın bitmeyip tekrar etmesi icin goto komutunu kullandım

                            //Ogrenci Isleri Personel islemleri
                            case 3:
                                Console.WriteLine("Ogrenci Isleri Personel kayit islemini sectiniz.");
                                Console.WriteLine("Ogrenci Isleri Personel eklemek icin 'ekle', \nsilmek icin 'sil', \nkayitli bir veriyi guncellemek icin ise 'guncelle' yaziniz.");
                                oipersonel_secim = Console.ReadLine();

                                OgrenciIsleri oipersonel = new();

                                if (oipersonel_secim == "ekle") oipersonel.OgrenciIsleriEkle();
                                else if (oipersonel_secim == "sil") oipersonel.OgrenciIsleriSil();
                                else if (oipersonel_secim == "guncelle") oipersonel.OgrenciIsleriGuncelle();
                                else Console.WriteLine("Yanlis komut girdiniz... \nProgram sonlandirildi.");
                                goto baslangic; // Programın bitmeyip tekrar etmesi icin goto komutunu kullandım

                            //Ogretim Gorevlisi Personel islemleri
                            case 4:
                                Console.WriteLine("Ogretim Gorevlisi Personel kayit islemini sectiniz.");
                                Console.WriteLine("Ogretim Gorevlisi Personel eklemek icin 'ekle', \nsilmek icin 'sil', \nkayitli bir veriyi guncellemek icin ise 'guncelle' yaziniz.");
                                ogretimpersonel_secim = Console.ReadLine();

                                OgretimGorevlisi ogretimpersonel = new();

                                if (ogretimpersonel_secim == "ekle") ogretimpersonel.OgretimGorevlisiEkle();
                                else if (ogretimpersonel_secim == "sil") ogretimpersonel.OgretimGorevlisiSil();
                                else if (ogretimpersonel_secim == "guncelle") ogretimpersonel.OgretimGorevlisiGuncelle();
                                else Console.WriteLine("Yanlis komut girdiniz... \nProgram sonlandirildi.");
                                goto baslangic; // Programın bitmeyip tekrar etmesi icin goto komutunu kullandım


                        } goto baslangic;
                    case 3:
                        break;
                }
            } 
            else
            {
                Console.WriteLine("Hata!!! Lutfen ekrandaki sayilardan birini giriniz.");
                goto baslangic;
            }
        }            
    }
}
    

