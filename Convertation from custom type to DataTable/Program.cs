using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertation_from_custom_type_to_DataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Standard> table = new List<Standard>()
            {
                new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="раздел 1", STclauseT="", STnum=" ГОСТ IEC 60255 - 5 - 2014 ",
                                STname ="Электроприборы нагревательные бытовые.Термины и определения",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="применяется до 31.09.2017",
                                LType="free", LComGrp="", LSpGrp="", Comment="", STsource="RU"},
                 new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="раздел 2", STclauseT="", STnum="ГОСТ IEC 16012 - 70 ",
                                STname ="Изделия бытовые электромеханические.Термины и определения ",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="",
                                LType="free", LComGrp="", LSpGrp="", Comment="", STsource="RU"},
                 new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="раздел 3", STclauseT="", STnum="ГОСТ IEC 15047 - 78",
                                STname =" Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="применяется до 01.10.2011",
                                LType="free", LComGrp="", LSpGrp="", Comment="", STsource="RU"},
                 new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="", STclauseT="раздел 4", STnum="ГОСТ IEC 30328 - 95",
                                STname =" Реле электрические.Испытание изоляции ",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="",
                                LType="test", LComGrp="", LSpGrp="", Comment="", STsource="RU"},
                 new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="", STclauseT="раздел 5", STnum=" ГОСТ IEC 60255 - 5 - 2014 ",
                                STname =" Реле электрические.Часть 5.Координация изоляции измерительных реле и защитных устройств. Требования и испытания ",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="",
                                LType="test", LComGrp="", LSpGrp="", Comment="", STsource="RU"},
                 new Standard(){ TRclause =" статья 4  ", TRnum ="ТР ТС 004/2011",
                                STclauseV ="", STclauseT="раздел 6", STnum=" ГОСТ IEC 60745 - 2 - 13 - 2012 ",
                                STname ="Машины ручные электрические. Безопасность и методы испытаний. Часть 2 - 13.Частные требования к цепным пилам ",
                                ExpirDateFSA ="", ExpirDatePubl="", ExpirDateTR="",
                                LType="test", LComGrp="", LSpGrp="", Comment="", STsource="RU"}
            };

            DataTable dataTable = Std2DT(table);

            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        Console.WriteLine(item);
                    }
                    
                    Console.WriteLine("\n");
                }
            }

            Console.ReadKey();
        }

        public static DataTable Std2DT(List<Standard> Table)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.TableName = "TRTable";
                foreach (PropertyInfo property in typeof(Standard).GetProperties())
                {
                    dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                }

                foreach (var line in Table)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (PropertyInfo property in line.GetType().GetProperties())
                    {
                        newRow[property.Name] = line.GetType().GetProperty(property.Name).GetValue(line, null);
                    }
                    dt.Rows.Add(newRow);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return null; }
            return dt;
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
    }
}
