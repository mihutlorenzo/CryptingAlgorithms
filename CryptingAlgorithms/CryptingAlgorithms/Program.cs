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

        // cheie Playfair : ROYALNEWZDVY
        // cheie Vigenere : TDBTATIALU
        // cheie Beaufort : TITAOMAPWMAE
        // cheie Autokey : BERCU
        // cheie FourSquare : BERCU|ADRIAN
        // cheie ColumnarTransposition : pangram
        //PAROLA : DNFTBEIOFEONAWOE
        // : DNFTBEIOFAW
        static void Main(string[] args)
        {
            ICryptingAlgorithm cryptingAlgorithm = new PlayfairAlgorithm();

            //Console.WriteLine("Give the encription key.The key should be an integer in the interval [-25,25]");
            //string key1 = Console.ReadLine();

            //StreamReader reader = new StreamReader(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\CipherText4.txt", System.Text.Encoding.UTF8);
            //string plainText = reader.ReadToEnd();
            //reader.Close();

            //string cipherText = cryptingAlgorithm.Decipher(plainText, key1);

            //Console.WriteLine("The encrypted text is:");
            //Console.WriteLine(cipherText);
            //Console.ReadLine();

            //StreamReader reader = new StreamReader(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\PlayfairCipherText.txt", System.Text.Encoding.UTF8);
            //string cipherText = reader.ReadToEnd();
            //reader.Close();

            //string plainText = cryptingAlgorithm.Encipher(cipherText, "ROYALNEWZDVY");

            //StreamWriter writer = new StreamWriter(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\PlayfairPlainText.txt", false, System.Text.Encoding.UTF8);
            //writer.Write(plainText);
            //writer.Close();


            //StreamReader reader = new StreamReader(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\VigenereCipherText.txt", System.Text.Encoding.UTF8);
            //string cipherText = reader.ReadToEnd();
            //reader.Close();

            //string plainText = cryptingAlgorithm.Decipher(cipherText, "TDBTATIALU");

            int option = 999;
            do
            {
                bool isInvalidOption = false;

                Console.WriteLine("Choose one of the following algorithms for the demo: \n 1=Caesar \n 2=Playfair \n 3=Vigenere  \n 4=Beaufort \n 5=Autokey \n 6=Four-Square \n 7= ColumnarTransposition \n 0=Exit");
                option = Int32.Parse(Console.ReadLine());

                string readPath = string.Empty;
                string writePath = string.Empty;
                string key = string.Empty;

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Give the encription key.The key should be an integer in the interval [-25,25]");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new CaesarAlgorithm();

                        StreamReader reader = new StreamReader(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\CaesarPlainText.txt", System.Text.Encoding.UTF8);
                        string plainText = reader.ReadToEnd();
                        reader.Close();

                        string cipherText = cryptingAlgorithm.Encipher(plainText, key);

                        Console.WriteLine("The encrypted text is:");
                        Console.WriteLine(cipherText);

                        int numberOfEncodedCharacters = 0;
                        var charactersDistribution = GetRepetitionOfEachCharacter(cipherText, ref numberOfEncodedCharacters);

                        foreach (var character in charactersDistribution)
                        {
                            char c = character.Key;
                            int cnt = character.Value;
                            double percentage = (cnt * 100) / numberOfEncodedCharacters;
                            Console.WriteLine("The character {0} it is used {1} %", c, percentage);
                        }

                        StreamWriter writer = new StreamWriter(@"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\CaesarCipherText.txt", false, System.Text.Encoding.UTF8);
                        writer.Write(cipherText);
                        writer.Close();

                        string decipheredText = cryptingAlgorithm.Decipher(cipherText, key);

                        Console.WriteLine("The plain text is:");
                        Console.WriteLine(decipheredText);

                        break;
                    case 2:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\PlayfairCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\PlayfairPlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new PlayfairAlgorithm();
                        break;
                    case 3:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\VigenereCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\VigenerePlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new VigenereAlgorithm();
                        break;
                    case 4:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\BeaufortCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\BeaufortPlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new BeaufortAlgorithm();
                        break;
                    case 5:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\AutokeyCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\AutokeyPlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new AutokeyAlgorithm();
                        break;
                    case 6:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\FourSquareCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\FourSquarePlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new FourSquareAlgorithm();
                        break;
                    case 7:
                        readPath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\ColumnarTranspositionCipherText.txt";
                        writePath = @"E:\Repositories\Github\CryptingAlgorithms\CryptingAlgorithms\ColumnarTranspositionPlainText.txt";
                        Console.WriteLine("Give the password:");
                        key = Console.ReadLine();
                        cryptingAlgorithm = new ColumnarTranspositionAlgorithm();
                        break;
                    default:
                        Console.WriteLine("You chose an invalid option. Please choose a valid one!");
                        isInvalidOption = true;
                        break;

                }

                if (!isInvalidOption && option != 1)
                {
                    StreamReader reader = new StreamReader(readPath, System.Text.Encoding.UTF8);
                    string text = reader.ReadToEnd();
                    reader.Close();

                    text = cryptingAlgorithm.Decipher(text, key);

                    Console.WriteLine("The decrypted text is:");
                    Console.WriteLine(text);

                    StreamWriter writer = new StreamWriter(writePath, false, System.Text.Encoding.UTF8);
                    writer.Write(text);
                    writer.Close();

                    text = cryptingAlgorithm.Encipher(text, key);

                    Console.WriteLine("The ciphered text is:");
                    Console.WriteLine(text);
                }

            } while (option != 0);

        }

        public static Dictionary<char, int> GetRepetitionOfEachCharacter(string cipherText, ref int numberOfEncodedCharacters)
        {
            var characters = new Dictionary<char, int>();
            for (int i = 0; i < cipherText.Length; i++)
            {
                char c = cipherText[i];
                if (char.IsLetter(c))
                {
                    numberOfEncodedCharacters++;
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
