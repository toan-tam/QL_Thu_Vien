using DoAnCNPM.Controllers;
using DoAnCNPM.Models;
using DoAnCNPM.Shareds;
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
    public partial class frm_capnhat_tacgia : Form
    {
        // enum variable used for select option
        private Option option = Option.Nodata;

        private tacgia_ctrl tacgia_ctrl = new tacgia_ctrl();

        // uses for save data from all textboxs
        private tacgia_ett tacgia_ett = new tacgia_ett();


        // set value to caculate later on
        private void get_info()
        {
            if (txt_matacgia.Text != null && txt_matacgia.Text != "")
            {
                tacgia_ett.matacgia = int.Parse(txt_matacgia.Text);
            }
            else tacgia_ett.matacgia = 0;
            tacgia_ett.tentacgia = txt_tentacgia.Text;
            tacgia_ett.gioitinh = cbx_gioitinh.Text;
            tacgia_ett.diachi = txt_diachi.Text;
        }

        //update data for dtgv
        private void load_data()
        {
            var dt = tacgia_ctrl.select_all_tacgia();
            switch (dt.errcode)
            {
                case Models.ErrorCode.NaN:
                    dtgv.DataSource = dt.data;
                    break;
                case Models.ErrorCode.sucess:
                    dtgv.DataSource = dt.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Mã TG", "Tên TG", "Giới tính", "Địa chỉ" });
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

        public frm_capnhat_tacgia()
        {
            InitializeComponent();
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi,txt_tentacgia }, true);
            btn_xoa.Visible = false;
            btn_sua.Visible = false;
        }



        private void frm_capnhat_tacgia_Load(object sender, EventArgs e)
        {
            load_data();

            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Họ tên", "hoten"));

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
            Utils.erase_text_box(new List<TextBox> { txt_matacgia, txt_diachi, txt_tentacgia });
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_tentacgia }, true);
            cbx_gioitinh.SelectedIndex = -1;
            option = Option.Nodata;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_box(new List<TextBox> { txt_matacgia, txt_diachi, txt_tentacgia });
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_tentacgia }, false);
            txt_tentacgia.Focus();
            option = Option.Insert;

        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow temp = dtgv.Rows[e.RowIndex];
                txt_matacgia.Text = temp.Cells[0].Value.ToString();
                txt_tentacgia.Text = temp.Cells[1].Value.ToString();
                cbx_gioitinh.Text = temp.Cells[2].Value.ToString();
                txt_diachi.Text = temp.Cells[3].Value.ToString();
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = tacgia_ctrl.select_tacgia_fields(txt_timkiem.Text, select_cbx);
            switch (temp.errcode)
            {
                case ErrorCode.NaN:
                    dtgv.DataSource = temp.data;
                    break;
                case ErrorCode.sucess:
                    dtgv.DataSource = temp.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Mã TG", "Tên TG", "Giới tính", "Địa chỉ" });

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
            Utils.readOnly_text_box(new List<TextBox> { txt_diachi,  txt_tentacgia }, false);

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case Option.Nodata:

                    break;

                case Option.Insert:
                    var check_ten = Utils.err_null_data(txt_tentacgia);
                    if (check_ten != null)
                    {
                        MessageBox.Show(check_ten);
                        break;
                    }
                    get_info();
                    //check if existing data
                    var check = true;
                    var data = dtgv.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (int.Parse(item.Cells[0].Value.ToString()) == tacgia_ett.matacgia)
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        Utils.err_duplicate_data();
                        break;
                    }
                    var temp = tacgia_ctrl.insert_tacgia(tacgia_ett);
                    switch (temp.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_box(new List<TextBox> { txt_matacgia, txt_diachi, txt_tentacgia });
                            break;
                        case ErrorCode.fail:
                            break;
                        default:
                            break;
                    }
                    break;

                case Option.Edit:
                    get_info();
                    //check if existing data
                    var check1 = true;
                    var data1 = dtgv.Rows;
                    foreach (DataGridViewRow item in data1)
                    {
                        if (int.Parse(item.Cells[0].Value.ToString()) == tacgia_ett.matacgia)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        Utils.err_no_duplicate_data();
                        break;
                    }
                    var temp1 = tacgia_ctrl.edit_tacgia(tacgia_ett);
                    switch (temp1.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_edit);
                            load_data();
                            Utils.erase_text_box(new List<TextBox> { txt_matacgia, txt_diachi, txt_tentacgia });
                            Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_tentacgia }, true);
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
                        var temp = tacgia_ctrl.delete_tacgia(int.Parse(item.Cells[0].Value.ToString()));
                        switch (temp.errcode)
                        {
                            case ErrorCode.NaN:
                                break;
                            case ErrorCode.sucess:
                                Utils.erase_text_box(new List<TextBox> { txt_matacgia, txt_diachi, txt_tentacgia });
                                Utils.readOnly_text_box(new List<TextBox> { txt_diachi, txt_tentacgia }, true);
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

        private void txt_matacgia_TextChanged(object sender, EventArgs e)
        {
            if (txt_matacgia.Text == null || txt_matacgia.Text == "")
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
    }
}
