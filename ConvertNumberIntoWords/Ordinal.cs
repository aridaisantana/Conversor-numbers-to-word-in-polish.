using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        protected static string[] OCONJUGATIONS = { "", "tysięczny", "milionowy", "miliardowa", "trilionowy", "triliardowa", "kwadrylionowy", "kwadryliardowa", "kwintylionowy", "kwintyliardowa", "sekstylionowy", "sekstyliardowa", "septylionowy", "septyliardowa", "oktylionowy", "oktyliardowa", "nonilionowy", "noniliardowa", "decylionowy", "decyliardowa", "undecylionowy", "undecyliardowa", "dodecylionowy", "dodecyliardowa", "tridecylionowy", "tridecyliardowa", "kwatuordecylionowy", "kwatuordecyliardowa", "kwindecylionowy", "kwindecyliardowa", "seksdecylionowy", "seksdecyliardowa", "septendecylionowy", "septendecyliardowa", "oktodecylionowy", "oktodecyliardowa", "nowemdecylionowy", "nowemdecyliardowa", "wicylionowy", "wicyliardowa", "unwicylionowy", "unwicyliardowa", "dowicylionowy", "dowicyliardowa", "triwicylionowy", "triwicyliardowa" };
        protected static string[] OPREFIXES = { "","stu", "dwu", "trzech", "czterech", "pięcio", "sześć", "siedem", "osiem", "dziewięć" };

        private IStringIterator iterator;
        private int conjugationIndex = 0;


        public Ordinal(IStringIterator iterator)
        {
            this.iterator = iterator;
        }

        public string ConvertIntoWords()
        {
            StringBuilder builder = new StringBuilder();
            List<string> groups = new List<string>();

            int number = 0;
            int sHundreds = 0;
            int dTens = 0;
            int jSingles = 0;
            int nTeens = 0;
            string conjugation = "";
            int conjugationType = 0;
            int countZeros = 0;

            while (iterator.HasNext())
            {

                try
                {
                    string numberString = iterator.Next();
                    number = int.Parse(numberString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha habido un error de tipo:" + ex.GetType() + " con Ordinal");
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

                    if (sHundreds + dTens + nTeens == 0 && jSingles != 0 && !String.IsNullOrWhiteSpace(CFIRSTCONJUGATION[conjugationIndex]))
                    {
                        if (jSingles == 1)
                        {
                            //do not say 'jeden tysiąc' but 'tysiąc'
                            jSingles = 0;
                        }
                    }

                    
                    if (countZeros == 0 && conjugationIndex == 0 && sHundreds > 0 && dTens + nTeens + jSingles == 0)
                    {

                        groups.Add(string.Format(" {0}", OHUNDREDS[sHundreds]));
                    }
                    else if (IsEmpty(groups))
                    {
                        if ((sHundreds + dTens + nTeens == 0 && jSingles > 0) && countZeros != 0)
                        {
                            conjugation = OPREFIXES[jSingles] + OCONJUGATIONS[conjugationIndex];
                            groups.Add(string.Format(" {0}", conjugation));

                        }else if (countZeros != 0)
                        {
                            groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], CTENS[dTens], CTEENS[nTeens], CSINGLES[jSingles], OCONJUGATIONS[conjugationIndex]));
                        }
                        else
                        {
                            groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], OTENS[dTens], OTEENS[nTeens], OSINGLES[jSingles], OCONJUGATIONS[conjugationIndex]));
                        }
                    }
                    else
                    {
                        conjugation = getConjugation(conjugationIndex, conjugationType);
                        groups.Add(string.Format(" {0} {1} {2} {3} {4}", CHUNDREDS[sHundreds], CTENS[dTens], CTEENS[nTeens], CSINGLES[jSingles], conjugation));
                    }

                }
                else
                {
                    countZeros++;
                }

                conjugationIndex++;
                conjugationType = iterator.getConjugationType();
            }


            groups.Reverse();
            groups.ForEach(x => builder.Append(x));

            string result = builder.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }

        public string getConjugation (int conjugationIndex, int conjugationType )
        {
            string conjugation = "";

            if (conjugationType == 0)
            {
                conjugation = CFIRSTCONJUGATION[conjugationIndex];
            }
            else if (conjugationType == 1)
            {
                conjugation = CSECONDCONJUGATION[conjugationIndex];
            }
            else
            {
                conjugation = CTHIRDCONJUGATION[conjugationIndex];
            }

            return conjugation;
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