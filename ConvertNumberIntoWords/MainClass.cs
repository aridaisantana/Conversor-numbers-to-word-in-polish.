using System;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        
        static void Main(string[] args)
        {
            Conversor conversor = new Conversor("999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999.999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999");
            foreach (string result in conversor.Convert())
            {
                Console.WriteLine(result + "\n");
            }

        }
    }
}

