using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dworzec
{
    abstract class Pociag
    {
        private int _numerPociagu;//k
        private double _peron;//k
        private int _tor;//k
        protected int _liczbaWagonow;
        private Kursowanie _datyKursowania;//done
        private Kursowanie _datyBezKursowania;//done
        private  int[] _dniTygodniaKursowania = new int[7];//done
        private StacjaGodzinaKilometry _stacjaDocelowa;//done
        private List <StacjaGodzinaKilometry> _stacjePosrednie=new List<StacjaGodzinaKilometry>();//done
        private DateTime _godzinaOdjazduZBdg;//done
        private int _miejscaSiedzace;//k
        private int _miejscaStojace;//k
        private int _miejscaDlaNiepelnosprawnych;//k

        public Pociag(int numerPociagu, double peron, int tor, int miejscaDlaNiepelnosprawnych, int liczbaWagonow)
        {
            NumerPociagu = numerPociagu;
            Peron = peron;
            Tor = tor;
            MiejscaDlaNiepelnosprawnych = miejscaDlaNiepelnosprawnych;
            LiczbaWagonow = liczbaWagonow;
            _miejscaSiedzace = 60 * _liczbaWagonow;
            _miejscaStojace = 40 * _liczbaWagonow;
        }

        public Pociag()
        {

        }


        public int NumerPociagu 
        { get => _numerPociagu;
            set 
            {
                if (value < 5000 || value > 60000)
                    throw new ArgumentException("Numer pociągu musi zawierać się w przedziale <5000, 60 000>");
                else
                    _numerPociagu = value;

            } 
        }
        public double Peron
        {
            get => _peron; 
            set
            {
                double[] dozwolonePerony = new double[7] { 1, 2, 3, 3.1, 4, 4.1, 5 };
                if(dozwolonePerony.Contains(value))
                    _peron = value;
                else
                    throw new ArgumentException("Numer peronu musi zawierać się w liście: {1, 2, 3, 3.1, 4, 4.1, 5}");

            }
        }
        public int Tor 
        { get => _tor;
            set 
            {
                switch(_peron)
                {
                    case 1:
                        {
                            if (value != 6)
                                throw new ArgumentException("Numer toru musi być równy 6");
                            else
                                _tor = value;
                        }
                        break;
                    case 2:
                        {
                            if (value != 2 && value!=4)
                                throw new ArgumentException("Numer toru musi być równy 2 lub 4");
                            else
                                _tor = value;
                        }
                        break;
                    case 3:
                        {
                            if (value != 1)
                                throw new ArgumentException("Numer toru musi być równy 1");
                            else
                                _tor = value;
                        }
                        break;
                    case 3.1:
                        {
                            if (value != 5)
                                throw new ArgumentException("Numer toru musi być równy 5");
                            else
                                _tor = value;
                        }
                        break;
                    case 4:
                        {
                            if (value != 11)
                                throw new ArgumentException("Numer toru musi być równy 11");
                            else
                                _tor = value;
                        }
                        break;
                    case 4.1:
                        {
                            if (value != 9)
                                throw new ArgumentException("Numer toru musi być równy 9");
                            else
                                _tor = value;
                        }
                        break;
                    case 5:
                        {
                            if (value != 12 && value != 21)
                                throw new ArgumentException("Numer toru musi być równy 12 lub 21");
                            else
                                _tor = value;
                        }
                        break;
                }
            }
        }
        public Kursowanie DatyKursowania { get => _datyKursowania; set => _datyKursowania = value; }
        public Kursowanie DatyBezKursowania 
        { get => _datyBezKursowania;

            set
            {
                if(value!=null)
                {
                    if (value.PoczatekKursowania > _datyKursowania.PoczatekKursowania && value.KoniecKursowania < _datyKursowania.KoniecKursowania)
                        _datyBezKursowania = value;
                    else
                        throw new ArgumentException($"Okres,w którym pociąg chwilowo nie kursuje musi mieścić się pomiędzy " +
                            $"<{_datyKursowania.PoczatekKursowania.Date.ToString("dd-MM-yyyy")}, {_datyKursowania.KoniecKursowania.Date.ToString("dd-MM-yyyy")}>");
                }
                
            }
        }//daty bez kursowania muszą mieścić się w przedziale kursowania
        // (chodzi o to, że jest to czasowe niekursowanie pociągu)
        public int[] DniTygodniaKursowania 
        { 
            get => _dniTygodniaKursowania; 
            set => _dniTygodniaKursowania = value;
        }
        
        public DateTime GodzinaOdjazduZBdg { get => _godzinaOdjazduZBdg; set => _godzinaOdjazduZBdg = value; }
        public int MiejscaSiedzace 
        { 
            get => _miejscaSiedzace;
            set 
            {
                if (value >= 0 && value<=60*_liczbaWagonow)
                    _miejscaSiedzace = value;
                else
                    throw new ArgumentException("Liczba miejsc siedzących musi być > 0");
            }
        }

        public int MiejscaStojace 
        { 
            get => _miejscaStojace;
            set
            {
                if (value >= 0 && value<=40*_liczbaWagonow)
                    _miejscaSiedzace = value;
                else
                    throw new ArgumentException("Liczba miejsc stojących musi być > 0");
            }
        }
        public int MiejscaDlaNiepelnosprawnych 
        { 
            get => _miejscaDlaNiepelnosprawnych;
            set
            {
                if (value >= 0)
                    _miejscaDlaNiepelnosprawnych = value;
                else
                    throw new ArgumentException("Liczba miejsc dla niepełnosprawnych musi być >= 0");
            }
        }
        public StacjaGodzinaKilometry StacjaDocelowa { get => _stacjaDocelowa; set => _stacjaDocelowa = value; }
        public List<StacjaGodzinaKilometry> StacjePosrednie 
        { get => _stacjePosrednie;
            set
            {
                if (value.Count >= 1 && value.Count <= 10)
                    _stacjePosrednie = value;
                else
                    throw new ArgumentException("Liczba stacji pośrednich musi zawierać się w przedziale <1,10>");
            }
        }
        public int LiczbaWagonow 
        { 
            get => _liczbaWagonow;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Liczba wagonów musi być co najmniej równa 1!");
                if (value <= 20)
                    _liczbaWagonow = value;
                else
                    throw new ArgumentException("Pociąg może się składać maksymalnie z 20 wagonów!");
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Pociag pociag &&
                   _numerPociagu == pociag._numerPociagu &&
                   EqualityComparer<Kursowanie>.Default.Equals(_datyKursowania, pociag._datyKursowania) &&
                   EqualityComparer<Kursowanie>.Default.Equals(_datyBezKursowania, pociag._datyBezKursowania) &&
                   EqualityComparer<StacjaGodzinaKilometry>.Default.Equals(_stacjaDocelowa, pociag._stacjaDocelowa) &&
                   _godzinaOdjazduZBdg == pociag._godzinaOdjazduZBdg;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_numerPociagu, _datyKursowania, _datyBezKursowania, _stacjaDocelowa, _godzinaOdjazduZBdg);
        }
    }
}
