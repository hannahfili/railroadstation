using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class Kursowanie
    {
        private DateTime _poczatekKursowania;
        private DateTime _koniecKursowania;

        public Kursowanie(DateTime poczatekKursowania, DateTime koniecKursowania)
        {
            bool poczatekKursowaniaSpr = Helper.DateCheck(poczatekKursowania);

            //Console.WriteLine("Sprawdzenie poczatku kursowania: "+ poczatekKursowaniaSpr.ToString());
            
            bool koniecKursowaniaSpr = Helper.DateCheck(koniecKursowania);

            //Console.WriteLine("Sprawdzenie konca kursowania: "+ koniecKursowaniaSpr.ToString());
            
            int compare = DateTime.Compare(poczatekKursowania, koniecKursowania);

            //Console.WriteLine("Compare: "+ compare.ToString());

            //Console.ReadKey();

            if (poczatekKursowaniaSpr == true && koniecKursowaniaSpr == true && compare <= 0)
            {
                PoczatekKursowania = poczatekKursowania;
                KoniecKursowania = koniecKursowania;
            }
            else
                
                throw new ArgumentException("Podano nieprawidłowe daty początku lub końca kursowania pociągu");
        }

        public DateTime PoczatekKursowania { get => _poczatekKursowania; set => _poczatekKursowania = value; }
        public DateTime KoniecKursowania { get => _koniecKursowania; set => _koniecKursowania = value; }

        public override bool Equals(object obj)
        {
            return obj is Kursowanie kursowanie &&
                   _poczatekKursowania == kursowanie._poczatekKursowania &&
                   _koniecKursowania == kursowanie._koniecKursowania;
        }
    }
}
