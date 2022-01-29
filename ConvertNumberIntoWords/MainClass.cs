using System;
using ConvertNumberIntoWords;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            Cardinal cardinal = new Cardinal();
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
            Ordinal ordinal = new Ordinal();
            Console.WriteLine("1º ->" + ordinal.ConvertIntoWords("23"));
            Console.WriteLine("2º ->" + ordinal.ConvertIntoWords("105"));
            Console.WriteLine("3º ->" + ordinal.ConvertIntoWords("317"));
            Console.WriteLine("4º ->" + ordinal.ConvertIntoWords("3509"));
            Console.WriteLine("5º ->" + ordinal.ConvertIntoWords("11324"));
            Console.WriteLine("6º ->" + ordinal.ConvertIntoWords("405701"));
            Console.WriteLine("7º ->" + ordinal.ConvertIntoWords("1003197"));
            Console.WriteLine("8º ->" + ordinal.ConvertIntoWords("1000"));
            Console.WriteLine("9º ->" + ordinal.ConvertIntoWords("1001"));
            Console.WriteLine("10º ->" + ordinal.ConvertIntoWords("2000"));
            Console.WriteLine("11º ->" + ordinal.ConvertIntoWords("2001"));
            Console.WriteLine("12º ->" + ordinal.ConvertIntoWords("2016"));
            Console.WriteLine("13º ->" + ordinal.ConvertIntoWords("5000"));
            Console.WriteLine("14º ->" + ordinal.ConvertIntoWords("100000"));
            Console.WriteLine("15º ->" + ordinal.ConvertIntoWords("100001"));
            Console.WriteLine("16º ->" + ordinal.ConvertIntoWords("100400"));
            Console.WriteLine("17º ->" + ordinal.ConvertIntoWords("101000"));
            Console.WriteLine("18º ->" + ordinal.ConvertIntoWords("200000"));
            Console.WriteLine("19º ->" + ordinal.ConvertIntoWords("200008"));
            Console.WriteLine("20º ->" + ordinal.ConvertIntoWords("300000"));
            Console.WriteLine("21º ->" + ordinal.ConvertIntoWords("300100"));
            Console.WriteLine("22º ->" + ordinal.ConvertIntoWords("500000"));
            Console.WriteLine("23º ->" + ordinal.ConvertIntoWords("1000000"));
            Console.WriteLine("24º ->" + ordinal.ConvertIntoWords("1001000"));
            Console.WriteLine("25º ->" + ordinal.ConvertIntoWords("1001100"));
            Console.WriteLine("26º ->" + ordinal.ConvertIntoWords("1002000"));
            Console.WriteLine("27º ->" + ordinal.ConvertIntoWords("1002004"));
            Console.WriteLine("28º ->" + ordinal.ConvertIntoWords("1500000"));
            Console.WriteLine("29º ->" + ordinal.ConvertIntoWords("6000000"));
            Console.WriteLine("30º ->" + ordinal.ConvertIntoWords("6200000"));
            Console.WriteLine("30º ->" + ordinal.ConvertIntoWords("114000"));
            Console.WriteLine("30º ->" + ordinal.ConvertIntoWords("3200000"));
           
        }
    }
}
