using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericTest.Test1;

namespace GenericTest {
    class GenericNode {
        public static void Test() {
            Node<Char> head = new Node<Char>('C');
            head = new Node<Char>('B', head);
            head = new Node<Char>('A', head);
            Console.WriteLine(head);
        }
    }

    namespace Test1 {
        /// <summary>
        /// 节点数据类型必须相同
        /// </summary>
        public sealed class Node<T> {
            private T data;
            private Node<T> next;

            public Node(T data) : this(data, null) { }
            public Node(T data, Node<T> next) {
                this.data = data;
                this.next = next;
            }

            public override string ToString() {
                return this.data.ToString() +
                    ((this.next != null) ? this.next.ToString() : string.Empty);
            }
        }
    }
}
