using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SplitStringToCoupleStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> one = new List<string> { "1", "статьи 4, 5 ТР ТС 004/2011",
                                                    "ГОСТ 1501-78",
                                                    "Электроприборы  ", "" };
            List<string> two = new List<string> { "2", "статьи 4, 5 ТР ТС 004/2011",
                                                    "разделы 1 -5  и 8 -10 ГОСТ 1507-78",
                                                    " нагревательные ", "" };
            List<string> three = new List<string> { "3", "статьи 4, 5 ТР ТС 004/2011",
                                                    " ГОСТ 1047-78",
                                                    " небытовые", "" };
            List<string> four = new List<string> { "4", "статьи 4, 5 ТР ТС 004/2011",
                                                   "разделы 1 и 5, 10 ГОСТ 16012-70", "Изделия бытовые электромеханические", "" };
            List<string> five = new List<string> { "1", "статьи 4, 5 ТР ТС 004/2011",
                                                   "раздел 5 ГОСТ 15.1.044-89 (ИСО 4589-84)",
                                                   "Номенклатура ", "" };
            List<string> six = new List<string> { "2", "статьи 4, 5 ТР ТС 004/2011",
                                                    "ГОСТ 12.3.33-89 (ИСО 4589-84)",
                                                    "показателей ", "" };
            List<string> seven = new List<string> { "3", "статьи 4, 5 ТР ТС 004/2011",
                                                    "разделы 1-5, 10 ГОСТ 122-89",
                                                    "методы ", "" };
            List<string> eight = new List<string> { "4", "статьи 4, 5 ТР ТС 004/2011",
                                                    "разделы 3  и 4  ГОСТ 433-73 ", "Кабели силовые ", "" };
            List<List<string>> labRat = new List<List<string>> { one, two, three, four, five, six, seven, eight };
            ShowThatWasHidden(TestMethod(labRat));
            Console.ReadKey();
        }
        private static void ShowThatWasHidden<T>(List<List<T>> tables) where T : class
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

        private static List<List<string>> TestMethod(List<List<string>> table)
        {
            List<List<string>> output = new List<List<string>>();

            foreach (var line in table)
            {
                List<string> buffer = new List<string>();
                foreach (var item in line)
                {
                    if (item.Contains("раздел"))
                    {
                        string[] buff1 = item.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        string sum = string.Empty;
                        int index = 0;
                        foreach (var element in buff1)
                        {
                            if (element == "ГОСТ")
                            {
                                break;
                            }
                            else
                            {
                                sum += element+" ";
                                index++;
                            }
                        }
                        buffer.Add(sum);
                        sum = string.Empty;
                        for (int k=index; k<buff1.Length; k++)
                        {
                            sum += buff1[k]+" ";
                        }
                        buffer.Add(sum);
                        continue;
                    }
                    buffer.Add(item);
                }
                output.Add(buffer);
            }
            return output;
        }
    }
}
