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

        }

        /*
	    полезные ссылки: 
	    https://ru.stackoverflow.com/questions/419718/Пересечение-и-разность-двух-listobject
	    https://docs.microsoft.com/ru-ru/dotnet/api/system.linq.enumerable.except?redirectedfrom=MSDN&view=netframework-4.8#System_Linq_Enumerable_Except__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IEnumerable___0__
	    https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/concepts/linq/set-operations
	    https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/concepts/linq/how-to-find-the-set-difference-between-two-lists-linq
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
                            (t) => {
                                t.TRclause = line[0];
                                t.TRnum = line[1];
                                t.STclauseV = line[2];
                                t.STnum = line[3];
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
                            (t) => {
                                t.TRclause = ln[0];
                                t.TRnum = ln[1];
                                t.STclauseT = ln[2];
                                t.STnum = ln[3];
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
            List<Standard> compressedDB = new List<Standard>();
           // TODO: Productive action! I don't know what is it. 
            return compressedDB;
        }
    }
    public class Standard
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
        public Standard GetStandard(Action<Standard> alteration)
        {
            alteration(this);
            return this;
        }

        public string RemoveSpaces(string item)
        {
            return Regex.Replace(item, @"\s", "");
        }
    }
}
