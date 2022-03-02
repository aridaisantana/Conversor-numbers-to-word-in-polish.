using System;
namespace ConvertNumberIntoWords
{
    public class SignedNumber
    {
        public string [] ProcessFractionalSignedNumber(string num, string den)
        {
            string numeratorSign = "";
            string denominatorSign = "";
            string sign = "";
            string numerator = num;
            string denominator = den;

            if (numerator.Contains("+"))
            {
                numerator = numerator.Remove(0, 1);
            }
            else if (numerator.Contains("-"))
            {
                numerator = numerator.Remove(0, 1);
                numeratorSign = "minus";
            }

            if (denominator.Contains("+"))
            {
                denominator = denominator.Remove(0, 1);
            }
            else if (denominator.Contains("-"))
            {
                denominator = denominator.Remove(0, 1);
                denominatorSign = "minus";
            }

            if (numeratorSign == "minus" && denominatorSign == "minus")
            {
                sign = "";
            }
            else if (numeratorSign == "minus" || denominatorSign == "minus")
            {
                sign = "minus ";
            }

            string [] result = new string[3] { numerator, denominator, sign };
            return result;
        }

        public string [] ProcessSignedNumber(string number)
        {
    
            string cardinalPart = number;
            string sign = "";
            if (cardinalPart.Contains("-"))
            {
                cardinalPart = cardinalPart.Remove(0, 1);
                sign = "minus ";
            }
            else if (cardinalPart.Contains("+"))
            {
                cardinalPart = cardinalPart.Remove(0, 1);
            }

            string [] result = new string[2] { cardinalPart, sign };
            return result;
        }
    }
}
