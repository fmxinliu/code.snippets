using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCTest {
    class GCFinalizeDefinedObj {
        public static void Test() {
            Console.WriteLine("-- 测试GC和Finalize执行顺序 --");
            MyObjectWithFinalize obj = new MyObjectWithFinalize();
            WeakReference wobj = new WeakReference(obj);
            Console.WriteLine("Before GC: obj current Gen=" + GC.GetGeneration(obj));
            obj = null;
            GC.Collect(0);
        }

        /// <summary>
        /// GC执行时，不会回收对象占用的内存，而是将对象提升一代【定义了终结器，终结器中可能会访问对象字段】
        /// </summary>
        class MyObjectWithFinalize {
            ~MyObjectWithFinalize() {
                Console.WriteLine("In Finalize: obj current Gen=" + GC.GetGeneration(this));
            }
        }
    }
}
