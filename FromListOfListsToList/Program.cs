using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromListOfListsToList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> first = new List<double> { 1,2,3};
            List<double> second = new List<double> { 4,5,6 };
            List<List<double>> tables = new List<List<double>> { first, second};

            List<string> third = new List<string> { "1", "2", "3" };
            List<string> fifth = new List<string> { "4", "5", "6" };
            List<List<string>> tables1 = new List<List<string>> { third, fifth };

            List<string> sixth = new List<string> { "0", "4", "5" };
            List<string> seventh = new List<string> { "3 and half", "7", "8" };
            List<string> eighth = new List<string> { "9", "10", "11"};
            List<List<string>> tables3 = new List<List<string>> { sixth, seventh, eighth };

            List<double> table1 = new List<double>();
            List<string> table2 = new List<string>();

            List<string> nighth = new List<string> { "1", "2" };
            List<double> tenth = new List<double> { 10.1, 21.5 };
            List<List<string>> tableArrow = new List<List<string>>();
            List<List<double>> tableBow = new List<List<double>>();

            table1 = FromListOfLists2List_Struct(tables, 2);
            table2 = FromListOfLists2List_Class(tables1, 1);

            tableArrow = ColumnIntoListOfLists_Class(tables3, nighth, 1);
            tableBow = ColumnIntoListOfLists_Struct(tables, tenth, 1);

            Console.WriteLine("*************table1*****************");
            foreach (var table in table1)
            {
                Console.WriteLine("{0}|", table);
            }
            Console.WriteLine("*************table2*****************");
            foreach (var table in table2)
            {
                Console.WriteLine("{0}|", table);
            }
            Console.WriteLine("*************tableArrow*****************");
            ShowThatWasHidden_Class(tableArrow);

            Console.WriteLine("*************tableBow*****************");
            ShowThatWasHidden_Struct(tableBow);

            Console.ReadLine();
        }

        private static void ShowThatWasHidden_Struct<T>(List<List<T>> tables) where T:struct
        {
            string sum = string.Empty;
            foreach (var table in tables)
            {
                foreach (var elem in table)
                {
                    sum = sum + "|" + elem.ToString();
                }
                Console.WriteLine("{0}|", sum);
                sum = string.Empty;
            }
        }

        private static void ShowThatWasHidden_Class<T>(List<List<T>> tables) where T:class
        {
            string sum = string.Empty;
            foreach (var table in tables)
            {
                foreach (var elem in table)
                {
                    sum = sum + "|" + elem;
                }
                Console.WriteLine("{0}|", sum);
                sum = string.Empty;
            }
        }
        
        /// <summary>
        /// Work with Types that are classes, e.g. string
        /// Transform List of list to list by copy to output list specific colomn.
        /// </summary>
        /// <typeparam name="T"> It's maybe string, int or any other simple type</typeparam>
        /// <param name="source"> What should be transformed</param>
        /// <param name="counter">Colomn number</param>
        /// <returns></returns>
        private static  List<T> FromListOfLists2List_Class<T>(List<List<T>> source, int counter) where T :class
        {
            List<T> ou = new List<T>();
            for (int i = 0; i < source.Count; i++)
            {
                List<T> temp = source[i];
                if (counter > temp.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                for (int j = 0; j < temp.Count; j++)
                {
                    if (j == counter)
                    {
                        ou.Add(temp[j]);
                    }
                }
            }
            return ou;
        }

        /// <summary>
        /// Work with Types that are struct, e.g. int, double and etc
        /// Transform List of list to list by copy to output list specific colomn.
        /// </summary>
        /// <typeparam name="T"> It's maybe string, int or any other simple type</typeparam>
        /// <param name="source"> What should be transformed</param>
        /// <param name="counter">Colomn number</param>
        /// <returns></returns>
        private static List<T> FromListOfLists2List_Struct<T>(List<List<T>> source, int counter) where T : struct
        {
            List<T> ou = new List<T>();
            for (int i = 0; i < source.Count; i++)
            {
                List<T> temp = source[i];
                if (counter > temp.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                for (int j = 0; j < temp.Count; j++)
                {
                    if (j == counter)
                    {
                        ou.Add(temp[j]);
                    }
                }
            }
            return ou;
        }

        /// <summary>
        /// Insert column between columns into List<List<T>>
        /// </summary>
        /// <typeparam name="T">T could be class type like string and so, so on.</typeparam>
        /// <param name="source"> Target List of lists</param>
        /// <param name="column">list to insert </param>
        /// <param name="counter"> after what column do you want to insert</param>
        /// <returns> Modified List<List<T>></returns>
        private static List<List<T>> ColumnIntoListOfLists_Class<T>(List<List<T>> source, List<T> column, int counter) where T: class
        {
            if (counter > source.Count)
            {
                Console.WriteLine("Error!");
                throw new IndexOutOfRangeException();
            }

            List<List<T>> output = new List<List<T>>(source.Count);
            foreach (List<T> table in source)
            {
                List<T> buff = new List<T>();
                foreach (var item in table)
                {
                    // Preparation of list<string>
                    if (column.Count != source.Count)
                    {
                        if (column.Count < source.Count)
                        {
                            int addition = source.Count - column.Count;
                            for (int n = 0; n < addition; n++)
                            {
                                column.Add(null);
                            }
                        }
                        else
                        {
                            int sub = column.Count - source.Count;
                            for (int m = 1; m < sub; m++)
                            {
                                column.RemoveAt(column.Count - m);
                            }
                        }
                    }

                    // Count of list<string> equil to count of list of lists ( == number of rows)
                    if (table.IndexOf(item) > counter && table.IndexOf(item) <= counter + 1)
                    {
                        buff.Add(column.ElementAt(source.IndexOf(table)));
                    }
                    buff.Add(item);
                }
                output.Add(buff);
            }
            return output;
        }

        /// <summary>
        /// Insert column between columns into List<List<T>>
        /// </summary>
        /// <typeparam name="T"> T could be any struct like int, double and so, so on</typeparam>
        /// <param name="source">Target List of lists</param>
        /// <param name="column">list to insert </param>
        /// <param name="counter"> after what column do you want to insert</param>
        /// <returns> Modified List<List<T>> </returns>
        private static List<List<T>> ColumnIntoListOfLists_Struct<T>(List<List<T>> source, List<T> column, int counter) where T : struct
        {
            if (counter > source.Count)
            {
                Console.WriteLine("Error!");
                throw new IndexOutOfRangeException();
            }

            List<List<T>> output = new List<List<T>>(source.Count);
            foreach (List<T> table in source)
            {
                List<T> buff = new List<T>();
                foreach (var item in table)
                {
                    // Preparation of list<string>
                    if (column.Count != source.Count)
                    {
                        if (column.Count < source.Count)
                        {
                            int addition = source.Count - column.Count;
                            for (int n = 0; n < addition; n++)
                            {
                                column.Add(default(T));
                            }
                        }
                        else
                        {
                            int sub = column.Count - source.Count;
                            for (int m = 1; m < sub; m++)
                            {
                                column.RemoveAt(column.Count - m);
                            }
                        }
                    }

                    // Count of list<string> equals to count of list of lists ( == number of rows)
                    if (table.IndexOf(item) > counter && table.IndexOf(item) <= counter + 1)
                    {
                        buff.Add(column.ElementAt(source.IndexOf(table)));
                    }
                    buff.Add(item);
                }
                output.Add(buff);
            }
            return output;
        }
    }
}
