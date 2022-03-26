using System;
namespace ServicioNumerosPolacoWCF
{
    public class Multiplicative : INumbers
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
