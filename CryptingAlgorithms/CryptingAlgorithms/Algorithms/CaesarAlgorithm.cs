﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CryptingAlgorithms.Algorithms
{
    class CaesarAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string cipherText, int key)
        {
            key = Math.Abs(key) % 26;
            return Encipher(cipherText, (26 - key));
        }

        public string Encipher(string plainText, int key)
        {
            string cipherText = "";

            key = Math.Abs(key) % 26;

            foreach (var character in plainText)
            {
                if (!char.IsLetter(character))
                {
                    cipherText += character;
                }
                else
                {
                    char startAsciiCode = char.IsUpper(character) ? 'A' : 'a';
                    cipherText += (char)((((character + key) - startAsciiCode) % 26) + startAsciiCode);


                }
                
            }
            return cipherText;
        }

    }
}
