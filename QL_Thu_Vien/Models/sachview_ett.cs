using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class sachview_ett
    {
        public int masach { get; set; }
        public string tensach { get; set; }
        public string tacgia { get; set; }
        public string nxb { get; set; }
        public string linhvuc { get; set; }
        public string sotrang { get; set; }
        public string soluong { get; set; }
        public string ngaynhap { get; set; }

        public sachview_ett() { }
        public sachview_ett(int ma, string ten, string tg, string nxb, string lv, string sotrang, string soluong, string ngaynhap)
        {
            masach = ma;
            tensach = ten;
            tacgia = tg;
            this.nxb = nxb;
            linhvuc = lv;
            this.sotrang = sotrang;
            this.soluong = soluong;
            this.ngaynhap = ngaynhap;
        }
    }
}
