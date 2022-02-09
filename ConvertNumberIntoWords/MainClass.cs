using System;
using ConvertNumberIntoWords;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        static void Main(string[] args)
        {
         
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
            string input = "34";
            Decimal decimals = new Decimal(new StringIterator("23"), input.Length);
            Multiplicative multiplicative = new Multiplicative();

            string number = "12/67";
            string [] coeficientes = number.Split('/');
            string numerador = coeficientes[0];
            string denominador = coeficientes[1];
            Console.WriteLine(new Cardinal(new StringIterator(numerador)).ConvertIntoWords() + " " + new Fractions(new StringIterator(denominador), int.Parse(numerador) % 10).ConvertIntoWords());
           
       

           
        }
    }
}
