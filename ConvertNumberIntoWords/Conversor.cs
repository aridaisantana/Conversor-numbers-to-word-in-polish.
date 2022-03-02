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
            try
            {
                string inputString = input;
                bool isScientificNotation = Regex.IsMatch(input, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");
                if (isScientificNotation)
                {
                    inputString = ScientificNotation(inputString);
                    if (inputString == "Infinity" || inputString == "error" )
                    {
                        throw new IndexOutOfRangeException();
                    }
                }

                bool isFractional = Regex.IsMatch(inputString, @"[+-]?\d+\/[+-]?\d+");
                bool isDecimal = Regex.IsMatch(inputString, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");
                bool isNumerical = Regex.IsMatch(inputString, @"^[+-]?[0-9]*$");

                if (isFractional)
                {
                    ValidateFractionalNumbers(inputString);
                    FractionalConversion(inputString);
                }
                else if (isDecimal)
                {
                    ValidateDecimalNumbers(inputString);
                    DecimalConversion(inputString);
                }
                else if (isNumerical)
                {
                    ValidateIntegerNumbers(inputString);
                    IntegerConversion(inputString);
                }
                else
                {
                    throw new FormatException();
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

        public void FractionalConversion(string input)
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
                if(Regex.IsMatch(numerator, @"[+-]") || Regex.IsMatch(denominator, @"[+-]"))
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
                Parallel.Invoke(
                    () => {
                        numeratorResult = sign + new Cardinal(new StringIterator(numerator)).ConvertIntoWords();
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
                    double doubleNumerator, doubleDenominator = 0;
                    if (double.TryParse(signedNumerator, out doubleNumerator) && double.TryParse(signedDenominator, out doubleDenominator))
                    {
                        double dividedResult = doubleNumerator / doubleDenominator;
                        string stringDividedResult = dividedResult.ToString();
                        bool isScientificNotation = Regex.IsMatch(stringDividedResult, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");
                        if (isScientificNotation)
                        {
                            stringDividedResult = ScientificNotation(stringDividedResult);
                        }
                        
                        if(stringDividedResult != "error")
                        {
                            bool isDivisionDecimal = Regex.IsMatch(stringDividedResult, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");
                            bool isNumerical = Regex.IsMatch(stringDividedResult, @"^[0-9]*$");
                            if (isNumerical)
                            {
                                ValidateIntegerNumbers(stringDividedResult);
                                IntegerConversion(stringDividedResult);
                            }
                            else if (isDivisionDecimal)
                            {
                                ValidateDecimalNumbers(stringDividedResult);
                                DecimalConversion(stringDividedResult);
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

        public void DecimalConversion(string input)
        {
            string[] decimalParts = input.Split('.');
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];
            string[] proccessedSign = new string[] { };
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
            Parallel.Invoke(
                () => {
                  cardinalPartResult = sign + new Cardinal(new StringIterator(cardinalPart)).ConvertIntoWords();
                },
                () => {
                    decimalPartResult = new Decimal(new StringIterator(decimalPart), decimalPart.Length).ConvertIntoWords();
                }
            );
            results.Add(cardinalPartResult + " " + decimalPartResult);
        }

        public void IntegerConversion(string input)
        {
            string inputTmp = input;
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
            Parallel.Invoke(
                () => {
                    results.Add(sign + new Cardinal(new StringIterator(inputTmp)).ConvertIntoWords());
                    results.Add(sign + new Multiplicative(new Cardinal(new StringIterator(inputTmp))).ConvertIntoWords());
                },
                () => {
                    results.Add(sign + new Ordinal(new StringIterator(inputTmp)).ConvertIntoWords());
                }
            );
        }

        public void ValidateFractionalNumbers(string input)
        {
            string[] operators = input.Split('/');
            if (operators.Length == 0)
            {
                throw new FormatException();
            }
            string numerator = operators[0];
            string denominator = operators[1];
            if(numerator.Contains("+") || numerator.Contains("-"))
            {
                numerator = numerator.Remove(0, 1);
            }

            if (denominator.Contains("+") || denominator.Contains("-"))
            {
                denominator = denominator.Remove(0, 1);
            }

            if (numerator.Length > 144 || denominator.Length > 144)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void ValidateDecimalNumbers(string input)
        {
            string[] decimalParts = input.Split('.');
            if (decimalParts.Length == 0)
            {
                throw new FormatException();
            }
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];

            if (cardinalPart.Contains("+") || cardinalPart.Contains("-"))
            {
                cardinalPart = cardinalPart.Remove(0, 1);
            }

            if (decimalPart.Contains("+") || decimalPart.Contains("-"))
            {
                decimalPart = decimalPart.Remove(0, 1);
            }

            if (cardinalPart.Length > 144 || decimalPart.Length > 144)
            {
                throw new IndexOutOfRangeException();
            }

        }

        public void ValidateIntegerNumbers(string input)
        {
            string inputTmp = input;
            if (inputTmp.Contains("+") || inputTmp.Contains("-"))
            {
                inputTmp = inputTmp.Remove(0, 1);
            }

            if (inputTmp.Length > 144)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public string ScientificNotation(string input)
        {
            string [] scientificNotationParts = input.Split('E');
            string firstPart = scientificNotationParts[0];
            string secondPart = scientificNotationParts[1];
            double result = 0;

            if (secondPart.Contains("+"))
            {
                if (firstPart.Contains("+"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }else if (firstPart.Contains("-"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = -1 * double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }
                else
                {
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }
            }
            else if (secondPart.Contains("-"))
            {
                if (firstPart.Contains("+"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, -1 * double.Parse(secondPart.Remove(0, 1))));
                }
                else if (firstPart.Contains("-"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = -1 * double.Parse(firstPart) * (Math.Pow(10, -1 * double.Parse(secondPart.Remove(0, 1))));
                }
                else
                {
                    result = double.Parse(firstPart) * (Math.Pow(10, -1 * double.Parse(secondPart.Remove(0, 1))));
                }
            }
            else
            {
                if (firstPart.Contains("+"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
                else if (firstPart.Contains("-"))
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = -1 * double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
                else
                {
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
            }

            if (result.ToString().Contains("E")) return "error";
            return result.ToString();
        }
    }


}
