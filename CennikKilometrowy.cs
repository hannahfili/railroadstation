using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class CennikKilometrowy
    {
        
        public static List<CenaOdcinka> CennikPospieszny() //cennik dla pociagu pospiesznego podany jest dla klasy 1
            //chcac znac cene biletu w klasie 2, nalezy pomnozyc cene z klasy 1 x 0.75
        {
            List <CenaOdcinka> cennikPospiesznyKlasa1= new List<CenaOdcinka>();

            CenaOdcinka c1 = new CenaOdcinka(20, 0, 40);
            CenaOdcinka c2 = new CenaOdcinka(28, 41, 80);
            CenaOdcinka c3 = new CenaOdcinka(32, 81, 120);
            CenaOdcinka c4 = new CenaOdcinka(39,121, 160);
            CenaOdcinka c5 = new CenaOdcinka(60, 161, 200);
            CenaOdcinka c6 = new CenaOdcinka(70, 201, 240);
            CenaOdcinka c7 = new CenaOdcinka(75, 241, 280);
            CenaOdcinka c8 = new CenaOdcinka(80, 281, 320);
            CenaOdcinka c9 = new CenaOdcinka(85, 321, 360);
            CenaOdcinka c10 = new CenaOdcinka(88, 361, 400);
            CenaOdcinka c11 = new CenaOdcinka(150, 401, 10000); //cena odcinkowa dla pociagow o km>400 jest juz stala (uproszczenie)

            cennikPospiesznyKlasa1.Add(c1);
            cennikPospiesznyKlasa1.Add(c2);
            cennikPospiesznyKlasa1.Add(c3);
            cennikPospiesznyKlasa1.Add(c4);
            cennikPospiesznyKlasa1.Add(c5);
            cennikPospiesznyKlasa1.Add(c6);
            cennikPospiesznyKlasa1.Add(c7);
            cennikPospiesznyKlasa1.Add(c8);
            cennikPospiesznyKlasa1.Add(c9);
            cennikPospiesznyKlasa1.Add(c10);
            cennikPospiesznyKlasa1.Add(c11);


            return cennikPospiesznyKlasa1;
        }

        public static List<CenaOdcinka> CennikArriva() 
        {
            List<CenaOdcinka> cennikArriva = new List<CenaOdcinka>();

            CenaOdcinka c1 = new CenaOdcinka(4.3, 0, 20);
            CenaOdcinka c2 = new CenaOdcinka(7.2, 21, 40);
            CenaOdcinka c3 = new CenaOdcinka(10.1, 41, 60);
            CenaOdcinka c4 = new CenaOdcinka(13, 61, 80);
            CenaOdcinka c5 = new CenaOdcinka(15.9, 81, 100);
            CenaOdcinka c6 = new CenaOdcinka(18.8, 101, 120);
            CenaOdcinka c7 = new CenaOdcinka(21.7, 121, 140);
            CenaOdcinka c8 = new CenaOdcinka(24.6, 141, 160);
            CenaOdcinka c9 = new CenaOdcinka(27.5, 161, 180);
            CenaOdcinka c10 = new CenaOdcinka(30.4, 181, 200);
            CenaOdcinka c11 = new CenaOdcinka(35, 201, 10000);
            //cena odcinkowa dla pociagow o km>200 jest juz stala (uproszczenie)

            cennikArriva.Add(c1);
            cennikArriva.Add(c2);
            cennikArriva.Add(c3);
            cennikArriva.Add(c4);
            cennikArriva.Add(c5);
            cennikArriva.Add(c6);
            cennikArriva.Add(c7);
            cennikArriva.Add(c8);
            cennikArriva.Add(c9);
            cennikArriva.Add(c10);
            cennikArriva.Add(c11);


            return cennikArriva;
        }

        public static List<CenaOdcinka> CennikPolregio()
        {
            List<CenaOdcinka> cennikPolregio = new List<CenaOdcinka>();

            CenaOdcinka c1 = new CenaOdcinka(18.3, 0, 80);
            CenaOdcinka c2 = new CenaOdcinka(27.6, 81, 160);
            CenaOdcinka c3 = new CenaOdcinka(33, 161, 240);
            CenaOdcinka c4 = new CenaOdcinka(37, 241, 320);
            CenaOdcinka c5 = new CenaOdcinka(40, 321, 400);
            CenaOdcinka c6 = new CenaOdcinka(42, 401, 480);
            CenaOdcinka c7 = new CenaOdcinka(44, 481, 560);
            CenaOdcinka c8 = new CenaOdcinka(46, 561, 640);
            CenaOdcinka c9 = new CenaOdcinka(48, 641, 720);
            CenaOdcinka c10 = new CenaOdcinka(50, 721, 800);
            CenaOdcinka c11 = new CenaOdcinka(52, 801, 10000);
            //cena odcinkowa dla pociagow o km>200 jest juz stala (uproszczenie)

            cennikPolregio.Add(c1);
            cennikPolregio.Add(c2);
            cennikPolregio.Add(c3);
            cennikPolregio.Add(c4);
            cennikPolregio.Add(c5);
            cennikPolregio.Add(c6);
            cennikPolregio.Add(c7);
            cennikPolregio.Add(c8);
            cennikPolregio.Add(c9);
            cennikPolregio.Add(c10);
            cennikPolregio.Add(c11);


            return cennikPolregio;
        }



    }
}
