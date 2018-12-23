using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class BifidAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string input, string decriptionKey)
        {
            int period = 5;
            bool switcher = true;
            string plainText = string.Empty;
            char[,] keySquare = GenerateKeySquare(decriptionKey);

            string tempText = RemoveOtherCharsThanLetter(input);
            IList<int> coordLine = new List<int>();
            IList<int> coordColumn = new List<int>();
            IList<int> cipheredCoord = new List<int>();

            for (int i = 0; i < tempText.Length; i++)
            {
                int row = 0;
                int col = 0;

                GetPosition(ref keySquare, char.ToUpper(tempText[i]), ref row, ref col);
                cipheredCoord.Add(row);
                cipheredCoord.Add(col);
                plainText += keySquare[row, col].ToString();
            }
            for(int i = 0; i < cipheredCoord.Count; i++)
            {
                if ((i != 0 && i % period == 0) || i == (cipheredCoord.Count))
                    switcher = !switcher;
                if (switcher)
                {
                    coordLine.Add(cipheredCoord[i]);
                }
                else
                {
                    coordColumn.Add(cipheredCoord[i]);
                }
            }

            for(int i =0; i< coordLine.Count; i++)
            {
                plainText += keySquare[coordLine[i], coordColumn[i]].ToString();
            }
            plainText = AdjustOutput(input, plainText);
            return plainText;
            
        }

        public string Encipher(string input, string encriptionKey)
        {
            int period = 5;
            string cipheredText = string.Empty;
            char[,] keySquare = GenerateKeySquare(encriptionKey);

            string tempText = RemoveOtherCharsThanLetter(input);

            IList<int> coordLine = new List<int>();
            IList<int> coordColumn = new List<int>();
            IList<int> cipheredCoord = new List<int>();

            for (int i = 0; i < tempText.Length; i++)
            {
                int row1 = 0;
                int col1 = 0;

                GetPosition(ref keySquare, char.ToUpper(tempText[i]), ref row1, ref col1);
                coordLine.Add(row1);
                coordColumn.Add(col1);
            }

            for (int i = 0, j = 0; i <= tempText.Length; i++)
            {
                if ((i != 0) && (i % period == 0) || i == tempText.Length)
                {
                    do
                    {
                        cipheredCoord.Add(coordColumn[j]);
                        j++;
                    } while (j % period != 0 && j < tempText.Length);
                }
                if (i < tempText.Length)
                    cipheredCoord.Add(coordLine[i]);
            }



            for (int i = 0; i < cipheredCoord.Count; i += 2)
            {
                cipheredText += keySquare[cipheredCoord[i], cipheredCoord[i + 1]];
            }
            cipheredText = AdjustOutput(input, cipheredText);
            return cipheredText;
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
    }
}
