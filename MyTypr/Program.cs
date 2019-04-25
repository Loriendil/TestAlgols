using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTypr
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> one = new List<string> { "ст", "ТР", "разделы 1-2, 5", "ГОСТ 433-87", "Наименование", " ", " ", " ", " ", " " };
            List<string> two = new List<string> { "ст", "ТР", "раздел 3, 5, 8 ", "ГОСТ Р МЭК 5555-2-2012", "Наименование", " ", " ", " ", " ", " " };
            List<string> three = new List<string> { "ст", "ТР", "раздел 1 ", "СТБ 111-87", "Наименование", " ", " ", " ", " ", " " };
            List<string> fo = new List<string> { "ст", "ТР", "разделы 9, 15, 18 ", "СТБ EN 433-87", "Наименование", " ", " ", " ", " ", " " };
            List<string> five = new List<string> { "ст", "ТР", "раздел 15 ", "ГОСТ 433-87 (ISO 14545-2000)", "Наименование", " ", " ", " ", " ", " " };
            List<List<string>> Table = new List<List<string>> { one, two, three, fo, five };

            List<Standard> st = new List<Standard>();

            foreach(var line in Table)
            {
                Standard temp = new Standard();
                foreach (var item in line)
                {
                    switch (line.IndexOf(item))
                    {
                        case 0:
                            temp.TRclause = item;
                            break;
                        case 1:
                            temp.TRnum = item;
                            break;
                        case 2:
                            temp.STclause = item;
                            break;
                        case 3:
                            temp.STnum = item;
                            break;
                        case 4:
                            temp.STname = item;
                            break;
                        case 5:
                            temp.Comment = item;
                            break;
                        case 6:
                            temp.LType = item;
                            break;
                        case 7:
                            temp.LComGrp = item;
                            break;
                        case 8:
                            temp.LSpGrp = item;
                            break;
                        case 9:
                            temp.STsource = item;
                            break;
                        default:
                            break;
                    }
                }
                st.Add(temp);
            }
            Console.WriteLine("Populated list:\n");
            foreach (Standard str in st)
            {
                Console.WriteLine("TRclause:" + str.TRclause + " | "+
                                  "TRnum : " + str.TRnum + " | " +
                                  "STclause: " + str.STclause + " | " +
                                  "STnum: " + str.STnum + " | " +
                                  "STname: " + str.STname + " | " +
                                  "Comment: " +  str.Comment + " | " +
                                  "LType: " + str.LType + " | " +
                                  "LComGrp: " + str.LComGrp + " | " +
                                  "LSpGrp: " + str.LSpGrp + " | " +
                                  "STsource: " + str.STsource + " | "
                                 );
            }
            Console.WriteLine("Changed list:\n");
            foreach (Standard s in st)
            {
                s.Comment = "Correction";
            }
            Console.WriteLine("Changed list:\n");
            foreach (Standard str in st)
            {
                Console.WriteLine("TRclause:" + str.TRclause + " | " +
                                  "TRnum : " + str.TRnum + " | " +
                                  "STclause: " + str.STclause + " | " +
                                  "STnum: " + str.STnum + " | " +
                                  "STname: " + str.STname + " | " +
                                  "Comment: " + str.Comment + " | " +
                                  "LType: " + str.LType + " | " +
                                  "LComGrp: " + str.LComGrp + " | " +
                                  "LSpGrp: " + str.LSpGrp + " | " +
                                  "STsource: " + str.STsource + " | "
                                 );
            }
            Console.ReadLine();
        }
    }
}
