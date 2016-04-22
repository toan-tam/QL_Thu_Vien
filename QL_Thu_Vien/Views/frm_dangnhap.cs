using DoAnCNPM.Controllers;
using DoAnCNPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCNPM.Views
{
    public partial class frm_dangnhap : Form
    {
        public frm_dangnhap()
        {
            InitializeComponent();
            txt_mk.ForeColor = Color.White;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            nhanvien_ctrl nhanvien = new nhanvien_ctrl();
            var temp = nhanvien.select_all_nhanvien();
            switch (temp.errcode)
            {
                case Models.ErrorCode.NaN:
                    break;
                case Models.ErrorCode.sucess:
                    bool check = false;
                    foreach (nhanvien_ett item in temp.data)
                    {
                        if (txt_ten.Text == item.taikhoan && txt_mk.Text == item.matkhau && item.taikhoan != "" && item.taikhoan != null)
                        {
                            check = true;
                            break;            
                        }
                    }

                    if (!check)
                    {
                        MessageBox.Show("Sai thông tin đăng nhập mời bạn nhập lại!");
                        txt_ten.Focus();
                        txt_ten.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã đăng nhập thành công!");
                        this.Hide();
                        if (txt_ten.Text == "admin" && txt_mk.Text == "admin")
                        {
                            frm_main main_form = new frm_main();
                            main_form.Show();
                            break;
                        }
                        frm_main nhanvien_form = new frm_main(txt_ten.Text);
                        nhanvien_form.Show();
                    }
                    break;
                case Models.ErrorCode.fail:
                    break;
                default:
                    break;
            }
        }
    }
}
