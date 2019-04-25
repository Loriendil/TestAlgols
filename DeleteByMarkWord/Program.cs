using System;
using System.Collections.Generic;
using System.Linq;

namespace DeleteByMarkWord
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> first = new List<string> { " ", "   ", "  " };
            List<string> second = new List<string> { "элемент", "шифр", "примечание" };
            List<string> three = new List<string> { "1", "2", "3" };
            List<string> forth = new List<string> { "1", "статья 4", "ГОСТ 31996-2012" };
            List<List<string>> tables = new List<List<string>> { first, second, three, forth };
            Console.WriteLine("\n ************************Start************************ \n");
            foreach (var table in tables)
            {
                foreach(var elem in table)
                {
                    Console.WriteLine("{0}", elem);
                }
            }
            Console.WriteLine("\n ************************Procced************************\n");
            tables = Rtt(tables, "элемент");
            // method finds row contains only numbers
            Console.WriteLine("\n************************Result************************");
            foreach (var table in tables)
            {
                foreach (var elem in table)
                {
                    Console.WriteLine("{0}", elem);
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Method deletes from list every row contains mark word
        /// </summary>
        /// <param name="listOfTargets">list of arrows of strings that reprecent a table from Word document</param>
        /// <param name="variable">Variable represents a mark of row</param>
        /// <returns>List of arrows of strings without row with mark word.</returns>
        private static List<List<string>> Rtt(List<List<string>> listOfTargets, string variable)
        {
            foreach(List<string> targets in listOfTargets.ToList())
            {
                foreach (string target in targets.ToList())
                {
                    int numOfColomns = targets.Count();
                    int indexOfTarElem = targets.FindIndex(x => x == variable);
                    if (indexOfTarElem != -1)
                    {
                        targets.RemoveRange(0, numOfColomns);
                    }
                    if (IsItNumber(target) == true)
                    {
                        int count = 0;
                        List<int> indexs = new List<int>();
                        indexs.Add(targets.IndexOf(target));
                        if (count <= numOfColomns)
                        {
                            foreach(int index in indexs)
                            {
                                targets.RemoveAt(index);
                            }
                        }
                        count++;
                    }
					if (string.IsNullOrWhiteSpace(target))
                    {
                        int count = 0;
                        List<int> indexs = new List<int>();
                        indexs.Add(targets.IndexOf(target));
                        if (count <= numOfColomns)
                        {
                            foreach(int index in indexs)
                            {
                                targets.RemoveAt(index);
                            }
                        }
                        count++;
                    }
                }
            }
            return listOfTargets;
        }

        /// <summary>
        /// Methods determine input string is it integer or not
        /// </summary>
        /// <param name="target"> input string</param>
        /// <returns>bool value: true, input string is an integer, false: input string is not. 
        /// Optional, int.TryParse() returns integer that stored as string variable.</returns>
        private static bool IsItNumber(string target)
        {
            if (!String.IsNullOrEmpty(target))
            {
                int intValue;
                bool myValue = int.TryParse(target, out intValue);
                if (myValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }


    }
}
