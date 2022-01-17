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

        public void setConjugation()
        {
           
            if (inputNumber.Length != 0)
            {
                if (inputNumber.Length <= 3)
                {

                    int jSingles = int.Parse(inputNumber) % 10;
                    kConjugation = setConjugationHelper(jSingles);

                }
                else if (inputNumber.Length > 3)
                {
                    int jSingles = int.Parse(inputNumber.Substring(inputNumber.Length - 3)) % 10;
                    kConjugation = setConjugationHelper(jSingles);
                }
            }
            
        }

        public int setConjugationHelper(int jSingles)
        {
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

        public string Next()
        {
            if(inputNumber.Length == 0) return "";

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

        public string getInputNumber()
        {
            return inputNumber;
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
