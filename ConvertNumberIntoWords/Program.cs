using System;
using ConvertNumberIntoWords;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class NumbersToText
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
                { "trylion" , "tryliony", "trylionow" }
            };

        public static String ConvertNumberIntoWords(string input)
        {
            StringBuilder builder = new StringBuilder();
            string inputNumber = input;
            

            bool isNegative = Regex.IsMatch(input, @"^[-][\d]+");

            if (isNegative)
            {
                builder.Append("minus ");
                inputNumber = inputNumber.Remove(0, 1);
            }

            //Ir recorriendo de tres en tres y aplicar lo mismo que el código 

            List<string> groups = new List<string>();

            StringIterator iterator = new StringIterator(inputNumber);

            while (iterator.getInputNumber().Length > 0)
            {

                int number = 0;
                try
                {
                    number = int.Parse(iterator.Next());
                    iterator.incrementGMagnitude();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                }

                int sHundreds = 0;
                int dTens = 0;
                int jSingles = 0;
                int nTeens = 0;


                if (number != 0)
                {
                    sHundreds = (number % 1000) / 100;
                    dTens = (number % 100) / 10;
                    jSingles = number % 10;

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
                            // we do not say 'jeden tysiąc' but 'tysiąc'
                            jSingles = 0;
                        }
                        groups.Add(string.Format(" {0} {1} {2} {3} {4}", HUNDREDS[sHundreds], TENS[dTens], TEENS[nTeens], SINGLES[jSingles], CONJUGATIONS[iterator.getGMagnitude(), iterator.getConjugation()]));
                    }

                    //process next three digits

                }

            }


            groups.Reverse();
            groups.ForEach(x => builder.Append(x));

            string result = builder.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(ConvertNumberIntoWords("1"));
            Console.WriteLine(ConvertNumberIntoWords("-23"));
            Console.WriteLine(ConvertNumberIntoWords("230"));
            Console.WriteLine(ConvertNumberIntoWords("2301"));
            Console.WriteLine(ConvertNumberIntoWords("423010"));
            Console.WriteLine(ConvertNumberIntoWords("1320435"));
            Console.WriteLine(ConvertNumberIntoWords("12343"));
            Console.WriteLine(ConvertNumberIntoWords("5390756"));

            
            
        }

    }

}
