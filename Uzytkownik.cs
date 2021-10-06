using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    abstract class Uzytkownik
    {
        private string _imie;
        private string _nazwisko;
        
        private double _stanKonta;
        

        public string Imie 
        {
            get => _imie;
            set
            {
                if (value == null)
                    throw new ArgumentException("Nie podano imienia użytkownika.");
                _imie= value;
            }
        }
        public string Nazwisko
        {
            get => _nazwisko;
            set
            {
                if (value == null)
                    throw new ArgumentException("Nie podano nazwiska użytkownika.");
                _nazwisko = value;
            }
        }
        
        public double StanKonta
        {
            get => _stanKonta;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stan konta musi być co najmniej równy 0!");
                _stanKonta = value;
            }
        }

        public Uzytkownik(string imie, string nazwisko, double stanKonta)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            StanKonta = stanKonta;
        }
        public Uzytkownik()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Uzytkownik uzytkownik &&
                   _imie.ToUpper() == uzytkownik._imie.ToUpper() &&
                   _nazwisko.ToUpper() == uzytkownik._nazwisko.ToUpper();
        }
        public override sealed string ToString()
        {
            return Imie+" "+Nazwisko;
        }

    }
}
