using System;


namespace ConvertNumberIntoWords
{
    public class StringIterator
    {

        private string inputNumber;
        private int iterations = 0;

        public StringIterator(string number)
        {
            inputNumber = number;
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
    }
}
