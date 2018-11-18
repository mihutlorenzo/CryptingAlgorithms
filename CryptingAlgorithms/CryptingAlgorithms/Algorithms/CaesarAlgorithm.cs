using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CryptingAlgorithms.Algorithms
{
    class CaesarAlgorithm : ICryptingAlgorithm
    {
        public string Decipher(string cipherText, string encriptionKey)
        {
            int key = Int32.Parse(encriptionKey);
            key = Math.Abs(key) % 26;
            return Encipher(cipherText, (26 - key).ToString());
        }

        public string Encipher(string plainText, string encriptionKey)
        {
            string cipherText = "";

            int key = Math.Abs(Int32.Parse(encriptionKey)) % 26;

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
