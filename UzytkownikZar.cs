using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dworzec
{
    class UzytkownikZar : Uzytkownik
    {
        private string _adresEmail;
        private int _numerTelefonu;
        private Ulga _rodzajStalejUlgi;
        private DateTime _dataKoncaWaznosciUlgi;


        public string AdresEmail
        {
            get => _adresEmail;
            set
            {
                if (value == null)
                    throw new ArgumentException("Adres e-mail nie może być nullem!");
                else if (!value.Contains("@"))
                    throw new ArgumentException("Adres e-mail musi zawierać @!");
                else if (value.Contains(" "))
                    throw new ArgumentException("Adres e-mail nie może zawierać spacji");
                else if (!value.Contains("."))
                    throw new ArgumentException("Adres e-mail musi zawierać co najmniej jedną kropkę!");
                else if (value[0] == '-' || value[0] == '_' || value[0] == '@')
                    throw new ArgumentException("Adres e-mail nie może zawierać zaczynać się od: [@], [-],[_] !");

                _adresEmail = value.ToLower();
            }
        }
        public int NumerTelefonu
        {
            get => _numerTelefonu;
            set
            {

                string check = value.ToString();
                if (check.Length != 9)
                    throw new ArgumentException("Numer telefonu musi składać się z 9 cyfr!");
                if (value < 100000000 || value > 999999999)
                    throw new ArgumentException("Numer telefonu musi zawierać się w przedziale <500 000 000, 899 999 999");

                _numerTelefonu = value;
            }
        }
        public Ulga RodzajStalejUlgi
        {
            get => _rodzajStalejUlgi;
            set
            {
                if (value is null)
                    _rodzajStalejUlgi = value;
                else
                {
                    bool p = false;
                    for (int i = 0; i < WszystkieUlgi.ListaUlg().Count; i++)
                    {
                        if (value.RodzajUlgi.ToUpper() == WszystkieUlgi.ListaUlg()[i].RodzajUlgi.ToUpper())
                        {
                            _rodzajStalejUlgi = value;
                            p = true;
                            break;
                        }
                    }
                    if (!p)
                        throw new ArgumentException("Podana ulga nie znajduje się na liście ulg");
                }
                
            }
        }
        public DateTime DataKoncaWaznosciUlgi
        {
            get => _dataKoncaWaznosciUlgi;
            set => _dataKoncaWaznosciUlgi = value;
        }

        public UzytkownikZar(string imie, string nazwisko, double stanKonta, string adresEmail,
            int numerTelefonu, Ulga rodzajStalejUlgi, DateTime dataKoncaWaznosciUlgi) : base(imie, nazwisko, stanKonta)
        {
            AdresEmail = adresEmail;
            NumerTelefonu = numerTelefonu;
            RodzajStalejUlgi = rodzajStalejUlgi;
            DataKoncaWaznosciUlgi = dataKoncaWaznosciUlgi;
        }

        public override bool Equals(object obj)
        {
            return obj is UzytkownikZar zar &&
                   base.Equals(obj) &&
                   _numerTelefonu == zar._numerTelefonu;
        }

        public static void PrzedluzUlge(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar,
            List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {

            Console.Clear();
            bool uzytkownikJestWsystemie = false;
            string imie = Helper.GetString("Podaj imię: ");
            string nazwisko = Helper.GetString("Podaj nazwisko: ");
            int numer_tel = Helper.GetInt("Podaj numer telefonu: ", 100000000, 999999999);

            UzytkownikZar z = new UzytkownikZar(imie, nazwisko, 100, "przykladowy@gmail.com", numer_tel, WszystkieUlgi.ListaUlg()[0], new DateTime(2022, 1, 1));

            for (int i = 0; i < bazaUzytkownikowZar.Count; i++)
            {
                //Console.WriteLine(bazaUzytkownikowZar[i].ToString());
                //Console.ReadKey();
                if (z.Equals(bazaUzytkownikowZar[i]))
                {
                    z = bazaUzytkownikowZar[i];
                    uzytkownikJestWsystemie = true;
                }
            }

            if (!uzytkownikJestWsystemie)
            {
                Console.WriteLine("Brak podanego użytkownika w systemie.\nPo naciśnięciu dowolnego przycisku, powrócisz do Głównego Menu");
                Console.ReadKey();
                Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);
            }

            Console.Clear();

            if (z.RodzajStalejUlgi != null)
            {
                if (z.RodzajStalejUlgi.RodzajUlgi.ToUpper() == "SENIOR")
                {
                    Console.WriteLine("\nJesteś seniorem, Twoja ulga jest bezterminowa.");
                }
                else
                {
                    Console.WriteLine("\nRodzaj ulgi: " + z.RodzajStalejUlgi.RodzajUlgi);
                    Console.WriteLine("Wysokość ulgi: " + z.RodzajStalejUlgi.WysokoscUlgi + "%");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ulga ważna do: " + z.DataKoncaWaznosciUlgi.ToShortDateString());
                    Console.ResetColor();

                    Helper.RysujEntery(3);
                    if (z.RodzajStalejUlgi.RodzajUlgi.ToUpper() == "STUDENT/DOKTORANT")
                        Console.WriteLine("Czy przedłużyć ulgę studencką o kolejny semestr?");
                    else
                        Console.WriteLine("Czy przedłużyć ulgę o kolejny rok?");

                    string odp = Helper.GetString("Wpisz T lub N: ");

                    if (odp.ToUpper() == "T" && z.RodzajStalejUlgi.RodzajUlgi.ToUpper() == "STUDENT/DOKTORANT")
                    {
                        z.DataKoncaWaznosciUlgi = z.DataKoncaWaznosciUlgi.AddDays(150);
                        Console.WriteLine("Zmieniono datę ważności ulgi");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nowa ulga ważna do: " + z.DataKoncaWaznosciUlgi.ToShortDateString());
                        Console.ResetColor();


                    }
                    else if (odp.ToUpper() == "T")
                    {
                        z.DataKoncaWaznosciUlgi = z.DataKoncaWaznosciUlgi.AddYears(1);
                        Console.WriteLine("Zmieniono datę ważności ulgi");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nowa ulga ważna do: " + z.DataKoncaWaznosciUlgi.ToShortDateString());
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Nie zmieniono daty ważności ulgi");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nie posiadasz stałej ulgi.");
            }

            


            Console.WriteLine("Po naciśnięciu dowolnego przycisku, powrócisz do Głównego Menu");
            Console.ReadKey();
            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);


        }

        public static void ZarejestrujSie(List<Pociag> bazaPociagow, List<UzytkownikZar> bazaUzytkownikowZar, List<UzytkownikNiezar> bazaUzytkownikowNiezar,
            List<int> listaZajetychNumerowBiletow, List<BiletOkresowy> listaBiletowOkresowych)
        {
            UzytkownikZar z = new UzytkownikZar("Anna", "Barbachen", 7800, "anna.barb.1959@onet.pl", 500100100,
              new Ulga("nauczyciel", 33), DateTime.Now.AddYears(1));

            string imie, nazwisko, email;
            double stanKonta;
            int numerTelefonu, wybor = 0;
            Ulga rodzajUlgi;
            bool nowyUzytkownik = true;
            DateTime koniecUlgi;

            Console.Clear();
            Helper.RysujEntery(3);
            Helper.RysujZnaki(25, '*');
            Helper.RysujEntery(3);

            while (true)
            {
                imie = Helper.GetString("Podaj imię: ");
                nazwisko = Helper.GetString("Podaj nazwisko: ");
                numerTelefonu = Helper.GetInt("Podaj numer telefonu: ");

                for (int i = 0; i < bazaUzytkownikowZar.Count; i++) //sprawdz czy uzytkownik jest juz w bazie
                {
                    if(imie.ToUpper()==bazaUzytkownikowZar[i].Imie.ToUpper()
                        && nazwisko.ToUpper() == bazaUzytkownikowZar[i].Nazwisko.ToUpper()
                        && numerTelefonu==bazaUzytkownikowZar[i].NumerTelefonu)
                    {
                        nowyUzytkownik = false;
                        break;
                    }
                }
                if (nowyUzytkownik)
                {
                    email = Helper.GetString("Podaj adres e-mail: ");
                    stanKonta = Helper.GetDouble("Podaj stan konta: ");

                    Helper.RysujEntery(2);
                    Console.WriteLine("Ulgi do wyboru:");
                    for (int i = 0; i < WszystkieUlgi.ListaUlg().Count; i++)
                    {
                        rodzajUlgi = WszystkieUlgi.ListaUlg()[i];
                        Console.WriteLine("[" + i + "] " + rodzajUlgi.RodzajUlgi + "\t" + rodzajUlgi.WysokoscUlgi + "%");
                    }
                    wybor = Helper.GetInt("Wybierz rodzaj ulgi: ", 0, WszystkieUlgi.ListaUlg().Count);
                    Helper.RysujEntery(3);
                    Helper.RysujZnaki(25, '*');
                    Console.WriteLine();

                    rodzajUlgi = WszystkieUlgi.ListaUlg()[wybor];
                    if (rodzajUlgi.RodzajUlgi.ToUpper() == "SENIOR")
                        koniecUlgi = DateTime.Now.AddYears(100);
                    else if(rodzajUlgi.RodzajUlgi.ToUpper()=="STUDENT/DOKTORANT")
                        koniecUlgi = DateTime.Now.AddDays(183);
                    else
                        koniecUlgi= DateTime.Now.AddYears(1);
                    try
                    {
                        z = new UzytkownikZar(imie, nazwisko, stanKonta, email, numerTelefonu, rodzajUlgi, koniecUlgi);
                        bazaUzytkownikowZar.Add(z);
                        break;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //Console.WriteLine(e.StackTrace);
                    }
                }
                else
                {
                    Console.WriteLine("Istnieje już zarejestrowany użytkownik o podanym imieniu, nazwisku i numerze telefonu.");
                    Helper.RysujEntery(3);
                    Helper.RysujZnaki(25, '*');
                    break;
                }
            }

            for (int i = 0; i < bazaUzytkownikowZar.Count; i++)
            {
                Console.WriteLine(bazaUzytkownikowZar[i].ToString());
            }

            Console.WriteLine("\nPo naciśnięciu dowolnego przycisku, powrócisz do Głównego Menu");
            Console.ReadKey();
            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);

        }


    }
}
