using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericTest.Test2;

namespace GenericTest {
    class GenericTypeNode {
        public static void Test() {
            Node head = new TypeNode<Char>('.');
            head = new TypeNode<DateTime>(DateTime.Now, head);
            head = new TypeNode<String>("Today is ", head);
            Console.WriteLine(head);
        }
    }

    namespace Test2 {
        /// <summary>
        /// 节点数据类型可以不相同
        /// </summary>
        public sealed class TypeNode<T> : Node {
            private T data;

            public TypeNode(T data) : this(data, null) { }
            public TypeNode(T data, Node next) : base(next) {
                this.data = data;
            }

            public override string ToString() {
                return this.data.ToString() +
                    ((this.next != null) ? this.next.ToString() : string.Empty);
            }
        }

        // 非泛型基类
        public class Node {
            protected Node next;
            public Node(Node next) {
                this.next = next;
            }
        }
    }
}
