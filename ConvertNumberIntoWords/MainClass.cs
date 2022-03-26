using System;
using System.Collections.Generic;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        
        static void Main(string[] args)
        {
            Conversor conversor = new Conversor("23E-2");
            foreach (KeyValuePair<string, string> result in conversor.Convert())
            {
                Console.WriteLine(result.Key + ":" + result.Value);
            }

        }
    }
}

