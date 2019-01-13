using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    class TrifidAlgorithm : ICryptingAlgorithm
    {

        private const int period = 5;

        public string Decipher(string input, string encriptionKey)
        {
            bool switcher = true;
            string plainText = string.Empty;
            char[,] keySquare = GenerateKeySquare(encriptionKey);

            string tempText = RemoveOtherCharsThanLetter(input);
            
            IList<int> cipheredCoord = new List<int>();

            for (int i = 0; i < tempText.Length; i++)
            {
                int row1 = 0;
                int col1 = 0;

                GetPosition(ref keySquare, char.ToUpper(tempText[i]), ref row1, ref col1);
                cipheredCoord.Add((int)col1 / 3);
                cipheredCoord.Add(row1);
                cipheredCoord.Add(col1 % 3);
            }

            IList<int> coordLine = new List<int>();
            IList<int> coordColumn = new List<int>();
            IList<int> squareNumber = new List<int>();

            int result = (int)cipheredCoord.Count / 15;
            int rest = cipheredCoord.Count % 15;
            int charactersInIncompleteSquare = rest / 3;
            int j = 0;

            for (int i = 0; i < cipheredCoord.Count; i++)
            {
                if(i < result * 15)
                {
                    int number = cipheredCoord.Count % 15;

                    if (i % 15 < 5)
                    {
                        squareNumber.Add(cipheredCoord[i]);
                    }
                    else if (i % 15 < 10)
                    {
                        coordLine.Add(cipheredCoord[i]);
                    }
                    else if (i % 15 < 15)
                    {
                        coordColumn.Add(cipheredCoord[i]);
                    }
                }
                else
                {
                    if((int)j / charactersInIncompleteSquare == 0)
                    {
                        squareNumber.Add(cipheredCoord[i]);
                    }
                    else if((int)j / charactersInIncompleteSquare == 1)
                    {
                        coordLine.Add(cipheredCoord[i]);
                    }
                    else if((int)j / charactersInIncompleteSquare == 2)
                    {
                        coordColumn.Add(cipheredCoord[i]);
                    }

                    j++;
                }
               
            }

            for (int i = 0; i < coordLine.Count; i++)
            {
                plainText += keySquare[coordLine[i], squareNumber[i]*3 + coordColumn[i]%3].ToString();
            }
            plainText = AdjustOutput(input, plainText);
            return plainText;
        }

        public string Encipher(string input, string encriptionKey)
        {
            string cipheredText = string.Empty;
            char[,] keySquare = GenerateKeySquare(encriptionKey);

            string tempText = RemoveOtherCharsThanLetter(input);

            IList<int> coordLine = new List<int>();
            IList<int> coordColumn = new List<int>();
            IList<int> squareNumber = new List<int>();
            IList<int> cipheredCoord = new List<int>();

            for (int i = 0; i < tempText.Length; i++)
            {
                int row1 = 0;
                int col1 = 0;

                GetPosition(ref keySquare, char.ToUpper(tempText[i]), ref row1, ref col1);
                coordLine.Add(row1);
                coordColumn.Add(col1%3);
                squareNumber.Add((int)col1 / 3);
            }

            int[,] indexesMatrix = new int[3,9];

            double numberOfCharacters = (double)tempText.Length / 5;
            double numberOfDivisions = Math.Ceiling(numberOfCharacters);

            for (int i = 0; i< numberOfDivisions; i++)
            {
                int j = 0;
                while(j < 15)
                {
                    if(j < 5 && ((i*5) + (j%5)) < squareNumber.Count)
                    {
                        cipheredCoord.Add(squareNumber[(i*5) + j%5]);
                    }
                    else if(j<10 && ((i * 5) + (j % 5)) < coordLine.Count)
                    {
                        cipheredCoord.Add(coordLine[(i * 5) + j % 5]);
                    }
                    else if(j<15 && ((i * 5) + (j % 5)) < coordColumn.Count)
                    {
                        cipheredCoord.Add(coordColumn[(i * 5) + j % 5]);
                    }
                    j++;
                }
            }

            for (int i = 0; i < cipheredCoord.Count; i += 3)
            {
                cipheredText += keySquare[cipheredCoord[i+1], (cipheredCoord[i]*3) + (cipheredCoord[i+2]%3)];
            }
            cipheredText = AdjustOutput(input, cipheredText);
            return cipheredText;
        }

        private char[,] GenerateKeySquare(string key)
        {
            char[,] keySquare = new char[3, 9];
            string alphabetEN = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string tempKey = string.IsNullOrEmpty(key) ? "CIPHER" : key.ToUpper();
            tempKey += alphabetEN;

            for (int i = 0; i < 25; i++)
            {
                List<int> indexes = FindAllOccurences(tempKey, alphabetEN[i]);
                tempKey = RemoveAllDuplicates(tempKey, indexes);
            }

            tempKey = tempKey.Substring(0, 26);

            int line = 0;
            int column = 0;
            int grid = 0;

            for (int i = 0; i < 26; i++)
            {
                int col = (grid * 3) + column % 3;
                keySquare[line, col] = tempKey[i];
                column++;
                if(column % 3 == 0)
                {
                    column = 0;
                    line++;
                    if(line %3 == 0)
                    {
                        grid++;
                        line = 0;
                    }
                }
            }
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
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 9; j++)
                    if (keySquare[i, j] == letter)
                    {
                        row = i;
                        column = j;
                    }
        }
    }
}
