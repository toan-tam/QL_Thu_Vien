using DoAnCNPM.Controllers;
using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCNPM.Views
{
    public partial class frm_capnhat_nhanvien : Form
    {

        // enum variable used for select option
        private Option option = Option.Nodata;

        private nhanvien_ctrl nhanvien_ctrl = new nhanvien_ctrl();

        // uses for save data from all textboxs
        private nhanvien_ett nhanvien_ett = new nhanvien_ett();


        // set value to caculate later on
        private void get_info()
        {
            if (txt_manv.Text != null && txt_manv.Text != "")
            {
                nhanvien_ett.manhanvien = int.Parse(txt_manv.Text);
            }
            else
                nhanvien_ett.manhanvien = 0;
            nhanvien_ett.tennhanvien = txt_tennv.Text;
            nhanvien_ett.diachi = txt_diachi.Text;
            nhanvien_ett.sdt = txt_sdt.Text;
            nhanvien_ett.chucvu = txt_chucvu.Text;
            if (txt_tuoi.Text != null && txt_tuoi.Text != "")
            {
                nhanvien_ett.tuoi = int.Parse(txt_tuoi.Text);

            }
            else
                nhanvien_ett.tuoi = 0;
            nhanvien_ett.email = txt_email.Text;
            nhanvien_ett.taikhoan = txt_taikhoan.Text;
            nhanvien_ett.matkhau = txt_matkhau.Text;
        }

        //update data for dtgv
        private void load_data()
        {
            var dt = nhanvien_ctrl.select_all_nhanvien();
            switch (dt.errcode)
            {
                case Models.ErrorCode.NaN:
                    MessageBox.Show(dt.errInfor);
                    dtgv.DataSource = dt.data;
                    break;
                case Models.ErrorCode.sucess:
                    dtgv.DataSource = dt.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Mã NV", "Tên NV", "Địa chỉ", "Điện thoại", "Email", "Chức vụ", "Tuổi", "Tài khoản", "Mật khẩu" });
                    break;
                case Models.ErrorCode.fail:
                    if (Utils.switch_false())
                    {
                        MessageBox.Show(dt.errInfor);
                    }
                    break;
                default:
                    break;
            }
        }

        public frm_capnhat_nhanvien()
        {
            InitializeComponent();
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, true);
            btn_xoa.Visible = false;
            btn_sua.Visible = false;
        }



        private void frm_capnhat_nhanvien_Load(object sender, EventArgs e)
        {
            load_data();

            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Họ tên", "hoten"));
            dt_source.Add(new how_to_search("Chức vụ", "chucvu"));
            dt_source.Add(new how_to_search("Tài khoản", "taikhoan"));

            cbx_option_search.DataSource = dt_source;
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.ValueMember = "key";
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (Utils.confirm_exit())
            {
                Application.Exit();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            option = Option.Nodata;
            Utils.erase_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email, txt_manv,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi });
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, true);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email, txt_manv, txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi });
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, false);
            txt_tennv.Focus();
            option = Option.Insert;

        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow temp = dtgv.Rows[e.RowIndex];
                txt_manv.Text = temp.Cells[0].Value.ToString();
                txt_tennv.Text = temp.Cells[1].Value.ToString();
                txt_diachi.Text = temp.Cells[2].Value.ToString();
                txt_sdt.Text = temp.Cells[3].Value.ToString();
                txt_email.Text = temp.Cells[4].Value.ToString();
                txt_chucvu.Text = temp.Cells[5].Value.ToString();
                if (temp.Cells[6].Value != null && temp.Cells[6].Value != "")
                {
                    txt_tuoi.Text = temp.Cells[6].Value.ToString();
                }
                else txt_tuoi.Text = "";
                txt_taikhoan.Text = temp.Cells[7].Value.ToString();
                txt_matkhau.Text = temp.Cells[8].Value.ToString();
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = nhanvien_ctrl.select_nhanvien_fields(txt_timkiem.Text, select_cbx);
            switch (temp.errcode)
            {
                case ErrorCode.NaN:
                    dtgv.DataSource = temp.data;
                    break;
                case ErrorCode.sucess:
                    dtgv.DataSource = temp.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Mã NV", "Tên NV", "Địa chỉ", "Điện thoại", "Email", "Chức vụ", "Tuổi", "Tài khoản", "Mật khẩu" });
                    break;
                case ErrorCode.fail:
                    dtgv.DataSource = temp.data;
                    if (Utils.switch_false())
                    {
                        MessageBox.Show(temp.errInfor);
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            option = Option.Edit;
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, false);

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case Option.Nodata:

                    break;

                case Option.Insert:
                    var check_ten = Utils.err_null_data(txt_tennv);
                    if (check_ten != null)
                    {
                        MessageBox.Show(check_ten);
                        break;
                    }
                    if (txt_tuoi.Text.Length > 2)
                    {
                        MessageBox.Show(Constants.error_age);
                        txt_tuoi.Focus();
                        break;
                    }
                    if (txt_sdt.Text != null && txt_sdt.Text != "")
                    {
                        if (txt_sdt.Text.Length < 10 || txt_sdt.Text.Length > 11)
                        {
                            MessageBox.Show(Constants.error_sdt);
                            txt_sdt.Focus();
                            break;
                        }
                    }            
                    try
                    {
                        if (txt_email.Text != null && txt_email.Text != "")
                        {
                            MailAddress m = new MailAddress(txt_email.Text);
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(Constants.error_email);
                        txt_email.Focus();
                        break;
                    }            

                    get_info();
                    //check if existing data
                    var check = true;
                    var data = dtgv.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (int.Parse(item.Cells[0].Value.ToString()) == nhanvien_ett.manhanvien)
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        Utils.err_duplicate_data();
                        break;
                    }
                    var temp = nhanvien_ctrl.insert_nhanvien(nhanvien_ett);
                    switch (temp.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email, txt_manv, txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi });
                            break;
                        case ErrorCode.fail:
                            break;
                        default:
                            break;
                    }
                    break;

                case Option.Edit:
                    if (txt_tuoi.Text.Length > 2)
                    {
                        MessageBox.Show(Constants.error_age);
                        txt_tuoi.Focus();
                        break;
                    }
                    if (txt_sdt.Text != null && txt_sdt.Text != "")
                    {
                        if (txt_sdt.Text.Length < 10 || txt_sdt.Text.Length > 11)
                        {
                            MessageBox.Show(Constants.error_sdt);
                            txt_sdt.Focus();
                            break;
                        }
                    }
                    try
                    {
                        if (txt_email.Text != null && txt_email.Text != "")
                        {
                            MailAddress m = new MailAddress(txt_email.Text);
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(Constants.error_email);
                        txt_email.Focus();
                        break;
                    }


                    get_info();
                    //check if existing data
                    var check1 = true;
                    var data1 = dtgv.Rows;
                    foreach (DataGridViewRow item in data1)
                    {
                        if (int.Parse(item.Cells[0].Value.ToString()) == nhanvien_ett.manhanvien)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        Utils.err_no_duplicate_data();
                        break;
                    }
                    var temp1 = nhanvien_ctrl.edit_nhanvien(nhanvien_ett);
                    switch (temp1.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_edit);
                            load_data();
                            Utils.erase_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email, txt_manv, txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi });
                            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, true);
                            break;
                        case ErrorCode.fail:
                            if (Utils.switch_false())
                            {
                                MessageBox.Show(temp1.errInfor);
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // get data user selected;
            var selected_r = dtgv.SelectedRows;
            if (selected_r.Count > 0)
            {
                if (Utils.confirm_delete())
                {
                    bool check = true;
                    string err_infor = "";
                    foreach (DataGridViewRow item in selected_r)
                    {
                        var temp = nhanvien_ctrl.delete_nhanvien(int.Parse(item.Cells[0].Value.ToString()));
                        switch (temp.errcode)
                        {
                            case ErrorCode.NaN:
                                break;
                            case ErrorCode.sucess:
                                Utils.erase_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email, txt_manv,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi });
                                Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_chucvu, txt_email,  txt_matkhau, txt_sdt, txt_taikhoan, txt_tennv, txt_tuoi }, true);
                                option = Option.Nodata;
                                break;
                            case ErrorCode.fail:
                                check = false;
                                err_infor = temp.errInfor;
                                break;
                            default:
                                break;
                        }
                    }

                    if (check)
                    {
                        MessageBox.Show(Constants.success_delete);
                        load_data();
                    }
                    else
                    {
                        MessageBox.Show(Constants.not_allow_to_delete);
                    }
                }
            }
        }

        private void txt_manhanvien_TextChanged(object sender, EventArgs e)
        {
            if (txt_manv.Text == null || txt_manv.Text == "")
            {
                btn_xoa.Visible = false;
                btn_sua.Visible = false;
            }
            else
            {
                btn_xoa.Visible = true;
                btn_sua.Visible = true;
            }
        }


        private void txt_sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                return;
            }

            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                MessageBox.Show(Constants.error_format_number);
                e.KeyChar = (char)0;
            }
        }
    }
}
