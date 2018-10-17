using CryptingAlgorithms.Algorithms;
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

            var writer = new StreamWriter(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\CipherText.txt",false , System.Text.Encoding.UTF8);
            writer.Write(cipherText);
            writer.Close();

            var decipheredText = cryptingAlgorithm.Decipher(cipherText,key);

            Console.WriteLine("The plain text is:");
            Console.WriteLine(decipheredText);
        }
    }
}
