using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public  class sach_ctrl
    {
        public sach_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<sach_ett>> select_all_sach()
        {
            Result<List<sach_ett>> rs = new Result<List<sach_ett>>();
            try
            {
                List<sach_ett> lst = new List<sach_ett>();
                var dt = db.tbl_saches;
                if (dt.Count() > 0)
                {
                    foreach (tbl_sach item in dt)
                    {
                        sach_ett temp = new sach_ett(item);
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

        public Result<List<sachview_ett>> select_all_sachview()
        {
            Result<List<sachview_ett>> rs = new Result<List<sachview_ett>>();
            try
            {
                List<sachview_ett> lst = new List<sachview_ett>();
                var dt = from o in db.tbl_saches
                         join p in db.tbl_tacgias on o.matg equals p.matg
                         join k in db.tbl_nxbs on o.manxb equals k.manxb
                         join l in db.tbl_linhvucs on o.malv equals l.malinhvuc
                         select new sachview_ett() { masach = o.masach, tensach = o.tensach, linhvuc = l.tenlinhvuc, tacgia = p.tentg, nxb = k.tennxb, soluong = o.soluong.ToString(), sotrang = o.sotrang.ToString(), ngaynhap = o.ngaynhap };
                if (dt.Count() > 0)
                {
                    foreach (sachview_ett item in dt)
                    {
                        lst.Add(item);
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

        public Result<List<sachview_ett>> select_sach_almost_null()
        {
            Result<List<sachview_ett>> rs = new Result<List<sachview_ett>>();
            try
            {
                List<sachview_ett> lst = new List<sachview_ett>();
                var dt = from o in db.tbl_saches
                         join p in db.tbl_tacgias on o.matg equals p.matg
                         join k in db.tbl_nxbs on o.manxb equals k.manxb
                         join l in db.tbl_linhvucs on o.malv equals l.malinhvuc
                         where o.soluong < 5
                         select new sachview_ett() { masach = o.masach, tensach = o.tensach, linhvuc = l.tenlinhvuc, tacgia = p.tentg, nxb = k.tennxb, soluong = o.soluong.ToString(), sotrang = o.sotrang.ToString(), ngaynhap = o.ngaynhap };
                if (dt.Count() > 0)
                {
                    foreach (sachview_ett item in dt)
                    {
                        lst.Add(item);
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

        public Result<List<sachview_ett>> select_sach_no_one_borrow()
        {
            Result<List<sachview_ett>> rs = new Result<List<sachview_ett>>();
            try
            {
                List<sachview_ett> lst = new List<sachview_ett>();
                var dt = from o in db.tbl_saches
                         where !(from b2 in db.tbl_phieumuon_tras
                                 join b3 in db.tbl_chitietphieus on b2.sophieumuon equals b3.sophieumuon
                                 select b3.masach).Contains(o.masach)
                         join p in db.tbl_tacgias on o.matg equals p.matg
                         join k in db.tbl_nxbs on o.manxb equals k.manxb
                         join l in db.tbl_linhvucs on o.malv equals l.malinhvuc
                         select new sachview_ett() { masach = o.masach, tensach = o.tensach, linhvuc = l.tenlinhvuc, tacgia = p.tentg, nxb = k.tennxb, soluong = o.soluong.ToString(), sotrang = o.sotrang.ToString(), ngaynhap = o.ngaynhap };

                if (dt.Count() > 0)
                {
                    foreach (sachview_ett item in dt)
                    {
                        lst.Add(item);
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

        public Result<bool> insert_sach(sach_ett sach)
        {
            Result<bool> rs = new Result<bool>();

            try
            {
                // create new tbl_sach to insert to database_context
                tbl_sach temp = new tbl_sach();
                temp.tensach = sach.tensach;
                temp.matg = sach.matg;
                temp.manxb = sach.manxb;
                temp.malv = sach.malv;
                temp.sotrang = sach.sotrang;
                temp.soluong = sach.soluong;
                temp.ngaynhap = sach.ngaynhap;

                db.tbl_saches.InsertOnSubmit(temp);
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

        public Result<bool> delete_sach(int masach)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_saches.Where(o => o.masach == masach);
                if (dt.Count() > 0)
                {
                    foreach (tbl_sach item in dt)
                    {
                        db.tbl_saches.DeleteOnSubmit(item);
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

        public Result<bool> edit_sach(sach_ett sach)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_saches.Where(o => o.masach == sach.masach).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (sach.tensach != null && sach.tensach != "")
                {
                    dt.tensach = sach.tensach;
                }
                if (sach.matg != null)
                {
                    dt.matg = sach.matg;
                }
                if (sach.manxb != null)
                {
                    dt.manxb = sach.manxb;
                }
                if (sach.malv != null)
                {
                    dt.malv = sach.malv;
                }
                if (sach.soluong != null)
                {
                    dt.soluong = sach.soluong;
                }
                if (sach.sotrang != null)
                {
                    dt.sotrang = sach.sotrang;
                }
                if (sach.ngaynhap != null || sach.ngaynhap != "")
                {
                    dt.ngaynhap = sach.ngaynhap;
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

        public Result<List<sachview_ett>> select_sach_fields(string input, string howtosearch)
        {
            Result<List<sachview_ett>> rs = new Result<List<sachview_ett>>();
            try
            {
                var dt = from o in db.tbl_saches
                         join p in db.tbl_tacgias on o.matg equals p.matg
                         join k in db.tbl_nxbs on o.manxb equals k.manxb
                         join l in db.tbl_linhvucs on o.malv equals l.malinhvuc
                         select new sachview_ett() { masach = o.masach, tensach = o.tensach, linhvuc = l.tenlinhvuc, tacgia = p.tentg, nxb = k.tennxb, soluong = o.soluong.ToString(), sotrang = o.sotrang.ToString(), ngaynhap = o.ngaynhap };
               // IQueryable<tbl_sach> dt = null;
                List<sachview_ett> lst = new List<sachview_ett>();
                switch (howtosearch)
                {
                    case "tensach":
                        dt = dt.Where(o => o.tensach.Contains(input));
                        break;
                    case "tacgia":
                        dt = dt.Where(o => o.tacgia.Contains(input));
                        break;
                    case "linhvuc":
                        dt = dt.Where(o => o.linhvuc.Contains(input));
                        break;
                    case "nxb":
                        dt = dt.Where(o => o.nxb.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (sachview_ett item in dt)
                    {
                        lst.Add(item);
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
