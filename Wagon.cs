using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class Wagon
    {
        private int _nrWagonu;
        private int _liczbaWolnychMiejsc;

        public int NrWagonu
        { 
            get => _nrWagonu;
            set
            {
                if (value <= 0 || value > 15)
                    throw new ArgumentException("Nr wagonu musi zawierać się w przedziale <1,15>");
                else
                    _nrWagonu = value;

            }
        }
        public int LiczbaWolnychMiejsc
        { 
            get => _liczbaWolnychMiejsc;
            set 
            {
                if (value < 0 || value > 60)
                    throw new ArgumentException("Liczba wolnych miejsc w wagonie musi zawierać się w przedziale <0,60>");
                else
                    _liczbaWolnychMiejsc = value;
            }
        }

        public Wagon(int nrWagonu, int liczbaWolnychMiejsc)
        {
            NrWagonu = nrWagonu;
            LiczbaWolnychMiejsc = liczbaWolnychMiejsc;
        }
    }
}
