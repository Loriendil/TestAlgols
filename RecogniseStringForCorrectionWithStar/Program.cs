using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecogniseStringForCorrectionWithStar
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<string> test = new List<string>
            { "разделы 3 и 4ГОСТ Р ИСО 15534-2-2009*",
              "ГОСТ 12.2.048.0-80 *",
              "разделы 3-12 ГОСТ Р 12.2.011-2012*",
              "раздел 3 ГОСТ 30701-2000* (МЭК 745-2-16-93)",
              "раздел 5 (ИСО 16368:2010 *",
              "ГОСТ 32342-2010* (МЭК 735-1-14-86)"
            };
            test = InsertCorrectionsFromDBPublisher(test);
            foreach (var elem in test){Console.WriteLine("{0}", elem);}
            Console.ReadKey();
        }

            protected static List<List<string>> InsertCorrectionsFromDBPublisher(List<List<string>> listOfTargets, int maxCol, string separator)
            {
                string singleString = string.Empty;
                int indexIntoString = 0;
                int indexIntoList = 0;
                int indexOfSeparator = 0;
                int indexIntoListOfLists = 0;
                string output = string.Empty;
                string focus = string.Empty;
                string stdname = "ГОСТ";
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
                                indexIntoString = focus.IndexOf(stdname);
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
                for (int i = 0; i < end.Count; i++)
                {
                    var temp = end[i];
                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (temp[j].Contains(separator))
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
                final = final.Where(x => x.Count != 1).ToList();

                return final;
            }

        private static IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)" + Regex.Escape(end));
            MatchCollection matches = r.Matches(input);
            foreach (Match match in matches)
                yield return match.Groups[1].Value;
        }

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

        protected static string CorrectStringByAnother(string target, string erratum, int indexIntoString, int indexOfSeparator)
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
