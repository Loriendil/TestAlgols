using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FarmTenTR
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> first = new List<string> { " I. Стандарты группы A (общетехнические вопросы безопасности)  " };
            List<string> second = new List<string> { "1",
                                                     " статьи 4 и 5, приложения 1 и 2 ",
                                                     " ГОСТ ЕН 1050-2002 ",
                                                     "Безопасность машин. Принципы оценки и определения риска",
                                                     ""};
            List<string> third = new List<string> { " II. Стандарты группы B (групповые вопросы безопасности) " };
            List<string> forth = new List<string> { "69",
                                                    "статьи 4 и 5, приложения 1 и 2",
                                                    "раздел 3 ГОСТ 12.2.007.0-75",
                                                    "Система стандартов безопасности труда. Изделия электротехнические." +
                                                    " Общие требования безопасности",
                                                    "Применяется до 15.04.2017"};
            List<string> fifth = new List<string> { "III. Стандарты группы C" };
            List<string> sixth = new List<string> { "1. Турбины"};
            List<string> seventh = new List<string> { "84",
                                                      "статьи 4 и 5, приложения 1 и 2",
                                                      "разделы 2 и 3 ГОСТ 10731-85",
                                                     "Испарители поверхностного типа для паротурбинных электростанций." +
                                                     "Общие технические условия"};
            List<string> eighth = new List<string> { "5. Приспособления для грузоподъемных операций" };
            List<string> ninth = new List<string> { "135",
                                                      "статьи 4 и 5, приложения 1 и 2",
                                                      "раздел 5, приложение Б ГОСТ 30441-97 (ИСО 3076-84)",
                                                     "Цепи короткозвенные грузоподъемные некалиброванные класса прочности Т(8). " +
                                                     "Технические условия"};

            List<List<string>> tables = new List<List<string>> { first, second, third, forth,
                                                                 fifth, sixth, seventh, eighth, ninth };
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

        static List<List<string>> TestMethod(List<List<string>> Table)
        {
            string buff = string.Empty;
             List<string> buffer = new List<string> { string.Empty, string.Empty};
            List<List<string>> output = new List<List<string>>();
            Regex regex = new Regex(@"\d");

            foreach (var row in Table)
            {
                if (row.Count == 1)
                {
                    foreach (var item in row)
                    {
                        if (regex.IsMatch(item))
                        {
                            buffer[1] = item;
                        }
                        else
                        {
                            buffer[0] = item;
                        }
                    }
                }
                else
                {
                    row.AddRange(buffer);
                    output.Add(row);
                }
            }
            return output;
        }
    }
}
