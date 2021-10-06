using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class WszystkiePociagi
    {
        
        public static List<Pociag> NowaListaPociagow()
        {
            List<Pociag> listaPociagow = new List<Pociag>();

            //pociag1 Bydgoszcz-Bielsko Biała Główna    p1 pospieszny
            #region
            Pospieszny p1 = new Pospieszny(54170, 4.1, 9, 3, 7, "ROZEWIE", true, true, true, true);
            
            
            p1.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 7, 28));
            p1.DatyBezKursowania = new Kursowanie(new DateTime(2021, 1, 2), new DateTime(2021, 1, 7,23,59,59));

            
            for(int i=0; i<p1.DniTygodniaKursowania.Length; i++) //pociag kursuje we wszystkie dni tygodnia
            {
                p1.DniTygodniaKursowania[i] = 1;
            }
           

            StacjaGodzinaKilometry stacjaDocelowa = new StacjaGodzinaKilometry("Bielsko-Biała Główna", new DateTime(1, 1, 1, 9, 19, 0), 490);
            p1.StacjaDocelowa = stacjaDocelowa;

            List<StacjaGodzinaKilometry> stacjePosrednie = new List<StacjaGodzinaKilometry>();

            StacjaGodzinaKilometry s1 = new StacjaGodzinaKilometry("Inowrocław", new DateTime(1, 1, 1, 0, 40, 0), 44);
            StacjaGodzinaKilometry s2 = new StacjaGodzinaKilometry("Gniezno", new DateTime(1, 1, 1, 1, 22, 0), 83);
            StacjaGodzinaKilometry s3 = new StacjaGodzinaKilometry("Poznań Główny", new DateTime(1, 1, 1, 2, 13, 0), 139);
            StacjaGodzinaKilometry s4 = new StacjaGodzinaKilometry("Jarocin", new DateTime(1, 1, 1, 3, 29, 0), 165);

            StacjaGodzinaKilometry s5 = new StacjaGodzinaKilometry("Wrocław Główny", new DateTime(1, 1, 1, 5, 22, 0), 311);
            StacjaGodzinaKilometry s6 = new StacjaGodzinaKilometry("Brzeg", new DateTime(1, 1, 1, 6, 7, 0), 380);
            StacjaGodzinaKilometry s7 = new StacjaGodzinaKilometry("Opole Główne", new DateTime(1, 1, 1, 6, 33, 0), 414);
            StacjaGodzinaKilometry s8 = new StacjaGodzinaKilometry("Gliwice", new DateTime(1, 1, 1, 7, 42, 0), 480);
            StacjaGodzinaKilometry s9 = new StacjaGodzinaKilometry("Katowice", new DateTime(1, 1, 1, 8, 5, 0), 485);
            StacjaGodzinaKilometry s10 = new StacjaGodzinaKilometry("Czechowice-Dziedzice", new DateTime(1, 1, 1, 9, 5, 0), 487);

            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);
            stacjePosrednie.Add(s7);
            stacjePosrednie.Add(s8);
            stacjePosrednie.Add(s9);
            stacjePosrednie.Add(s10);

            p1.StacjePosrednie = stacjePosrednie;
            p1.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 0, 6, 0);
            p1.StworzListeWagonow();

            #endregion
            //--------------------koniec pociagu1--------------------------------------

            //pociag2 Bydgoszcz - Gdynia Główna         p2 pospieszny
            #region
            Pospieszny p2 = new Pospieszny(45191, 4, 11, 5, 8, "ROZEWIE", true, true, true, false);
          

            p2.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 3, 13));
            p2.DatyBezKursowania = new Kursowanie(new DateTime(2021, 1, 15), new DateTime(2021, 1, 17,23,59,59));

            for (int i = 0; i < p2.DniTygodniaKursowania.Length-1; i++) //pociag kursuje we wszystkie dni tygodnia oprócz niedzieli
            {
                p2.DniTygodniaKursowania[i] = 1;
            }

            stacjaDocelowa = new StacjaGodzinaKilometry("Gdynia Główna", new DateTime(1, 1, 1, 6, 32, 0), 193);
            p2.StacjaDocelowa = stacjaDocelowa;

            stacjePosrednie = new List<StacjaGodzinaKilometry>();

            s1 = new StacjaGodzinaKilometry("Laskowice Pomorskie", new DateTime(1, 1, 1, 4, 56, 0), 44);
            s2 = new StacjaGodzinaKilometry("Tczew", new DateTime(1, 1, 1, 5, 43, 0), 83);
            s3 = new StacjaGodzinaKilometry("Gdańsk Główny", new DateTime(1, 1, 1, 6, 5, 0), 139);
            s4 = new StacjaGodzinaKilometry("Gdańsk Wrzeszcz", new DateTime(1, 1, 1, 6, 12, 0), 165);

            s5 = new StacjaGodzinaKilometry("Gdańsk Oliwa", new DateTime(1, 1, 1, 6, 17, 0), 311);
            s6 = new StacjaGodzinaKilometry("Sopot", new DateTime(1, 1, 1, 6, 22, 0), 380);
            

            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);

            p2.StacjePosrednie = stacjePosrednie;
            p2.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 4, 20, 0);
            p2.StworzListeWagonow();

            #endregion
            //--------------------koniec pociagu2--------------------------------------

            //pociag3 Bydgoszcz-Toruń Wschodni          o1 osobowy polregio
            #region
            Osobowy o1 = new Osobowy(55214, 2, 2, 3, 5, "Polregio");

            o1.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
            o1.DatyBezKursowania = new Kursowanie(new DateTime(2021, 12, 24), new DateTime(2021, 12, 26,23,59,59));

            for (int i = 0; i < o1.DniTygodniaKursowania.Length - 1; i++) //pociag kursuje we wszystkie dni tygodnia oprócz niedzieli
            {
                o1.DniTygodniaKursowania[i] = 1;
            }

            stacjaDocelowa = new StacjaGodzinaKilometry("Toruń Wschodni", new DateTime(1, 1, 1, 6, 34, 0), 193);
            o1.StacjaDocelowa = stacjaDocelowa;

            stacjePosrednie = new List<StacjaGodzinaKilometry>();

            s1 = new StacjaGodzinaKilometry("Bydgoszcz Leśna", new DateTime(1, 1, 1, 5, 41, 0), 44);
            s2 = new StacjaGodzinaKilometry("Bydgoszcz Bielawy", new DateTime(1, 1, 1, 5, 44, 0), 83);
            s3 = new StacjaGodzinaKilometry("Bydgoszcz Łęgnowo", new DateTime(1, 1, 1, 5, 51, 0), 139);
            s4 = new StacjaGodzinaKilometry("Solec Kujawski", new DateTime(1, 1, 1, 5, 58, 0), 165);
            s5 = new StacjaGodzinaKilometry("Przyłubie", new DateTime(1, 1, 1, 6, 3, 0), 311);
            s6 = new StacjaGodzinaKilometry("Toruń Główny", new DateTime(1, 1, 1, 6, 23, 0), 380);


            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);

            o1.StacjePosrednie = stacjePosrednie;
            o1.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 5, 38, 0);
            #endregion
            //--------------------koniec pociagu3--------------------------------------

            //pociag4 Bydgoszcz-Tuchola                 o2 osobowy arriva
            #region
            Osobowy o2 = new Osobowy(50291, 3.1, 5, 2, 4, "Arriva");
            

            o2.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
            o2.DatyBezKursowania = null; //brak okresu bez kursowania

            for (int i = 0; i < o2.DniTygodniaKursowania.Length; i++) //pociag kursuje we wszystkie dni tygodnia
            {
                o2.DniTygodniaKursowania[i] = 1;
            }

            stacjaDocelowa = new StacjaGodzinaKilometry("Tuchola", new DateTime(1, 1, 1, 23, 59, 0), 62);
            o2.StacjaDocelowa = stacjaDocelowa;

            stacjePosrednie = new List<StacjaGodzinaKilometry>();

            s1 = new StacjaGodzinaKilometry("Rynkowo Wiadukt", new DateTime(1, 1, 1, 22, 50, 0),7.5);
            s2 = new StacjaGodzinaKilometry("Maksymilianowo", new DateTime(1, 1, 1, 22, 57, 0), 14.9);
            s3 = new StacjaGodzinaKilometry("Stronno", new DateTime(1, 1, 1, 23, 5, 0), 25);
            s4 = new StacjaGodzinaKilometry("Wudzyn", new DateTime(1, 1, 1, 23, 09, 0), 29.7);
            s5 = new StacjaGodzinaKilometry("Serock", new DateTime(1, 1, 1, 23, 14, 0), 34);
            s6 = new StacjaGodzinaKilometry("Lubania-Lipiny", new DateTime(1, 1, 1, 23, 18, 0), 42);
            s7 = new StacjaGodzinaKilometry("Świekatowo", new DateTime(1, 1, 1, 23, 22, 0), 45);
            s8 = new StacjaGodzinaKilometry("Lipienica", new DateTime(1, 1, 1, 23, 26, 0), 74);
            s9 = new StacjaGodzinaKilometry("Błądzim", new DateTime(1, 1, 1, 23, 30, 0), 51);
            s10 = new StacjaGodzinaKilometry("Wierzchucin", new DateTime(1, 1, 1, 23, 38, 0), 59);


            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);
            stacjePosrednie.Add(s7);
            stacjePosrednie.Add(s8);
            stacjePosrednie.Add(s9);
            stacjePosrednie.Add(s10);

            o2.StacjePosrednie = stacjePosrednie;
            o2.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 22, 47, 0);
            #endregion
            //--------------------koniec pociagu4--------------------------------------

            //pociag5 Bydgoszcz Gorzów Wlkp.    p3 pospieszny
            #region
            Pospieszny p3 = new Pospieszny(58113, 3, 1, 4, 6, "KOCIEWIE", false, true, true, false);


            p3.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 3, 13));
            p3.DatyBezKursowania = new Kursowanie(new DateTime(2021, 3, 2), new DateTime(2021, 3, 5,23,59,59));

            for (int i = 0; i < p3.DniTygodniaKursowania.Length-1; i++) //pociag kursuje we wszystkie dni tygodnia oprócz niedzieli
            {
                p3.DniTygodniaKursowania[i] = 1;
            }

            stacjaDocelowa = new StacjaGodzinaKilometry("Gorzów Wielkopolski", new DateTime(1, 1, 1, 23, 1, 0), 211);
            p3.StacjaDocelowa = stacjaDocelowa;

            stacjePosrednie = new List<StacjaGodzinaKilometry>();

            s1 = new StacjaGodzinaKilometry("Nakło nad Notecią", new DateTime(1, 1, 1, 20, 3, 0), 30);
            s2 = new StacjaGodzinaKilometry("Wyrzysk Osiek", new DateTime(1, 1, 1, 20, 21, 0), 54);
            s3 = new StacjaGodzinaKilometry("Piła Główna", new DateTime(1, 1, 1, 20, 48, 0), 92);
            s4 = new StacjaGodzinaKilometry("Trzcianka", new DateTime(1, 1, 1, 21, 28, 0), 114);
            s5 = new StacjaGodzinaKilometry("Wieleń", new DateTime(1, 1, 1, 21, 48, 0), 144);
            s6 = new StacjaGodzinaKilometry("Krzyż", new DateTime(1, 1, 1, 21, 59, 0), 156);
            s7 = new StacjaGodzinaKilometry("Nowe Drezdenko", new DateTime(1, 1, 1, 22, 17, 0), 170);
            s8 = new StacjaGodzinaKilometry("Strzelce Krajeńskie Wschód", new DateTime(1, 1, 1, 22, 32, 0), 190);
            s9 = new StacjaGodzinaKilometry("Gorzów Wlkp. Wschodni", new DateTime(1, 1, 1, 22, 56, 0), 210);
            


            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);
            stacjePosrednie.Add(s7);
            stacjePosrednie.Add(s8);
            stacjePosrednie.Add(s9);

            p3.StacjePosrednie = stacjePosrednie;
            p3.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 19, 33, 0);
            p3.StworzListeWagonow();
            #endregion

            //--------------------koniec pociagu5--------------------------------------

            //pociag p6 Bydgoszcz - Bielsko-Biała Główna    p4 pospieszny
            #region
            Pospieszny p4 = new Pospieszny(5420, 2, 2, 4, 12, "HUTNIK", true, true, true, false);


            p4.DatyKursowania = new Kursowanie(new DateTime(2021, 1, 1), new DateTime(2021, 3, 13));
            p4.DatyBezKursowania = new Kursowanie(new DateTime(2021, 3, 2), new DateTime(2021, 3, 5, 23, 59, 59));

            for (int i = 0; i < p4.DniTygodniaKursowania.Length; i++) //pociag kursuje we wszystkie dni tygodnia
            {
                p4.DniTygodniaKursowania[i] = 1;
            }

            stacjaDocelowa = new StacjaGodzinaKilometry("Bielsko-Biała Główna", new DateTime(1, 1, 1, 23, 59, 0), 490);
            p4.StacjaDocelowa = stacjaDocelowa;

            stacjePosrednie = new List<StacjaGodzinaKilometry>();

             s1 = new StacjaGodzinaKilometry("Toruń Główny", new DateTime(1, 1, 1, 17, 45, 0), 44);
             s2 = new StacjaGodzinaKilometry("Włocławek", new DateTime(1, 1, 1, 18, 22, 0), 83);
             s3 = new StacjaGodzinaKilometry("Kutno", new DateTime(1, 1, 1, 18, 58, 0), 139);
             s4 = new StacjaGodzinaKilometry("Łowicz Główny", new DateTime(1, 1, 1, 19,40, 0), 165);

             s5 = new StacjaGodzinaKilometry("Częstochowa", new DateTime(1, 1, 1, 22, 9, 0), 311);
             s6 = new StacjaGodzinaKilometry("Sosnowiec", new DateTime(1, 1, 1, 23, 11, 0), 380);
             s7 = new StacjaGodzinaKilometry("Katowice", new DateTime(1, 1, 1, 23, 22, 0), 414);
             s8 = new StacjaGodzinaKilometry("Czechowice-Dziedzice", new DateTime(1, 1, 1, 23, 44, 0), 487);

            stacjePosrednie.Add(s1);
            stacjePosrednie.Add(s2);
            stacjePosrednie.Add(s3);
            stacjePosrednie.Add(s4);
            stacjePosrednie.Add(s5);
            stacjePosrednie.Add(s6);
            stacjePosrednie.Add(s7);
            stacjePosrednie.Add(s8);


            p4.StacjePosrednie = stacjePosrednie;
            p4.GodzinaOdjazduZBdg = new DateTime(1, 1, 1, 17, 9, 0);
            p4.StworzListeWagonow();
            #endregion
            //--------------------koniec pociagu6--------------------------------------

            listaPociagow.Add(p1);
            listaPociagow.Add(p2);
            listaPociagow.Add(p3);
            listaPociagow.Add(o1);
            listaPociagow.Add(o2);
            listaPociagow.Add(p4);

            return listaPociagow;

            
        }
    }
}
