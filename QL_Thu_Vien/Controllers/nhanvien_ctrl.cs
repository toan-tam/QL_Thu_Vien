using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public class nhanvien_ctrl
    {

        public nhanvien_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<nhanvien_ett>> select_all_nhanvien()
        {
            Result<List<nhanvien_ett>> rs = new Result<List<nhanvien_ett>>();
            try
            {
                List<nhanvien_ett> lst = new List<nhanvien_ett>();
                var dt = db.tbl_nhanviens;
                if (dt.Count() > 0)
                {
                    foreach (tbl_nhanvien item in dt)
                    {
                        nhanvien_ett temp = new nhanvien_ett(item);
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

        public Result<bool> insert_nhanvien(nhanvien_ett nhanvien)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_nhanvien to insert to database_context
                tbl_nhanvien temp = new tbl_nhanvien();
                temp.tennv = nhanvien.tennhanvien;
                temp.diachi = nhanvien.diachi;
                temp.dienthoai = nhanvien.sdt;
                temp.email = nhanvien.email;
                temp.chucvu = nhanvien.chucvu;
                temp.tuoi = nhanvien.tuoi;
                temp.taikhoan = nhanvien.taikhoan;
                temp.matkhau = nhanvien.matkhau;

                db.tbl_nhanviens.InsertOnSubmit(temp);
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

        public Result<bool> delete_nhanvien(int manhanvien)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_nhanviens.Where(o => o.manv == manhanvien);
                if (dt.Count() > 0)
                {
                    foreach (tbl_nhanvien item in dt)
                    {
                        db.tbl_nhanviens.DeleteOnSubmit(item);
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

        public Result<bool> edit_nhanvien(nhanvien_ett nhanvien)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_nhanviens.Where(o => o.manv == nhanvien.manhanvien).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (nhanvien.tennhanvien != null && nhanvien.tennhanvien != "")
                {
                    dt.tennv = nhanvien.tennhanvien;
                }
                if (nhanvien.diachi != null && nhanvien.diachi != "")
                {
                    dt.diachi = nhanvien.diachi;
                }
                if (nhanvien.sdt != null && nhanvien.sdt != "")
                {
                    dt.dienthoai = nhanvien.sdt;
                }
                if (nhanvien.email != null && nhanvien.email != "")
                {
                    dt.email = nhanvien.email;
                }
                if (nhanvien.chucvu != null && nhanvien.chucvu != "")
                {
                    dt.chucvu = nhanvien.chucvu;
                }
                if (nhanvien.tuoi != null)
                {
                    dt.tuoi = nhanvien.tuoi;
                }
                if (nhanvien.taikhoan != null && nhanvien.taikhoan != "")
                {
                    dt.taikhoan = nhanvien.taikhoan;
                }
                if (nhanvien.matkhau != null && nhanvien.matkhau != "")
                {
                    dt.matkhau = nhanvien.matkhau;
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

        public Result<List<nhanvien_ett>> select_nhanvien_fields(string input, string howtosearch)
        {
            Result<List<nhanvien_ett>> rs = new Result<List<nhanvien_ett>>();
            try
            {
                IQueryable<tbl_nhanvien> dt = null;
                List<nhanvien_ett> lst = new List<nhanvien_ett>();
                switch (howtosearch)
                {
                    case "hoten":
                        dt = db.tbl_nhanviens.Where(o => o.tennv.Contains(input));
                        break;
                    case "chucvu":
                        dt = db.tbl_nhanviens.Where(o => o.chucvu.Contains(input));
                        break;
                    case "taikhoan":
                        dt = db.tbl_nhanviens.Where(o => o.taikhoan.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (tbl_nhanvien item in dt)
                    {
                        nhanvien_ett temp = new nhanvien_ett(item);
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
