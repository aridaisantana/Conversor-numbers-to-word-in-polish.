using System;


namespace ConvertNumberIntoWords
{
    public class StringIterator
    {

        private string inputNumber;
        private int gMagnitude = -1;
        private string lastDigits = "";

        public StringIterator(string number)
        {
            inputNumber = number;
        }

        public int getConjugation()
        {
            if (lastDigits.Length != 0)
            {
                if (lastDigits.Length == 1)
                {
                    int number = int.Parse(lastDigits);
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
            return 2;
          
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
                lastDigits = inputNumber;
                inputNumber = "";
                

            }
            else{
                result = inputNumber;
                lastDigits = inputNumber;
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
