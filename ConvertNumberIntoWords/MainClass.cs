using System;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        
        static void Main(string[] args)
        {
            Conversor conversor = new Conversor();
            foreach (string result in conversor.Convert("-2.999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"))
            {
                Console.WriteLine(result + "\n");
            }

        }
    }
}

