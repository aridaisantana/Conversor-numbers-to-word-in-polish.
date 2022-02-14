using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConvertNumberIntoWords
{
    public class Fractions : Numbers
    {
        protected static string[] SINGLES = { "", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
        protected static string[] TEENS = { "", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
        protected static string[] TENS = { "", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
        protected static string[] HUNDREDS = { "", "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
        protected static string[] FIRSTCONJUGATION = { "", "tysiąc", "milion", "miliard", "bilion", "biliard", "trylion", "tryliard", "kwadrylion", "kwadryliard", "kwintylion", "kwintyliard", "sekstylion", "sekstyliard", "septylion", "septyliard", "oktylion", "oktyliard", "nonilion", "noniliard", "decylion", "decyliard", "undecylion", "undecyliard", "dodecylion", "dodecyliard", "tridecylion", "tridecyliard", "kwatuordecylion", "kwatuordecyliard", "kwindecylion", "kwindecyliard", "seksdecylion", "seksdecyliard", "septendecylion", "septendecyliard", "oktodecylion", "oktodecyliard", "nowemdecylion", "nowemdecyliard", "wicylion", "wicyliard", "unwicylion", "unwicyliard", "dowicylion", "dowicyliard", "triwicylion", "triwicyliard" };
        protected static string[] SECONDCONJUGATION = { "", "tysiące", "miliony", "miliardy", "biliony", "biliardy", "tryliony", "tryliardy", "kwadryliony", "kwadryliardy", "kwintyliony", "kwintyliardy", "sekstyliony", "sekstyliardy", "septyliony", "septyliardy", "oktyliony", "oktyliardy", "noniliony", "noniliardy", "decyliony", "decyliardy", "undecyliony", "undecyliardy", "dodecyliony", "dodecyliardy", "tridecyliony", "tridecyliardy", "kwatuordecyliony", "kwatuordecyliardy", "kwindecyliony", "kwindecyliardy", "seksdecyliony", "seksdecyliardy", "septendecyliony", "septendecyliardy", "oktodecyliony", "oktodecyliardy", "nowemdecyliony", "nowemdecyliardy", "wicyliony", "wicyliardy", "unwicyliony", "unwicyliardy", "dowicyliony", "dowicyliardy", "triwicyliony", "triwicyliardy" };
        protected static string[] THIRDCONJUGATION = { "", "tysięcy", "milionów", "miliardów", "bilionow", "biliardow", "trylionow", "tryliardow", "kwadrylionow", "kwadryliardow", "kwintylionow", "kwintyliardow", "sekstylionow", "sekstyliardow", "septylionow", "septyliardow", "oktylionow", "oktyliardow", "nonilionow", "noniliardow", "decylionow", "decyliardow", "undecylionow", "undecyliardow", "dodecylionow", "dodecyliardow", "tridecylionow", "tridecyliardow", "kwatuordecylionow", "kwatuordecyliardow", "kwindecylionow", "kwindecyliardow", "seksdecylionow", "seksdecyliardow", "septendecylionow", "septendecyliardow", "oktodecylionow", "oktodecyliardow", "nowemdecylionow", "nowemdecyliardow", "wicylionow", "wicyliardow", "unwicylionow", "unwicyliardow", "dowicylionow", "dowicyliardow", "triwicylionow", "triwicyliardow" };


        protected static string[] FIRSTFRACTIONALSINGLES = { "",  "","pół", "trzecia", "czwarta", "piąta", "szósta", "siódma", "ósma", "dziewiąta" };
        protected static string[] SECONDFRACTIONALSINGLES = { "", "", "pół", "trzecie", "czwarte", "piąte", "szóste", "siódme", "ósme", "dziewiąte" };
        protected static string[] THIRDFRACTIONALSINGLES = { "", "", "pół", "trzecich", "czwartych", "piątych", "szóstych", "siódmych", "ósmych", "dziewiątych" };

        protected static string[] FIRSTFRACTIONALTEENS = { "", "jedenasta", "dwunasta", "trzynasta", "czternasta", "piętnasta", "szesnasta", "siedemnasta", "osiemnasta", "dziewiętnasta" };
        protected static string[] SECONDFRACTIONALTEENS = { "", "jedenaste", "dwunaste", "trzynaste", "czternaste", "piętnaste", "szesnaste", "siedemnaste", "osiemnaste", "dziewiętnaste" };
        protected static string[] THIRDFRACTIONALTEENS = { "", "jedenasty", "dwunasty", "trzynasty", "czternasty", "piętnasty", "szesnasty", "siedemnasty", "osiemnasty", "dziewiętnasty" };

        protected static string[] FIRSTFRACTIONALTENS = { "", "dziesiąta", "dwudziesta", "trzydziesta", "czterdziesta", "pięćdziesiąta", "sześćdziesiąta", "siedemdziesiąta", "osiemdziesiąta", "dziewięćdziesiąta" };
        protected static string[] SECONDFRACTIONALTENS = { "", "dziesiąte", "dwudzieste", "trzydzieste", "czterdzieste", "pięćdziesiąte", "sześćdziesiąte", "siedemdziesiąte", "osiemdziesiąte", "dziewięćdziesiąte" };
        protected static string[] THIRDFRACTIONALTENS = { "", "dziesiątych", "dwudziestych", "trzydziestych", "czterdziestych", "pięćdziesiątych", "sześćdziesiątych", "siedemdziesiątych", "osiemdziesiątych", "dziewięćdziesiątych" };

        protected static string[] DECIMALFIRSTCONJUGATION = { "setne", "tysięczne", "milionowe", "miliardowe", "bilionowe", "biliardowe", "trylionowe", "tryliardowe", "kwadrylionowe", "kwadryliardowe", "kwintylionowe", "kwintyliardowe", "sekstylion", "sekstyliardowe", "septylionowe", "septyliardowe", "oktylionowe", "oktyliardowe", "nonilionowe", "noniliardowe", "decylionowe", "decyliardowe", "undecylionowe", "undecyliardowe", "dodecylionowe", "dodecyliardowe", "tridecylionowe", "tridecyliardowe", "kwatuordecylionowe", "kwatuordecyliardowe", "kwindecylionowe", "kwindecyliardowe", "seksdecylionowe", "seksdecyliardowe", "septendecylionowe", "septendecyliardowe", "oktodecylionowe", "oktodecyliardowe", "nowemdecylionowe", "nowemdecyliardowe", "wicylionowe", "wicyliardowe", "unwicylionowe", "unwicyliardowe", "dowicylionowe", "dowicyliardowe", "triwicylionowe", "triwicyliardowe" };
        protected static string[] DECIMALSECONDCONJUGATION = { "setnych", "tysięcznych", "milionowych", "miliardowych", "bilionowych", "biliardowych", "trylionowych", "tryliardowych", "kwadrylionowych", "kwadryliardowych", "kwintylionowych", "kwintyliardowych", "sekstylionowych", "sekstyliardowych", "septylionowych", "septyliardowych", "oktylionowych", "oktyliardowych", "nonilionowych", "noniliardowych", "decylionowych", "decyliardowych", "undecylionowych", "undecyliardowych", "dodecylionowych", "dodecyliardowych", "tridecylionowych", "tridecyliardowych", "kwatuordecylionowych", "kwatuordecyliardowych", "kwindecylionowych", "kwindecyliardowych", "seksdecylionowych", "seksdecyliardowych", "septendecylionowych", "septendecyliardowych", "oktodecylionowych", "oktodecyliardowych", "nowemdecylionowych", "nowemdecyliardowych", "wicylionowych", "wicyliardowych", "unwicylionowych", "unwicyliardowych", "dowicylionowych", "dowicyliardowych", "triwicylionowych", "triwicyliardowych" };

        private int ordinalSingle;
        private IStringIterator iterator;
        private int conjugationIndex = 0;


        public Fractions(IStringIterator iterator, int ordinalSingle)
        {
            this.ordinalSingle = ordinalSingle;
            this.iterator = iterator; 
        }

        public String ConvertIntoWords()
        {

            StringBuilder builder = new StringBuilder();
            List<string> groups = new List<string>();

            int number = 0;
            string numberString = "";
            int sHundreds = 0;
            int dTens = 0;
            int jSingles = 0;
            int nTeens = 0;
            int conjugationType = 0;
            string conjugation = "";
            int countZeros = 0;

            while (iterator.HasNext())
            {

                try
                {
                    numberString = iterator.Next();
                    number = int.Parse(numberString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha habido un error de tipo:" + ex.GetType() + " con Fracciones");
                }


                sHundreds = (number % 1000) / 100;
                dTens = (number % 100) / 10;
                jSingles = number % 10;
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


                    if (sHundreds + dTens + nTeens == 0 && jSingles == 1 && !String.IsNullOrWhiteSpace(FIRSTCONJUGATION[conjugationIndex]))
                    {
                        //do not say 'jeden tysiąc' but 'tysiąc'
                        jSingles = 0;
                    }

                    if (IsEmpty(groups))
                    {
                        if (nTeens > 0)
                        {
                            string teensForm = "";
                            if(ordinalSingle == 1)
                            {
                                teensForm = FIRSTFRACTIONALTEENS[nTeens];
                            }else if(ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                teensForm = SECONDFRACTIONALTEENS[nTeens];
                            }
                            else
                            {
                                teensForm = THIRDFRACTIONALTEENS[nTeens];
                            }

                            groups.Add(string.Format(" {0} {1} {2} ", HUNDREDS[sHundreds], teensForm, conjugation));
                        }else if(dTens + nTeens + sHundreds == 0 && jSingles > 0)
                        {
                            string singleForm = "";
                            if (ordinalSingle == 1)
                            {
                                singleForm = FIRSTFRACTIONALSINGLES[jSingles];
                            }
                            else if (ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                singleForm = SECONDFRACTIONALSINGLES[jSingles];
                            }
                            else
                            {
                                singleForm = THIRDFRACTIONALSINGLES[jSingles];
                            }

                            if(countZeros == 0)
                            {
                                groups.Add(string.Format(" {0} {1}", singleForm, conjugation));
                            }
                            else
                            {
                                string fractionalConjugation = "";
                                if (ordinalSingle >= 2 && ordinalSingle <= 4)
                                {
                                    fractionalConjugation = DECIMALFIRSTCONJUGATION[conjugationIndex];
                                }
                                else
                                {
                                    fractionalConjugation = DECIMALSECONDCONJUGATION[conjugationIndex];
                                }
                                groups.Add(string.Format(" {0} {1}", SINGLES[jSingles], fractionalConjugation));
                            }
                        }
                        else if(dTens + jSingles > 0 && countZeros == 0)
                        {
                            string tensForm = "";
                            string singleForm = "";
                            if (ordinalSingle == 1)
                            {
                                singleForm = FIRSTFRACTIONALSINGLES[jSingles];
                                tensForm = FIRSTFRACTIONALTENS[dTens];
                            }
                            else if (ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                singleForm = SECONDFRACTIONALSINGLES[jSingles];
                                tensForm = SECONDFRACTIONALTENS[dTens];

                            }
                            else
                            {
                                singleForm = THIRDFRACTIONALSINGLES[jSingles];
                                tensForm = THIRDFRACTIONALTENS[dTens];

                            }
                            groups.Add(string.Format(" {0} {1} {2} {3}", HUNDREDS[sHundreds], tensForm, singleForm, conjugation));
                        }else if(dTens + jSingles == 0 && sHundreds > 0)
                        {
                            string fractionalConjugation = "";
                            if(ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                fractionalConjugation = DECIMALFIRSTCONJUGATION[conjugationIndex];
                            }
                            else
                            {
                                fractionalConjugation = DECIMALSECONDCONJUGATION[conjugationIndex];
                            }

                            groups.Add(string.Format(" {0} {1}", HUNDREDS[sHundreds],  fractionalConjugation));
                        }else if(dTens > 0 && sHundreds + jSingles == 0)
                        {
                            string fractionalConjugation = "";
                            if (ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                fractionalConjugation = DECIMALFIRSTCONJUGATION[conjugationIndex];
                            }
                            else
                            {
                                fractionalConjugation = DECIMALSECONDCONJUGATION[conjugationIndex];
                            }

                            string tensForm = "";
                            if (ordinalSingle == 1)
                            {
                                tensForm = FIRSTFRACTIONALTENS[dTens];
                            }
                            else if (ordinalSingle >= 2 && ordinalSingle <= 4)
                            {
                                tensForm = SECONDFRACTIONALTENS[dTens];

                            }
                            else
                            {
                                tensForm = THIRDFRACTIONALTENS[dTens];

                            }
                            groups.Add(string.Format(" {0} {1}", tensForm, fractionalConjugation));
                        }
                    }
                    else
                    {
                        conjugation = getConjugation(conjugationIndex, conjugationType);
                        groups.Add(string.Format(" {0} {1} {2} {3} {4}", HUNDREDS[sHundreds], TENS[dTens], TEENS[nTeens], SINGLES[jSingles], conjugation));
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

        

        public string getConjugation(int conjugationIndex, int conjugationType)
        {
            string conjugation = "";

            if (conjugationType == 0)
            {
                conjugation = FIRSTCONJUGATION[conjugationIndex];
            }
            else if (conjugationType == 1)
            {
                conjugation = SECONDCONJUGATION[conjugationIndex];
            }
            else
            {
                conjugation = THIRDCONJUGATION[conjugationIndex];
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

