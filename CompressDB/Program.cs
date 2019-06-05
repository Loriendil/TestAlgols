using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CompressDB
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> first = new List<string>
            { " статья 4  ",  "ТР ТС 004/2011",  "раздел 1", " ГОСТ IEC 60255 - 5 - 2014 ", "Электроприборы нагревательные бытовые.Термины и определения",  "применяется до 31.09.2017", "free", "",  "",  "RU" };
            List<string> second = new List<string>
            { "статья 4",     "ТР ТС 004/2011",  "раздел 2", "  ГОСТ IEC 60745 - 2 - 13 - 2012 ",  "Изделия бытовые электромеханические.Термины и определения ",  "", "free", "", "", "RU"};
            List<string> third = new List<string>
            { " статья 4 ", "ТР ТС 004/2011",  "раздел 3",  "ГОСТ IEC 15047 - 78",  " Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
              "применяется до 01.10.2011",  "free",  "",  "",  "RU" };
            List<string> forth = new List<string>
            { "статья 4", "ТР ТС 004/2011", "раздел 4",  "ГОСТ IEC 30328 - 95",  " Реле электрические.Испытание изоляции ",  " ", "test",  "", "", "RU"};
            List<string> fifth = new List<string>
            { " статья 4", "ТР ТС 004/2011",  "раздел 5", " ГОСТ IEC 60255 - 5 - 2014 ",  " Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
              "",  "test", "", "", "RU"};
            List<string> sixth = new List<string>
            { " статья 4", "ТР ТС 004/2011", "раздел 6", " ГОСТ IEC 60745 - 2 - 13 - 2012 ", "Машины ручные электрические. Безопасность и методы испытаний. Часть 2 - 13.Частные требования к цепным пилам ",
              "", "test", "", "", "RU"};
            List<List<string>> table = new List<List<string>>() { first, second, third, forth, fifth, sixth };

            List<Standard> VTable = new List<Standard>();
            List<Standard> TTable = new List<Standard>();
            CreateVolAndTestLists(table, out VTable, out TTable);
            VTable = FormatStdNum(VTable);
            TTable = FormatStdNum(TTable);
            var SortedVTable = VTable.OrderBy(x => x.STnum).ToList();
            var SortedTTable = TTable.OrderBy(y => y.STnum).ToList();
            List<Standard> output = CompressedDB(VTable, TTable);
            Console.WriteLine("\nВывод output после отработки второго метода:");
            foreach (var st in output)
            {
                 Console.WriteLine("clause: " + st.STclauseV + " clause: " + st.STclauseT + " | Cipher: " + st.STnum + " | Type: " + st.LType); 
            }
            Console.ReadKey();
        }

        /*
	    полезные ссылки: 
        https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sequenceequal?redirectedfrom=MSDN&view=netframework-4.8#System_Linq_Enumerable_SequenceEqual__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IEqualityComparer___0__
	    https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1?redirectedfrom=MSDN&view=netframework-4.8
        https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?redirectedfrom=MSDN&view=netframework-4.8#overloads
        */

        public static void CreateVolAndTestLists(List<List<string>> Table, out List<Standard> VTable, out List<Standard> TTable)
        {
            VTable = new List<Standard>();
            TTable = new List<Standard>();
            var VlTable = new List<List<string>>();
            var TtTable = new List<List<string>>();

            foreach (var line in Table)
            {
                if (line.Exists(x => x.Contains("free")))
                { VlTable.Add(line); }
                if (line.Exists(x => x.Contains("test")))
                { TtTable.Add(line); }
            }

            foreach (var line in VlTable)
            {
                var buffer = new Standard();
                var fbuffer = buffer.GetStandard
                    (
                            (t) =>
                            {
                                t.TRclause = line[0];
                                t.TRnum = line[1];
                                t.STclauseV = line[2];
                                t.STnum = Standard.TemplatedString(line[3]);
                                t.STname = line[4];
                                t.Comment = line[5];
                                t.LType = line[6];
                                t.LComGrp = line[7];
                                t.LSpGrp = line[8];
                                t.STsource = line[9];
                            }
                    );
                VTable.Add(buffer);
            }
            foreach (var ln in TtTable)
            {
                var buffer = new Standard();
                var fbuffer = buffer.GetStandard
                    (
                            (t) =>
                            {
                                t.TRclause = ln[0];
                                t.TRnum = ln[1];
                                t.STclauseT = ln[2];
                                t.STnum = Standard.TemplatedString(ln[3]);
                                t.STname = ln[4];
                                t.Comment = ln[5];
                                t.LType = ln[6];
                                t.LComGrp = ln[7];
                                t.LSpGrp = ln[8];
                                t.STsource = ln[9];
                            }
                    );
                TTable.Add(buffer);
            }
        }

        public static List<Standard> FormatStdNum(List<Standard> Table)
        {
            foreach (Standard item in Table)
            {
                item.STnum = FormattingMethod(item.STnum);
            }
            return Table;
        }
        private static string FormattingMethod(string line)
        {
            line = line.Trim();
            char[] foo = line.ToCharArray();
            StringBuilder outp = new StringBuilder();
            for (int i = 0; i < foo.Length; i++)
            {
                if (char.IsLetterOrDigit(foo[i]))
                {
                    outp.Append(foo[i]);
                }
                if (char.IsPunctuation(foo[i]))
                {
                    outp.Append(foo[i]);
                }
                if (char.IsWhiteSpace(foo[i]))
                {
                    if (char.IsWhiteSpace(foo[i - 1])) { continue; }
                    if (char.IsPunctuation(foo[i - 1]) || char.IsPunctuation(foo[i + 1])) { continue; }
                    else { outp.Append(foo[i]); }
                }

            }
            return outp.ToString();
        }
        public static List<Standard> CompressedDB(List<Standard> VolTable, List<Standard> TestTable)
        {
            // http://www.cyberforum.ru/csharp-beginners/thread1442293.html
            // https://stackoverflow.com/questions/10454519/best-way-to-compare-two-complex-objects
            // https://www.c-sharpcorner.com/article/comparing-objects-in-c-sharp/
            // https://grantwinney.com/how-to-compare-two-objects-testing-for-equality-in-c/
            // For TR CU 004 there 696 * 809 = 563 064 operations! It's too much! 
            // TODO: Productive action! I don't know what is it. 

            Console.WriteLine("\nИсходная матрица:");
            Console.WriteLine("\n Добровольная таблица");
            foreach (var st in VolTable)
            {
                Console.WriteLine("clause: " + st.STclauseV + " | Cipher: " + st.STnum + " | Type: " + st.LType);
            }
            Console.WriteLine("\n Тестовая таблица");
            foreach (var st in TestTable)
            {
                Console.WriteLine("clause: " + st.STclauseT + " | Cipher: " + st.STnum + " | Type: " + st.LType);
            }

            Console.WriteLine("\nИспользование метода расширения LINQ .Intersect():");
            var Source4Analysis = VolTable.Intersect(TestTable);

            foreach (var st in Source4Analysis)
            {
                if (st.LType == "free")
                { Console.WriteLine("clause: " + st.STclauseV + " | Cipher: " + st.STnum + " | Type: " + st.LType); }
                else
                { Console.WriteLine("clause: " + st.STclauseT + " | Cipher: " + st.STnum + " | Type: " + st.LType); }
            } 

            TestTable.Sort();
            // interesting thing about sorting with different tools! 
            // source #1: http://orydberg.azurewebsites.net/Posts/2013/4_HowToSortACSharpList.aspx
            // source #2: https://www.c-sharpcorner.com/UploadFile/80ae1e/icomparable-icomparer-and-iequatable-interfaces-in-C-Sharp/

            Console.WriteLine("\nСортируем:");
            foreach (var st in TestTable)
            {
                if (st.LType == "free")
                { Console.WriteLine("clause: " + st.STclauseV + " | Cipher: " + st.STnum + " | Type: " + st.LType); }
                else
                { Console.WriteLine("clause: " + st.STclauseT + " | Cipher: " + st.STnum + " | Type: " + st.LType); }
            }

            foreach (var line in Source4Analysis)
            {
                Standard temp = TestTable.Find(g=>g.STnum==line.STnum);
                line.STclauseV += temp.STclauseV;
                line.STclauseT += temp.STclauseT;
                line.LType = line.LType+ ";"+ temp.LType;
            }
            List<Standard> end = new List<Standard>();
            end.AddRange(VolTable);
            end.AddRange(TestTable);
            List<Standard> result = end.Distinct(new StandardComparer()).ToList();
            return result;
        }

        public class Standard : IEquatable<Standard>, IComparable<Standard>
        {
            #region field for properties
            private string tRclause = string.Empty;
            private string tRnum = string.Empty;
            private string sTclauseV = string.Empty;
            private string sTclauseT = string.Empty;
            private string sTnum = string.Empty;
            private string sTname = string.Empty;
            private string comment = string.Empty;
            private string lType = string.Empty;
            private string lComGrp = string.Empty;
            private string lSpGrp = string.Empty;
            private string sTsource = string.Empty;
            private string expirDateTR = string.Empty;
            private string expirDateFSA = string.Empty;
            private string expirDatePubl = string.Empty;
            #endregion

            #region Properties 
            public string TRclause { get => tRclause; set => tRclause = value; }
            public string TRnum { get => tRnum; set => tRnum = value; }
            public string STclauseV { get => sTclauseV; set => sTclauseV = value; }
            public string STclauseT { get => sTclauseT; set => sTclauseT = value; }
            public string STnum { get => sTnum; set => sTnum = value; }
            public string STname { get => sTname; set => sTname = value; }
            public string Comment { get => comment; set => comment = value; }
            public string ExpirDateTR { get => expirDateTR; set => expirDateTR = value; }
            public string ExpirDateFSA { get => expirDateFSA; set => expirDateFSA = value; }
            public string ExpirDatePubl { get => expirDatePubl; set => expirDatePubl = value; }
            public string LType { get => lType; set => lType = value; }
            public string LComGrp { get => lComGrp; set => lComGrp = value; }
            public string LSpGrp { get => lSpGrp; set => lSpGrp = value; }
            public string STsource { get => sTsource; set => sTsource = value; }
            #endregion

            /// <summary>
            /// This method gives an ability to fill properties of custom class by one click
            /// </summary>
            /// <param name="alteration"> a set of all properties</param>
            /// <returns>delegate with set of properties</returns>
            /// 
            public Standard GetStandard(Action<Standard> alteration)
            {
                alteration(this);
                return this;
            }

            public static string TemplatedString(string line)
            {
                line = line.Trim();
                char[] foo = line.ToCharArray();
                StringBuilder outp = new StringBuilder();
                for (int i = 0; i < foo.Length; i++)
                {
                    if (char.IsLetterOrDigit(foo[i]))
                    {
                        outp.Append(foo[i]);
                    }
                    if (char.IsPunctuation(foo[i]))
                    {
                        outp.Append(foo[i]);
                    }
                    if (char.IsWhiteSpace(foo[i]))
                    {
                        if (char.IsWhiteSpace(foo[i - 1])) { continue; }
                        if (char.IsPunctuation(foo[i - 1]) || char.IsPunctuation(foo[i + 1])) { continue; }
                        else { outp.Append(foo[i]); }
                    }

                }
                return outp.ToString();
            }

            #region Equalty: Implementation of IEquatable<T>
            // This is implemented Equals method, which you need to create when implementing the IEquatable interface.
            public bool Equals(Standard other)
            {
                if (other == null) { return false; }

                return (this.STnum == other.STnum) && (this.STclauseV == other.STclauseT);
            }
            // This implimented Object.Equals method, to prevent incorrect logic.
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals(obj as Standard);
            }

            public override int GetHashCode() { return this.STnum.GetHashCode(); }

            // This is overriding for equal and unequal operators to avoid boxing and unboxing when doing an equality check. 
            public static bool operator ==(Standard stdA, Standard stdB)
            {
                if (object.ReferenceEquals(stdA, stdB)) return true;
                if (object.ReferenceEquals(stdA, null)) return false;
                if (object.ReferenceEquals(stdB, null)) return false;

                return stdA.Equals(stdB);
            }
            public static bool operator !=(Standard stdA, Standard stdB)
            {
                if (object.ReferenceEquals(stdA, stdB)) return false;
                if (object.ReferenceEquals(stdA, null)) return true;
                if (object.ReferenceEquals(stdB, null)) return true;

                return !stdA.Equals(stdB);
            }
            #endregion

            #region Comparation: Implementation of IComparable<T>
            public int CompareTo(Standard other)
            {
                if (other == null)
                    return 1;
                else
                    return this.STnum.CompareTo(other.STnum);
            }

            // Define the is greater than operator.
            public static bool operator >(Standard operand1, Standard operand2)
            {
                return operand1.CompareTo(operand2) == 1;
            }

            // Define the is less than operator.
            public static bool operator <(Standard operand1, Standard operand2)
            {
                return operand1.CompareTo(operand2) == -1;
            }

            // Define the is greater than or equal to operator.
            public static bool operator >=(Standard operand1, Standard operand2)
            {
                return operand1.CompareTo(operand2) >= 0;
            }

            // Define the is less than or equal to operator.
            public static bool operator <=(Standard operand1, Standard operand2)
            {
                return operand1.CompareTo(operand2) <= 0;
            }
            #endregion

        }
        
        public class StandardComparer: IEqualityComparer<Standard>
        {
            #region Comparer: Implimentation of IComparer<T>
            public bool Equals(Standard stdA, Standard stdB)
            {
                if (stdA == null && stdB == null)
                {
                    return true;
                }
                else if (stdA == null || stdB == null)
                {
                    return false;
                }
                else if (stdA.STnum == stdB.STnum)
                {
                    return true;
                }
                else
                { return false; }
            }
            public int GetHashCode(Standard std)
            {
                return GetHashCode();
            }
            #endregion
        }

    }
}
