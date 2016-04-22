using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class nhaxuatban_ett
    {
        public int manxb { get; set; }
        public string tennxb { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }

        public nhaxuatban_ett() { }
        public nhaxuatban_ett(tbl_nxb nxb)
        {
            manxb = nxb.manxb;
            tennxb = nxb.tennxb;
            diachi = nxb.diachi;
            sdt = nxb.sdt;
        }
        public nhaxuatban_ett(int ma, string ten, string diachi, string sdt)
        {
            manxb = ma;
            tennxb = ten;
            this.diachi = diachi;
            this.sdt = sdt;
        }
    }
}
