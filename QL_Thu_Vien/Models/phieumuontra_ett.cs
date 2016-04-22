using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class phieumuontra_ett
    {
        public int sophieumuon { get; set; }
        public int? madg { get; set; }
        public int? manv { get; set; }
        public string ngaymuon { get; set; }
        public string ngaytra { get; set; }
        public bool? xacnhantra { get; set; }
        public string ghichu { get; set; }

        public phieumuontra_ett() { }
        public phieumuontra_ett(tbl_phieumuon_tra phieu)
        {
            sophieumuon = phieu.sophieumuon;
            madg = (int)phieu.madg;
            manv = (int)phieu.manv;
            ngaymuon = phieu.ngaymuon;
            ngaytra = phieu.ngaytra;
            xacnhantra = (bool)phieu.xacnhantra;
            ghichu = phieu.ghichu;
        }
        public phieumuontra_ett(int sopm, int madg, int manv, string ngaymuon, string ngaytra, bool xacnhantra, string ghichu)
        {
            sophieumuon = sopm;
            this.madg = madg;
            this.manv = manv;
            this.ngaymuon = ngaymuon;
            this.ngaytra = ngaytra;
            this.xacnhantra = xacnhantra;
            this.ghichu = ghichu;
        }
    }
}
