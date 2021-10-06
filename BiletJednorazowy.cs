using System;
using System.Collections.Generic;
using System.Text;

namespace Dworzec
{
    class BiletJednorazowy : Bilet
    {
        private int _nrMiejsca;
        private int _nrWagonu;
        private TimeSpan _waznoscBiletu;
        private DateTime _dataPodrozy;

        public int NrMiejsca
        {
            get => _nrMiejsca;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Numer miejsca do siedzenia musi być minimalnie równy 0");
                if (value <= 60)
                    _nrMiejsca = value;
                else
                    throw new ArgumentException("Numer miejsca może być równy maksymalnie 60");
            }
        }


        public TimeSpan WaznoscBiletu { get => _waznoscBiletu; }

        public DateTime DataPodrozy
        {

            get => _dataPodrozy;
            set
            {
                int dzienTygodnia = 0;

                switch (value.DayOfWeek.ToString())
                {
                    case "Monday":
                        dzienTygodnia = 0;
                        break;
                    case "Tuesday":
                        dzienTygodnia = 1;
                        break;
                    case "Wednesday":
                        dzienTygodnia = 2;
                        break;
                    case "Thursday":
                        dzienTygodnia = 3;
                        break;
                    case "Friday":
                        dzienTygodnia = 4;
                        break;
                    case "Saturday":
                        dzienTygodnia = 5;
                        break;
                    case "Sunday":
                        dzienTygodnia = 6;
                        break;

                }

                if (value >= _pociag.DatyKursowania.PoczatekKursowania && value <= _pociag.DatyKursowania.KoniecKursowania)
                {
                    if (_pociag.DatyBezKursowania != null)
                    {
                        if (value < _pociag.DatyBezKursowania.PoczatekKursowania || value > _pociag.DatyBezKursowania.KoniecKursowania)
                        {
                            if (_pociag.DniTygodniaKursowania[dzienTygodnia] == 1)
                            {
                                _dataPodrozy = value;
                            }
                            else
                            {

                                throw new ArgumentException("Dzień tygodnia podróży musi zawierać się w dniach tygodnia kursowania pociągu.");
                            }

                        }
                        else
                        {

                            throw new ArgumentException("Data podróży musi zawierać się w okresie kursowania pociągu");
                        }

                    }
                    else
                        _dataPodrozy = value;

                }
                else
                    throw new ArgumentException("Data podróży musi zawierać się w okresie kursowania pociągu");
            }
        }
        public int NrWagonu
        {
            get => _nrWagonu;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Numer miejsca do siedzenia musi być minimalnie równy 0");
                else if (value <= _pociag.LiczbaWagonow)
                    _nrWagonu = value;
                else
                    throw new ArgumentException($"Numer wagonu może być równy maksymalnie {_pociag.LiczbaWagonow}.");
            }
        }

        public override void ObliczWaznoscBiletu()
        {
            _waznoscBiletu = _celPodrozy.GodzinaPrzyjazdu - _pociag.GodzinaOdjazduZBdg;
        }

        public BiletJednorazowy(int numerBiletu, Pociag pociag, int nrMiejsca, int nrWagonu):base(numerBiletu,pociag)
        {
            NrMiejsca = nrMiejsca;
            NrWagonu = nrWagonu;
        }

    }
}
