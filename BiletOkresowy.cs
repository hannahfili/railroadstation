using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class BiletOkresowy : Bilet
    {
        private string _stronyBiletu;
        private string _okresBiletu;
        private Kursowanie _waznoscBiletu;
        private Uzytkownik _wlascicielBiletu;



        public string StronyBiletu
        {
            get => _stronyBiletu;
            set
            {
                string o1 = "TAM";
                string o2 = "TU";
                string o3 = "TUITAM";

                if (value.ToUpper() == o1 || value.ToUpper() == o2 || value.ToUpper() == o3)
                    _stronyBiletu = value;
                else throw new ArgumentException("Brak poprawnej nazwy rodzaju biletu. Może być: TU, TAM, TUITAM");

            }
        }
        public string OkresBiletu
        {
            get => _okresBiletu;
            set
            {
                string o1 = "MIESIECZNY";
                string o2 = "KWARTALNY";
                string o3 = "POLROCZNY";
                string o4 = "ROCZNY";
                if (value.ToUpper() == o1 || value.ToUpper() == o2 || value.ToUpper() == o3 || value.ToUpper() == o4)
                    _okresBiletu = value.ToUpper();
                else throw new ArgumentException("Brak poprawnej nazwy rodzaju biletu. Może być: miesieczny, kwartalny, polroczny, roczny");
            }
        }

        public Kursowanie WaznoscBiletu { get => _waznoscBiletu; }
        public Uzytkownik WlascicielBiletu { get => _wlascicielBiletu; set => _wlascicielBiletu = value; }

        public override void ObliczWaznoscBiletu()
        {
            DateTime koniecKursowania = new DateTime();

            switch (_okresBiletu)
            {
                case "MIESIECZNY":
                    koniecKursowania = _dataZakupu.AddDays(30);
                    break;
                case "KWARTALNY":
                    koniecKursowania = _dataZakupu.AddDays(90);
                    break;
                case "POLROCZNY":
                    koniecKursowania = _dataZakupu.AddDays(183);
                    break;
                case "ROCZNY":
                    koniecKursowania = _dataZakupu.AddDays(366);
                    break;
            }
            _waznoscBiletu = new Kursowanie(_dataZakupu, koniecKursowania);

        }

        public BiletOkresowy(int numerBiletu, Pociag pociag, string stronyBiletu, string okresBiletu)
            : base(numerBiletu, pociag)
        {
            StronyBiletu = stronyBiletu;
            OkresBiletu = okresBiletu;
        }

        public BiletOkresowy()
        {
        }

        public static void SprawdzWaznoscBiletu(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar,
            List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {
            Console.Clear();
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("SPRAWDZANIE WAŻNOŚCI BILETU OKRESOWEGO");
            Helper.RysujEntery(3);

            Console.WriteLine("[1] Sprawdź, podając dane osobowe (tylko dla użytkowników zarejestrowanych)");
            Console.WriteLine("[2] Sprawdź, podając numer biletu");
            Console.WriteLine("[0] Powrót do Menu Głównego");

            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            int wybor = Helper.GetInt("Wybierz opcję: ", 0, 2);

            switch (wybor)
            {
                case 0:
                    Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
                    break;
                case 1:
                    SprWaznoscPrzezDane(bazaUzytkownikowZar, listaBiletowOkresowych);
                    break;
                case 2:
                    SprWaznoscPrzezNumer(listaBiletowOkresowych);
                    break;
            }
            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);

        }

        public static int SprWaznoscPrzezDane(List<UzytkownikZar> bazaUzytkownikowZar, List<BiletOkresowy> listaBiletowOkresowych)
        {
            
            int powrot = 0;

            UzytkownikZar z;
            BiletOkresowy bilet = new BiletOkresowy();
            List<BiletOkresowy> listaBiletowJednegoWlasciciela = new List<BiletOkresowy>();

            Console.Clear();
            string imie = Helper.GetString("Podaj imię: ");
            string nazwisko = Helper.GetString("Podaj nazwisko: ");
            int numer_tel = Helper.GetInt("Podaj numer telefonu: ", 100000000, 999999999);

            z = new UzytkownikZar(imie, nazwisko, 100, "przykladowy@gmail.com", numer_tel, WszystkieUlgi.ListaUlg()[0], new DateTime(2022, 1, 1));



            for (int i = 0; i < listaBiletowOkresowych.Count; i++)
            {
                if (listaBiletowOkresowych[i].WlascicielBiletu.Equals(z))
                {

                    z = (UzytkownikZar)listaBiletowOkresowych[i].WlascicielBiletu;
                    bilet = listaBiletowOkresowych[i];
                    listaBiletowJednegoWlasciciela.Add(bilet);
                }
            }


            if (listaBiletowJednegoWlasciciela.Count == 0)
            {
                Console.WriteLine("Podany użytkownik nie posiada biletu okresowego.");
                Console.WriteLine("Po naciśnięciu dowolnego klawisza, powrócisz do Menu Głównego");
                Console.ReadKey();
                return powrot;
            }
            else if (listaBiletowJednegoWlasciciela.Count == 1)
            {

                Rysuj(bilet); //rysuj dane dot. biletu

                Console.WriteLine();
                Console.WriteLine("Po naciśnięciu dowolnego klawisza, powrócisz do Menu Głównego");
                Console.ReadKey();

                return powrot;
            }
            else //wlasciciel ma wiecej niz jeden bilet okresowy
            {
                Console.Clear();
                Console.WriteLine("\nLista biletów posiadanych przez użytkownika:");
                Console.WriteLine();
                for (int i = 0; i < listaBiletowJednegoWlasciciela.Count; i++)
                {
                    Rysuj(listaBiletowJednegoWlasciciela[i]);
                }

                Console.WriteLine();
                Console.WriteLine("Po naciśnięciu dowolnego klawisza, powrócisz do Menu Głównego");
                Console.ReadKey();

                return powrot;
            }


        }
        public static void SprWaznoscPrzezNumer(List<BiletOkresowy> listaBiletowOkresowych)
        {
            int numer = Helper.GetInt("Wpisz numer biletu: ");
            for (int i = 0; i < listaBiletowOkresowych.Count; i++)
            {
                if (listaBiletowOkresowych[i].NumerBiletu == numer)
                {
                    Rysuj(listaBiletowOkresowych[i]);
                    break;
                }
            }
            Console.WriteLine("Po naciśnięciu dowolnego klawisza, powrócisz do Menu Głównego");
            Console.ReadKey();
        }

        public static void Rysuj(BiletOkresowy bilet)
        {

            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
            Console.WriteLine("Bilet nr " + bilet.NumerBiletu);
            Console.WriteLine();
            Console.WriteLine("Rodzaj biletu: " + bilet.StronyBiletu);
            Console.WriteLine("Okres biletu: " + bilet.OkresBiletu);
            if (bilet.StronyBiletu == "TAM")
            {
                Console.WriteLine("Trasa biletu: Bydgoszcz Główna - " + bilet.CelPozdrozy.NazwaStacji);
            }
            else if (bilet.StronyBiletu == "TU")
            {
                Console.WriteLine("Trasa biletu: " + bilet.CelPozdrozy.NazwaStacji + " - Bydgoszcz Główna");
            }
            else
            {
                Console.WriteLine("Trasa biletu: Bydgoszcz Główna - " + bilet.CelPozdrozy.NazwaStacji +
                    "\n" + bilet.CelPozdrozy.NazwaStacji + " - Bydgoszcz Główna");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Daty ważności biletu: " + bilet.WaznoscBiletu.PoczatekKursowania + " - " + bilet.WaznoscBiletu.KoniecKursowania);

            Console.ResetColor();

            Console.WriteLine("Właściciel biletu: " + bilet.WlascicielBiletu.ToString());
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);
        }
    }
}
