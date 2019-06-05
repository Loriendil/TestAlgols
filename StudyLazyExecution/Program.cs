using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyLazyExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            //IEnumerable<int> enumerable = CreateSimpleIterator();
            //using (IEnumerator<int> enumerator =
            //enumerable.GetEnumerator())
            //{
            //    while (enumerator.MoveNext())
            //    {
            //        int value = enumerator.Current;
            //        Console.WriteLine(value);
            //    }
            //}

            foreach (string value in Iterator())
            {
                Console.WriteLine("Received value: {0}", value);
            }

        }
        static IEnumerable<int> CreateSimpleIterator()
        {
            yield return 10;
            for (int i = 0; i < 3; i++)
            {
                yield return i;
            }
            yield return 20;
        }

        static IEnumerable<string> Iterator()
        {
            try
            {
                Console.WriteLine("Before first yield");
                yield return "first";
                Console.WriteLine("Between yields");
                yield return "second";
                Console.WriteLine("After second yield");
            }
            finally
            {
                Console.WriteLine("In finally block");
            }
        }
    }
}
