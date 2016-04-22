using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public class chitietphieu_ctrl
    {

        public chitietphieu_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<chitietphieu_ett>> select_all_chitietphieu_by_sopm(int sopm)
        {
            Result<List<chitietphieu_ett>> rs = new Result<List<chitietphieu_ett>>();
            try
            {
                List<chitietphieu_ett> lst = new List<chitietphieu_ett>();
                var dt = db.tbl_chitietphieus.Where(o => o.sophieumuon == sopm);
                if (dt.Count() > 0)
                {
                    foreach (tbl_chitietphieu item in dt)
                    {
                        chitietphieu_ett temp = new chitietphieu_ett(item);
                        lst.Add(temp);
                    }
                    rs.data = lst;
                    rs.errcode = ErrorCode.sucess;
                }
                else
                {
                    rs.data = null;
                    rs.errInfor = Constants.empty_data;
                }
                return rs;
            }
            catch (Exception e)
            {
                rs.data = null;
                rs.errInfor = e.ToString();
                rs.errcode = ErrorCode.fail;
                return rs;
            }
        }

        public Result<bool> insert_chitietphieu(chitietphieu_ett chitietphieu)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_chitietphieu to insert to database_context
                tbl_chitietphieu temp = new tbl_chitietphieu();
                temp.sophieumuon = chitietphieu.sophieumuon;
                temp.masach = chitietphieu.masach;
                temp.trangthaisach = chitietphieu.trangthaisach;

                db.tbl_chitietphieus.InsertOnSubmit(temp);
                db.SubmitChanges();

                rs.data = true;
                rs.errcode = ErrorCode.sucess;
                return rs;
            }
            catch (Exception e)
            {
                rs.data = false;
                rs.errcode = ErrorCode.fail;
                rs.errInfor = e.ToString();
                return rs;
            }
        }

        public Result<bool> delete_chitietphieu(int sopm, int masach)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_chitietphieus.Where(o => o.sophieumuon == sopm && o.masach == masach);
                if (dt.Count() > 0)
                {
                    foreach (tbl_chitietphieu item in dt)
                    {
                        db.tbl_chitietphieus.DeleteOnSubmit(item);
                    }
                    db.SubmitChanges();
                    rs.data = true;
                    rs.errcode = ErrorCode.sucess;
                }
                else
                {
                    rs.data = false;
                    rs.errcode = ErrorCode.NaN;
                    rs.errInfor = Constants.empty_data;
                }

                return rs;
            }
            catch (Exception e)
            {
                rs.data = false;
                rs.errcode = ErrorCode.fail;
                rs.errInfor = e.ToString();
                return rs;
            }
        }
    }
}
