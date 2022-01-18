using System;


namespace ConvertNumberIntoWords
{
    public class StringIterator
    {

        private string inputNumber;
        private int gMagnitude = 0;
        private int kConjugation = 0;

        public StringIterator(string number)
        {
            inputNumber = number;
        }

        public void updateConjugation()
        {
           
            if (inputNumber.Length != 0)
            {
                if (inputNumber.Length <= 3)
                {
                    kConjugation = setConjugation(inputNumber);
                }
                else if (inputNumber.Length > 3)
                {
                    kConjugation = setConjugation(inputNumber.Substring(inputNumber.Length - 3));
                }
            }
            
        }

        private int setConjugation(string number)
        {
            int jSingles = 0;

            try
            {
                jSingles = int.Parse(number) % 10;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha habido un error de tipo:" + ex.GetType() + " con la cifra ingresada");
            }

            
            if (jSingles == 1)
            {
                return 0;
            }
            else if (jSingles >= 2 && jSingles <= 4)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public int getConjugation()
        {
            return kConjugation;
        }

        public bool HasNext()
        {
            return inputNumber.Length != 0;
        }

        public string Next()
        {
            if(!HasNext()) return "";

            string result = "";

            if ( inputNumber.Length >= 3)
            {
                result = inputNumber.Substring(inputNumber.Length - 3);
                inputNumber = inputNumber.Substring(0, inputNumber.Length - 3);
            }
            else
            {
                result = inputNumber;
                inputNumber = "";
            }
              
            return result;
            
        }

        public void incrementGMagnitude()
        {
            gMagnitude++;
        }

        public int getGMagnitude()
        {
            return gMagnitude;
        }
    }
}
