using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class Conversor
    {

        private ArrayList results = new ArrayList();

        public ArrayList Convert(string input)
        {
            bool isFractional = Regex.IsMatch(input, @"\d+\/\d+");
            bool isDecimal = Regex.IsMatch(input, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");

            if (isFractional)
            {
                FractionalConversion(input);
            }else if (isDecimal)
            {
                DecimalConversion(input);
            }
            
                
        
            return results;
        }

        public void FractionalConversion(string input)
        {
            string[] operators = input.Split('/');
            string numerator = operators[0];
            string denominator = operators[1];
            bool isNumeratorCardinal = !Regex.IsMatch(numerator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");
            bool isDenominatorCardinal = !Regex.IsMatch(denominator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");

            if (isNumeratorCardinal && isDenominatorCardinal)
            {

                string numeratorResult = "";
                string denominatorResult = "";

                Parallel.Invoke(
                    () => {
                        numeratorResult = new Cardinal(new StringIterator(numerator)).ConvertIntoWords();
                    },

                    () => {
                        try
                        {
                            int ordinalSingle = 0;
                            if (denominator.Length >= 3)
                            {
                                ordinalSingle = int.Parse(denominator.Substring(denominator.Length - 3)) % 10;
                            }
                            else if (denominator.Length != 0)
                            {
                                ordinalSingle = int.Parse(denominator) % 10;
                            }
                            denominatorResult = new Fractions(new StringIterator(denominator), ordinalSingle).ConvertIntoWords();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
                        }

                    }
                );
                results.Add(numeratorResult + " " + denominatorResult);
                try
                {
                    double dividedResult = (double.Parse(numerator) / double.Parse(denominator));
                    string stringDividedResult = dividedResult.ToString();
                    bool isDivisionDecimal = Regex.IsMatch(stringDividedResult, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");

                    if (!isDivisionDecimal)
                    {
                        string cardinal = "";
                        Parallel.Invoke(
                            () => {
                                cardinal = new Cardinal(new StringIterator(stringDividedResult)).ConvertIntoWords();
                                string multiplicative = cardinal + " razy";
                                results.Add(cardinal);
                                results.Add(multiplicative);
                            },
                            () => {
                                results.Add(new Ordinal(new StringIterator(stringDividedResult)).ConvertIntoWords());
                            }
                        );
                    }
                    else
                    {
                        DecimalConversion(stringDividedResult);
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("Error of type: " + e.GetType());
                }
            }
        }

        public void DecimalConversion(string input)
        {
            string[] decimalParts = input.Split('.');
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];
            string cardinalPartResult = "";
            string decimalPartResult = "";
            Parallel.Invoke(
                () => {
                    cardinalPartResult = new Cardinal(new StringIterator(cardinalPart)).ConvertIntoWords();
                },
                () => {
                    decimalPartResult = new Decimal(new StringIterator(decimalPart), decimalPart.Length).ConvertIntoWords();
                }
            );
            results.Add(cardinalPartResult + " " + decimalPartResult);
        }

    }
}
