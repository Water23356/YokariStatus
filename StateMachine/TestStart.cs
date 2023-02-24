using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    internal class TestStart
    {
        public static void Main(string[] args)
        {
            Queue<string> myQ = new Queue<string>();
            myQ.Enqueue("Hello");
            myQ.Enqueue("World");
            myQ.Enqueue("233");

            Console.WriteLine(myQ.Count);
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Dequeue());
            Console.WriteLine(myQ.Count);
        }
        

    }


}

