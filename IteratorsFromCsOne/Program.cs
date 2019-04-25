using System;
using System.Collections;
using System.ComponentModel;

namespace IteratorsFromCsOne
{
    class Program : IEnumerable
    {
        object[] values;
        int startingPoint;

        public Program(object[] values, int startingPoint)
        {
            this.values = values;
            this.startingPoint = startingPoint;
        }

        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < values.Length; index++)
            {
                yield return values[(index + startingPoint) % values.Length];
            }
        }

        static void Main(string[] args)
        {
            object[] values = { "a", "b", "c", "d", "e" };
            Program collection = new Program(values, 3);
            foreach (object x in collection)
            {
                Console.WriteLine(x);
            }
            Console.ReadKey();
        }
    }
}
