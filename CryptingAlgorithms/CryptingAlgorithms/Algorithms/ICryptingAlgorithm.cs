﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptingAlgorithms.Algorithms
{
    interface ICryptingAlgorithm
    {
        string Encipher(string input, string encriptionKey);
        string Decipher(string input, string encriptionKey);
    }
}
