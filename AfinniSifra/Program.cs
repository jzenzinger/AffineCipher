using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace AfinniSifra
{
    class sifra
    {
        private static char[] arrayAlphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] arrayOfDiactritics = { 'Á', 'Č', 'Ď', 'É', 'Ě', 'Í', 'Ň', 'Ó', 'Ř', 'Š', 'Ť', 'Ú', 'Ů', 'Ý', 'Ž' };
        private static char[] changeDiacritics = { 'A', 'C', 'D', 'E', 'E', 'I', 'N', 'O', 'R', 'S', 'T', 'U', 'U', 'Y', 'Z' };
        private static string[] specialChars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "{" ,"}" ,"~","^" };



        public int a { get; set; }
        public int b { get; set; }
        //public int prvek { get; set; }

        public static string EncrypteText(string userText, int a, int b)
        {
            Console.WriteLine($"Text k šifrování: {userText}");
            bool validation = sifra.InputValidation(a, b);
            string cipherText = "";

            if (validation == true)
            {
                string filtratedText = sifra.FiltrationOfChars(userText);


                char[] chars = filtratedText.ToUpper().ToCharArray();

                //string cipherText = "";

                int m = arrayAlphabet.Length;

                for (int j = 0; j < chars.Length; j++)
                {
                    //if(chars[j] != ' ')
                    //{
                    int x = Convert.ToInt32(chars[j] - 65);
                    cipherText += Convert.ToChar(((a * x + b) % m) + 65);
                    //}              
                }
            }
                string finalPlainText = PartitionCipher(cipherText);
                Console.WriteLine($"Šifrovaný text: {finalPlainText}");

                sifra.DescrypteText(cipherText, a, b);
            


            return finalPlainText;
        }

        public static string DescrypteText(string usetText,int a, int b)
        {
            string plainText = "";

            int aInversed = MultiplicativeInverse(a);

            char[] chars = usetText.ToUpper().ToCharArray();

            foreach (char c in chars)
            {
                //if (c != ' ')
                //{
                    int x = Convert.ToInt32(c - 65);

                    x = Convert.ToInt32(x) + 26;
                    plainText += Convert.ToChar(((aInversed * (x - b)) % 26) + 65);
                //}
            }

            plainText = plainText.Replace("XSPACEX", " ");
            Console.WriteLine($"Dešifrovaný text: {plainText}");

            return plainText;
        }

        private static int MultiplicativeInverse(int a)
        {
            int x = 0;
            for (int i = 0; i < 26; i++)
            {
                if ((a * i) % 26 == 1)
                {
                    x = i;
                }
            }
            return x;

            throw new Exception("No multiplicative inverse found.");
        }


        private static string PartitionCipher (string text)
        {
            int numOfSpaces = text.Length / 5;
            int change = 5;

            for (int i = 0; i < numOfSpaces; i++)
            {
                text = text.Insert(change, " ");
                change += 6;
            }
            return text;
        }

        private static string FiltrationOfChars (string userText)
        {
            string upperText = userText.ToUpper();
            string filtrated = "";

            for (int j = 0; j < specialChars.Length; j++)
            {
                if (userText.Contains(specialChars[j]))
                {
                    userText = userText.Replace(specialChars[j], "");
                }
            }

            for (int i = 0; i < upperText.Length; i++)
            {
                if (char.IsWhiteSpace(upperText[i]))
                {
                    filtrated += "XSPACEX";
                }
                else
                {
                    for (int j = 0; j < arrayAlphabet.Length; j++)
                    {
                        if (upperText[i] == arrayAlphabet[j])
                        {
                            filtrated += arrayAlphabet[j];
                        }
                    }
                    for (int j = 0; j < arrayOfDiactritics.Length; j++)
                    {
                        if (upperText[i] == arrayOfDiactritics[j])
                        {
                            filtrated += changeDiacritics[j];
                        }
                    }
                }
            }
            return filtrated;
        }
        private static bool InputValidation (int a, int b)
        {
            bool validation = false;
            int inputA = a;
            int inputB = b;


            while(validation != true)
            {
                while(a!=0 && b != 0)
                {
                    if (a>b)
                    {
                        a = a % b;
                    }
                    else
                    {
                        b = b % a;
                    }
                }
                int gcd = a | b;
                if ((gcd == 1 && (inputA > 0) && (inputA < 26)) && ((inputB > 0) && (inputB < 26)))
                {
                    validation = true;
                }
                else
                {
                    Console.WriteLine("Validation of input keys is incorrect!");
                    validation = false;
                    break;
                }
            }
            return validation;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            sifra.EncrypteText("Affine cipher.", 17, 20);
        }
    }
}
