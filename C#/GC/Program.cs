﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCTest {
    class Program {
        static void Main(string[] args) {
            //GCTimerObjInAdvance.Test();
            GCCollects.Test();
            FreeNativeResources.Test();
            Finalizer.Test();
            GCWeakReferenceObj.Test();
            GCFinalizeDefinedObj.Test();
            Console.ReadKey();
        }
    }
}
