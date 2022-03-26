using System;
using System.Text.RegularExpressions;

namespace ServicioNumerosPolacoWCF
{
    public class Validator
    {
        public bool Validate(string input)
        {
            bool isFractional = Regex.IsMatch(input, @"[+-]?\d+\/[+-]?\d+");
            bool isDecimal = Regex.IsMatch(input, @"[+-]?([0-9]+[.,][0-9]+|\.[0-9]+)");
            bool isNumerical = Regex.IsMatch(input, @"^[+-]?[0-9]*$");

            if (isFractional)
            {
                return isFractionalNumberValid(input);
            }
            else if (isDecimal)
            {
                return isDecimalNumberValid(input);
            }
            else if (isNumerical)
            {
                return isIntegerNumberValid(input);
            }

            return false;
        }

        private bool isFractionalNumberValid(string input)
        {
            string[] operators = input.Split('/');
            if (operators.Length == 0)
            {
                return false;
            }
            string numerator = operators[0];
            string denominator = operators[1];

            if (!Regex.IsMatch(numerator, @"\d{1,144}")
                || !Regex.IsMatch(denominator, @"\d{1,144}"))
            {
                return false;
            }
            return true;
        }

        private bool isDecimalNumberValid(string input)
        {
            input = input.Replace(',', '.');
            string[] decimalParts = input.Split('.');
            if (decimalParts.Length == 0)
            {
                return false;
            }
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];


            if (!Regex.IsMatch(cardinalPart, @"\d{1,144}")
                || !Regex.IsMatch(decimalPart, @"\d{1,144}"))
            {
                return false;
            }

            return true;

        }

        private bool isIntegerNumberValid(string input)
        {
            string inputTmp = input;
            if (!Regex.IsMatch(inputTmp, @"\d{1,144}"))
            {
                return false;
            }
            return true;
        }
    }
}
