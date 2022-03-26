using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Web;


namespace ServicioNumerosPolacoWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NumberConverterService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NumberConverterService.svc o NumberConverterService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioNumerosPolaco : IServicioNumerosPolaco
    {

        private string input = "";
        private string Originalinput = "";
        private CultureInfo language;
        private List<Conversion> conversions = new List<Conversion>();
        private Cabecera cabecera = null;

        //public (Cabecera, List<Conversion>) Traducir(string value, string lenguaje)
        public List<Conversion> Traducir(string value, string lenguaje)
        {
            
            input = value;
            Originalinput = value;

            if (lenguaje != null)
            {
                if (lenguaje.IndexOf("-") > -1) lenguaje = lenguaje.Substring(0, lenguaje.IndexOf("-"));
                language = new CultureInfo(lenguaje);
            }
            else language = new CultureInfo("es");
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            
            string signoMas = "";
            string sinFormatear = value;
            input = input.Replace(" ", "");
            if (input[0] == '+') signoMas = "+";
            input = input.Replace("+", "");

            string pattern = @"^[IVXLCDMivxlcdm]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(input))
            {
                input = input.ToUpper();
                input = RomanToArabic(input);
                List<string> romanoCorrecto = arabicToRoman(input);
                if (value.Replace(" ", "").ToUpper() != romanoCorrecto[0])
                {
                    conversion.ErrorRomano = true;
                    conversion.Titulo = HttpContext.GetGlobalResourceObject("Resource", "NumeroRomanoInCorrecto").ToString();
                    conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "NumeroRomanoCorrecto").ToString();
                    conversion.Respuestas = new List<string>();
                    string cardinal = new Cardinal(new StringIterator(input)).ConvertIntoWords();
                    conversion.Respuestas.Add(cardinal + ": " + romanoCorrecto[0]);
                    addMoreRomano(ref conversion, romanoCorrecto);
                    conversions.Add(conversion);
                    cabecera = new Cabecera(sinFormatear, HttpContext.GetGlobalResourceObject("Resource", "NumeroRomanoInCorrecto").ToString() + ": " + sinFormatear.ToUpper().Replace(" ", ""));
                    return conversions;
                }
            }

            //Check if is Scientific Notation and convert it back to numeric format.
            bool isScientificNotation = Regex.IsMatch(value, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");

            if (isScientificNotation)
            {
                input = new ScientificNotation().Convert(input);
                input = input.Replace(',', '.');
                if (input == "Infinity" || input == "error")
                {
                    conversions.Add(getErrorMessage());
                    return conversions;
                }
            }

            if (new Validator().Validate(input))
            {
                bool isFractional = Regex.IsMatch(input, @"[+-]?\d+\/[+-]?\d+");
                bool isDecimal = Regex.IsMatch(input, @"[+-]?([0-9]+[.,][0-9]+|\.[0-9]+)");
                bool isNumerical = Regex.IsMatch(input, @"^[+-]?[0-9]*$");

                if (isFractional)
                {
                    FractionalConversion(input);
                }
                else if (isDecimal)
                {
                    DecimalConversion(input);
                }
                else if (isNumerical)
                {
                    IntegerConversion(input);
                }
                else
                {
                    conversions.Add(getErrorMessage());
                    return conversions;
                }
            }
           

            if(conversions.Count() == 0)
            {
                conversions.Add(getErrorMessage());
            }
            //return (rellenarCabecera(value, input), conversions);
            return conversions;
            
        }

        private void FractionalConversion(string inputString)
        {
            string[] operators = inputString.Split('/');
            string numerator = operators[0];
            string signedNumerator = numerator;
            string denominator = operators[1];
            string signedDenominator = denominator;
            bool isNumeratorCardinal = !Regex.IsMatch(numerator, @"[+-]?([0-9]+\.[0-9]*|\.[0-9]+)");
            bool isDenominatorCardinal = !Regex.IsMatch(denominator, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");

            if (isNumeratorCardinal && isDenominatorCardinal)
            {
                string numeratorResult = "";
                string denominatorResult = "";
                string[] proccessedSign = new string[] { };

                //If there is any sign operator we process it accordignly.
                if (Regex.IsMatch(numerator, @"[+-]") || Regex.IsMatch(denominator, @"[+-]"))
                {
                    proccessedSign = new SignedNumber().ProcessFractionalSignedNumber(numerator, denominator);
                }
                string sign = "";

                if (proccessedSign.Length > 0)
                {
                    numerator = proccessedSign[0];
                    denominator = proccessedSign[1];
                    sign = proccessedSign[2];
                }
                try
                {
                    Parallel.Invoke(
                        () =>
                        {
                            numeratorResult = sign + new Cardinal(new StringIterator(numerator)).ConvertIntoWords();
                        },
                        () =>
                        {
                            int ordinalSingle = 0;
                            if (denominator.Length >= 3)
                            {
                                ordinalSingle = int.Parse(denominator.Substring(denominator.Length - 3)) % 10;
                            }
                            else if (denominator.Length != 0)
                            {
                                ordinalSingle = int.Parse(denominator) % 10;
                            }
                            denominatorResult = new Fractions(new StringIterator(denominator), ordinalSingle).ConvertIntoWords();
                        }
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
                }
                conversions.Add(FraccionarioConversionHelper(numeratorResult + " " + denominatorResult));
                try
                {
                    double doubleNumerator, doubleDenominator = 0;
                    if (double.TryParse(signedNumerator, out doubleNumerator) && double.TryParse(signedDenominator, out doubleDenominator))
                    {
                        double dividedResult = doubleNumerator / doubleDenominator;
                        string stringDividedResult = dividedResult.ToString();
                        bool isScientificNotation = Regex.IsMatch(stringDividedResult, @"[+-]?\d(\.\d+)?[Ee][+-]?\d+");
                        if (isScientificNotation)
                        {
                            stringDividedResult = new ScientificNotation().Convert(stringDividedResult);
                        }

                        if (stringDividedResult != "error")
                        {
                            stringDividedResult = stringDividedResult.Replace(',', '.');
                            bool isDivisionDecimal = Regex.IsMatch(stringDividedResult, @"[+-]?([0-9]+\.[0-9]+|\.[0-9]+)");
                            bool isNumerical = Regex.IsMatch(stringDividedResult, @"^[+-]?[0-9]*$");
                            if (isNumerical)
                            {
                                if (new Validator().Validate(stringDividedResult)) IntegerConversion(stringDividedResult);
                            }
                            else if (isDivisionDecimal)
                            {
                                if (new Validator().Validate(stringDividedResult)) DecimalConversion(stringDividedResult);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(OverflowException))
                    {
                        return;
                    }
                    else if (e.GetType() == typeof(IndexOutOfRangeException))
                    {
                        Console.WriteLine("El número es demasiado grande");
                    }
                }
            }
        }

        private void DecimalConversion(string inputString)
        {
            inputString = inputString.Replace(',', '.');
            string[] decimalParts = inputString.Split('.');
            string cardinalPart = decimalParts[0];
            string decimalPart = decimalParts[1];
            string[] proccessedSign = new string[] { };
            //If there is any sign operator we process it accordingly.
            if (Regex.IsMatch(cardinalPart, @"[+-]"))
            {
                proccessedSign = new SignedNumber().ProcessSignedNumber(cardinalPart);
            }
            string sign = "";
            if (proccessedSign.Length > 0)
            {
                cardinalPart = proccessedSign[0];
                sign = proccessedSign[1];
            }
            string cardinalPartResult = "";
            string decimalPartResult = "";
            try
            {
                Parallel.Invoke(
                    () =>
                    {
                        cardinalPartResult = sign + new Cardinal(new StringIterator(cardinalPart)).ConvertIntoWords();
                    },
                    () =>
                    {
                        decimalPartResult = new Decimal(new StringIterator(decimalPart), decimalPart.Length).ConvertIntoWords();
                    }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
            }
            conversions.Add(DecimalConversionHelper(cardinalPartResult + " " + decimalPartResult));
        }

        private void IntegerConversion(string inputString)
        {
            string inputTmp = inputString;
            if (inputTmp.Length == 1 && int.Parse(inputTmp) == 0)
            {
                conversions.Add(CardinalConversionHelper("zero"));
            }
            else
            {
                string[] proccessedSign = new string[] { };
                if (Regex.IsMatch(inputTmp, @"[+-]"))
                {
                    proccessedSign = new SignedNumber().ProcessSignedNumber(inputTmp);
                }
                string sign = "";
                if (proccessedSign.Length > 0)
                {
                    inputTmp = proccessedSign[0];
                    sign = proccessedSign[1];
                }
                try
                {
                    if (sign != "")
                    {
                        conversions.Add(CardinalConversionHelper(sign + new Cardinal(new StringIterator(inputTmp)).ConvertIntoWords()));
                    }
                    else
                    {
                        Parallel.Invoke(
                        () =>
                        {
                            conversions.Add(CardinalConversionHelper(sign + new Cardinal(new StringIterator(inputTmp)).ConvertIntoWords()));
                            conversions.Add(MultiplicativoConversionHelper(sign + new Multiplicative(new Cardinal(new StringIterator(inputTmp))).ConvertIntoWords()));
                        },
                        () =>
                        {
                            conversions.Add(OrdinalConversionHelper(sign + new Ordinal(new StringIterator(inputTmp)).ConvertIntoWords()));
                        }
                        );
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ha ocurrido un error de tipo:" + e.GetType());
                }

            }
        }

        private Conversion CardinalConversionHelper(string result)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            conversion.Respuestas = new List<string>();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "CardinalTipo").ToString();
            if (Originalinput.Contains("E") || Originalinput.Contains("e"))
            {
                conversion.TitValorNumerico = HttpContext.GetGlobalResourceObject("Resource", "ValorNumerico").ToString();
                conversion.ValorNumerico = result;
            }
            conversion.TitNotas = HttpContext.GetGlobalResourceObject("Resource", "Notas").ToString();
            conversion.Notas = new List<string>();
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas1").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas2").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas3").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas4").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas5").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas6").ToString());
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas7").ToString());

            conversion.Respuestas.Add(result);

            return conversion;
        }

        private Conversion OrdinalConversionHelper(string result)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            conversion.Respuestas = new List<string>();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "OrdinalTipo").ToString();
            conversion.Notas = new List<string>();
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas7").ToString());

            conversion.Respuestas.Add(result);

            return conversion;
        }

        private Conversion MultiplicativoConversionHelper(string result)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            conversion.Respuestas = new List<string>();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "MultiplicativoTipo").ToString();
            conversion.Notas = new List<string>();
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas7").ToString());
            conversion.Respuestas.Add(result);

            return conversion;
        }

        private Conversion FraccionarioConversionHelper(string result)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            conversion.Respuestas = new List<string>();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "FraccionarioTipo").ToString();
            conversion.Notas = new List<string>();
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "CardinalNotas7").ToString());
            conversion.Respuestas.Add(result);

            return conversion;
        }

        private Conversion DecimalConversionHelper(string result)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            Conversion conversion = new Conversion();
            conversion.Respuestas = new List<string>();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "DecimalTipo").ToString();
            conversion.Respuestas.Add(result);

            return conversion;
        }

        public string RomanToArabic(string roman)
        {
            int result = 0;
            int actualNum = 0;
            int nextNum = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                actualNum = ToArabic(roman.Substring(i, 1));
                if (i + 1 != roman.Length)
                {
                    nextNum = ToArabic(roman.Substring(i + 1, 1));
                }
                if (actualNum < nextNum)
                {
                    result -= actualNum;
                }
                else
                {
                    result += actualNum;
                }
            }
            return result.ToString();
        }

        public int ToArabic(string roman)
        {
            switch (roman)
            {
                case "I":
                    return 1;
                case "V":
                    return 5;
                case "X":
                    return 10;
                case "L":
                    return 50;
                case "C":
                    return 100;
                case "D":
                    return 500;
                case "M":
                    return 1000;
            }
            return 0;
        }

        public List<String> arabicToRoman(string arabic)
        {
            if (arabic.Length > 13) return null;
            if (long.Parse(arabic) > 3999999999999) return null;
            List<String> resultList = new List<string>();
            string result = "";
            string romano1 = "";
            string romano2 = "";
            string romano3 = "";
            int j = 0;
            long n = long.Parse(arabic);
            if (n < 1) return null;

            while (n > 3999)
            {
                j++;
                result = "";
                string lastString = arabic.Substring(arabic.Length - 4, 4);
                if (int.Parse(lastString) > 3999)
                {
                    lastString = arabic.Substring(arabic.Length - 3, 3);
                    arabic = arabic.Substring(0, arabic.Length - 3);
                }
                else
                {
                    arabic = arabic.Substring(0, arabic.Length - 4);
                    arabic += 0;
                }
                n = long.Parse(arabic);

                for (int i = 0; i < lastString.Length; i++)
                {
                    result += toRoman(int.Parse(lastString.Substring(i, 1)), lastString.Length - i);
                }
                if (j == 1) romano1 = result;
                else if (j == 2) romano2 = result;
                //resultList.Add(result);
                //j++;
            }
            result = "";
            for (int i = 0; i < arabic.Length; i++)
            {
                result += toRoman(int.Parse(arabic.Substring(i, 1)), arabic.Length - i);
            }
            romano3 = result;

            if (j == 2)
                result = "<span style=\"background - image:url(./ Graphics / lineas2.gif); background - repeat:repeat - x; margin - top:-2px; padding - top:2px\">" +
                    romano3 + "</span><span style=\"text - decoration:overline; \">" +
                    romano2 + "</span>" + romano1;
            else if (j == 1)
                result = "<span style=\"text - decoration:overline; \">" +
                    romano3 + "</span>" + romano1;

            else result = romano3;

            resultList.Add(result);
            return resultList;
        }

        public string toRoman(int arabic, int position)
        {
            string result = "";
            double num = arabic * Math.Pow(10, position - 1);
            while (num != 0)
            {
                if (num >= 1000)
                {
                    result += "M";
                    num -= 1000;
                }
                else if (num == 900)
                {
                    return "CM";
                }
                else if (num >= 500)
                {
                    result += "D";
                    num -= 500;
                }
                else if (num == 400)
                {
                    return "CD";
                }
                else if (num >= 100)
                {
                    result += "C";
                    num -= 100;
                }
                else if (num == 90)
                {
                    return "XC";
                }
                else if (num >= 50)
                {
                    result += "L";
                    num -= 50;
                }
                else if (num == 40)
                {
                    return "XL";
                }
                else if (num >= 10)
                {
                    result += "X";
                    num -= 10;
                }
                else if (num == 9)
                {
                    return "IX";
                }
                else if (num >= 5)
                {
                    result += "V";
                    num -= 5;
                }
                else if (num == 4)
                {
                    return "IV";
                }
                else if (num >= 1)
                {
                    result += "I";
                    num -= 1;
                }
            }
            return result;
        }
        private void addMoreRomano(ref Conversion conversion, List<string> respuesta)
        {
            conversion.TitNotas = HttpContext.GetGlobalResourceObject("Resource", "Notas").ToString();
            conversion.Notas = new List<string>();
            conversion.Notas.Add(HttpContext.GetGlobalResourceObject("Resource", "RomanoTipo").ToString());

            conversion.TitReferencias = HttpContext.GetGlobalResourceObject("Resource", "Referencias").ToString();
        }
        private Cabecera rellenarCabecera(string sinFormatear, string formateado)
        {
            Cabecera cabecera;
            cabecera = new Cabecera(formateado, HttpContext.GetGlobalResourceObject("Resource", "CabeceraNumeroFormateado2").ToString() + " " + sinFormatear.Replace(" ", "").ToUpper() + " " + HttpContext.GetGlobalResourceObject("Resource", "CabeceraNumeroFormateado3").ToString());
            return cabecera;
        }

        private Conversion getErrorMessage()
        {
            Conversion conversion = new Conversion();

            conversion.Tipo = HttpContext.GetGlobalResourceObject("Resource", "ERROR").ToString();
            conversion.Titulo = HttpContext.GetGlobalResourceObject("Resource", "Lista8").ToString();

            conversion.Respuestas = new List<string>();

            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista19").ToString());
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista20").ToString());
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista12").ToString());
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista2").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;13289&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;53625999567&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-345676");
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista3").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;13 289&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;53 625 999 567&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-345 676");
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista4").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;12.58&nbsp;&nbsp;&nbsp;&nbsp;45,78997&nbsp;&nbsp;&nbsp;&nbsp;-47.2&nbsp;&nbsp;&nbsp;&nbsp;-98,712");
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista5").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;3/4&nbsp;&nbsp;&nbsp;&nbsp;78/125&nbsp;&nbsp;&nbsp;&nbsp;-3/4&nbsp;&nbsp;&nbsp;&nbsp;78/-125");
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista6").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;2,4E10&nbsp;&nbsp;&nbsp;&nbsp;5E-3&nbsp;&nbsp;&nbsp;&nbsp;-2,4E10&nbsp;&nbsp;&nbsp;&nbsp;-5.23E-3");
            conversion.Respuestas.Add(HttpContext.GetGlobalResourceObject("Resource", "Lista7").ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;DLVI&nbsp;&nbsp;&nbsp;&nbsp;IX&nbsp;&nbsp;&nbsp;&nbsp;XXXVI");

            return conversion;
        }
    }
}
