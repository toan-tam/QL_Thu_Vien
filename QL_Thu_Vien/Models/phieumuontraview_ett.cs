using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class phieumuontraview_ett
    {
        public int sophieumuon { get; set; }
        public string docgia { get; set; }
        public string nhanvien { get; set; }
        public string ngaymuon { get; set; }
        public string ngaytra { get; set; }
        public bool? xacnhantra { get; set; }
        public string ghichu { get; set; }

        public phieumuontraview_ett() { }

        public phieumuontraview_ett(int sopm, string dg, string nv, string ngaymuon, string ngaytra, bool xacnhantra, string ghichu)
        {
            sophieumuon = sopm;
            docgia = dg;
            nhanvien = nv;
            this.ngaymuon = ngaymuon;
            this.ngaytra = ngaytra;
            this.xacnhantra = xacnhantra;
            this.ghichu = ghichu;
        }
    }
}
