﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumTest {
    class Program {
        static void Main(string[] args) {
            BitFlags.Test();
            EnumUtils.Test();
            ExtensionMethods.Test();
            Console.ReadKey();
        }
    }
}
