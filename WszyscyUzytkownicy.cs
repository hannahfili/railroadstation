using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dworzec
{
    class WszyscyUzytkownicy
    {
        public static List<UzytkownikZar> ListaUzytkownikowZar()
        {
            //uzytkownik zar1       senior 30
            #region
            UzytkownikZar z1 = new UzytkownikZar("Elżbieta", "Stankiewicz", 800.52, "elzbieta.stan@wp.pl", 508701704,
                new Ulga("senior", 30), DateTime.Now.AddYears(100));
            #endregion
            //--------------------------
            //uzytkownik zar2       student 51
            #region
            UzytkownikZar z2 = new UzytkownikZar("Maksymilian", "Kandulski", 200, "max.kand@onet.pl", 512821829,
                new Ulga("student/doktorant", 51), new DateTime(2021,1,10));
            #endregion
            //--------------------------
            //uzytkownik zar3       student 51 
            #region
            UzytkownikZar z3 = new UzytkownikZar("Michał", "Cieszkowski", 12000, "michal.a.ciesz@gmail.com", 711721783,
               new Ulga("student/doktorant", 51), DateTime.Now.AddDays(150));
            #endregion
            //--------------------------
            //uzytkownik zar4       nauczyciel 33
            UzytkownikZar z4 = new UzytkownikZar("Anna", "Barbachen", 7800, "anna.barb.1959@onet.pl", 500100100,
               null, new DateTime(1,1,1));
            //--------------------------






            List<UzytkownikZar> lista = new List<UzytkownikZar>();
            lista.Add(z1);
            lista.Add(z2);
            lista.Add(z3);
            lista.Add(z4);
            return lista;
        }
        public static List<UzytkownikNiezar> ListaUzytkownikowNiezar()
        {
            List<UzytkownikNiezar> lista = new List<UzytkownikNiezar>();
           
            return lista;
        }
    }
}
