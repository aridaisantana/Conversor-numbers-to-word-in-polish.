using System;
using ConvertNumberIntoWords;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class Decimal : Numbers
    {

        protected static string[] SINGLES = { "", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
        protected static string[] TEENS = { "", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
        protected static string[] TENS = { "", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
        protected static string[] HUNDREDS = { "", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
        protected static string[] FIRSTCONJUGATION = { "", "tysiąc", "milion", "miliard", "bilion", "biliard", "trylion", "tryliard", "kwadrylion", "kwadryliard", "kwintylion", "kwintyliard", "sekstylion", "sekstyliard", "septylion", "septyliard", "oktylion", "oktyliard", "nonilion", "noniliard", "decylion", "decyliard", "undecylion", "undecyliard", "dodecylion", "dodecyliard", "tridecylion", "tridecyliard", "kwatuordecylion", "kwatuordecyliard", "kwindecylion", "kwindecyliard", "seksdecylion", "seksdecyliard", "septendecylion", "septendecyliard", "oktodecylion", "oktodecyliard", "nowemdecylion", "nowemdecyliard", "wicylion", "wicyliard", "unwicylion", "unwicyliard", "dowicylion", "dowicyliard", "triwicylion", "triwicyliard" };
        protected static string[] SECONDCONJUGATION = { "", "tysiące", "miliony", "miliardy", "biliony", "biliardy", "tryliony", "tryliardy", "kwadryliony", "kwadryliardy", "kwintyliony", "kwintyliardy", "sekstyliony", "sekstyliardy", "septyliony", "septyliardy", "oktyliony", "oktyliardy", "noniliony", "noniliardy", "decyliony", "decyliardy", "undecyliony", "undecyliardy", "dodecyliony", "dodecyliardy", "tridecyliony", "tridecyliardy", "kwatuordecyliony", "kwatuordecyliardy", "kwindecyliony", "kwindecyliardy", "seksdecyliony", "seksdecyliardy", "septendecyliony", "septendecyliardy", "oktodecyliony", "oktodecyliardy", "nowemdecyliony", "nowemdecyliardy", "wicyliony", "wicyliardy", "unwicyliony", "unwicyliardy", "dowicyliony", "dowicyliardy", "triwicyliony", "triwicyliardy" };
        protected static string[] THIRDCONJUGATION = { "", "tysięcy", "milionów", "miliardów", "bilionow", "biliardow", "trylionow", "tryliardow", "kwadrylionow", "kwadryliardow", "kwintylionow", "kwintyliardow", "sekstylionow", "sekstyliardow", "septylionow", "septyliardow", "oktylionow", "oktyliardow", "nonilionow", "noniliardow", "decylionow", "decyliardow", "undecylionow", "undecyliardow", "dodecylionow", "dodecyliardow", "tridecylionow", "tridecyliardow", "kwatuordecylionow", "kwatuordecyliardow", "kwindecylionow", "kwindecyliardow", "seksdecylionow", "seksdecyliardow", "septendecylionow", "septendecyliardow", "oktodecylionow", "oktodecyliardow", "nowemdecylionow", "nowemdecyliardow", "wicylionow", "wicyliardow", "unwicylionow", "unwicyliardow", "dowicylionow", "dowicyliardow", "triwicylionow", "triwicyliardow" };
        protected static string[] DECIMALFIRSTCONJUGATION = { "", "dziesiąte", "setne", "tysięczne", "dziesięciotysięczne", "stutysięczne", "milionowe", "dziesięciomilionowe", "stumilionowe", "miliardowe", "dziesięciomiliardowe", "stumiliardowe", "bilionowe", "dziesięciobilionowe", "stubilionowe", "biliardowe", "dziesięciobiliardowe", "stubiliardowe" };
        protected static string[] DECIMALSECONDCONJUGATION = { "", "dziesiątych", "setnych", "tysięcznych", "dziesięciotysięcznych", "stutysięcznych", "milionowych", "dziesięciomilionowych", "stumilionowych", "miliardowych", "dziesięciomiliardowych", "stumiliardowych", "bilionowych", "dziesięciobilionowych", "stubilionowych", "biliardowych", "dziesięciobiliardowych", "stubiliardowych" };

        private IStringIterator iterator;
        private int conjugationIndex = 0;
        private int numberLength = 0;

        public Decimal(IStringIterator iterator, int numberLength)
        {
            this.iterator = iterator;
            this.numberLength = numberLength;
        }

        public String ConvertIntoWords()
        {

            StringBuilder builder = new StringBuilder();
            List<string> groups = new List<string>();

            int number = 0;
            int sHundreds = 0;
            int dTens = 0;
            int jSingles = 0;
            int nTeens = 0;
            int conjugationType = 0;
            int decimalConjugationType = 0;
            string conjugation = "";

            while (iterator.HasNext())
            {

                try
                {
                    number = int.Parse(iterator.Next());
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
                    if (IsEmpty(groups))
                    {
                        decimalConjugationType = jSingles;
                    }

                    if (sHundreds + dTens + nTeens == 0 && jSingles == 1 && !String.IsNullOrWhiteSpace(FIRSTCONJUGATION[conjugationIndex]))
                    {
                        //do not say 'jeden tysiąc' but 'tysiąc'
                        jSingles = 0;
                    }

                    conjugation = getConjugation(conjugationIndex, conjugationType);
                    groups.Add(string.Format(" {0} {1} {2} {3} {4}", HUNDREDS[sHundreds], TENS[dTens], TEENS[nTeens], SINGLES[jSingles], conjugation));
                }

                conjugationIndex++;
                conjugationType = iterator.getConjugationType();
            }
    
            groups.Reverse();
            groups.Add(getDecimalConjugation(numberLength, decimalConjugationType));
            groups.ForEach(x => builder.Append(x));

            string result = builder.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }

        public string getConjugation(int decimalConjugationIndex, int conjugationType)
        {
            string conjugation = "";

            if (conjugationType == 0)
            {
                conjugation = FIRSTCONJUGATION[decimalConjugationIndex];
            }
            else if (conjugationType == 1)
            {
                conjugation = SECONDCONJUGATION[decimalConjugationIndex];
            }
            else
            {
                conjugation = THIRDCONJUGATION[decimalConjugationIndex];
            }

            return conjugation;
        }

        public string getDecimalConjugation(int decimalConjugationIndex, int decimalConjugationType)
        {
            string conjugation = "";

            if (decimalConjugationType >=2 && decimalConjugationType <=4)
            {
                conjugation = DECIMALFIRSTCONJUGATION[decimalConjugationIndex];
            }
            else
            {
                conjugation = DECIMALSECONDCONJUGATION[decimalConjugationIndex];
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
