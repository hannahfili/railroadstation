using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class WszystkieUlgi
    {
        public static List<Ulga> ListaUlg()
        {
            List<Ulga> listaUlg = new List<Ulga>();
            try
            {
                Ulga u1 = new Ulga("senior", 30);
                Ulga u2 = new Ulga("ulga szkolna", 37);
                Ulga u3 = new Ulga("student/doktorant", 51);
                Ulga u4 = new Ulga("dziecko/młodzież niepełnosprawna", 78);
                Ulga u5 = new Ulga("dziecko do lat 4", 100);
                Ulga u6 = new Ulga("nauczyciel", 33);
                Ulga u7 = new Ulga("Karta Dużej Rodziny", 37);
                listaUlg.Add(u1);
                listaUlg.Add(u2);
                listaUlg.Add(u3);
                listaUlg.Add(u4);
                listaUlg.Add(u5);
                listaUlg.Add(u6);
                listaUlg.Add(u7);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
            }




            return listaUlg;
        }

        //public static List<Ulga> ListaUlgArriva()
        //{
        //    List<Ulga> listaUlg = new List<Ulga>();

        //    Ulga u1 = new Ulga("nauczyciel", 33);
        //    Ulga u2 = new Ulga("Karta Dużej Rodziny", 37);
        //    Ulga u3 = new Ulga("dziecko/młodzież", 37);
        //    Ulga u4 = new Ulga("student/doktorant", 51);
        //    Ulga u5 = new Ulga("dziecko do lat 4", 100);






        //    listaUlg.Add(u1);
        //    listaUlg.Add(u2);
        //    listaUlg.Add(u3);
        //    listaUlg.Add(u4);
        //    listaUlg.Add(u5);

        //    return listaUlg;
        //}

        //public static List<Ulga> ListaUlgPolregio()
        //{
        //    List<Ulga> listaUlg = new List<Ulga>();


        //    Ulga u1 = new Ulga("osoba niezdolna do sam. egzyst.", 49);
        //    Ulga u2 = new Ulga("inwalida wojenny", 78);
        //    Ulga u3 = new Ulga("dziecko/młodzież niepełnosprawna", 100);
        //    Ulga u4 = new Ulga("Straż Graniczna/Policja/Żołnierz", 100);
        //    Ulga u5 = new Ulga("dziecko do lat 4", 100);
        //    Ulga u6 = new Ulga("opiekun", 95);
        //    Ulga u7 = new Ulga("opozycjonista antykomunistyczny", 78);
        //    Ulga u8 = new Ulga("niewidomy", 93);
        //    Ulga u9 = new Ulga("student/doktorant", 51);
        //    Ulga u10 = new Ulga("dziecko/młodzież", 37);


        //    listaUlg.Add(u1);
        //    listaUlg.Add(u2);
        //    listaUlg.Add(u3);
        //    listaUlg.Add(u4);
        //    listaUlg.Add(u5);
        //    listaUlg.Add(u6);
        //    listaUlg.Add(u7);
        //    listaUlg.Add(u8);
        //    listaUlg.Add(u9);
        //    listaUlg.Add(u10);

        //    return listaUlg;
        //}

    }
}
