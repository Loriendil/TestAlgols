using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Merge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> zero = new List<string> { " ", " ", " " };
            List<string> first = new List<string> { "1", "статья 4", "ГОСТ 3196-62" };
            List<string> second = new List<string> { "2", "статья 4", "раздел 1, 2-5 ГОСТ 31996-2012 *" };
            List<string> three = new List<string> { "*Вероятно, ошибка оригинала. Следует читать: \"ГОСТ Р ИСО 15534 - 2 - 2011\". - Примечание изготовителя базы данных.", "", "" };
            List<string> forth = new List<string> { "3", "статья 4,5", "ГОСТ 3996-2012" };
            List<string> forthAndharlf = new List<string> { " ", " ", " " };
            List<string> fifth = new List<string> { "4", "статья 4", "ГОСТ 396-78" };
            List<string> sixth = new List<string> { "5", "статья 5", "раздел 5 (ИСО 16368:2010 *" };
            List<string> seventh = new List<string> { "________________     * Вероятно, ошибка оригинала. Следует читать: \"Раздел 5 ГОСТ Р 53037-2013 (ИСО 16368:2010)\"." +
                                                      " - Примечание изготовителя базы данных.", "", "" };
            List<string> eighth = new List<string> { "6", "статья 4,5", "ГОСТ 3199-2002" };
            List<List<string>> tables = new List<List<string>> { zero, first, second, three, forth, forthAndharlf, fifth, sixth, seventh, eighth };
            Console.WriteLine("************************Start************************");
            ShowThatWasHidden(tables);
            Console.WriteLine("************************Procced************************");
            int maxCol = 3;
            string separator = "*";
            tables = TestMethod(tables, maxCol, separator);
            // method finds row contains only numbers
            Console.WriteLine("\n************************Result************************");
            ShowThatWasHidden(tables);
            Console.ReadLine();
        }
        private static void ShowThatWasHidden(List<List<string>> tables)
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

        private static List<List<string>> TestMethod(List<List<string>> listOfTargets, int maxCol, string separator)
        {
            string singleString = string.Empty;
            int indexIntoString = 0;
            int indexIntoList = 0;
            int indexOfSeparator = 0;
            int indexIntoListOfLists = 0;
            string output = string.Empty;
            string focus = string.Empty;
            //string stdname = "ГОСТ";
            List<string> StdNanes = new List<string> { "ГОСТ", "СТБ", "СТ РК", "(ИСО"};

            //-------------------------------------------------------------------
            List<List<string>> end = new List<List<string>>(listOfTargets.Count);

            for (int i = 0; i < listOfTargets.Count; i++)
            {
                List<string> temp = new List<string>();
                temp = listOfTargets[i];

                for (int k = 0; k < temp.Count; k++)
                {
                    if (temp[k].Contains(separator))
                    {
                        if (WhereStarIs(temp[k].ToString(), separator))
                        {
                            indexIntoList = k;
                            indexIntoListOfLists = i;
                            focus = temp[k].ToString();
                            foreach (var name in StdNanes)
                            {
                                if (focus.Contains(name))
                                {
                                    indexIntoString = focus.IndexOf(name);
                                }
                            }
                            //indexIntoString = focus.IndexOf(stdname);
                            indexOfSeparator = focus.IndexOf(separator);
                        }
                        else
                        {
                            List<string> item = end[indexIntoListOfLists];
                            IEnumerable<string> result = GetSubStrings(temp[k].ToString(), "\"", "\"");
                            singleString = string.Join(",", result);
                            output = CorrectStringByAnother(item[indexIntoList].ToString(), singleString, indexIntoString, indexOfSeparator);
                            Console.WriteLine(output);
                            item[indexIntoList] = output;
                            indexIntoList = 0;
                        }
                    }
                }
                end.Add(temp);
            }
            
            // copy with out specific keyword
            for (int i =0; i< end.Count; i++)
            {
                var temp = end[i];
                for(int j =0; j<temp.Count; j++)
                {
                    if(temp[j].Contains(separator))
                    {
                        temp.RemoveAt(j);
                    }
                }
                end[i] = temp;
            }
            // clean from empty strings
            List<List<string>> final = new List<List<string>>(end.Count);
            foreach (List<string> list in end)
            {
                    if (IsListEmpty(list))
                    {
                    final.Add(list);
                    }
            }

            return final;
        }

        /// <summary>
        ///  Method determine empty string. COunts string.Empty, whitespaces and null values.
        /// </summary>
        /// <param name="targets"> List of string for analysis. </param>
        /// <returns></returns>
        private static bool IsListEmpty(List<string> targets)
        {
            int countOfSpaces = 0;
            foreach (string target in targets)
            {
                if (string.IsNullOrEmpty(target) || string.IsNullOrWhiteSpace(target))
                {
                    countOfSpaces++;
                }                
            }
            if (countOfSpaces != targets.Count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        ///      find index of string
        ///      find index of separator( = "*")
        ///      if(index of separator<index of string)
        ///      {
        ///           star before string; // it's a remark
        ///      }
        ///      else
        ///      {
        ///      star after string; // it's string with corection
        ///      }
        /// </summary>
        /// <param name="target"> Target, where method will serach a separator</param>
        /// <param name="separator">Any sign that could be a separator</param>
        /// <returns>True: separator after keyword (Here i need to fix a hard written sequence. Here is "ГОСТ") False: if separator is before keyword.</returns>
        private static bool WhereStarIs(string target, string separator)
        {
            // get index of  separator
            int sepIndex = target.IndexOf(separator);
            // get index of keyword
            int keywordIndex = target.IndexOf("ГОСТ");
            if (sepIndex < keywordIndex) 
            {
                return false;
            }
            else
            {
                return true;
            }            
        }

        /// <summary>
        /// Gets all sub strings from start to end expresstion
        /// </summary>
        /// <param name="input"> String, which will be under analysis</param>
        /// <param name="start"> Escape string that marks a start of coping.</param>
        /// <param name="end"> Escape string that marks an end of coping. This string will be excluded from result! </param>
        /// <returns> IEnumerable<string> type. For convertion to string you need to use this: "string.Join(",", result);" </returns>
        /// <remark>If you want to toogle on start string into output string you need to write "yield return match.Groups[0].Value;",
        ///  if you want not - "yield return match.Groups[1].Value;" </remark>
        private static IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            MatchCollection matches = r.Matches(input);
            foreach (Match match in matches)
                yield return match.Groups[1].Value;
        }

        private static string CorrectStringByAnother(string target, string erratum, int indexIntoString, int indexOfSeparator)
        {
            StringBuilder outputValueBuilder = new StringBuilder(target);
            int Range = indexOfSeparator - indexIntoString + 1; // rewrite this shit magic!
            outputValueBuilder.Remove(indexIntoString, Range);
            outputValueBuilder.Insert(indexIntoString, erratum);
            target = outputValueBuilder.ToString();
            return target;
        }
    }
}
