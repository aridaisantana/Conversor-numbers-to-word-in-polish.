using System;
using System.Text.RegularExpressions;
namespace ConvertNumberIntoWords
{
    public class ScientificNotation
    {
        public string Convert(string input)
        {
            return ConvertHelper(input);
        }

        private string ConvertHelper(string input)
        {
            string[] scientificNotationParts = input.Split('E');
            string firstPart = scientificNotationParts[0];
            string secondPart = scientificNotationParts[1];
            double result = 0;
            string firstPartSign = Regex.Match(firstPart, @"[+-]").ToString();
            string secondPartSign = Regex.Match(firstPart, @"[+-]").ToString();

            if (secondPartSign == "+")
            {
                if (firstPartSign == "+")
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }
                else if (firstPartSign == "-")
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = -1 * double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }
                else
                {
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart.Remove(0, 1))));
                }
            }
            else if (secondPartSign == "-")
            {
                if (firstPartSign == "+")
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, -1 * double.Parse(secondPart.Remove(0, 1))));
                }
                else if (firstPartSign == "-")
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
                if (firstPartSign == "+")
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
                else if (firstPartSign == "-")
                {
                    firstPart = firstPart.Remove(0, 1);
                    result = -1 * double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
                else
                {
                    result = double.Parse(firstPart) * (Math.Pow(10, double.Parse(secondPart)));
                }
            }

            if (Regex.IsMatch(result.ToString(), @"[Ee]")) return "error";
            return result.ToString();
        }
    }
}
