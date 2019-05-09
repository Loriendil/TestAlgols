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
            { " статья 4  ",  "ТР ТС 004/2011",  "раздел 1", "ГОСТ 15047 - 78", "Электроприборы нагревательные бытовые.Термины и определения",  "применяется до 31.09.2017", "free", "",  "",  "RU" };
            List<string> second = new List<string>
            { "статья 4",     "ТР ТС 004/2011",  "раздел 2", " ГОСТ 16012 - 70 ",  "Изделия бытовые электромеханические.Термины и определения ",  "", "free", "", "", "RU"};
            List<string> third = new List<string>
            { " статья 4 ", "ТР ТС 004/2011",  "раздел 3",  " ГОСТ IEC 60255 - 5 - 2014 ",  " Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
              "применяется до 01.10.2011",  "free",  "",  "",  "RU" };
            List<string> forth = new List<string>
            { "статья 4", "ТР ТС 004/2011", "раздел 4",  "ГОСТ 30328 - 95",  " Реле электрические.Испытание изоляции ",  " ", "test",  "", "", "RU"};
            List<string> fifth = new List<string>
            { " статья 4", "ТР ТС 004/2011",  "раздел 5", " ГОСТ IEC 60255 - 5 - 2014 ",  " Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
              "",  "test", "", "", "RU"};
            List<string> sixth = new List<string>
            { " статья 4", "ТР ТС 004/2011", "раздел 6", " ГОСТ Р IEC 60745 - 2 - 13 - 2012 ", "Машины ручные электрические. Безопасность и методы испытаний. Часть 2 - 13.Частные требования к цепным пилам ",
              "", "test", "", "", "RU"};
            List<List<string>> table = new List<List<string>>() { first, second, third, forth, fifth, sixth };

            List<Standard> VTable = new List<Standard>();
            List<Standard> TTable = new List<Standard>();
            CreateVolAndTestLists(table, out VTable, out TTable);
            List<Standard> output = CompressedDB(VTable, TTable);
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

        public static List<Standard> CompressedDB(List<Standard> VolTable, List<Standard> TestTable)
        {
            // http://www.cyberforum.ru/csharp-beginners/thread1442293.html
            // https://stackoverflow.com/questions/10454519/best-way-to-compare-two-complex-objects
            // https://www.c-sharpcorner.com/article/comparing-objects-in-c-sharp/
            // https://grantwinney.com/how-to-compare-two-objects-testing-for-equality-in-c/
            // For TR CU 004 there 696 * 809 = 563 064 operations! It's too much! 
            //

            List<Standard> compressedDB = new List<Standard>();
            // TODO: Productive action! I don't know what is it. 
            foreach (var line in VolTable)
            {
                foreach (var ln in TestTable)
                {
                    if (ln.Equals(line))
                    {
                        Console.WriteLine("True");
                    }
                    else
                    {
                        Console.WriteLine("False");
                    }
                }
            }
            //bool equalAB = VolTable.SequenceEqual(TestTable, new StandardComparer());
            //Console.WriteLine("Equal?" + equalAB);
            return compressedDB;
        }

        public static void Compress()
        {
            Standard s1 = new Standard();

            s1.TRclause = "статья 4";
            s1.TRnum = "ТР ТС 004/2011";
            s1.STclauseV = "раздел 1";
            s1.STclauseT = string.Empty;
            s1.STnum = "ГОСТ 15047 - 78";
            s1.STname = "Электроприборы нагревательные бытовые.Термины и определения";
            s1.Comment = "применяется до 31.09.2017";
            s1.ExpirDateTR = string.Empty;
            s1.ExpirDateFSA = string.Empty;
            s1.ExpirDatePubl = string.Empty;
            s1.LType = "free";
            s1.LComGrp = string.Empty;
            s1.LSpGrp = string.Empty;
            s1.STsource = "RU";

            Standard s2 = new Standard();

            s2.TRclause = "статья 4";
            s2.TRnum = "ТР ТС 004/2011";
            s2.STclauseV = string.Empty;
            s2.STclauseT = "раздел 6" ;
            s2.STnum = " ГОСТ Р IEC 60745 - 2 - 13 - 2012 ";
            s2.STname = "Машины ручные электрические. Безопасность и методы испытаний. Часть 2 - 13.Частные требования к цепным пилам ";
            s2.Comment = string.Empty;
            s2.ExpirDateTR = string.Empty;
            s2.ExpirDateFSA = string.Empty;
            s2.ExpirDatePubl = string.Empty;
            s2.LType = "test";
            s2.LComGrp = string.Empty;
            s2.LSpGrp = string.Empty;
            s2.STsource = "RU";

            Console.WriteLine(s1.Equals(s2));

            Console.ReadKey();


        }
        public class Standard : IEquatable<Standard>
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

            public override bool Equals(object obj)
            {
                return this.Equals(obj as Standard);
            }

            public bool Equals(Standard other)
            {
                if (other == null)
                { return false; }
                return this.STnum.Equals(other.STnum);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        class StandardComparer : IEqualityComparer<Standard>
        {
            public bool Equals(Standard x, Standard y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;
                //Check whether the products' properties are equal.
                return x.STnum == y.STnum;
            }

            public int GetHashCode(Standard standard)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(standard, null)) return 0;

                //Get hash code for the Code field.
                int hashStandardNum = standard.STnum.GetHashCode();

                //Calculate the hash code for the product.
                return hashStandardNum;
            }
        }
    }
}
