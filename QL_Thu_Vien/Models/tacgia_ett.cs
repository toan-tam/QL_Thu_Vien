using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class tacgia_ett
    {
        public int matacgia { get; set; }
        public string tentacgia { get; set; }
        public string gioitinh { get; set; }
        public string diachi { get; set; }

        public tacgia_ett() { }
        public tacgia_ett(tbl_tacgia tg)
        {
            matacgia = tg.matg;
            tentacgia = tg.tentg;
            diachi = tg.diachi;
            gioitinh = tg.gioitinh;
        }
        public tacgia_ett(int matg, string tentg, string gt, string dc)
        {
            matacgia = matg;
            tentacgia = tentg;
            gioitinh = gt;
            diachi = dc;
        }
    }
}
