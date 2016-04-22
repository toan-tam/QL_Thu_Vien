using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class chitietphieu_ett
    {
        public int sophieumuon { get; set; }
        public int masach { get; set; }
        public string trangthaisach { get; set; }

        public chitietphieu_ett() { }

        public chitietphieu_ett(tbl_chitietphieu chitiet)
        {
            sophieumuon = (int)chitiet.sophieumuon;
            masach = (int)chitiet.masach;
            trangthaisach = chitiet.trangthaisach;
        }
        public chitietphieu_ett(int sopm, int masach, string trangthai)
        {
            sophieumuon = sopm;
            this.masach = masach;
            trangthaisach = trangthai;
        }
    }
}
