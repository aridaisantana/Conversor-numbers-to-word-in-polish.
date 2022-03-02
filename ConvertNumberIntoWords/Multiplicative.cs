using System;
namespace ConvertNumberIntoWords
{
    public class Multiplicative : Numbers
    {
        private Cardinal cardinal;

        public Multiplicative(Cardinal cardinal)
        {
            this.cardinal = cardinal;
        }

        public string ConvertIntoWords()
        {
            return cardinal.ConvertIntoWords() + " razy";
        }
    }
}
