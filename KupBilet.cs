using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dworzec
{
    class KupBilet
    {
        public static void Jednorazowy(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar,
            List<UzytkownikNiezar> bazaUzytkownikowNiezar, List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {
            Console.Clear();
            List<Pociag> listaOdjazdowZaChwile = Menu.Odjazdy(bazaPociagow, true);

            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("[0] Wróć do Menu Głównego");
            Console.WriteLine("[1] Kup bilet na najbliższy pociąg");
            Console.WriteLine("[2] Kup bilet na inny pociąg");
            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            int wybor = Helper.GetInt("Wybierz, co chcesz zrobic: ", 0, 2);

            switch (wybor)
            {
                case 0:
                    Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
                    break;
                case 1:
                    Najblizszy(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
                    break;
                case 2:
                    Inny(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, true, listaBiletowOkresowych);
                    break;
            }

        }
        static void Najblizszy(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar,
            List<UzytkownikNiezar> bazaUzytkownikowNiezar, List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {

            List<Pociag> listaOdjazdowZaChwile = Menu.Odjazdy(bazaPociagow, false);

            if (listaOdjazdowZaChwile.Count < 1)
            {
                Console.WriteLine("Aby kupić bilet na najbliższy pociąg,\nmusi istnieć przynajmniej 1 pociąg, który kursuje w najbliższym czasie.");
                Console.WriteLine("W najbliższym czasie nie kursuje żaden pociąg,\nzatem po naciśnięciu dowolnego klawisza powrócisz do Głównego Menu.");
                Console.ReadKey();
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            }

            int liczbaStacjiPosrednich;


            Pociag pociagWybrany = new Osobowy();
            StacjaGodzinaKilometry wyborStacji = new StacjaGodzinaKilometry();

            Console.Clear();
            Console.WriteLine(); //wypisz pociagi wraz z ich stacjami pośrednimi
            for (int i = 0; i < listaOdjazdowZaChwile.Count; i++)
            {
                Console.WriteLine("Pociąg [" + (i + 1) + "]\t" + "Odjazd: " + listaOdjazdowZaChwile[i].GodzinaOdjazduZBdg.ToString("HH:mm") +
                    "\tStacja docelowa: " + listaOdjazdowZaChwile[i].StacjaDocelowa.NazwaStacji);
                Console.WriteLine("Przez:");
                liczbaStacjiPosrednich = listaOdjazdowZaChwile[i].StacjePosrednie.Count;

                for (int j = 0; j < liczbaStacjiPosrednich - 1; j++)
                    Console.Write(listaOdjazdowZaChwile[i].StacjePosrednie[j].NazwaStacji + ", ");
                Console.WriteLine(listaOdjazdowZaChwile[i].StacjePosrednie[liczbaStacjiPosrednich - 1].NazwaStacji);
                Helper.RysujEntery(2);
            }

            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            // wybierz pociag, na ktorym ci zalezy

            int wybor = Helper.GetInt("Wybierz numer pociągu lub naciśnij 0 aby powrócić do Menu Głównego: ", 0, listaOdjazdowZaChwile.Count);

            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            if (wybor == 0)
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            else
                pociagWybrany = listaOdjazdowZaChwile[wybor - 1];


            if (pociagWybrany.MiejscaStojace == 0) //sprawdz czy w pociagu sa jeszcze wolne miejsca
            {
                Console.WriteLine("Brak miejsc w danym pociągu, nie można kupić biletu");
                Console.WriteLine("Po naciśnięciu przycisku powrócisz do Menu Głównego");
                Console.ReadKey();
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            }

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Stacja docelowa: " + pociagWybrany.StacjaDocelowa.NazwaStacji);
            Console.WriteLine("Stacje pośrednie:");
            Console.WriteLine();
            Console.WriteLine("Przyjazd");

            for (int i = 0; i < pociagWybrany.StacjePosrednie.Count; i++)
            {
                Console.WriteLine(pociagWybrany.StacjePosrednie[i].GodzinaPrzyjazdu.ToString("HH:mm") + "\t\t[" + (i + 1) + "] " + pociagWybrany.StacjePosrednie[i].NazwaStacji);
            }
            Console.WriteLine();
            Console.WriteLine(pociagWybrany.StacjaDocelowa.GodzinaPrzyjazdu.ToString("HH:mm") + "\t\t[0] " + pociagWybrany.StacjaDocelowa.NazwaStacji);
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            //wybierz stacje docelowa, do ktorej chcesz jechac
            wybor = Helper.GetInt("Wybierz stację docelową lub naciśnij -1 aby wrócić do Menu Głównego: ", -1, pociagWybrany.StacjePosrednie.Count);

            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            if (wybor == -1)
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            else if (wybor == 0)
                wyborStacji = pociagWybrany.StacjaDocelowa;
            else
                wyborStacji = pociagWybrany.StacjePosrednie[wybor - 1];

            GenerujBiletJednorazowy(bazaPociagow, pociagWybrany, wyborStacji, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                DateTime.Now, listaBiletowOkresowych);


        }

        static void Inny(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar,
            List<int> listaZajetychNumerowBiletow, bool jednorazowy, List<BiletOkresowy> listaBiletowOkresowych)
        {
            int n = 1;
            string wybranaStacja = "";
            Pociag pociagWybrany = new Pospieszny();
            List<string> listaStacjiDocelowych = StacjeDocelowe(bazaPociagow);
            List<Pociag> listaPociagowJadacychDoWybranejStacji = new List<Pociag>();

            foreach (string ss in listaStacjiDocelowych)
            { Console.WriteLine("[" + n + "] " + ss); n++; }

            //wybierz stacje docelowa
            int wybor = Helper.GetInt("Wybierz numer stacji docelowej lub naciśnij 0 aby powrócić do Menu Głównego: ", 0, listaStacjiDocelowych.Count);

            if (wybor == 0)
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            else
            {
                wybranaStacja = listaStacjiDocelowych[wybor - 1];
            }

            for (int i = 0; i < bazaPociagow.Count; i++) //stworz liste pociagow prowadzacych do wybranej stacji docelowej
            {
                if (wybranaStacja == bazaPociagow[i].StacjaDocelowa.NazwaStacji)
                {
                    listaPociagowJadacychDoWybranejStacji.Add(bazaPociagow[i]);
                    Console.WriteLine("DOCEL Dodano pociag nr: " + bazaPociagow[i].NumerPociagu);
                    Console.WriteLine(wybranaStacja + "\t" + bazaPociagow[i].StacjaDocelowa.NazwaStacji);
                }
                for (int j = 0; j < bazaPociagow[i].StacjePosrednie.Count; j++)
                {
                    if (wybranaStacja == bazaPociagow[i].StacjePosrednie[j].NazwaStacji)
                    {
                        listaPociagowJadacychDoWybranejStacji.Add(bazaPociagow[i]);
                        Console.WriteLine("POSR Dodano pociag nr: " + bazaPociagow[i].NumerPociagu);
                        Console.WriteLine(wybranaStacja + "\t"+j+"\t" + bazaPociagow[i].StacjePosrednie[j].NazwaStacji);
                    }
                }

            }

            //for (int i = 0; i < listaPociagowJadacychDoWybranejStacji.Count; i++)
            //    Console.WriteLine("Pociag nr "+listaPociagowJadacychDoWybranejStacji[i].NumerPociagu);

            //Console.ReadKey();

            DateTime data = WybierzDateOdjazdu(listaPociagowJadacychDoWybranejStacji, jednorazowy);
            List<Pociag> listaPociagowDlaWybranejDaty = new List<Pociag>();

            //Console.WriteLine("Ile pociagow dla wybranej daty1: " + listaPociagowDlaWybranejDaty.Count);
            //Console.ReadKey();

            for (int i = 0; i < listaPociagowJadacychDoWybranejStacji.Count; i++) //stworz liste pociagow dla wybranej daty
            {
                if (Menu.SprawdzDatePodrozy(listaPociagowJadacychDoWybranejStacji[i], data))
                {
                    listaPociagowDlaWybranejDaty.Add(listaPociagowJadacychDoWybranejStacji[i]);
                }
            }

            //Console.WriteLine("Ile pociagow dla wybranej daty2: " + listaPociagowDlaWybranejDaty.Count);
            //Console.ReadKey();
            //for (int i = 0; i < listaPociagowDlaWybranejDaty.Count; i++)
            //{
            //    Console.WriteLine(listaPociagowDlaWybranejDaty[i].NumerPociagu);
            //}
            //Console.ReadKey();

            Console.Clear();

            Pospieszny x;
            Pociag y;

            for (int i = 0; i < listaPociagowDlaWybranejDaty.Count; i++)
            {
                y = listaPociagowDlaWybranejDaty[i];
                Helper.RysujZnaki(60, '.');
                Helper.RysujEntery(2);
                Console.WriteLine("POCIĄG [" + (i + 1) + "]\t\tGodzina odjazdu: " + y.GodzinaOdjazduZBdg.ToString("HH:mm") + "\n");
                Console.Write("Numer pociągu: " + y.NumerPociagu);

                if (y is Pospieszny)
                {
                    x = (Pospieszny)y;
                    Console.Write("\tNazwa pociągu: " + x.NazwaPociagu);
                }
                Console.WriteLine("\nStacja docelowa: " + y.StacjaDocelowa.NazwaStacji + "\tGodzina przyjazdu: "
                    + y.StacjaDocelowa.GodzinaPrzyjazdu.ToString("HH:mm") + "\n");
                Console.WriteLine("Stacje pośrednie:");

                for (int j = 0; j < y.StacjePosrednie.Count; j++)
                {
                    Console.Write(y.StacjePosrednie[j].NazwaStacji + "\t");
                }
                Helper.RysujEntery(3);


            }

            wybor = Helper.GetInt("Wybierz pociąg lub naciśnij 0 aby powrócić do Menu Głównego: ", 0, listaPociagowDlaWybranejDaty.Count);


            if (wybor == 0)
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            else
            {
                pociagWybrany = listaPociagowDlaWybranejDaty[wybor - 1];
            }

            StacjaGodzinaKilometry wyborStacji = new StacjaGodzinaKilometry();

            //Console.WriteLine("pociag wybrany:" + pociagWybrany.StacjaDocelowa.NazwaStacji);

            //Console.WriteLine("wybrana stacja: " + wybranaStacja);
            //Console.ReadKey();

            if (pociagWybrany.StacjaDocelowa.NazwaStacji == wybranaStacja)
            {
                wyborStacji = pociagWybrany.StacjaDocelowa;
            }
            else
            {
                for (int i = 0; i < pociagWybrany.StacjePosrednie.Count; i++)
                {
                    if (pociagWybrany.StacjePosrednie[i].NazwaStacji == wybranaStacja)
                    {
                        wyborStacji = pociagWybrany.StacjePosrednie[i];
                        break;
                    }
                }
            }

            


            if (jednorazowy)
                GenerujBiletJednorazowy(bazaPociagow, pociagWybrany, wyborStacji, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                    data, listaBiletowOkresowych);
            else
            {
                GenerujBiletOkresowy(bazaPociagow, pociagWybrany, wyborStacji, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow,
                    data, listaBiletowOkresowych);
            }

        }
        public static void Okresowy(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar,
            List<UzytkownikNiezar> bazaUzytkownikowNiezar, List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {
            Inny(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, false, listaBiletowOkresowych);

        }

        static List<string> StacjeDocelowe(List<Pociag> bazaPociagow)
        {
            List<string> listaStacjiDocelowych = new List<string>(); //utworz liste wszystkich mozliwych stacji docelowych


            foreach (Pociag p in bazaPociagow)
            {
                if (!listaStacjiDocelowych.Contains(p.StacjaDocelowa.NazwaStacji))
                    listaStacjiDocelowych.Add(p.StacjaDocelowa.NazwaStacji);
                foreach (StacjaGodzinaKilometry s in p.StacjePosrednie)
                {
                    if (!listaStacjiDocelowych.Contains(s.NazwaStacji))
                        listaStacjiDocelowych.Add(s.NazwaStacji);
                }
            }

            listaStacjiDocelowych.Sort();

            return listaStacjiDocelowych;
        }


        static DateTime WybierzDateOdjazdu(List<Pociag> listaPociagowJadacychDoWybranejStacji, bool jednorazowy)
        {
            bool czyTegoDniaKursujePociag = false;
            DateTime data;
            Pociag pociag;


            Console.Clear();

            List<DateTime> listaDat = new List<DateTime>();
            List<DateTime> listaDatMozliwych = new List<DateTime>();
            if (jednorazowy == false)
                listaDat.Add(DateTime.Now);
            listaDat.Add(DateTime.Now.AddDays(1));
            listaDat.Add(DateTime.Now.AddDays(2));
            listaDat.Add(DateTime.Now.AddDays(3));
            listaDat.Add(DateTime.Now.AddDays(4));
            listaDat.Add(DateTime.Now.AddDays(5));
            listaDat.Add(DateTime.Now.AddDays(6));
            listaDat.Add(DateTime.Now.AddDays(7));


            for (int i = 0; i < listaDat.Count; i++)
            {
                data = listaDat[i];
                for (int j = 0; j < listaPociagowJadacychDoWybranejStacji.Count; j++)
                {
                    pociag = listaPociagowJadacychDoWybranejStacji[j];
                    czyTegoDniaKursujePociag = Menu.SprawdzDatePodrozy(pociag, data);
                }
                if (czyTegoDniaKursujePociag)
                    listaDatMozliwych.Add(data);
            }

            for (int i = 0; i < listaDatMozliwych.Count; i++)
                Console.WriteLine("[" + i + "] " + listaDatMozliwych[i].ToShortDateString());

            int wybor = 0;
            if (jednorazowy)
                wybor = Helper.GetInt("Wybierz datę podróży: ", 0, listaDatMozliwych.Count - 1);
            else
                wybor = Helper.GetInt("Wybierz datę początku ważności biletu okresowego: ", 0, listaDatMozliwych.Count - 1);

            return listaDatMozliwych[wybor];
        }

        static void GenerujBiletJednorazowy(List<Pociag> bazaPociagow, Pociag pociagWybrany, StacjaGodzinaKilometry wyborStacji,
            List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar, List<int> listaZajetychNumerowBiletow,
            DateTime dataPodrozy, List<BiletOkresowy> listaBiletowOkresowych)
        {

            var rand = new Random();
            int numerBiletu = 0;
            string rodzajMiejsca = "";
            int miejsce = 0;
            int wagon = 0;
            int wyborUlgi = 0;
            double cenaBiletuPodst = 0;
            double cenaBiletuOstateczna = 0;
            double odleglosc = 0;
            double ulga = 0;
            Wagon wagonPospieszny = new Wagon(1, 1);
            Pospieszny wybranyPospieszny = new Pospieszny();
            Osobowy wybranyOsobowy = new Osobowy();
            UzytkownikZar uZar;

            //wprowadz dane uzytkownika
            Uzytkownik u = WprowadzDaneUzytkownika(bazaUzytkownikowZar, bazaUzytkownikowNiezar);

            

            while (true) //generuj losowy numer biletu tak długo, aż nie będzie go na liście zajętych numerów biletów
            {
                numerBiletu = rand.Next();
                if (!listaZajetychNumerowBiletow.Contains(numerBiletu))
                    break;
            }

            //Console.WriteLine("wygenerowany nr biletu: " + numerBiletu);
            //Console.ReadKey();
            listaZajetychNumerowBiletow.Add(numerBiletu);

            odleglosc = wyborStacji.Kilometry;

            if (pociagWybrany is Pospieszny) //uzupelnij dane do biletu dla pociagu Pospiesznego
            {
                wybranyPospieszny = (Pospieszny)pociagWybrany;
                wagonPospieszny = WybierzWagon(wybranyPospieszny);

                if (wagonPospieszny == null) //brak miejsc siedzacych w pociagu, bedzie miejsce stojace
                {
                    miejsce = 0;
                    rodzajMiejsca = "miejsce stojące";
                    wybranyPospieszny.MiejscaStojace -= 1; //usuń jedno miejsce z liczby miejsc stojących
                    wagon = 0;
                }
                else
                {
                    wagon = wagonPospieszny.NrWagonu;
                    miejsce = WybierzMiejsce(wybranyPospieszny, wagonPospieszny);
                    wybranyPospieszny.MiejscaSiedzace -= 1;

                    rodzajMiejsca = "miejsce siedzące";
                }
               

                for (int i = 0; i < CennikKilometrowy.CennikPospieszny().Count; i++) //oblicz cene
                {
                    if (odleglosc >= CennikKilometrowy.CennikPospieszny()[i].MinKm
                        && odleglosc <= CennikKilometrowy.CennikPospieszny()[i].MaxKm)
                    {
                        cenaBiletuPodst = CennikKilometrowy.CennikPospieszny()[i].OplataZaOdcinek;
                        break;
                    }
                }


            }
            else //uzupelnij dane do biletu dla pociagu Osobowego
            {
                wybranyOsobowy = (Osobowy)pociagWybrany;
                miejsce = 0;
                wagon = 0;
                if (wybranyOsobowy.MiejscaSiedzace > 0)
                {
                    wybranyOsobowy.MiejscaSiedzace -= 1;
                    rodzajMiejsca = "miejsce siedzące";
                    //Console.WriteLine(wybranyOsobowy.NumerPociagu + "\t\t" + wybranyOsobowy.MiejscaSiedzace);
                }
                else
                {
                    wybranyOsobowy.MiejscaStojace -= 1;
                    rodzajMiejsca = "miejsce stojące";
                    //Console.WriteLine(wybranyOsobowy.NumerPociagu + "\t\t" + wybranyOsobowy.MiejscaSiedzace);
                }
                //Console.ReadKey();

                if (wybranyOsobowy.Przewoznik.ToUpper() == "ARRIVA") //oblicz cene dla Arrivy
                {
                    for (int i = 0; i < CennikKilometrowy.CennikArriva().Count; i++)
                    {
                        if (odleglosc >= CennikKilometrowy.CennikArriva()[i].MinKm
                            && odleglosc <= CennikKilometrowy.CennikArriva()[i].MaxKm)
                        {
                            cenaBiletuPodst = CennikKilometrowy.CennikArriva()[i].OplataZaOdcinek;
                            break;
                        }
                    }
                }
                else // oblicz cene dla Polregio
                {
                    for (int i = 0; i < CennikKilometrowy.CennikPolregio().Count; i++) //oblicz cene
                    {
                        if (odleglosc >= CennikKilometrowy.CennikPolregio()[i].MinKm
                            && odleglosc <= CennikKilometrowy.CennikPolregio()[i].MaxKm)
                        {
                            cenaBiletuPodst = CennikKilometrowy.CennikPolregio()[i].OplataZaOdcinek;
                            break;
                        }
                    }
                }

            }
            if (u is UzytkownikZar) //dodaj ulge do ceny
            {
                uZar = (UzytkownikZar)u;
                if (uZar.RodzajStalejUlgi != null)
                {
                    ulga = uZar.RodzajStalejUlgi.WysokoscUlgi / 100.0;
                }
                else
                    ulga = 0;
                //Console.WriteLine("ulga: " + ulga);
            }
            else
            {
                Console.Clear();

                for (int i = 0; i < WszystkieUlgi.ListaUlg().Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + WszystkieUlgi.ListaUlg()[i].RodzajUlgi);
                }
                wyborUlgi = Helper.GetInt("Wybierz rodzaj ulgi, który posiadasz lub naciśnij -1, jeżeli nie masz ulgi:", -1, WszystkieUlgi.ListaUlg().Count - 1);

                if (wyborUlgi == -1)
                    ulga = 0;
                else
                    ulga = (WszystkieUlgi.ListaUlg()[wyborUlgi].WysokoscUlgi) / 100.0;

            }
            Console.WriteLine("Podstawowa cena biletu: " + cenaBiletuPodst);
            

            cenaBiletuOstateczna = Math.Round(((1 - ulga) * cenaBiletuPodst), 2); //oblicz ostateczna cene biletu
            if(ulga!=0)
                Console.WriteLine("Cena biletu z ulgą: " + cenaBiletuOstateczna);
            Helper.RysujEntery(2);
            Console.WriteLine("Zakup biletu nastąpi po naciśnięciu dowolnego przycisku.");
            
            Console.ReadKey();



            if (cenaBiletuOstateczna > u.StanKonta) //sprawdz czy stac uzytkownika na bilet
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Brak funduszy na zakup biletu.");
                Console.ResetColor();

                Console.WriteLine("Po naciśnięciu dowolnego przycisku powrócisz do Menu Głównego");
                Console.ReadKey();
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            }
            //odejmij cene biletu ze stanu konta uzytkownika
            if (u is UzytkownikNiezar)
            {
                u = (UzytkownikNiezar)u;
                u.StanKonta -= cenaBiletuOstateczna;
            }
            else
            {
                u = (UzytkownikZar)u;
                u.StanKonta -= cenaBiletuOstateczna;
            }
            
            //stworz obiekt bilet
            BiletJednorazowy bilet = new BiletJednorazowy(numerBiletu, pociagWybrany, miejsce, wagon);

            bilet.CelPozdrozy = wyborStacji;
            bilet.Cena = cenaBiletuOstateczna;
            bilet.ObliczCzasPodrozy();
            bilet.DataPodrozy = dataPodrozy;
            bilet.DataZakupu = DateTime.Now.Date;
            bilet.ObliczWaznoscBiletu();

            //-----------------stworz obiekt bilet--------------------

            
            //rysuj bilet
            RysujBiletJednorazowy(bilet, pociagWybrany, wyborStacji, u, wybranyPospieszny, rodzajMiejsca, wybranyOsobowy);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Zakup biletu zrealizowano pomyślnie");
            Console.ResetColor();

            Helper.RysujEntery(5);
            Console.WriteLine("Po naciśnięciu dowolnego przycisku, powrócisz do Menu Główego");
            Console.ReadKey();
            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
        }

        static void GenerujBiletOkresowy(List<Pociag> bazaPociagow, Pociag pociagWybrany, StacjaGodzinaKilometry wyborStacji,
            List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar, List<int> listaZajetychNumerowBiletow,
            DateTime dataPodrozy, List<BiletOkresowy> listaBiletowOkresowych)
        {


            var rand = new Random();
            int numerBiletu = 0;
            double odleglosc = wyborStacji.Kilometry;
            double cenaBiletuPodst = 0;
            double cenaBiletuOstateczna = 0;
            Osobowy wybranyOsobowy = new Osobowy();
            Pospieszny wybranyPospieszny = new Pospieszny();
            UzytkownikZar uZar;
            double ulga = 0;

            //wprowadz dane uzytkownika
            Uzytkownik u = WprowadzDaneUzytkownika(bazaUzytkownikowZar, bazaUzytkownikowNiezar);

            while (true) //generuj losowy numer biletu tak długo, aż nie będzie go na liście zajętych numerów biletów
            {
                numerBiletu = rand.Next();
                if (!listaZajetychNumerowBiletow.Contains(numerBiletu))
                    break;
            }



            listaZajetychNumerowBiletow.Add(numerBiletu);

            //oblicz cene podstawowa
            if (pociagWybrany is Pospieszny) //cena podst dla pospiesznego
            {
                wybranyPospieszny = (Pospieszny)pociagWybrany;

                for (int i = 0; i < CennikKilometrowy.CennikPospieszny().Count; i++) //oblicz cene
                {
                    if (odleglosc >= CennikKilometrowy.CennikPospieszny()[i].MinKm
                        && odleglosc <= CennikKilometrowy.CennikPospieszny()[i].MaxKm)
                    {
                        cenaBiletuPodst = CennikKilometrowy.CennikPospieszny()[i].OplataZaOdcinek;
                        break;
                    }
                }
            }
            else //cena podst dla osobowego
            {
                wybranyOsobowy = (Osobowy)pociagWybrany;
                if (wybranyOsobowy.Przewoznik.ToUpper() == "ARRIVA") //oblicz cene dla Arrivy
                {
                    for (int i = 0; i < CennikKilometrowy.CennikArriva().Count; i++)
                    {
                        if (odleglosc >= CennikKilometrowy.CennikArriva()[i].MinKm
                            && odleglosc <= CennikKilometrowy.CennikArriva()[i].MaxKm)
                        {
                            cenaBiletuPodst = CennikKilometrowy.CennikArriva()[i].OplataZaOdcinek;
                            break;
                        }
                    }
                }
                else // oblicz cene dla Polregio
                {
                    for (int i = 0; i < CennikKilometrowy.CennikPolregio().Count; i++) //oblicz cene
                    {
                        if (odleglosc >= CennikKilometrowy.CennikPolregio()[i].MinKm
                            && odleglosc <= CennikKilometrowy.CennikPolregio()[i].MaxKm)
                        {
                            cenaBiletuPodst = CennikKilometrowy.CennikPolregio()[i].OplataZaOdcinek;
                            break;
                        }
                    }
                }
            }
            if (u is UzytkownikZar) //dodaj ulge do ceny
            {
                uZar = (UzytkownikZar)u;
                if (uZar.RodzajStalejUlgi != null)
                    ulga = uZar.RodzajStalejUlgi.WysokoscUlgi / 100.0;
                else
                    ulga = 0;

                //Console.WriteLine("ulga: " + ulga);
            }
            else
            {
                Console.Clear();
                for (int i = 0; i < WszystkieUlgi.ListaUlg().Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + WszystkieUlgi.ListaUlg()[i].RodzajUlgi);
                }
                int wyborUlgi = Helper.GetInt("Wybierz rodzaj ulgi, który posiadasz lub wciśnij -1, jeżeli nie masz ulgi: ", -1, WszystkieUlgi.ListaUlg().Count - 1);

                if (wyborUlgi == -1)
                    ulga = 0;
                else
                    ulga = (WszystkieUlgi.ListaUlg()[wyborUlgi].WysokoscUlgi) / 100.0;

            }
            //Console.WriteLine("cena biletu " + cenaBiletuPodst);
            //Console.ReadKey();


            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("[1] Bilet TAM");
            Console.WriteLine("[2] Bilet TU");
            Console.WriteLine("[3] Bilet TUITAM");
            Console.WriteLine("[0] Wyjście");
            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            int wybor = Helper.GetInt("Wybierz rodzaj biletu: ", 0, 3);
            double mnoznik = 0;
            string stronyBiletu = "";
            switch (wybor)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    stronyBiletu = "TAM";
                    mnoznik = 0.25;
                    break;
                case 2:
                    stronyBiletu = "TU";
                    mnoznik = 0.25;
                    break;
                case 3:
                    stronyBiletu = "TUITAM";
                    mnoznik = 0.5;
                    break;
            }


            Console.Clear();
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("[1] Bilet MIESIĘCZNY");
            Console.WriteLine("[2] Bilet KWARTALNY");
            Console.WriteLine("[3] Bilet PÓŁROCZNY");
            Console.WriteLine("[4] Bilet ROCZNY");
            Console.WriteLine("[0] Wyjście");
            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            wybor = Helper.GetInt("Wybierz rodzaj biletu: ", 0, 4);
            double okresCzasu = 0;
            string okresBiletu = "";
            switch (wybor)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    okresBiletu = "MIESIECZNY";
                    okresCzasu = 30;
                    break;
                case 2:
                    okresBiletu = "KWARTALNY";
                    okresCzasu = 90;
                    break;
                case 3:
                    okresBiletu = "POLROCZNY";
                    okresCzasu = 183;
                    break;
                case 4:
                    okresBiletu = "ROCZNY";
                    okresCzasu = 366;
                    break;
            }


            cenaBiletuOstateczna = Math.Round((cenaBiletuPodst * okresCzasu * mnoznik * (1 - ulga)), 2); //oblicz ostateczna cene biletu

            //Console.WriteLine("BILET " + okresBiletu + "\nOstateczna cena biletu:" + cenaBiletuOstateczna);
            //Console.WriteLine("Stan konta: " + u.StanKonta);

            if (cenaBiletuOstateczna > u.StanKonta) //sprawdz czy stac uzytkownika na bilet
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Brak funduszy na zakup biletu.");
                Console.ResetColor();
                Console.WriteLine("Po naciśnięciu dowolnego przycisku powrócisz do Menu Głównego");
                Console.ReadKey();
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            }
            //odejmij cene biletu ze stanu konta uzytkownika
            if (u is UzytkownikNiezar)
            {
                u = (UzytkownikNiezar)u;
                u.StanKonta -= cenaBiletuOstateczna;
            }
            else
            {
                u = (UzytkownikZar)u;
                u.StanKonta -= cenaBiletuOstateczna;
            }


            //stworz obiekt bilet

            BiletOkresowy bilet = new BiletOkresowy(numerBiletu, pociagWybrany, stronyBiletu, okresBiletu);
            bilet.CelPozdrozy = wyborStacji;
            bilet.Pociag = pociagWybrany;
            bilet.Cena = cenaBiletuOstateczna;
            bilet.ObliczCzasPodrozy();
            bilet.DataZakupu = dataPodrozy;
            bilet.ObliczWaznoscBiletu();
            bilet.WlascicielBiletu = u;

            listaBiletowOkresowych.Add(bilet);

            //-----------------stworz obiekt bilet--------------------

            RysujBiletOkresowy(bilet, pociagWybrany, wyborStacji, u, wybranyPospieszny, wybranyOsobowy);

            
            Helper.RysujEntery(5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Zakup biletu zrealizowano pomyślnie");
            Console.ResetColor();
            Console.WriteLine("Po naciśnięciu dowolnego przycisku, powrócisz do Menu Główego");
            Console.ReadKey();
            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);


        }


        static void RysujBiletJednorazowy(BiletJednorazowy bilet, Pociag pociagWybrany, StacjaGodzinaKilometry wyborStacji, Uzytkownik u,
            Pospieszny wybranyPospieszny, string rodzajMiejsca, Osobowy wybranyOsobowy)
        {
            //utworz nazwe pliku z biletem
            string nazwaPliku = @".\bilet nr " + bilet.NumerBiletu.ToString() + ".txt";


            //--------------------------utworz nazwe pliku z biletem--------------------------------------


            //zapisz bilet do pliku

            StreamWriter writer = new StreamWriter(nazwaPliku);
            try
            {
                using (writer)
                {
                    writer.WriteLine("                                          &&&&&&&&&");
                    writer.WriteLine("                                        &&&");
                    writer.WriteLine("                                       &&");
                    writer.WriteLine("                                      &  _____ ___________");
                    writer.WriteLine("                                     II__|[] | |   I I   |");
                    writer.WriteLine("                                    |        |_|_  I I  _|");
                    writer.WriteLine("                                   < OO----OOO   OO---OO");
                    writer.WriteLine("**********************************************************");
                    writer.WriteLine();
                    writer.WriteLine("BILET JEDNORAZOWY\n");
                    writer.WriteLine("Numer biletu: " + bilet.NumerBiletu);
                    writer.WriteLine("\n\nOd: Bydgoszcz Główna");
                    writer.WriteLine("Czas odjazdu: " + pociagWybrany.GodzinaOdjazduZBdg.ToString("HH:mm"));
                    writer.WriteLine("\n\nDo: " + bilet.CelPozdrozy.NazwaStacji);
                    writer.WriteLine("Czas przyjazdu: " + wyborStacji.GodzinaPrzyjazdu.ToString("HH:mm"));
                    writer.WriteLine("\n\nData podróży: " + bilet.DataPodrozy.ToString("dd.MM.yyyy"));
                    writer.WriteLine("Nr pociągu: " + pociagWybrany.NumerPociagu);

                    if (pociagWybrany is Pospieszny)
                    {
                        writer.WriteLine("Nazwa pociagu: " + wybranyPospieszny.NazwaPociagu);
                        writer.WriteLine("Rodzaj pociągu: pospieszny");
                        if (rodzajMiejsca == "miejsce siedzące")
                        {
                            writer.WriteLine("\n\nNumer wagonu: " + bilet.NrWagonu);
                            writer.Write("Numer miejsca: " + bilet.NrMiejsca);
                            if (bilet.NrMiejsca % 2 == 0)
                                writer.WriteLine("\t okno");
                            else
                                writer.WriteLine("\t korytarz");
                        }
                        else
                            writer.WriteLine("BRAK MIEJSCÓWKI: brak gwarancji miejsca do siedzenia");

                    }
                    else
                    {
                        writer.WriteLine("Rodzaj pociągu: osobowy");
                        writer.WriteLine("Przewoźnik: " + wybranyOsobowy.Przewoznik);
                        writer.WriteLine("\nRodzaj miejsca: " + rodzajMiejsca);
                    }

                    writer.WriteLine("\n\nCena: " + bilet.Cena + " zł");
                    writer.WriteLine("Dane użytkownika: " + u.Imie.ToUpper() + " " + u.Nazwisko.ToUpper());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                System.Diagnostics.Process.Start("notepad", nazwaPliku);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Console.Clear();

        }


        static void RysujBiletOkresowy(BiletOkresowy bilet, Pociag pociagWybrany, StacjaGodzinaKilometry wyborStacji, Uzytkownik u,
            Pospieszny wybranyPospieszny, Osobowy wybranyOsobowy)
        {
            //utworz nazwe pliku z biletem
            string nazwaPliku = @".\bilet nr " + bilet.NumerBiletu.ToString() + ".txt";


            //--------------------------utworz nazwe pliku z biletem--------------------------------------


            //zapisz bilet do pliku

            StreamWriter writer = new StreamWriter(nazwaPliku);
            try
            {

                using (writer)
                {
                    writer.WriteLine("                                          &&&&&&&&&");
                    writer.WriteLine("                                        &&&");
                    writer.WriteLine("                                       &&");
                    writer.WriteLine("                                      &  _____ ___________");
                    writer.WriteLine("                                     II__|[] | |   I I   |");
                    writer.WriteLine("                                    |        |_|_  I I  _|");
                    writer.WriteLine("                                   < OO----OOO   OO---OO");
                    writer.WriteLine("**********************************************************");
                    writer.WriteLine();

                    if (bilet.OkresBiletu == "MIESIECZNY")
                    {
                        writer.WriteLine("BILET MIESIĘCZNY");
                    }
                    else if (bilet.OkresBiletu == "KWARTALNY")
                    {
                        writer.WriteLine("BILET KWARTALNY");
                    }
                    else if (bilet.OkresBiletu == "POLROCZNY")
                    {
                        writer.WriteLine("BILET PÓŁROCZNY");
                    }
                    else //roczny
                    {
                        writer.WriteLine("BILET ROCZNY");
                    }
                    writer.WriteLine("\nNumer biletu: " + bilet.NumerBiletu);
                    writer.WriteLine("\nOd: Bydgoszcz Główna");
                    writer.WriteLine("Czas odjazdu: " + pociagWybrany.GodzinaOdjazduZBdg.ToString("HH:mm"));
                    writer.WriteLine("\nDo: " + bilet.CelPozdrozy.NazwaStacji);
                    writer.WriteLine("Czas przyjazdu: " + wyborStacji.GodzinaPrzyjazdu.ToString("HH:mm"));
                    writer.WriteLine("\nNr pociągu: " + pociagWybrany.NumerPociagu);

                    if (pociagWybrany is Pospieszny)
                    {
                        writer.WriteLine("\nNazwa pociagu: " + wybranyPospieszny.NazwaPociagu);
                        writer.WriteLine("Rodzaj pociągu: pospieszny");
                    }
                    else
                    {
                        writer.WriteLine("\nRodzaj pociągu: osobowy");
                        writer.WriteLine("Przewoźnik: " + wybranyOsobowy.Przewoznik);
                    }
                    bilet.ObliczWaznoscBiletu();

                    writer.WriteLine("\nWażność biletu: ");
                    writer.WriteLine("Od: " + bilet.WaznoscBiletu.PoczatekKursowania.ToString("dd.MM.yyyy H:mm"));
                    writer.WriteLine("Do: " + bilet.WaznoscBiletu.KoniecKursowania.ToString("dd.MM.yyyy H:mm"));
                    writer.WriteLine("\nCena: " + bilet.Cena + " zł");
                    writer.WriteLine("Dane użytkownika: " + bilet.WlascicielBiletu.Imie.ToUpper() + " " + bilet.WlascicielBiletu.Nazwisko.ToUpper());


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                System.Diagnostics.Process.Start("notepad", nazwaPliku);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }


            Console.Clear();

        }
        static Uzytkownik WprowadzDaneUzytkownika(List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar)
        {
            UzytkownikZar z;
            UzytkownikNiezar n = new UzytkownikNiezar("a", "a", 1);
            bool zar = false;
            bool niezarJestNaLiscie = false;
            string imie = "";
            string nazwisko = "";
            double stanKonta = 0;


            Console.Clear();
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("[1] Użytkownik niezarejestrowany");
            Console.WriteLine("[2] Użytkownik zarejestrowany");
            Console.WriteLine("[0] Wyjście");
            Helper.RysujEntery(2);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            int wybor = Helper.GetInt("Wybierz, co chcesz zrobic: ", 0, 2);

            switch (wybor)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    break;
                case 2:
                    zar = true;
                    break;
            }

            if (zar)
            {
                for (int i = 0; i < bazaUzytkownikowZar.Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + bazaUzytkownikowZar[i].Imie.ToUpper()
                        + " " + bazaUzytkownikowZar[i].Nazwisko.ToUpper());
                }
                wybor = Helper.GetInt("Wybierz, swoje dane: ", 0, bazaUzytkownikowZar.Count - 1);
                z = bazaUzytkownikowZar[wybor];
                return z;
            }
            else
            {
                imie = Helper.GetString("Podaj swoje imię: ");
                Console.WriteLine();
                nazwisko = Helper.GetString("Podaj swoje nazwisko: ");
                Console.WriteLine();

                if (bazaUzytkownikowNiezar.Count >= 1)
                {
                    for (int i = 0; i < bazaUzytkownikowNiezar.Count; i++)
                    {
                        if (imie.ToUpper() == bazaUzytkownikowNiezar[i].Imie.ToUpper()
                            && nazwisko.ToUpper() == bazaUzytkownikowNiezar[i].Nazwisko.ToUpper())
                        {
                            n = bazaUzytkownikowNiezar[i];
                            niezarJestNaLiscie = true;
                            break;
                        }
                    }

                    if (!niezarJestNaLiscie)
                    {
                        stanKonta = Helper.GetDouble("Podaj swój stan konta:", 0);
                        n = new UzytkownikNiezar(imie, nazwisko, stanKonta);
                        bazaUzytkownikowNiezar.Add(n);
                    }
                }
                else
                {
                    stanKonta = Helper.GetDouble("Podaj swój stan konta:", 0);
                    n = new UzytkownikNiezar(imie, nazwisko, stanKonta);
                    bazaUzytkownikowNiezar.Add(n);
                }




                return n;
            }
        }
        static Wagon WybierzWagon(Pospieszny wybranyPospieszny)
        {

            Wagon wagon = new Wagon(1, 1);
            bool czySaWolneMiejscaSiedzace = false;

            for (int i = 0; i < wybranyPospieszny.ListaWagonow.Count; i++) //sprawdz czy sa wolne miejsca siedzace w wagonach i wybierz jeden wagon
            {
                if (wybranyPospieszny.ListaWagonow[i].LiczbaWolnychMiejsc > 0)
                {
                    wagon = wybranyPospieszny.ListaWagonow[i];
                    czySaWolneMiejscaSiedzace = true;
                    break;
                }
            }
            if (czySaWolneMiejscaSiedzace)//jesli sa wolne miejsca w wagonie, zwroc wagon
            {
                //Console.WriteLine("nr wagonu: " + wagon.NrWagonu + "liczba msc:" + wagon.LiczbaWolnychMiejsc);
                return wagon;
            }

            else
                return null;


        }
        static int WybierzMiejsce(Pospieszny wybranyPospieszny, Wagon wagon)
        {
            int miejsce = 0;

            if (wybranyPospieszny.ListaWagonow.Contains(wagon))
            {
                miejsce = wagon.LiczbaWolnychMiejsc;
                wagon.LiczbaWolnychMiejsc -= 1;
                //Console.WriteLine("Wybierz miejsce: " + miejsce);

                //foreach (Wagon w in wybranyPospieszny.ListaWagonow)
                //    Console.WriteLine(w.NrWagonu + ":\t\t" + w.LiczbaWolnychMiejsc);
            }
            return miejsce;
        }


    }
}

