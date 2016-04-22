using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public  class linhvuc_ctrl
    {

        public linhvuc_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<linhvuc_ett>> select_all_linhvuc()
        {
            Result<List<linhvuc_ett>> rs = new Result<List<linhvuc_ett>>();
            try
            {
                List<linhvuc_ett> lst = new List<linhvuc_ett>();
                var dt = db.tbl_linhvucs;
                if (dt.Count() > 0)
                {
                    foreach (tbl_linhvuc item in dt)
                    {
                        linhvuc_ett temp = new linhvuc_ett(item);
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

        public Result<bool> insert_linhvuc(linhvuc_ett linhvuc)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_linhvuc to insert to database_context
                tbl_linhvuc temp = new tbl_linhvuc();
                temp.tenlinhvuc = linhvuc.tenlinhvuc;

                db.tbl_linhvucs.InsertOnSubmit(temp);
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

        public Result<bool> delete_linhvuc(int malinhvuc)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_linhvucs.Where(o => o.malinhvuc == malinhvuc);
                if (dt.Count() > 0)
                {
                    foreach (tbl_linhvuc item in dt)
                    {
                        db.tbl_linhvucs.DeleteOnSubmit(item);
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

        public Result<bool> edit_linhvuc(linhvuc_ett linhvuc)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_linhvucs.Where(o => o.malinhvuc == linhvuc.malinhvuc).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (linhvuc.tenlinhvuc != null && linhvuc.tenlinhvuc != "")
                {
                    dt.tenlinhvuc = linhvuc.tenlinhvuc;
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

        public Result<List<linhvuc_ett>> select_linhvuc_fields(string input, string howtosearch)
        {
            Result<List<linhvuc_ett>> rs = new Result<List<linhvuc_ett>>();
            try
            {
                IQueryable<tbl_linhvuc> dt = null;
                List<linhvuc_ett> lst = new List<linhvuc_ett>();
                switch (howtosearch)
                {
                    case "tenlv":
                        dt = db.tbl_linhvucs.Where(o => o.tenlinhvuc.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_linhvuc item in dt)
                    {
                        linhvuc_ett temp = new linhvuc_ett(item);
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
