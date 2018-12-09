using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class BeaufortAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string input, string encriptionKey)
        {
            string plainText = FormatText(input);
            string key = BuildThePassword(plainText.Length, encriptionKey);
            char[,] alphabetMap = GenerateMap();

            string decipheredText = string.Empty;


            for (int i = 0; i < plainText.Length; i++)
            {
                int column = GetIndex(plainText[i]);
                char decipheredCharacter = GetCipheredCharacter(alphabetMap, column, key[i]);

                decipheredText += decipheredCharacter;
            }
            AdjustOutput(ref decipheredText);
            return decipheredText;
        }

        public string Encipher(string input, string encriptionKey)
        {
            string plainText = FormatText(input);
            string key = BuildThePassword(plainText.Length, encriptionKey);
            char[,] alphabetMap = GenerateMap();

            string cipheredText = string.Empty;


            for (int i = 0; i < plainText.Length; i++)
            {
                int column = GetIndex(plainText[i]);
                char cipheredCharacter = GetCipheredCharacter(alphabetMap, column, key[i]);

                cipheredText += cipheredCharacter;
            }
            AdjustOutput(ref cipheredText);
            return cipheredText;
        }

        private char GetCipheredCharacter(char[,] alphabetMap, int column, char v)
        {
            char character = 'a';
            for(int i =0; i<26; i++)
            {
                if(alphabetMap[i,column] == v)
                {
                    character = GetCharacterAt(i);
                    break;
                }
            }
            return character;
        }

        private char GetCharacterAt(int i)
        {
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabetEN[i];
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

        private int GetIndex(char character)
        {
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabetEN.IndexOf(character);
        }
    }
}
