using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;

namespace ConvertNumberIntoWords
{
    public class MainClass
    {
        
        static void Main(string[] args)
        {
            Conversor conversor = new Conversor();
            foreach (string result in conversor.Convert("23E3"))
            {
                Console.WriteLine(result + "\n");
            }

        }
    }
}

