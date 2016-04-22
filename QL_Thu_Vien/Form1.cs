using DoAnCNPM.Shareds;
using DoAnCNPM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCNPM
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        public frm_main(string taikhoan)
        {
            InitializeComponent();
            btn_capnhat_nhanvien.Visible = false;
            tabctrl_baocao.Visible = false;
            item_baocao.Visible = false;
            item_capnhat_nhanvien.Visible = false;
        }

        private void item_thoat_Click(object sender, EventArgs e)
        {
            if (Utils.confirm_exit())
            {
                Application.Exit();
            }
        }

        private void btn_capnhat_docgia_Click(object sender, EventArgs e)
        {
            frm_capnhat_docgia temp = new frm_capnhat_docgia();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_capnhat_nhanvien_Click(object sender, EventArgs e)
        {
            frm_capnhat_nhanvien temp = new frm_capnhat_nhanvien();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_capnhat_sach_Click(object sender, EventArgs e)
        {
            frm_capnhat_sach temp = new frm_capnhat_sach();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_capnhat_tacgia_Click(object sender, EventArgs e)
        {
            frm_capnhat_tacgia temp = new frm_capnhat_tacgia();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_capnhat_nxb_Click(object sender, EventArgs e)
        {
            frm_capnhat_nxb temp = new frm_capnhat_nxb();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_capnhat_linhvuc_Click(object sender, EventArgs e)
        {
            frm_capnhat_linhvuc temp = new frm_capnhat_linhvuc();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_timkiem_sach_Click(object sender, EventArgs e)
        {
            frm_timkiem_sach temp = new frm_timkiem_sach();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_timkiem_docgia_Click(object sender, EventArgs e)
        {
            frm_timkiem_docgia temp = new frm_timkiem_docgia();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_muontra_sach_Click(object sender, EventArgs e)
        {
            frm_muon_tra_sach temp = new frm_muon_tra_sach();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_bcsach_hetsoluong_Click(object sender, EventArgs e)
        {
            frm_baocao_sachsaphet temp = new frm_baocao_sachsaphet();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_bc_sachchuaduocmuon_Click(object sender, EventArgs e)
        {
            frm_baocao_sachchuaduocmuon temp = new frm_baocao_sachchuaduocmuon();
            Utils.add_form_to_panel(temp, panel1);
        }

        private void btn_bcdocgia_muonquahan_Click(object sender, EventArgs e)
        {
            frm_baocao_docgiamuonquahan temp = new frm_baocao_docgiamuonquahan();
            Utils.add_form_to_panel(temp, panel1);
        }
    }
}
