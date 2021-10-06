using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class CenaOdcinka
    {
        private double _oplataZaOdcinek;
        private double _minKm;
        private double _maxKm;

        public double OplataZaOdcinek
        { 
            get => _oplataZaOdcinek;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Opłata za odcinek musi być większa od 0!");
                _oplataZaOdcinek = value;
            }
        }
        public double MaxKm
        { 
            get => _maxKm; 
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Górna granica kilometracji musi być większa od 0!");
                if(value<=10000)
                    _maxKm = value;
                else
                    throw new ArgumentException("Górna granica kilometracji musi być maksymalnie równa 10 000 km!");
            }
        }

        public double MinKm 
        {
            get => _minKm;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Dolna granica kilometracji musi być większa od 0!");
                _minKm = value;
            }
        }

        public CenaOdcinka(double oplataZaOdcinek,  double minKm, double maxKm )
        {
            OplataZaOdcinek = oplataZaOdcinek;
            MaxKm = maxKm;
            MinKm = minKm;
        }
    }
}
