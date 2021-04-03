using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirKelimeBirIslemSon
{
    public class Program
    {

        static void Main(string[] args)
        {
            List<OyunRakamlari> sayilar = new List<OyunRakamlari>();
            Islemler islem = new Islemler();
            for (int i = 0; i < 6; i++)
            {
                OyunRakamlari sayi = new OyunRakamlari();

                if (i != 5)
                {

                    sayi.Deger = islem.SayiIste(1, 10);
                    sayi.KullanildiMi = false;
                }
                else
                {
                    sayi.Deger = islem.SayiIste(30, 80);
                    sayi.KullanildiMi = false;
                }
                sayilar.Add(sayi);
            }
            Islemler.sayiListesi = sayilar;
            StringBuilder sb = new StringBuilder();
            Islemler.toplam = 0;
            var result = (from p in sayilar
                          where p.KullanildiMi == false
                          select p).ToList();
            List<OyunRakamlari> gelenListe;
            int sayac = 0;

            while (result.Count > 0)
            {
                Islemler islem2 = new Islemler();

                if (sayilar.Count == 0)
                {
                    throw new Exception("Liste 0 geliyor.");
                }
                gelenListe = islem2.IkiSayiAl(sayilar);
                if (sayac == 0)
                {

                    string islAdi = islem2.IslemAdiSec();
                    if (islAdi == "Çıkarma")
                    {

                        while (gelenListe[0].Deger <= gelenListe[1].Deger || gelenListe[0].Deger < 0 || gelenListe[1].Deger < 0)
                        {
                            gelenListe[0].KullanildiMi = false;
                            gelenListe[1].KullanildiMi = false;
                            Islemler.sayac--;
                            gelenListe = islem2.IkiSayiAl(sayilar);

                        }
                    }
                    else if (islAdi == "Çarpma")
                    {
                        while (gelenListe[0].Deger < 0 || gelenListe[1].Deger < 0)
                        {
                            gelenListe = islem2.IkiSayiAl(sayilar);
                        }
                    }

                   Islemler.toplam = islem2.IslemYap(gelenListe[0].Deger, gelenListe[1].Deger, islAdi);
                    sb.AppendLine(Islemler.toplamHesapSayaci++ + " . adim icin " + Islemler.toplam + " - ");
                    string yapilanIslem = islem2.IslemAdi;
                }
                else
                {
                    string islAdi = islem2.IslemAdiSec();
                    if (islAdi == "Toplama")
                    {

                        Islemler.toplam = islem2.IslemYap(Islemler.toplam, gelenListe[0].Deger, islAdi);
                        sb.AppendLine(Islemler.toplamHesapSayaci++ + " . adim icin " + Islemler.toplam + " - ");
                    }
                    else if (islAdi == "Çıkarma")
                    {
                        if (islAdi == "Çıkarma")
                        {

                            while (Islemler.toplam <= gelenListe[0].Deger || gelenListe[0].Deger < 0)
                            {
                                int eskiDeger = gelenListe[0].Deger;
                                gelenListe[0].KullanildiMi = false;
                                gelenListe = islem2.IkiSayiAl(sayilar);
                                if (gelenListe[0].Deger == eskiDeger)
                                {
                                    while (islAdi == "Çıkarma")
                                    {
                                        islAdi = islem2.IslemAdiSec();
                                    }
                                }
                            }
                        }
                        Islemler.toplam = islem2.IslemYap(Islemler.toplam, gelenListe[0].Deger, islAdi);
                        sb.AppendLine(Islemler.toplamHesapSayaci++ + " . adim icin " + Islemler.toplam + " - ");
                    }
                    else if (islAdi == "Çarpma")
                    {
                        while (gelenListe[0].Deger < 0)
                        {
                            gelenListe[0].KullanildiMi = false;
                            gelenListe = islem2.IkiSayiAl(sayilar);
                        }


                        Islemler.toplam = islem2.IslemYap(Islemler.toplam, gelenListe[0].Deger, islAdi);

                        sb.AppendLine(Islemler.toplamHesapSayaci++ + " . adim icin " + Islemler.toplam + " - ");
                    }
                }


                sayac++;
                result = (from p in sayilar
                          where p.KullanildiMi == false
                          select p).ToList();
            }


            for (int i = 0; i < sayilar.Count; i++)
            {
                Console.WriteLine(i + 1 + ". sayi : " + sayilar[i].Deger);
            }
            Console.WriteLine("Sonuç: " + Islemler.toplam);
            for (int j = 0; j < 10; j++)
                Console.WriteLine("*");
            Console.WriteLine("Toplam değerinin hesaplanma sonuçları - " + "\n" + sb.ToString());
            Console.WriteLine("Tebrikler !!! 10 puan kazandınız..");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            
            Console.Read();
       

        }


    }
    public class OyunRakamlari
    {

        public int Deger { get; set; }
        public bool KullanildiMi { get; set; }
    }
    public class Islemler
    {
        public static int toplamHesapSayaci = 1;
        public static int toplam = 0;
        public static List<OyunRakamlari> sayiListesi = new List<OyunRakamlari>();
        public string IslemAdi { get; set; }
        public static readonly Random rnd = new Random();
        public int SayiIste(int kucuk, int buyuk)
        {

            return rnd.Next(kucuk, buyuk);
        }
        public static int sayac = 1;
        public static bool metodaGirdiMi = false;
        public List<OyunRakamlari> IkiSayiAl(List<OyunRakamlari> liste)
        {
            if (liste.Count == 0)
            {
                throw new Exception("Count 0");
            }
            List<OyunRakamlari> donenListe = new List<OyunRakamlari>();
            liste = liste.Where(x => x.KullanildiMi == false).ToList();

            if (sayac < 2)
            {
                int deger1 = 0, deger2 = 0;
                while (deger1 == deger2)
                {
                    donenListe.Clear();
                    for (int i = 0; i < 2; i++)
                    {

                        if (i == 1)
                        {

                            deger2 = SayiIste(1, liste.Count);
                            OyunRakamlari o = liste[deger2];
                            o.KullanildiMi = true;
                            donenListe.Add(o);
                        }

                        if (i != 1)
                        {

                            deger1 = SayiIste(1, liste.Count);
                            OyunRakamlari o = liste[deger1];
                            o.KullanildiMi = true;
                            donenListe.Add(o);
                        }


                    }
                }
            }
            else
            {
                int deger1 = 0;

                donenListe.Clear();

                if (liste.Count == 1)
                {
                    deger1 = 0;
                }
                else
                {
                    deger1 = SayiIste(1, liste.Count);
                }
                OyunRakamlari o = liste[deger1];
                o.KullanildiMi = true;
                donenListe.Add(o);

            }
            sayac++;
            return donenListe;
        }

        public string IslemAdiSec()
        {
            int value = SayiIste(1, 4);
            switch (value)
            {
                case 1:
                    return "Toplama";
                    break;
                case 2:
                    return "Çıkarma";
                    break;
                case 3:
                    return "Çarpma";
                    break;
                default:
                    return "";
                    break;
            }
        }
        public int IslemYap(int firstNum, int secondNum, string isleAdi)
        {

            if (firstNum < 1 || secondNum < 1)
            {
                throw new Exception("Count 0");
            }
            int result = 0;

            switch (isleAdi)
            {
                case "Toplama":
                    IslemAdi = "Toplama";
                    result = firstNum + secondNum;
                    sayac++;
                    metodaGirdiMi = true;
                    return result;
                    break;
                case "Çıkarma":
                    IslemAdi = "Çıkarma";
                    result = firstNum - secondNum;
                    sayac++;

                    if (result < 1)
                    {
                        throw new Exception("toplam(result) 0 ın altında");
                    }
                    return result;
                    break;
                case "Çarpma":
                    IslemAdi = "Çarpma";
                    result = firstNum * secondNum;
                    sayac++;
                    metodaGirdiMi = true;
                    if (result < 1)
                    {
                        throw new Exception("toplam(result) 0 ın altında");
                    }
                    return result;
                    break;
                default:
                    sayac++;
                    metodaGirdiMi = true;
                    return 0;
                    break;
            }
           

        }
       

    }
    

}
