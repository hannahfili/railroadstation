using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class Osobowy : Pociag
    {
        private string _przewoznik;
        private List<CenaOdcinka> _cennik;
        private List<Ulga> _rodzajeUlg;
        public string Przewoznik
        {
            get => _przewoznik;
            set
            {
                if (value == null)
                    throw new ArgumentException("Brak nazwy przewoźnika.");
                value = value.ToUpper();
                if (value == "ARRIVA" || value == "POLREGIO")
                    _przewoznik = value;
                else
                    throw new ArgumentException("Przewoźnicy ze stacji Bydgoszcz Główna to ARRIVA lub POLREGIO, ustaw poprawnego przewoźnika.");
            }
        }

        public List<CenaOdcinka> Cennik { get => _cennik; }
        public List<Ulga> RodzajeUlg { get => _rodzajeUlg;  }

        public void UstawCennikOrazUlgi()
        {
            if(Przewoznik=="ARRIVA")
            {
                _cennik = CennikKilometrowy.CennikArriva();
                _rodzajeUlg = WszystkieUlgi.ListaUlg();

            }
            else if(Przewoznik=="POLREGIO")
            {
                _cennik = CennikKilometrowy.CennikPolregio();
                _rodzajeUlg = WszystkieUlgi.ListaUlg();
            }
        }

        public Osobowy(string przewoznik)
        {
            Przewoznik = przewoznik;
        }

        public Osobowy()
        {

        }

        public Osobowy(int numerPociagu, double peron, int tor,
            int miejscaDlaNiepelnosprawnych, int liczbaWagonow, string przewoznik)
            : base(numerPociagu, peron, tor,miejscaDlaNiepelnosprawnych, liczbaWagonow)
        {
            Przewoznik = przewoznik;
        }
    }
}
