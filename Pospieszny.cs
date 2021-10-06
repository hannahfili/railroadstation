using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class Pospieszny:Pociag
    {
        private string _nazwaPociagu;//k
        private List<CenaOdcinka> _cennik = CennikKilometrowy.CennikPospieszny();
        private List<Ulga> _rodzajeUlg = WszystkieUlgi.ListaUlg();
        private bool _klasa1;//k
        private bool _klasa2;//k
        private bool _wagonRestauracyjny;//k
        private bool _wagonSypialniany;//k
        private List<Wagon> _listaWagonow;

        public string NazwaPociagu
        { 
            get => _nazwaPociagu;
            set
            {
                if (value == null)
                    throw new ArgumentException("Brak nazwy rodzaju ulgi.");
                _nazwaPociagu= value.ToUpper();
            }
        }


        public bool Klasa1 { get => _klasa1; set => _klasa1 = value; }
        public bool Klasa2 { get => _klasa2; set => _klasa2 = value; }
        public bool WagonRestauracyjny { get => _wagonRestauracyjny; set => _wagonRestauracyjny = value; }
        public bool WagonSypialniany { get => _wagonSypialniany; set => _wagonSypialniany = value; }
        public List<CenaOdcinka> Cennik { get => _cennik; }
        public List<Ulga> RodzajeUlg { get => _rodzajeUlg;}
        public List<Wagon> ListaWagonow { get => _listaWagonow; }

        public void StworzListeWagonow()
        {
            _listaWagonow = new List<Wagon>();
            Wagon wagon;
            for (int i = 0; i < _liczbaWagonow; i++)
            {
                wagon = new Wagon(i + 1, 60);
                _listaWagonow.Add(wagon);
            }
            
        }
        public Pospieszny(int numerPociagu, double peron, int tor,
            int miejscaDlaNiepelnosprawnych, int liczbaWagonow, string nazwa,
            bool klasa1, bool klasa2, bool wagonRestauracyjny, bool wagonSypialniany)
            :base(numerPociagu,peron,tor,miejscaDlaNiepelnosprawnych,liczbaWagonow)
        {
            NazwaPociagu = nazwa;
            Klasa1 = klasa1;
            Klasa2 = klasa2;
            WagonRestauracyjny = wagonRestauracyjny;
            WagonSypialniany = wagonSypialniany;
        }

        public Pospieszny()
        {

        }
    }
}
