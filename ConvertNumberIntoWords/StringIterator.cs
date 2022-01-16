using System;


namespace ConvertNumberIntoWords
{
    public class StringIterator
    {

        private string inputNumber;
        private int gMagnitude = -1;

        public StringIterator(string number)
        {
            inputNumber = number;
        }

        public int getConjugation()
        {
            if (inputNumber.Length != 0)
            {
                if (inputNumber.Length == 1)
                {
                    int number = int.Parse(inputNumber);
                    if (number == 1)
                    {
                        return 0;
                    }
                    else if (number >= 2 && number <= 4)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 2;
                }

                
            }
            return 0;
          
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
            else if (inputNumber.Length == 2){
                result = inputNumber;
                inputNumber = "";

            }
            else{
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
