using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class Ulga
    {
        private string _rodzajUlgi;
        private int _wysokoscUlgi;

        public string RodzajUlgi 
        {
            get => _rodzajUlgi;
            set
            {
                if (value == null)
                    throw new ArgumentException("Brak nazwy rodzaju ulgi.");
                _rodzajUlgi= value;
            }
        }
        public int WysokoscUlgi 
        {
            get => _wysokoscUlgi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Wysokość ulgi musi być większa od 0!!!");
                else if(value>100)
                    throw new ArgumentException("Wysokość ulgi musi być maksymalnie równa 100!!!");
                _wysokoscUlgi = value;
            }
        }

        public Ulga(string rodzajUlgi, int wysokoscUlgi)
        {
            RodzajUlgi = rodzajUlgi;
            WysokoscUlgi = wysokoscUlgi;
        }
    }
}
