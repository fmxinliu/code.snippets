using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTest {
    class GenericType {
         public static void Test() {
             Int32 i = Custom<Single>.Converter<Int32>(8.5f);
             Console.WriteLine("Single -> Int32 : {0} -> {1}", 8.5f, i);
         }
    }

    sealed class Custom<T> {
        public static TOutput Converter<TOutput>(T data) {
            TOutput output = (TOutput)Convert.ChangeType(data, typeof(TOutput));
            return output;
        }
    }
}
