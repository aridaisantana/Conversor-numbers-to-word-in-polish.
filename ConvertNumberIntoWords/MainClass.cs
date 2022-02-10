using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        static void Main(string[] args)
        {

            string input = args[0];

            bool containsSlashCharacter = Regex.IsMatch(input, @"\d+\/\d+");
            if (containsSlashCharacter)
            {
                string[] operators = input.Split('/');
                string numerator = operators[0];
                string denominator = operators[1];
                bool isNumeratorCardinal = !Regex.IsMatch(numerator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");
                bool isDenominatorCardinal = !Regex.IsMatch(denominator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");

                if(isNumeratorCardinal && isDenominatorCardinal)
                {
                    Parallel.Invoke(
                        () =>{
                            Console.WriteLine("Begin first task...");
                            Console.WriteLine(new Cardinal(new StringIterator(numerator)).ConvertIntoWords());
                        },  

                        () =>{
                            Console.WriteLine("Begin second task...");
                            int ordinalSingle = 0;
                            if(denominator.Length >= 3)
                            {
                                ordinalSingle= int.Parse(denominator.Substring(denominator.Length - 3)) % 10;
                            }
                            else if(denominator.Length != 0)
                            {
                                ordinalSingle = int.Parse(denominator) % 10;
                            }
                            Console.WriteLine(new Fractions(new StringIterator(denominator), ordinalSingle).ConvertIntoWords());
                        }, 

                        () =>
                             {
                                 Console.WriteLine("Begin third task...");
                                 try
                                 {
                                     double dividedResult = (double.Parse(numerator) / double.Parse(denominator));
                                     string stringDividedResult = dividedResult.ToString();
                                     bool isDecimal = Regex.IsMatch(stringDividedResult, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");

                                     if (!isDecimal)
                                     {
                                        Parallel.Invoke(
                                            () =>{
                                                Console.WriteLine(new Cardinal(new StringIterator(stringDividedResult)).ConvertIntoWords());
                                            },
                                            () =>{
                                                Console.WriteLine(new Ordinal(new StringIterator(stringDividedResult)).ConvertIntoWords());
                                            }
                                        );
                                     }
                                     else
                                     {
                                         string[] decimalParts = stringDividedResult.Split('.');
                                         string cardinalPart = decimalParts[0];
                                         string decimalPart = decimalParts[1];
                                         Parallel.Invoke(
                                             () => {
                                                 Console.WriteLine(new Cardinal(new StringIterator(cardinalPart)).ConvertIntoWords());
                                             },
                                             () => {
                                                 Console.WriteLine(new Decimal(new StringIterator(decimalPart), decimalPart.Length).ConvertIntoWords());
                                             }
                                         );
                                     }
                                     
                                     
                                 }
                                 catch(Exception e)
                                 {
                                     Console.WriteLine("Error of type: " + e.GetType());
                                 }
                                
                             } 
                         ); 

                }

            }
            
            


            Cardinal cardinal = new Cardinal(new StringIterator("123456789"));
            Console.WriteLine(cardinal.ConvertIntoWords());

            Ordinal ordinal = new Ordinal(new StringIterator("123456789"));
            Console.WriteLine(ordinal.ConvertIntoWords());
            //Console.WriteLine("1º ->" + cardinal.ConvertIntoWords("0"));
            //Console.WriteLine("1º ->" + cardinal.ConvertIntoWords("1"));
            //Console.WriteLine("2º->" + cardinal.ConvertIntoWords("-23"));
            //Console.WriteLine("3º->" + cardinal.ConvertIntoWords("230"));
            //Console.WriteLine("4º->" + cardinal.ConvertIntoWords("2301"));
            //Console.WriteLine("5º->" + cardinal.ConvertIntoWords("423010"));
            //Console.WriteLine("6º->" + cardinal.ConvertIntoWords("1320435"));
            //Console.WriteLine("7º->" + cardinal.ConvertIntoWords("12343"));
            //Console.WriteLine("8º->" + cardinal.ConvertIntoWords("5390756"));
            //Console.WriteLine("9º->" + cardinal.ConvertIntoWords("12340234"));
            //Console.WriteLine("10º->" + cardinal.ConvertIntoWords("456789456"));
            //Console.WriteLine("11º->" + cardinal.ConvertIntoWords("2340543567"));
            //Console.WriteLine("12º->" + cardinal.ConvertIntoWords("1000000000000000000000"));
            //Console.WriteLine("13º->" + cardinal.ConvertIntoWords("7685768756568"));
            //Console.WriteLine("13º->" + cardinal.ConvertIntoWords("587634876598654724"));
            //Console.WriteLine("13º->" + cardinal.ConvertIntoWords("100003000000000040000000004000000"));
            //Console.WriteLine("13º->" + cardinal.ConvertIntoWords("9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"));
            //Console.WriteLine("13º->" + cardinal.ConvertIntoWords("0000001"));
            string input2 = "34";
            Decimal decimals = new Decimal(new StringIterator("23"), input2.Length);
            Multiplicative multiplicative = new Multiplicative();

            string number = "12/67";
            string [] coeficientes = number.Split('/');
            string numerador = coeficientes[0];
            string denominador = coeficientes[1];
            Console.WriteLine(new Cardinal(new StringIterator(numerador)).ConvertIntoWords() + " " + new Fractions(new StringIterator(denominador), int.Parse(numerador) % 10).ConvertIntoWords());
           
       

           
        }
    }
}
