using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
   public class linhvuc_ett
    {
        public int malinhvuc { get; set; } 
        public string tenlinhvuc { get; set; }

        public linhvuc_ett() { }
        public linhvuc_ett(tbl_linhvuc lv)
        {
            malinhvuc = lv.malinhvuc;
            tenlinhvuc = lv.tenlinhvuc;
        }

        public linhvuc_ett(int ma, string ten)
        {
            malinhvuc = ma;
            tenlinhvuc = ten;
        }
    }
}
