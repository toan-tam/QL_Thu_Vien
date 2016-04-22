using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCNPM.Controllers
{
    public class phieumuontra_ctrl
    {

        public phieumuontra_ctrl() { }

        QL_Thu_VienDataContext db = new QL_Thu_VienDataContext();

        public Result<List<phieumuontraview_ett>> select_all_phieumuontra_view()
        {
            Result<List<phieumuontraview_ett>> rs = new Result<List<phieumuontraview_ett>>();
            try
            {
                List<phieumuontraview_ett> lst = new List<phieumuontraview_ett>();
                var dt = from o in db.tbl_phieumuon_tras
                         join p in db.tbl_docgias on o.madg equals p.madg
                         join k in db.tbl_nhanviens on o.manv equals k.manv
                         select new phieumuontraview_ett() { sophieumuon = o.sophieumuon, docgia = p.tendg,
                         nhanvien = k.tennv, ngaymuon = o.ngaymuon, ngaytra = o.ngaytra, xacnhantra = o.xacnhantra, ghichu = o.ghichu};

                if (dt.Count() > 0)
                {
                    foreach (phieumuontraview_ett item in dt)
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

        public Result<phieumuontra_ett> insert_phieumuontra(phieumuontra_ett phieumuontra, List<string> list_masach)
        {
            Result<phieumuontra_ett> rs = new Result<phieumuontra_ett>();

            try
            {
                // create new tbl_phieumuontra to insert to database_context
                tbl_phieumuon_tra temp = new tbl_phieumuon_tra();
                temp.manv = phieumuontra.manv;
                temp.madg = phieumuontra.madg;
                temp.ngaytra = phieumuontra.ngaytra;
                temp.ngaymuon = phieumuontra.ngaymuon;
                temp.xacnhantra = phieumuontra.xacnhantra;
                temp.ghichu = phieumuontra.ghichu;

                db.tbl_phieumuon_tras.InsertOnSubmit(temp);

                if (list_masach.Count() > 0)
                {
                    foreach (string item in list_masach)
                    {
                        var dt = db.tbl_saches.Where(o => o.masach == int.Parse(item)).SingleOrDefault();
                        dt.soluong--;
                    }
                }   

                db.SubmitChanges();

                var last_record = db.tbl_phieumuon_tras.OrderByDescending(o => o.sophieumuon).Take(1);
                if (last_record.Count() > 0)
                {
                    foreach (var item in last_record)
                    {
                        phieumuontra_ett temp1 = new phieumuontra_ett(item);
                        rs.data = temp1;
                    }
                }
                rs.errcode = ErrorCode.sucess;
                return rs;
            }
            catch (Exception e)
            {
                rs.data = null;
                rs.errcode = ErrorCode.fail;
                rs.errInfor = e.ToString();
                return rs;
            }
        }

        public Result<bool> delete_phieumuontra(int maphieumuontra)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                var dt = db.tbl_phieumuon_tras.Where(o => o.sophieumuon == maphieumuontra);
                if (dt.Count() > 0)
                {
                    foreach (tbl_phieumuon_tra item in dt)
                    {
                        db.tbl_phieumuon_tras.DeleteOnSubmit(item);
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

        public Result<bool> edit_phieumuontra(phieumuontra_ett phieumuontra, List<string> list_masach)
        {
            Result<bool> rs = new Result<bool>();
            try
            {
                // find the only row to edit
                var dt = db.tbl_phieumuon_tras.Where(o => o.sophieumuon == phieumuontra.sophieumuon).SingleOrDefault();
                // if fields are null or "" then maintaining the old data;
                if (phieumuontra.madg != null)
                {
                    dt.madg = phieumuontra.madg;
                }
                if (phieumuontra.manv != null)
                {
                    dt.manv = phieumuontra.manv;
                }
                if (phieumuontra.ngaymuon != null && phieumuontra.ngaymuon != "")
                {
                    dt.ngaymuon = phieumuontra.ngaymuon;
                }
                if (phieumuontra.ngaytra != null && phieumuontra.ngaytra != "")
                {
                    dt.ngaytra = phieumuontra.ngaytra;
                }
                if (phieumuontra.ghichu != null && phieumuontra.ghichu != "")
                {
                    dt.ghichu = phieumuontra.ghichu;
                }
                if (phieumuontra.xacnhantra != null)
                {
                    dt.xacnhantra = phieumuontra.xacnhantra;
                    if (phieumuontra.xacnhantra==true)
                    {
                        if (list_masach.Count() > 0)
                        {
                            foreach (string item in list_masach)
                            {
                                var data = db.tbl_saches.Where(o => o.masach == int.Parse(item)).SingleOrDefault();
                                data.soluong++;
                            }
                        }               
                    }
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

        public Result<List<phieumuontraview_ett>> select_phieumuontra_fields(string input, string howtosearch)
        {
            Result<List<phieumuontraview_ett>> rs = new Result<List<phieumuontraview_ett>>();
            try
            {
                var dt = from o in db.tbl_phieumuon_tras
                         join p in db.tbl_docgias on o.madg equals p.madg
                         join k in db.tbl_nhanviens on o.manv equals k.manv
                         select new phieumuontraview_ett()
                         {
                             sophieumuon = o.sophieumuon,
                             docgia = p.tendg,
                             nhanvien = k.tennv,
                             ngaymuon = o.ngaymuon,
                             ngaytra = o.ngaytra,
                             xacnhantra = o.xacnhantra,
                             ghichu = o.ghichu
                         };
                List<phieumuontraview_ett> lst = new List<phieumuontraview_ett>();
                switch (howtosearch)
                {
                    case "sophieumuon":
                        dt = dt.Where(o => o.sophieumuon.ToString().Contains(input));
                        break;
                    case "docgia":
                        dt = dt.Where(o => o.docgia.Contains(input));
                        break;
                    default:
                        break;
                }

                if (dt.Count() > 0)
                {
                    foreach (phieumuontraview_ett item in dt)
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
