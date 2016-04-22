using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public class nhaxuatban_ctrl
    {
        public nhaxuatban_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<nhaxuatban_ett>> select_all_nhaxuatban()
        {
            Result<List<nhaxuatban_ett>> rs = new Result<List<nhaxuatban_ett>>();
            try
            {
                List<nhaxuatban_ett> lst = new List<nhaxuatban_ett>();
                var dt = db.tbl_nxbs;
                if (dt.Count() > 0)
                {
                    foreach (tbl_nxb item in dt)
                    {
                        nhaxuatban_ett temp = new nhaxuatban_ett(item);
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

        public Result<bool> insert_nhaxuatban(nhaxuatban_ett nhaxuatban)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_nhaxuatban to insert to database_context
                tbl_nxb temp = new tbl_nxb();
                temp.tennxb = nhaxuatban.tennxb;
                temp.sdt = nhaxuatban.sdt;
                temp.diachi = nhaxuatban.diachi;

                db.tbl_nxbs.InsertOnSubmit(temp);
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

        public Result<bool> delete_nhaxuatban(int manhaxuatban)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_nxbs.Where(o => o.manxb == manhaxuatban);
                if (dt.Count() > 0)
                {
                    foreach (tbl_nxb item in dt)
                    {
                        db.tbl_nxbs.DeleteOnSubmit(item);
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

        public Result<bool> edit_nhaxuatban(nhaxuatban_ett nhaxuatban)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_nxbs.Where(o => o.manxb == nhaxuatban.manxb).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (nhaxuatban.tennxb != null && nhaxuatban.tennxb != "")
                {
                    dt.tennxb = nhaxuatban.tennxb;
                }
                if (nhaxuatban.sdt != null && nhaxuatban.sdt != "")
                {
                    dt.sdt = nhaxuatban.sdt;
                }
                if (nhaxuatban.diachi != null && nhaxuatban.diachi != "")
                {
                    dt.diachi = nhaxuatban.diachi;
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

        public Result<List<nhaxuatban_ett>> select_nhaxuatban_fields(string input, string howtosearch)
        {
            Result<List<nhaxuatban_ett>> rs = new Result<List<nhaxuatban_ett>>();
            try
            {
                IQueryable<tbl_nxb> dt = null;
                List<nhaxuatban_ett> lst = new List<nhaxuatban_ett>();
                switch (howtosearch)
                {
                    case "tennxb":
                        dt = db.tbl_nxbs.Where(o => o.tennxb.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_nxb item in dt)
                    {
                        nhaxuatban_ett temp = new nhaxuatban_ett(item);
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
