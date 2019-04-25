using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConcanStrFromDiffCells
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> first = new List<string> { " статья 4  ",
                                                    "ГОСТ 15047 - 78",
                                                    "Электроприборы нагревательные ",
                                                    "" };
            List<string> second = new List<string> { "",
                                                     " ГОСТ 16012 - 70 ",
                                                     " Изделия бытовые электромеханические.Термины и определения ",
                                                     "" };
            List<string> fifth = new List<string> { " абзацы первый - четвертый и шестой -двенадцатый ",
                                                    " ГОСТ 30506 - 97(МЭК 745 - 2 - 13 - 89) ",
                                                    " Машины ручные электрические. Частные требования безопасности и методы испытаний цепных пил ",
                                                    "" };
            List<string> sixth = new List<string> { " статьи 4 ,  статья 5 ",
                                                    " ГОСТ Р IEC 60745 - 2 - 13 - 2012 ",
                                                    " Машины ручные электрические. Безопасность и методы испытаний. ",
                                                    "" };

                               List <List<string>> tables = new List<List<string>> { first, second, // third, forth,
                                   fifth, sixth };
            List<List<string>> output = new List<List<string>>();
            output = TestMethod(tables);
            ShowThatWasHidden_Class(output);

            Console.ReadKey();
        }

        private static void ShowThatWasHidden_Class<T>(List<List<T>> tables) where T : class
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

        private static List<List<string>> TestMethod(List<List<string>> listOfTargets)
        {
            List<string> temp = new List<string>();
            string buff = string.Empty;
            foreach (List<string> list in listOfTargets)
            {
                temp.Add(list.FirstOrDefault());
            }

            for (int i =0; i<temp.Count; i++)
            {
                string s = temp[i].TrimEnd();
                if (string.IsNullOrWhiteSpace(s) || (Regex.Match(s, "(\\d+)$").Success))
                {
                    temp[i] = string.Concat(buff, temp[i]);
                    buff = temp[i];
                }
                else
                {
                    buff = temp[i];
                }
            }
            buff = string.Empty;
            //
            for (int i = temp.Count-1; i >=0; i--)
            {
                string s = temp[i].TrimEnd();
                if ((Regex.Match(s, "(\\D+)$").Success))
                {
                    temp[i] = buff;
                }
                else
                {
                    buff = temp[i];
                }
            }
            //
            return InsertListIntoListOfLIsts(listOfTargets, temp, 0);
        }

        private static void ShowD<T>(List<T> list)
        {
            int i = 1;
            foreach(var item in list)
            {
                Console.WriteLine(i.ToString()+". " +item.ToString());
                i++;
            }
        }
        
        /// <summary>
        /// Change data from choiced column in List<List<string>> on data from List<string> 
        /// </summary>
        /// <param name="source">List of list that needs a changes</param>
        /// <param name="column">Prepared column for insert</param>
        /// <param name="counter">Int variable that represents place where you want to insert </param>
        /// <returns>Proceed List of lists</returns>
        private static List<List<string>> InsertListIntoListOfLIsts(List<List<string>> source, List<string> column, int counter)
        {
            if (counter > source.Count)
            {
                Console.WriteLine("Error!");
                throw new IndexOutOfRangeException();
            }

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

            for (int j = 0; j<source.Count; j++)
            {
                List<string> temp = source[j];
                for (int i = 0; i< source[j].Count; i++)
                {
                    if (i == 0)
                    {
                        temp[i] =column[j];
                    }
                }
            }
            return source;
        }
    }
}
