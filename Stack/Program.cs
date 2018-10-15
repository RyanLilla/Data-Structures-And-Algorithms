using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("cow");
            stack.Push("dog");
            stack.Push("cat");
            stack.Push("mouse");
            stack.Push("tiger");
            stack.Push("bear");

            Console.WriteLine($"The number of animals on the stack are {stack.Count}");
            Console.WriteLine();

            Console.WriteLine($"Removing {stack.Pop()} from the stack");
            Console.WriteLine($"The number of animals now on the stack are {stack.Count}");
            Console.WriteLine();

            Console.WriteLine($"The next animal on the stack is: {stack.Peek()}");
            Console.WriteLine();

            Console.WriteLine("Adding 'monkey' to the stack");
            Console.WriteLine();
            stack.Push("monkey");

            Console.WriteLine($"The next animal on the stack is: {stack.Peek()}");
            Console.WriteLine();

        }
    }

    //public class Stack<T> : IEnumerable<T>
    //{
    //    private LinkedList<T> list = new LinkedList<T>();

    //    public void Push(T item)
    //    {
    //        list.AddFirst(item);
    //    }

    //    public T Pop()
    //    {
    //        if (list.Count == 0)
    //        {
    //            throw new InvalidOperationException("The stack is empty.");
    //        }

    //        T value = list.First.Value;
    //        list.RemoveFirst();

    //        return value;
    //    }

    //    public T Peek()
    //    {
    //        if (list.Count == 0)
    //        {
    //            throw new InvalidOperationException("The stack is empty.");
    //        }

    //        return list.First.Value;
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return list.Count;
    //        }
    //    }

    //    public void Clear()
    //    {
    //        list.Clear();
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return list.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return list.GetEnumerator();
    //    }
    //}
}
