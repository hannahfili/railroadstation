using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Dworzec
{
    public static class Helper
    {
        public static void RysujEntery(int liczba)
        {
            for (int i = 0; i < liczba; i++)
                Console.WriteLine();
        }

        public static void RysujZnaki(int liczba, char znak)
        {
            for (int i = 0; i < liczba; i++)
                Console.Write(znak);
        }

        public static int GetInt(string msg, int begin = int.MinValue, int end = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(msg);
                if (int.TryParse(Console.ReadLine(), out result) && result >= begin && result <= end) return result;
                Console.WriteLine($"Błędnie wprowadzono liczbę. Powinna zawierać się w zakresie [{begin}, {end}]" +
                    $"oraz być liczbą całkowitą.\nWprowadź liczbę jeszcze raz.");
            }

        }

        public static decimal GetDecimal(string msg, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                Console.Write(msg);
                if (decimal.TryParse(Console.ReadLine(), out result) && result >= min && result <= max) return result;
                Console.WriteLine($"Błędnie wprowadzono liczbę. Powinna zawierać się w zakresie [{min}, {max}]. \nWprowadź liczbę jeszcze raz.");
            }

        }

        

        public static double GetDouble(string msg, double min = double.MinValue, double max = double.MaxValue)
        {
            double result;
            while (true)
            {
                Console.Write(msg);
                if (double.TryParse(Console.ReadLine(), out result) && result >= min && result <= max) return result;
                Console.WriteLine($"Błędnie wprowadzono liczbę. Powinna zawierać się w zakresie [{min}, {max}]. \nWprowadź liczbę jeszcze raz.");
            }

        }

        public static string GetString(string msg)
        {
            Console.Write(msg);
            string s;

            while (true)
            {
                s = Console.ReadLine();
                if (s != null & s != "") break;
                else Console.WriteLine("Wprowadzono NULLa - spróbuj jeszcze raz.");
                Console.Write(msg);
            }
            return s;
        }

        public static bool CzyRokJestPrzestepny(int rok)
        {
            bool p;

            if (rok % 4 == 0)
            {
                if (rok % 100 == 0)
                {
                    if (rok % 400 == 0)
                        p = true;
                    else
                        p = false;
                }
                else
                {
                    p = true;
                }
            }
            else
                p = false;


            return p;
        }


       

        public static bool CheckMajority(DateTime dateOfBirth)
        {
            DateTime currentDateMinus18 = DateTime.Now.AddYears(-18);

            int compare = dateOfBirth.CompareTo(currentDateMinus18);
            if (compare <= 0) //pelnoletni
                return true;
            else // niepelnoletni
                return false;

        }
        public static bool DateCheck(DateTime data)//sprawdzanie czy data jest późniejsza niż 2019 rok
        {
            DateTime rok2020 = new DateTime(2020,12,31);
            int compare = DateTime.Compare(rok2020, data);

            if (compare >= 0)
                return false;

            return true;

        }
        public static String RemoveDiacritics(this String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        


    }
}
