using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    abstract class Bilet
    {
        private int _numerBiletu;
        protected Pociag _pociag;
        protected StacjaGodzinaKilometry _celPodrozy;
        private double _cena;
        private TimeSpan _czasPodrozy;
        
        protected DateTime _dataZakupu; //dla biletu okresowego data zakupu to data poczatku waznosci biletu, a nie faktyczna data zakupu

        private static List<int> _listaZajetychNumerowBiletow;

        public int NumerBiletu
        { get => _numerBiletu;
            set
            {
                 _numerBiletu= value;
            }
        }
        public double Cena 
        { get => _cena;
            set
            {
                if (value <0)
                    throw new ArgumentException("Cena biletu musi być przynajmniej równa 0");
                else
                    _cena = value;

            }
        }
        
        
        public Pociag Pociag { get => _pociag; set => _pociag = value; }
        public StacjaGodzinaKilometry CelPozdrozy
        { get => _celPodrozy;
            set
            {
                List<StacjaGodzinaKilometry> gdzieMoznaJechac = new List<StacjaGodzinaKilometry>();

                gdzieMoznaJechac.Add(_pociag.StacjaDocelowa);
                for (int i = 0; i < _pociag.StacjePosrednie.Count; i++)
                    gdzieMoznaJechac.Add(_pociag.StacjePosrednie[i]);

                bool check = false;

                for (int i = 0; i < gdzieMoznaJechac.Count; i++)
                {
                    if (gdzieMoznaJechac[i].NazwaStacji.ToUpper() == value.NazwaStacji.ToUpper())
                    {
                        check = true;
                        _celPodrozy = value;
                        _celPodrozy.NazwaStacji = gdzieMoznaJechac[i].NazwaStacji;
                        _celPodrozy.GodzinaPrzyjazdu = gdzieMoznaJechac[i].GodzinaPrzyjazdu;
                        _celPodrozy.Kilometry = gdzieMoznaJechac[i].Kilometry;
                        break;
                    }
                }
                if (check == false)
                    throw new ArgumentException("Cel podróży musi znajdować się na trasie pociągu!");
            }
        }


        public DateTime DataZakupu { get => _dataZakupu; set => _dataZakupu = value; }

        

        public static List<int> ListaZajetychNumerowBiletow 
        { 
            get => _listaZajetychNumerowBiletow;
            set => _listaZajetychNumerowBiletow = value; 
        }
        public TimeSpan CzasPodrozy { get => _czasPodrozy;}

        public void ObliczCzasPodrozy()
        {
            //obliczenie czasu podróży
            DateTime t1 = _celPodrozy.GodzinaPrzyjazdu;
            DateTime t2 = _pociag.GodzinaOdjazduZBdg;
            _czasPodrozy = t2 - t1;
        }

        public abstract void ObliczWaznoscBiletu();

        public override bool Equals(object obj)
        {
            return obj is Bilet bilet &&
                   _numerBiletu == bilet._numerBiletu;
        }

        public override int GetHashCode()
        {
            return _numerBiletu;
        }

        public Bilet(int numerBiletu, Pociag pociag)
        {
            NumerBiletu = numerBiletu;
            Pociag = pociag;
            ListaZajetychNumerowBiletow = new List<int>();
        }

        public Bilet()
        {

        }
    }
}
