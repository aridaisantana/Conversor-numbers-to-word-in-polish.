﻿using System;
using ConvertNumberIntoWords;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class Ordinal: Numbers
    {
        protected static string[] CSINGLES = { "", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
        protected static string[] CTEENS = { "", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
        protected static string[] CTENS = { "", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
        protected static string[] CHUNDREDS = { "", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
        protected static string[] CFIRSTCONJUGATION = { "", "tysiąc", "milion", "miliard", "bilion", "biliard", "trylion", "tryliard", "kwadrylion", "kwadryliard", "kwintylion", "kwintyliard", "sekstylion", "sekstyliard", "septylion", "septyliard", "oktylion", "oktyliard", "nonilion", "noniliard", "decylion", "decyliard", "undecylion", "undecyliard", "dodecylion", "dodecyliard", "tridecylion", "tridecyliard", "kwatuordecylion", "kwatuordecyliard", "kwindecylion", "kwindecyliard", "seksdecylion", "seksdecyliard", "septendecylion", "septendecyliard", "oktodecylion", "oktodecyliard", "nowemdecylion", "nowemdecyliard", "wicylion", "wicyliard", "unwicylion", "unwicyliard", "dowicylion", "dowicyliard", "triwicylion", "triwicyliard" };
        protected static string[] CSECONDCONJUGATION = { "", "tysiące", "miliony", "miliardy", "biliony", "biliardy", "tryliony", "tryliardy", "kwadryliony", "kwadryliardy", "kwintyliony", "kwintyliardy", "sekstyliony", "sekstyliardy", "septyliony", "septyliardy", "oktyliony", "oktyliardy", "noniliony", "noniliardy", "decyliony", "decyliardy", "undecyliony", "undecyliardy", "dodecyliony", "dodecyliardy", "tridecyliony", "tridecyliardy", "kwatuordecyliony", "kwatuordecyliardy", "kwindecyliony", "kwindecyliardy", "seksdecyliony", "seksdecyliardy", "septendecyliony", "septendecyliardy", "oktodecyliony", "oktodecyliardy", "nowemdecyliony", "nowemdecyliardy", "wicyliony", "wicyliardy", "unwicyliony", "unwicyliardy", "dowicyliony", "dowicyliardy", "triwicyliony", "triwicyliardy" };
        protected static string[] CTHIRDCONJUGATION = { "", "tysięcy", "milionów", "miliardów", "bilionow", "biliardow", "trylionow", "tryliardow", "kwadrylionow", "kwadryliardow", "kwintylionow", "kwintyliardow", "sekstylionow", "sekstyliardow", "septylionow", "septyliardow", "oktylionow", "oktyliardow", "nonilionow", "noniliardow", "decylionow", "decyliardow", "undecylionow", "undecyliardow", "dodecylionow", "dodecyliardow", "tridecylionow", "tridecyliardow", "kwatuordecylionow", "kwatuordecyliardow", "kwindecylionow", "kwindecyliardow", "seksdecylionow", "seksdecyliardow", "septendecylionow", "septendecyliardow", "oktodecylionow", "oktodecyliardow", "nowemdecylionow", "nowemdecyliardow", "wicylionow", "wicyliardow", "unwicylionow", "unwicyliardow", "dowicylionow", "dowicyliardow", "triwicylionow", "triwicyliardow" };

        protected static string[] OSINGLES = { "", "pierwszy", "drugi", "trzeci", "czwarty", "piąty", "szósty", "siódmy", "ósmy", "dziewiąty" };
        protected static string[] OTEENS = { "", "jedenasty", "dwunasty", "trzynasty", "czternasty", "piętnasty", "szesnasty", "siedemnasty", "osiemnasty", "dziewiętnasty" };
        protected static string[] OTENS = { "", "dziesiąty", "dwudziesty", "trzydziesty", "czterdziesty", "pięćdziesiąty", "sześćdziesiąty", "siedemdziesiąty", "osiemdziesiąty", "dziewięćdziesiąty" };
        protected static string[] OHUNDREDS = { "", "setny", "dwusetny", "trzechsetny", "czterechsetny", "pięćsetny", "sześćsetny", "siedemsetny", "osiemsetny", "dziewięćsetny" };
        protected static string[] OCONJUGATIONS = { "tysięczny" };

        public string ConvertIntoWords(string input)
        {
            StringBuilder builder = new StringBuilder();


            bool isNegative = Regex.IsMatch(input, @"^[-][\d]+");

            if (isNegative)
            {
                builder.Append("minus ");
                input = input.Remove(0, 1);
            }


            List<string> groups = new List<string>();
            StringIterator iterator = new StringIterator(input);

            int number = 0;
            int sHundreds = 0;
            int dTens = 0;
            int jSingles = 0;
            int nTeens = 0;
            int conjugation = 0;

            while (iterator.HasNext())
            {

                try
                {
                    string numberString = iterator.Next();
                    number = int.Parse(numberString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha habido un error de tipo:" + ex.GetType() + " con la cifra ingresada");
                }


                sHundreds = (number % 1000) / 100;
                dTens = (number % 100) / 10;
                jSingles = jSingles = number % 10;
                nTeens = 0;

                //check if teens form should be used
                if (dTens == 1 && jSingles > 0)
                {
                    nTeens = jSingles;
                    dTens = 0;
                    jSingles = 0;
                }
                else
                {
                    nTeens = 0;
                }

                //add text if there is any houndred, ten, teen or single
                if (sHundreds + dTens + nTeens + jSingles > 0)
                {

                    if (sHundreds + dTens + nTeens == 0 && jSingles == 1 && !String.IsNullOrWhiteSpace(CFIRSTCONJUGATION[iterator.getGMagnitude()]))
                    {
                        //do not say 'jeden tysiąc' but 'tysiąc'
                        jSingles = 0;
                    }

                    conjugation = iterator.getConjugation();
                    Console.WriteLine(IsEmpty(groups));
                    if (IsEmpty(groups))
                    {
                        groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], OTENS[dTens], OTEENS[nTeens], OSINGLES[jSingles], CFIRSTCONJUGATION[iterator.getGMagnitude()]));
                    }
                    else
                    {
                        if (conjugation == 0)
                        {
                            groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], CTENS[dTens], CTEENS[nTeens], CSINGLES[jSingles], CFIRSTCONJUGATION[iterator.getGMagnitude()]));
                        }
                        else if (conjugation == 1)
                        {
                            groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], CTENS[dTens], CTEENS[nTeens], CSINGLES[jSingles], CSECONDCONJUGATION[iterator.getGMagnitude()]));
                        }
                        else
                        {
                            groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], CTENS[dTens], CTEENS[nTeens], CSINGLES[jSingles], CTHIRDCONJUGATION[iterator.getGMagnitude()]));
                        }
                    }

                }

                iterator.incrementGMagnitude();
                iterator.updateConjugation();
            }


            groups.Reverse();
            groups.ForEach(x => builder.Append(x));

            string result = builder.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }


        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return list.FirstOrDefault() == null;
        }
    }

}