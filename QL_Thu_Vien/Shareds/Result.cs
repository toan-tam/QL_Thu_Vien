using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Models
{
    public class Result<T>
    {
        public ErrorCode errcode { get; set; }
        public string errInfor { get; set; }
        public T data { get; set; }
        public Result() {
            errInfor = "";
            errcode = ErrorCode.NaN;
        }
    }
}
