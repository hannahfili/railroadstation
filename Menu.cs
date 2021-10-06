using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dworzec
{
    class Menu
    {
        public static void Pokaz(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar,
            List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {

            Odjazdy(bazaPociagow,true);

            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("[1] Kup bilet jednorazowy");
            Console.WriteLine("[2] Kup bilet okresowy odcinkowy");
            Console.WriteLine("[3] Sprawdź ważność biletu okresowego");
            Console.WriteLine("[4] Przedłuż ważność ulgi (tylko dla zarejestrowanych użytkowników)");
            Console.WriteLine("[5] Zarejestruj się w systemie");
            Console.WriteLine("[0] Wyjście");
            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            int wybor = Helper.GetInt("Wybierz, co chcesz zrobic: ", 0, 5);

            switch (wybor)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    KupBilet.Jednorazowy(bazaPociagow,bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
                    break;
                case 2:
                    KupBilet.Okresowy(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
                    break;
                case 3:
                    BiletOkresowy.SprawdzWaznoscBiletu(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                        listaBiletowOkresowych);
                    break;
                case 4:
                    UzytkownikZar.PrzedluzUlge(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                        listaBiletowOkresowych);
                    break;
                case 5:
                    UzytkownikZar.ZarejestrujSie(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                        listaBiletowOkresowych);
                    break;
            }
        }

        public static List <Pociag> Odjazdy(List<Pociag> bazaPociagow, bool rysujTablice)
        {

            //znajdowanie najdluzszej nazwy pociagu
            #region
            Pospieszny a;
            int dlugoscNazwyPociaguMax = 0;

            for (int i = 0; i < bazaPociagow.Count; i++) //znajdz najdluzsza nazwe pociagu
            {
                if (bazaPociagow[i] is Pospieszny)
                {
                    a = (Pospieszny)bazaPociagow[i];
                    if (a.NazwaPociagu.Length > dlugoscNazwyPociaguMax)
                    {
                        dlugoscNazwyPociaguMax = a.NazwaPociagu.Length;
                        //Console.WriteLine(a.NazwaPociagu + "\t" + dlugoscSlowaMax);
                    }
                }
            }
            #endregion
            //------------------------znajdowanie najdluzszej nazwy pociagu------------------------

            // znajdowanie najdluzszej nazwy trasy
            #region
            int dlugoscNazwyTrasy = 0;
            int dlugoscNazwyTrasyMax = 0;
            string nazwaTrasy;

            for (int i = 0; i < bazaPociagow.Count; i++) //znajdz najdluzsza nazwe trasy
            {
                nazwaTrasy = $"Bydgoszcz Gł. -> {bazaPociagow[i].StacjaDocelowa.NazwaStacji}";
                dlugoscNazwyTrasy = nazwaTrasy.Length;
                if (dlugoscNazwyTrasy > dlugoscNazwyTrasyMax)
                {
                    dlugoscNazwyTrasyMax = dlugoscNazwyTrasy;
                    //Console.WriteLine(nazwaTrasy + "\t" + dlugoscNazwyTrasy);
                }
            }
            #endregion
            //-------------znajdowanie najdluzszej nazwy trasy------------------

            List<Pociag> listaOdjazdowDzis = new List<Pociag>();
            List<Pociag> listaOdjazdowZaChwile = new List<Pociag>();

            DateTime now = DateTime.Now;

            //dodaj do listyOdjazdowDzis te pociagi, ktore kursuja dzisiaj - w tym momencie lub później
            for (int i = 0; i < bazaPociagow.Count; i++)
            {
                if (SprawdzDatePodrozy(bazaPociagow[i], DateTime.Now))
                {
                    if(bazaPociagow[i].GodzinaOdjazduZBdg.TimeOfDay>=now.TimeOfDay)   //IF KTORY SPRAWDZA GODZINE ODJAZDU
                    listaOdjazdowDzis.Add(bazaPociagow[i]);

                }
            }


            listaOdjazdowDzis = listaOdjazdowDzis.OrderBy(x => x.GodzinaOdjazduZBdg).ToList(); //posortuj odjazdy wg godzin odjazdu

            for (int i = 0; i < listaOdjazdowDzis.Count; i++) //dodaj do tablicy odjazdów pierwsze 5 najbliższych pociągów
            {
                if (listaOdjazdowZaChwile.Count < 5)
                    listaOdjazdowZaChwile.Add(listaOdjazdowDzis[i]);
                else break;
            }

            //rysuj tablice odjazdow jesli rysujTablice=true
            
            if (rysujTablice)
                RysujTabliceOdjazdow(dlugoscNazwyPociaguMax, dlugoscNazwyTrasyMax, listaOdjazdowZaChwile);

            

            //--------------------------rysuj tablice odjazdow-----------------------------

            

            return listaOdjazdowDzis;
        }
        
        public static bool SprawdzDatePodrozy(Pociag pociag, DateTime data)
        {
            bool sprawdz = false;

            int dzienTygodnia = 0;

            switch (data.DayOfWeek.ToString())
            {
                case "Monday":
                    dzienTygodnia = 0;
                    break;
                case "Tuesday":
                    dzienTygodnia = 1;
                    break;
                case "Wednesday":
                    dzienTygodnia = 2;
                    break;
                case "Thursday":
                    dzienTygodnia = 3;
                    break;
                case "Friday":
                    dzienTygodnia = 4;
                    break;
                case "Saturday":
                    dzienTygodnia = 5;
                    break;
                case "Sunday":
                    dzienTygodnia = 6;
                    break;

            }

            if (data >= pociag.DatyKursowania.PoczatekKursowania && data <= pociag.DatyKursowania.KoniecKursowania)
            {
                if (pociag.DatyBezKursowania == null)
                {
                    if (pociag.DniTygodniaKursowania[dzienTygodnia] == 1)
                        sprawdz = true;
                }

                else if (data < pociag.DatyBezKursowania.PoczatekKursowania || data > pociag.DatyBezKursowania.KoniecKursowania)
                {
                    
                    if (pociag.DniTygodniaKursowania[dzienTygodnia] == 1)
                        sprawdz = true;
                    //Console.WriteLine("NIENULLsprawdz: " + sprawdz);
                    //Console.ReadKey();
                }
            }
            //Console.WriteLine("sprawdz: " + sprawdz);
            //Console.ReadKey();
            return sprawdz;

        }

        static void RysujTabliceOdjazdow(int dlugoscNazwyPociaguMax, int dlugoscNazwyTrasyMax, List<Pociag> listaOdjazdowZaChwile)
        {
            Console.Clear();
            Helper.RysujZnaki(100, '*');
            Helper.RysujEntery(1);
            Console.WriteLine("\t\t\t\t\tTABLICA ODJAZDÓW\n");
            string pociag = "NAZWA";
            string relacja = "RELACJA";
            string spacje = "     ";
            string nazwaRelacji = "";
            string peron = "";
            string przewoznik = "INTERCITY";
            Console.WriteLine("GODZINA" + spacje + "PERON" + spacje + "TOR" + spacje + "PRZEWOŹNIK" + spacje +
                pociag.PadRight(dlugoscNazwyPociaguMax) + spacje + "NR POCIĄGU" + spacje + relacja.PadRight(dlugoscNazwyTrasyMax));
            Console.WriteLine();
            for (int i = 0; i < listaOdjazdowZaChwile.Count; i++)
            {
                Console.Write(listaOdjazdowZaChwile[i].GodzinaOdjazduZBdg.ToString("H:mm").PadRight(7));
                Console.Write(spacje);

                if (listaOdjazdowZaChwile[i].Peron.ToString() == "3.1")
                {
                    peron = "3A";
                    Console.Write(peron.PadRight(5));
                }
                else if (listaOdjazdowZaChwile[i].Peron.ToString() == "4.1")
                {
                    peron = "4A";
                    Console.Write(peron.PadRight(5));
                }
                else
                    Console.Write(listaOdjazdowZaChwile[i].Peron.ToString().PadRight(5));

                Console.Write(spacje);
                Console.Write(listaOdjazdowZaChwile[i].Tor.ToString().PadRight(3));
                Console.Write(spacje);
                if (listaOdjazdowZaChwile[i] is Osobowy)
                {
                    Osobowy o = (Osobowy)listaOdjazdowZaChwile[i];
                    Console.Write(o.Przewoznik.PadRight(10));
                }
                else
                    Console.Write(przewoznik.PadRight(10));

                Console.Write(spacje);

                if (listaOdjazdowZaChwile[i] is Pospieszny)
                {
                    Pospieszny p = (Pospieszny)listaOdjazdowZaChwile[i];
                    Console.Write(p.NazwaPociagu.PadRight(dlugoscNazwyPociaguMax));
                }
                else
                    Console.Write(spacje.PadRight(dlugoscNazwyPociaguMax));

                Console.Write(spacje);
                Console.Write(listaOdjazdowZaChwile[i].NumerPociagu.ToString().PadRight(10));
                Console.Write(spacje);
                nazwaRelacji = "BYDGOSZCZ GŁ. - " + listaOdjazdowZaChwile[i].StacjaDocelowa.NazwaStacji.ToUpper();
                Console.Write(nazwaRelacji.PadRight(dlugoscNazwyTrasyMax));
                Helper.RysujEntery(2);
            }
        }
    }
}
