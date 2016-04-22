using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
   public class tacgia_ctrl
    {
        public tacgia_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<tacgia_ett>> select_all_tacgia()
        {
            Result<List<tacgia_ett>> rs = new Result<List<tacgia_ett>>();
            try
            {
                List<tacgia_ett> lst = new List<tacgia_ett>();
                var dt = db.tbl_tacgias;
                if (dt.Count() > 0)
                {
                    foreach (tbl_tacgia item in dt)
                    {
                        tacgia_ett temp = new tacgia_ett(item);
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

        public Result<bool> insert_tacgia(tacgia_ett tacgia)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_tacgia to insert to database_context
                tbl_tacgia temp = new tbl_tacgia();
                temp.tentg = tacgia.tentacgia;
                temp.gioitinh = tacgia.gioitinh;
                temp.diachi = tacgia.diachi;

                db.tbl_tacgias.InsertOnSubmit(temp);
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

        public Result<bool> delete_tacgia(int matacgia)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_tacgias.Where(o => o.matg == matacgia);
                if (dt.Count() > 0)
                {
                    foreach (tbl_tacgia item in dt)
                    {
                        db.tbl_tacgias.DeleteOnSubmit(item);
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

        public Result<bool> edit_tacgia(tacgia_ett tacgia)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_tacgias.Where(o => o.matg == tacgia.matacgia).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (tacgia.tentacgia != null && tacgia.tentacgia != "")
                {
                    dt.tentg = tacgia.tentacgia;
                }
                if (tacgia.gioitinh != null && tacgia.gioitinh != "")
                {
                    dt.gioitinh = tacgia.gioitinh;
                }
                if (tacgia.diachi != null && tacgia.diachi != "")
                {
                    dt.diachi = tacgia.diachi;
                }

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

        public Result<List<tacgia_ett>> select_tacgia_fields(string input, string howtosearch)
        {
            Result<List<tacgia_ett>> rs = new Result<List<tacgia_ett>>();
            try
            {
                IQueryable<tbl_tacgia> dt = null;
                List<tacgia_ett> lst = new List<tacgia_ett>();
                switch (howtosearch)
                {
                    case "hoten":
                        dt = db.tbl_tacgias.Where(o => o.tentg.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_tacgia item in dt)
                    {
                        tacgia_ett temp = new tacgia_ett(item);
                        lst.Add(temp);
                    }
                    rs.data = lst;
                    rs.errcode = ErrorCode.sucess;
                    return rs;
                }
                else
                {
                    rs.data = null;
                    rs.errInfor = Constants.empty_data;
                    return rs;
                }
            }
            catch (Exception e)
            {
                rs.data = null;
                rs.errInfor = e.ToString();
                rs.errcode = ErrorCode.fail;
                return rs;
            }
        }
    }
}
