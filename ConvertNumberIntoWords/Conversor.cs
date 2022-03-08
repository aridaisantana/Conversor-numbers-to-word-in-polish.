using System;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class Conversor
    {

        private ConcurrentDictionary<string, string> results;
        private string input = "";

        public Conversor(string input)
        {
            this.results = new ConcurrentDictionary<string, string>();
            this.input = input;
        }

        public ConcurrentDictionary<string, string> Convert()
        {
            try
            {
                string inputString = input;
                //Check if is Scientific Notation and convert it back to numeric format.
                bool isScientificNotation = Regex.IsMatch(input, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");
                if (isScientificNotation)
                {
                    inputString = new ScientificNotation().Convert(inputString);
                    if (inputString == "Infinity" || inputString == "error")
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                
                if(new Validator().Validate(inputString))
                {
                    bool isFractional = Regex.IsMatch(inputString, @"[+-]?\d+\/[+-]?\d+");
                    bool isDecimal = Regex.IsMatch(inputString, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");
                    bool isNumerical = Regex.IsMatch(inputString, @"^[+-]?[0-9]*$");

                    if (isFractional)
                    {
                        FractionalConversion(inputString);
                    }
                    else if (isDecimal)
                    {
                        DecimalConversion(inputString);
                    }
                    else if (isNumerical)
                    {
                        IntegerConversion(inputString);
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
            }
            catch(Exception e)
            {
                if (e.GetType() == typeof(IndexOutOfRangeException))
                {
                    Console.WriteLine("El número es demasiado grande");
                }

                if(e.GetType() == typeof(FormatException))
                {
                    Console.WriteLine("Formato inválido");
                }
            }
            return results;
        }

        private void FractionalConversion(string input)
        {
            string[] operators = input.Split('/');
            string numerator = operators[0];
            string signedNumerator = numerator;
            string denominator = operators[1];
            string signedDenominator = denominator;
            bool isNumeratorCardinal = !Regex.IsMatch(numerator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");
            bool isDenominatorCardinal = !Regex.IsMatch(denominator, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");

            if (isNumeratorCardinal && isDenominatorCardinal)
            {
                string numeratorResult = "";
                string denominatorResult = "";
                string [] proccessedSign = new string[] { };

                //If there is any sign operator we process it accordignly.
                if (Regex.IsMatch(numerator, @"[+-]") || Regex.IsMatch(denominator, @"[+-]"))
                {
                    proccessedSign = new SignedNumber().ProcessFractionalSignedNumber(numerator, denominator);
                }
                string sign = "";
            
                if (proccessedSign.Length > 0)
                {
                    numerator = proccessedSign[0];
                    denominator = proccessedSign[1];
                    sign = proccessedSign[2];
                }
                try
                {
                    Parallel.Invoke(
                        () => {
                            numeratorResult = sign + new Cardinal(new StringIterator(numerator)).ConvertIntoWords();
                        },
                        () => {
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
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
                }
                results["Fraction"] = numeratorResult + " " + denominatorResult;
                try
                {
                    double doubleNumerator, doubleDenominator = 0;
                    if (double.TryParse(signedNumerator, out doubleNumerator) && double.TryParse(signedDenominator, out doubleDenominator))
                    {
                        double dividedResult = doubleNumerator / doubleDenominator;
                        string stringDividedResult = dividedResult.ToString();
                        bool isScientificNotation = Regex.IsMatch(stringDividedResult, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");
                        if (isScientificNotation)
                        {
                            stringDividedResult = new ScientificNotation().Convert(stringDividedResult);
                        }
                        
                        if(stringDividedResult != "error")
                        {
                            bool isDivisionDecimal = Regex.IsMatch(stringDividedResult, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");
                            bool isNumerical = Regex.IsMatch(stringDividedResult, @"^[+-]?[0-9]*$");
                            if (isNumerical)
                            {
                                if(new Validator().Validate(stringDividedResult)) IntegerConversion(stringDividedResult);
                            }
                            else if (isDivisionDecimal)
                            {
                                if(new Validator().Validate(stringDividedResult)) DecimalConversion(stringDividedResult);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if(e.GetType() == typeof(OverflowException))
                    {
                        return;
                    }else if(e.GetType() == typeof(IndexOutOfRangeException))
                    {
                        Console.WriteLine("El número es demasiado grande");
                    }
                }
            }
        }

        private void DecimalConversion(string input)
        {
            string[] decimalParts = input.Split('.');
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];
            string[] proccessedSign = new string[] { };
            //If there is any sign operator we process it accordingly.
            if (Regex.IsMatch(cardinalPart, @"[+-]"))
            {
                proccessedSign = new SignedNumber().ProcessSignedNumber(cardinalPart);
            }
            string sign = "";
            if (proccessedSign.Length > 0)
            {
                cardinalPart = proccessedSign[0];
                sign = proccessedSign[1];
            }
            string cardinalPartResult = "";
            string decimalPartResult = "";
            try
            {
                Parallel.Invoke(
                    () => {
                        cardinalPartResult = sign + new Cardinal(new StringIterator(cardinalPart)).ConvertIntoWords();
                    },
                    () => {
                        decimalPartResult = new Decimal(new StringIterator(decimalPart), decimalPart.Length).ConvertIntoWords();
                    }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
            }
            results["Decimal"] = cardinalPartResult + " " + decimalPartResult;
        }

        private void IntegerConversion(string input)
        {
            string inputTmp = input;
            if (inputTmp.Length == 1 && int.Parse(inputTmp) == 0) {
                results["Cardinal"] = "zero";
            }
            else
            {
                string[] proccessedSign = new string[] { };
                if (Regex.IsMatch(inputTmp, @"[+-]"))
                {
                    proccessedSign = new SignedNumber().ProcessSignedNumber(inputTmp);
                }
                string sign = "";
                if (proccessedSign.Length > 0)
                {
                    inputTmp = proccessedSign[0];
                    sign = proccessedSign[1];
                }
                try
                {
                    if(sign != "")
                    {
                        results["Cardinal"] = sign + new Cardinal(new StringIterator(inputTmp)).ConvertIntoWords();
                    }
                    else
                    {
                        Parallel.Invoke(
                        () => {
                          results["Cardinal"] = sign + new Cardinal(new StringIterator(inputTmp)).ConvertIntoWords();
                          results["Multiplicativo"] = sign + new Multiplicative(new Cardinal(new StringIterator(inputTmp))).ConvertIntoWords();
                        },
                        () => {
                          results["Ordinal"] = sign + new Ordinal(new StringIterator(inputTmp)).ConvertIntoWords();
                        }
                        );
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
                }

            }
        }
    }
}
