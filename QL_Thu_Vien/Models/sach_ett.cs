using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class sach_ett
    {
        public int masach { get; set; }
        public string tensach { get; set; }
        public int? matg { get; set; }
        public int? manxb { get; set; }
        public int? malv { get; set; }
        public int? sotrang { get; set; }
        public int? soluong { get; set; }
        public string ngaynhap { get; set; }

        public sach_ett() { }
        public sach_ett(tbl_sach sach)
        {
            masach = sach.masach;
            tensach = sach.tensach;
            matg = (int)sach.matg;
            manxb = (int)sach.manxb;
            malv = (int)sach.malv;
            sotrang = (int)sach.sotrang;
            soluong = (int)sach.soluong;
            ngaynhap = sach.ngaynhap.ToString();
        }
        public sach_ett(int ma, string ten, int matg, int manxb, int malv, int sotrang, int soluong, string ngaynhap)
        {
            masach = ma;
            tensach = ten;
            this.matg = matg;
            this.malv = malv;
            this.manxb = manxb;
            this.sotrang = sotrang;
            this.soluong = soluong;
            this.ngaynhap = ngaynhap;
        }
    }
}
