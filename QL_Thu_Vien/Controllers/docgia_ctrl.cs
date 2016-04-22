using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public class docgia_ctrl
    {
        public docgia_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<docgia_ett>> select_all_docgia()
        {
            Result<List<docgia_ett>> rs = new Result<List<docgia_ett>>();
            try
            {
                List<docgia_ett> lst = new List<docgia_ett>();
                var dt = db.tbl_docgias;
                if (dt.Count() > 0)
                {
                    foreach (tbl_docgia item in dt)
                    {
                        docgia_ett temp = new docgia_ett(item);
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

        public Result<bool> insert_docgia(docgia_ett docgia)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_docgia to insert to database_context
                tbl_docgia temp = new tbl_docgia();
                temp.tendg = docgia.tendocgia;
                temp.lop = docgia.lop;
                temp.gioitinh = docgia.gioitinh;
                temp.ngaysinh = docgia.ngaysinh;
                temp.email = docgia.email;
                temp.diachi = docgia.diachi;

                db.tbl_docgias.InsertOnSubmit(temp);
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

        public Result<List<docgia_ett>> select_expired_docgia()
        {
            Result<List<docgia_ett>> rs = new Result<List<docgia_ett>>();
            try
            {
                List<docgia_ett> lst = new List<docgia_ett>();
                var dt = (from b1 in db.tbl_docgias
                           join b2 in db.tbl_phieumuon_tras on b1.madg equals b2.madg
                           where b2.xacnhantra == false
                           select new { b1, b2.ngaytra}).AsEnumerable();

                var data = from o in dt
                           where DateTime.ParseExact(o.ngaytra, "dd/MM/yyyy", null) < DateTime.Today
                           group o by o.b1 into g
                           select new { g.Key};
                if (data.Count() > 0)
                {
                    foreach (var item in data)
                    {
                        docgia_ett temp = new docgia_ett(item.Key);
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

        public Result<bool> delete_docgia(int madocgia)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_docgias.Where(o => o.madg == madocgia);
                if (dt.Count() > 0)
                {
                    foreach (tbl_docgia item in dt)
                    {
                        db.tbl_docgias.DeleteOnSubmit(item);
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

        public Result<bool> edit_docgia(docgia_ett docgia)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_docgias.Where(o => o.madg == docgia.madocgia).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (docgia.tendocgia != null && docgia.tendocgia != "")
                {
                    dt.tendg = docgia.tendocgia;
                }
                if (docgia.ngaysinh != null && docgia.ngaysinh != "")
                {
                    dt.ngaysinh = docgia.ngaysinh;
                }
                if (docgia.gioitinh != null && docgia.gioitinh != "")
                {
                    dt.gioitinh = docgia.gioitinh;
                }
                if (docgia.lop != null && docgia.lop != "")
                {
                    dt.lop = docgia.lop;
                }
                if (docgia.diachi != null && docgia.diachi != "")
                {
                    dt.diachi = docgia.diachi;
                }
                if (docgia.email != null && docgia.email != "")
                {
                    dt.email = docgia.email;
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

        public Result<List<docgia_ett>> select_docgia_fields(string input, string howtosearch)
        {
            Result<List<docgia_ett>> rs = new Result<List<docgia_ett>>();
            try
            {
                IQueryable<tbl_docgia> dt = null;
                List<docgia_ett> lst = new List<docgia_ett>();
                switch (howtosearch)
                {
                    case "hoten":
                        dt = db.tbl_docgias.Where(o => o.tendg.Contains(input));
                        break;
                    case "lop":
                        dt = db.tbl_docgias.Where(o => o.lop.Contains(input));
                        break;
                    case "email":
                        dt = db.tbl_docgias.Where(o => o.email.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_docgia item in dt)
                    {
                        docgia_ett temp = new docgia_ett(item);
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
