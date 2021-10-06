using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Dworzec
{
    class Program
    {
        static void Main(string[] args)
        {

            //program glowny - Dworzec


            List<BiletOkresowy> listaBiletowOkresowych = new List<BiletOkresowy>();
            List<int> listaZajetychNumerowBiletow = new List<int>();
            List<Pociag> bazaPociagow = WszystkiePociagi.NowaListaPociagow();
            List<UzytkownikZar> bazaUzytkownikowZar = WszyscyUzytkownicy.ListaUzytkownikowZar();

            //sprawdzenie dat waznosci ulg uzytkownikow zarejestrowanych - jezeli juz minela, ustaw rodzaj ulgi na null
            for(int i=0; i<bazaUzytkownikowZar.Count; i++)
            {
                UzytkownikZar u = bazaUzytkownikowZar[i];
                if(DateTime.Now>=u.DataKoncaWaznosciUlgi)
                {
                    u.DataKoncaWaznosciUlgi = new DateTime(1, 1, 1);
                    u.RodzajStalejUlgi = null;
                }
            }


            List<UzytkownikNiezar> bazaUzytkownikowNiezar = WszyscyUzytkownicy.ListaUzytkownikowNiezar();

            Menu.Pokaz(bazaPociagow, bazaUzytkownikowZar, bazaUzytkownikowNiezar, listaZajetychNumerowBiletow, listaBiletowOkresowych);


            //-------------------------------program glowny - Dworzec-----------------------------------------


           









        }
    }
}
