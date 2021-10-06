using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class StacjaGodzinaKilometry
    {
        private string _nazwaStacji;
        private DateTime _godzinaPrzyjazdu;
        private double _kilometry;

        public string NazwaStacji
        { get => _nazwaStacji;
            set
            {
                if (value == null)
                    throw new ArgumentException("Brak nazwy stacji");
                _nazwaStacji = value;
            }
        }
        public DateTime GodzinaPrzyjazdu
        {
            get => _godzinaPrzyjazdu;
            set => _godzinaPrzyjazdu = value;
        }
        public double Kilometry
        { get => _kilometry;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Liczba kilometrów musi być większa od 0!!!");
                _kilometry = value;
            } 
        }

        public StacjaGodzinaKilometry(string nazwaStacji, DateTime godzinaPrzyjazdu, double kilometry)
        {
            NazwaStacji = nazwaStacji;
            GodzinaPrzyjazdu = godzinaPrzyjazdu;
            Kilometry = kilometry;
        }
        public StacjaGodzinaKilometry()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is StacjaGodzinaKilometry kilometry &&
                   _nazwaStacji == kilometry._nazwaStacji &&
                   _godzinaPrzyjazdu == kilometry._godzinaPrzyjazdu &&
                   _kilometry == kilometry._kilometry;
        }
    }
}
