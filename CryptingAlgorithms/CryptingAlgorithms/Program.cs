﻿using CryptingAlgorithms.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\PlainText.txt", System.Text.Encoding.UTF8);
            var plainText = reader.ReadToEnd();
            reader.Close();

            Console.WriteLine("Give the encription key.The key should be an integer in the interval [-25,25]");
            int key = int.Parse(Console.ReadLine());

            ICryptingAlgorithm cryptingAlgorithm = new CaesarAlgorithm();

            var cipherText = cryptingAlgorithm.Encipher(plainText, key);

            Console.WriteLine("The encrypted text is:");
            Console.WriteLine(cipherText);

            var charactersDistribution = GetRepetitionOfEachCharacter(cipherText);

            foreach (var character in charactersDistribution)
            {
                char c = character.Key;
                int cnt = character.Value;
                Console.WriteLine("The character {0} it is used {1} times",c,cnt);
            }

            var writer = new StreamWriter(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\CipherText.txt",false , System.Text.Encoding.UTF8);
            writer.Write(cipherText);
            writer.Close();

            var decipheredText = cryptingAlgorithm.Decipher(cipherText,key);

            Console.WriteLine("The plain text is:");
            Console.WriteLine(decipheredText);
        }

        public static Dictionary<char, int> GetRepetitionOfEachCharacter(string cipherText)
        {
            var characters = new Dictionary<char, int>();
            for (int i = 0; i < cipherText.Length; i++)
            {
                char c = cipherText[i];
                if (char.IsLetter(c))
                {
                    if (characters.ContainsKey(c))
                    {
                        characters[c]++;
                    }
                    else
                    {
                        characters.Add(c, 1);
                    }
                }
            }
            return characters;
        }
    }
}
