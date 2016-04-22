using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class docgia_ett
    {
        public int madocgia { get; set; }
        public string tendocgia { get; set; }
        public string ngaysinh { get; set; }
        public string gioitinh { get; set; }
        public string lop { get; set; }
        public string diachi { get; set; }
        public string email { get; set; }

        public docgia_ett() { }
        public docgia_ett(tbl_docgia dg)
        {
            madocgia = dg.madg;
            tendocgia = dg.tendg;
            ngaysinh = dg.ngaysinh;
            gioitinh = dg.gioitinh;
            lop = dg.lop;
            diachi = dg.diachi;
            email = dg.email;
        }

        public docgia_ett(int ma, string ten, string ns, string gt, string lop, string dc, string email)
        {
            madocgia = ma;
            tendocgia = ten;
            ngaysinh = ns;
            this.lop = lop;
            diachi = dc;
            this.email = email;
        }
    }
}
