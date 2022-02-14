using System;


namespace ConvertNumberIntoWords
{
    public class StringIterator : IStringIterator
    {

        private string inputNumber;
        private int conjugationType = 0;

        public StringIterator(string number)
        {
            inputNumber = number;
        }


        public string Next()
        {
            if (!HasNext()) return "";

            string result = "";

            if (inputNumber.Length >= 3)
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

        public bool HasNext()
        {
            return inputNumber.Length != 0;
        }

        public int getConjugationType()
        {
            getConjugationTypeHelper();
            return conjugationType;
        }

        private void getConjugationTypeHelper()
        {

            if (inputNumber.Length != 0)
            {
                if (inputNumber.Length <= 3)
                {
                    conjugationType = getConjugationTypeHelper(inputNumber);
                }
                else if (inputNumber.Length > 3)
                {
                    conjugationType = getConjugationTypeHelper(inputNumber.Substring(inputNumber.Length - 3));
                }
            }

        }

        private int getConjugationTypeHelper(string number)
        {
            int jSingles = 0;

            try
            {
                jSingles = int.Parse(number) % 10;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha habido un error de tipo:" + ex.GetType() + " con Iterador");
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
    }
}

