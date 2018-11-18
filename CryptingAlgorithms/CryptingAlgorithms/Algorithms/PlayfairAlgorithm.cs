using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class PlayfairAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string input, string encriptionKey)
        {
            return Cipher(input, encriptionKey, false);
        }

        public string Encipher(string input, string encriptionKey)
        {
            return Cipher(input, encriptionKey, true);
        }

        private string Cipher(string input, string key, bool encipher)
        {
            string cipheredText = string.Empty;
            char[,] keySquare = GenerateKeySquare(key);

            string tempText = RemoveOtherCharsThanLetter(input);
            int e = encipher ? 1 : -1;
            if ((tempText.Length % 2) != 0)
                tempText += "X";

            for (int i = 0; i < tempText.Length; i += 2)
            {
                int row1 = 0;
                int col1 = 0;
                int row2 = 0;
                int col2 = 0;

                GetPosition(ref keySquare, char.ToUpper(tempText[i]), ref row1, ref col1);
                GetPosition(ref keySquare, char.ToUpper(tempText[i+1]), ref row2, ref col2);

                if(row1 == row2 && col1 == col2)
                {
                    GetPosition(ref keySquare, 'X', ref row2, ref col2);
                }

                if (row1 == row2)
                {
                    cipheredText += new string(SameRow(ref keySquare, row1, col1, col2, e));
                }
                else if(col1 == col2)
                {
                    cipheredText += new string(SameColumn(ref keySquare, col1, row1, row2, e));
                }
                else
                {
                    cipheredText += new string(DifferentRowColumn(ref keySquare, row1, col1, row2, col2));
                }

            }

            cipheredText = AdjustOutput(input, cipheredText);

            return cipheredText;
        }

        private string AdjustOutput(string input, string output)
        {
            StringBuilder cipheredText = new StringBuilder(output);
            for(int i = 0; i< input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                    cipheredText = cipheredText.Insert(i, input[i].ToString());
                if (char.IsLower(input[i]))
                    cipheredText[i] = char.ToLower(cipheredText[i]);
            }

            return cipheredText.ToString();
        }

        private char[] DifferentRowColumn(ref char[,] keySquare, int row1, int column1, int row2, int column2)
        {
            return new char[]
            {
                keySquare[row1,column2],
                keySquare[row2,column1],
            };
        }

        private char[] SameColumn(ref char[,] keySquare, int column, int row1, int row2, int encipher)
        {
            return new char[]
            {
                keySquare[Mod((row1 + encipher),5),column],
                keySquare[Mod((row2 + encipher),5),column],
            };
        }

        private char[] SameRow(ref char[,] keySquare, int row, int column1, int column2, int encipher)
        {
            return new char[]
            {
                keySquare[row,Mod((column1 + encipher),5)],
                keySquare[row,Mod((column2 + encipher),5)],
            };
        }

        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        private void GetPosition(ref char[,] keySquare, char letter, ref int row, ref int column)
        {
            if (letter == 'J')
                GetPosition(ref keySquare, 'I', ref row, ref column);
            for(int i = 0; i<5; i++)
                for(int j=0; j<5; j++)
                    if(keySquare[i,j] == letter)
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

            for(int i=0; i< 25;i++)
            {
                List<int> indexes = FindAllOccurences(tempKey, alphabetEN[i]);
                tempKey = RemoveAllDuplicates(tempKey, indexes);
            }

            tempKey = tempKey.Substring(0, 25);

            for (int i = 0; i < 25; i++)
                keySquare[(i / 5),(i % 5)] = tempKey[i];
            return keySquare;
        }

        private List<int> FindAllOccurences(string tempKey, char character)
        {
            List<int> indexes = new List<int>();
            int index = 0;
            while((index = tempKey.IndexOf(character, index)) != -1)
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
