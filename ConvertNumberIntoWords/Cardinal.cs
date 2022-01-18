using System;
using ConvertNumberIntoWords;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ConvertNumberIntoWords

{
    public class Cardinal : Numbers
    {
        protected static string[] SINGLES = { "", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
        protected static string[] TEENS = { "", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
        protected static string[] TENS = { "", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
        protected static string[] HUNDREDS = { "", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
        protected static string[,] CONJUGATIONS = new string[,]
            {
                { "", "", ""                           },
                { "tysiąc", "tysiące", "tysięcy"       },
                { "milion" , "miliony" , "milionów"    },
                { "miliard", "miliardy", "miliardów"   },
                { "bilion" , "biliony" , "bilionow"    },
                { "biliard", "biliardy", "biliardow"   },
                { "trylion" , "tryliony", "trylionow" },
                { "tryliard", "tryliardy", "tryliardow" },
                { "kwadrylion", "kwadryliony", "kwadrylionow" },
                { "kwadryliard", "kwadryliardy", "kwadryliardow" },
                { "kwintylion", "kwintyliony", "kwintylionow" },
                { "kwintyliard", "kwintyliardy", "kwintyliardow" },
                { "sekstylion", "sekstyliony", "sekstylionow" },
                { "sekstyliard", "sekstyliardy", "sekstyliardow" },
                { "septylion", "septyliony", "septylionow"},
                { "septyliard", "septyliardy", "septyliardow" },
                { "oktylion", "oktyliony", "oktylionow" },
                { "oktyliard", "oktyliardy", "oktyliardow" },
                { "nonilion", "noniliony", "nonilionow" },
                { "noniliard", "noniliardy", "noniliardow" },
                { "decylion", "decyliony", "decylionow" },
                { "decyliard", "decyliardy", "decyliardow" },
                { "undecylion", "undecyliony", "undecylionow" },
                { "undecyliard", "undecyliardy", "undecyliardow" },
                { "dodecylion", "dodecyliony", "dodecylionow" },
                { "dodecyliard", "dodecyliardy", "dodecyliardow" },
                { "tridecylion", "tridecyliony", "tridecylionow" },
                { "tridecyliard", "tridecyliardy", "tridecyliardow" },
                { "kwatuordecylion", "kwatuordecyliony", "kwatuordecylionow" },
                { "kwatuordecyliard", "kwatuordecyliardy", "kwatuordecyliardow" },
                { "kwindecylion", "kwindecyliony", "kwindecylionow" },
                { "kwindecyliard", "kwindecyliardy", "kwindecyliardow" },
                { "seksdecylion", "seksdecyliony", "seksdecylionow" },
                { "seksdecyliard", "seksdecyliardy", "seksdecyliardow" },
                { "septendecylion", "septendecyliony", "septendecylionow" },
                { "septendecyliard", "septendecyliardy", "septendecyliardow" },
                { "oktodecylion", "oktodecyliony", "oktodecylionow" },
                { "oktodecyliard", "oktodecyliardy", "oktodecyliardow" },
                { "nowemdecylion", "nowemdecyliony", "nowemdecylionow" },
                { "nowemdecyliard", "nowemdecyliardy", "nowemdecyliardow" },
                { "wicylion", "wicyliony", "wicylionow" },
                { "wicyliard", "wicyliardy", "wicyliardow" },
                { "unwicylion", "unwicyliony", "unwicylionow" },
                { "unwicyliard", "unwicyliardy", "unwicyliardow" },
                { "dowicylion", "dowicyliony", "dowicylionow" },
                { "dowicyliard", "dowicyliardy", "dowicyliardow" },
                { "triwicylion", "triwicyliony", "triwicylionow" },
                { "triwicyliard", "triwicyliardy", "triwicyliardow" },



            };

        public String ConvertIntoWords(string input)
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

            while (iterator.HasNext())
            {

                try
                {
                    if (input.Length == 1 && int.Parse(input) == 0)
                    {
                        builder.Append("zero");
                        break;
                    }

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

                    if (sHundreds + dTens + nTeens == 0 && jSingles == 1 && !String.IsNullOrWhiteSpace(CONJUGATIONS[iterator.getGMagnitude(), iterator.getConjugation()]))
                    {
                        //do not say 'jeden tysiąc' but 'tysiąc'
                        jSingles = 0;
                    }


                    groups.Add(string.Format(" {0} {1} {2} {3} {4}", HUNDREDS[sHundreds], TENS[dTens], TEENS[nTeens], SINGLES[jSingles], CONJUGATIONS[iterator.getGMagnitude(), iterator.getConjugation()]));

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
    }
}

