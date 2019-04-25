using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTypr
{
    class Standard
    {
        private string tRclause = string.Empty;
        private string tRnum = string.Empty;
        private string sTclause = string.Empty;
        private string sTnum = string.Empty;
        private string sTname = string.Empty;
        private string comment = string.Empty;
        private string lType = string.Empty;
        private string lComGrp = string.Empty;
        private string lSpGrp = string.Empty;
        private string sTsource = string.Empty;

        public string TRclause { get => tRclause; set => tRclause = value; }
        public string TRnum { get => tRnum; set => tRnum = value; }
        public string STclause { get => sTclause; set => sTclause = value; }
        public string STnum { get => sTnum; set => sTnum = value; }
        public string STname { get => sTname; set => sTname = value; }
        public string Comment { get => comment; set => comment = value; }
        public string LType { get => lType; set => lType = value; }
        public string LComGrp { get => lComGrp; set => lComGrp = value; }
        public string LSpGrp { get => lSpGrp; set => lSpGrp = value; }
        public string STsource { get => sTsource; set => sTsource = value; }
    }
}
