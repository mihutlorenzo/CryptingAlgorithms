using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class FourSquareAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string input, string encriptionKey)
        {
            string cipheredText = string.Empty;
            string[] keys = encriptionKey.Split('|');
            char[,] keySquareP1 = new char[,] { { 'A', 'B' , 'C', 'D', 'E'},
                                                { 'F', 'G' , 'H', 'I', 'K'},
                                                { 'L', 'M' , 'N', 'O', 'P'},
                                                { 'Q', 'R' , 'S', 'T', 'U'},
                                                { 'V', 'W' , 'X', 'Y', 'Z'}
                                              };
            char[,] keySquareC1 = GenerateKeySquare(keys[0]);
            char[,] keySquareC2 = GenerateKeySquare(keys[1]);

            string tempText = RemoveOtherCharsThanLetter(input);

            if ((tempText.Length % 2) != 0)
                tempText += "X";

            for (int i = 0; i < tempText.Length; i += 2)
            {
                int row1 = 0;
                int col1 = 0;
                int row2 = 0;
                int col2 = 0;

                GetPosition(ref keySquareC1, char.ToUpper(tempText[i]), ref row1, ref col1);
                GetPosition(ref keySquareC2, char.ToUpper(tempText[i + 1]), ref row2, ref col2);

                cipheredText += keySquareP1[row1, col2].ToString() + keySquareP1[row2, col1].ToString();


            }

            cipheredText = AdjustOutput(input, cipheredText);

            return cipheredText;
        }

        public string Encipher(string input, string encriptionKey)
        {
            string cipheredText = string.Empty;
            string[] keys = encriptionKey.Split('|');
            char[,] keySquareP1 = new char[,] { { 'A', 'B' , 'C', 'D', 'E'},
                                                { 'F', 'G' , 'H', 'I', 'K'},
                                                { 'L', 'M' , 'N', 'O', 'P'},
                                                { 'Q', 'R' , 'S', 'T', 'U'},
                                                { 'V', 'W' , 'X', 'Y', 'Z'}
                                              };
            char[,] keySquareC1 = GenerateKeySquare(keys[0]);
            char[,] keySquareC2 = GenerateKeySquare(keys[1]);

            string tempText = RemoveOtherCharsThanLetter(input);

            if ((tempText.Length % 2) != 0)
                tempText += "X";

            for (int i = 0; i < tempText.Length; i += 2)
            {
                int row1 = 0;
                int col1 = 0;
                int row2 = 0;
                int col2 = 0;

                GetPosition(ref keySquareP1, char.ToUpper(tempText[i]), ref row1, ref col1);
                GetPosition(ref keySquareP1, char.ToUpper(tempText[i + 1]), ref row2, ref col2);

                cipheredText += keySquareC1[row1, col2].ToString() + keySquareC2[row2, col1].ToString();


            }

            cipheredText = AdjustOutput(input, cipheredText);

            return cipheredText;
        }

        private string AdjustOutput(string input, string output)
        {
            StringBuilder cipheredText = new StringBuilder(output);
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                    cipheredText = cipheredText.Insert(i, input[i].ToString());
                if (char.IsLower(input[i]))
                    cipheredText[i] = char.ToLower(cipheredText[i]);
            }

            return cipheredText.ToString();
        }

        private void GetPosition(ref char[,] keySquare, char letter, ref int row, ref int column)
        {
            if (letter == 'J')
                GetPosition(ref keySquare, 'I', ref row, ref column);
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (keySquare[i, j] == letter)
                    {
                        row = i;
                        column = j;
                    }
        }

        private string RemoveOtherCharsThanLetter(string input)
        {
            string text = input;
            for (int i = 0; i < text.Length; i++)
            {
                if (!Char.IsLetter(text[i]) || text[i] == ' ')
                    text = text.Remove(i, 1);
            }
            return text;

        }

        private char[,] GenerateKeySquare(string key)
        {
            char[,] keySquare = new char[5, 5];
            string alphabetEN = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            string tempKey = string.IsNullOrEmpty(key) ? "CIPHER" : key.ToUpper();
            tempKey = tempKey.Replace("J", "");
            tempKey += alphabetEN;

            for (int i = 0; i < 25; i++)
            {
                List<int> indexes = FindAllOccurences(tempKey, alphabetEN[i]);
                tempKey = RemoveAllDuplicates(tempKey, indexes);
            }

            tempKey = tempKey.Substring(0, 25);

            for (int i = 0; i < 25; i++)
                keySquare[(i / 5), (i % 5)] = tempKey[i];
            return keySquare;
        }

        private List<int> FindAllOccurences(string tempKey, char character)
        {
            List<int> indexes = new List<int>();
            int index = 0;
            while ((index = tempKey.IndexOf(character, index)) != -1)
            {
                indexes.Add(index++);
            }
            return indexes;
        }

        private string RemoveAllDuplicates(string tempKey, List<int> indexes)
        {
            string retVal = tempKey;

            for (int i = indexes.Count - 1; i >= 1; i--)
                retVal = retVal.Remove(indexes[i], 1);
            return retVal;
        }
    }
}
