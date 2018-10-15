using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeChains
{
    class Program
    {
        static void Main(string[] args)
        {
            // +-----+------+
            // |  1  | null +
            // +-----+------+
            Node first = new Node { Value = 1 };

            // +-----+------+   +-----+------+
            // |  1  | null +   |  2  | null +
            // +-----+------+   +-----+------+ 
            Node middle = new Node { Value = 2 };

            // +-----+------+   +-----+------+
            // |  1  |  *---+-->|  2  | null +
            // +-----+------+   +-----+------+ 
            first.Next = middle;

            // +-----+------+   +-----+------+   +-----+------+
            // |  1  |  *---+-->|  2  | null +   |  3  | null +
            // +-----+------+   +-----+------+   +-----+------+ 
            Node last = new Node { Value = 3 };

            // +-----+------+   +-----+------+   +-----+------+
            // |  1  |  *---+-->|  2  |  *---+-->|  3  | null +
            // +-----+------+   +-----+------+   +-----+------+ 
            middle.Next = last;

            // Now iterate over each node and print the value
            PrintList(first);
        }

        private static void PrintList(Node node)
        {
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }


}
