using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class AutokeyAlgorithm: ICryptingAlgorithm
    {
        private string _key;

        public string Decipher(string input, string encriptionKey)
        {
            string cipheredText = FormatText(input);
            _key = encriptionKey + "ANAAREMERESIBERE";
            char[,] alphabetMap = GenerateMap();

            string plainText = string.Empty;


            for (int i = 0; i < cipheredText.Length; i++)
            {
                int row = GetIndex(_key[i]);

                plainText += GetLetterFromTheMap(alphabetMap, cipheredText[i], row);

            }

            AdjustOutput(ref plainText);
            return plainText;
        }

        private void AdjustOutput(ref string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (((i + 1) % 6 == 0) && (i > 0))
                {
                    text = text.Insert(i, " ");
                }
            }
        }

        private char GetLetterFromTheMap(char[,] alphabetMap, char cipheredLetter, int row)
        {
            char letter;
            int index = 0;
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < 26; i++)
            {
                if (alphabetMap[row, i] == cipheredLetter)
                {
                    index = i;
                    break;
                }
            }
            letter = alphabetEN[index];

            return letter;
        }

        public string Encipher(string input, string encriptionKey)
        {
            string plainText = FormatText(input);
          
            char[,] alphabetMap = GenerateMap();

            string cipheredText = string.Empty;


            for (int i = 0; i < plainText.Length; i++)
            {
                int row = GetIndex(_key[i]);
                int column = GetIndex(plainText[i]);

                cipheredText += alphabetMap[row, column];
            }
            AdjustOutput(ref cipheredText);
            return cipheredText;
        }

        private string FormatText(string input)
        {
            string plainText = input.ToUpper();
            for (int i = 0; i < plainText.Length; i++)
            {
                if (!Char.IsLetter(plainText[i]) || plainText[i] == ' ')
                    plainText = plainText.Remove(i, 1);
            }
            return plainText;
        }

        private int GetIndex(char character)
        {
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabetEN.IndexOf(character);
        }

        private string BuildThePassword(int textLength, string encriptionKey)
        {
            string key = encriptionKey;
            while (textLength >= key.Length)
            {
                key += encriptionKey;
                //if(key.Length > 26)
                //{
                //    key = key.Substring(0, 26);
                //    break;
                //}
            }
            return key;
        }

        private char[,] GenerateMap()
        {
            char[,] alphabetMap = new char[26, 26];
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 26; j++)
                    alphabetMap[i, j] = alphabetEN[(i + j) % 26];
            return alphabetMap;
        }
    }
}

