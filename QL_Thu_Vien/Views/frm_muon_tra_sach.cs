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
    public partial class frm_muon_tra_sach : Form
    {

        // enum variable used for select option
        private Option option = Option.Nodata;

        private phieumuontra_ctrl phieumuontra_ctrl = new phieumuontra_ctrl();

        // uses for save data from all textboxs
        private phieumuontra_ett phieumuontra_ett = new phieumuontra_ett();


        // set value to caculate later on
        private void get_info()
        {
            if (txt_soPM.Text != null && txt_soPM.Text != "")
            {
                phieumuontra_ett.sophieumuon = int.Parse(txt_soPM.Text);
            }
            else phieumuontra_ett.sophieumuon = 0;
            if (cbx_docgia.SelectedValue != null)
            {
                phieumuontra_ett.madg = int.Parse(cbx_docgia.SelectedValue.ToString());
            }
            if (cbx_nhanvien.SelectedValue != null)
            {
                phieumuontra_ett.manv = int.Parse(cbx_nhanvien.SelectedValue.ToString());
            }
            phieumuontra_ett.ngaymuon = dtpk_ngaymuon.Text;
            phieumuontra_ett.ngaytra = dtpk_ngaytra.Text;
            phieumuontra_ett.ghichu = txt_ghichu.Text;
            phieumuontra_ett.xacnhantra = chbox_xacnhantra.Checked;
        }

        //update data for dtgv
        private void load_data()
        {
            var dt = phieumuontra_ctrl.select_all_phieumuontra_view();
            switch (dt.errcode)
            {
                case Models.ErrorCode.NaN:
                    dtgv.DataSource = dt.data;
                    break;
                case Models.ErrorCode.sucess:
                    dtgv.DataSource = dt.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Số PM", "Độc giả", "Nhân viên", "Ngày mượn", "Ngày trả", "Xác nhận trả", "ghi chú" });
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

        private void load_data_sachmuon()
        {

        }

        public frm_muon_tra_sach()
        {
            InitializeComponent();
            Utils.readOnly_text_box(new List<TextBox> {txt_ghichu }, true);
            Utils.enable_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien }, false);
            chbox_xacnhantra.Enabled = false;
            dtgv_sachmuon.Enabled = false;
            dtpk_ngaymuon.Enabled = false;
            dtpk_ngaytra.Enabled = false;
            btn_xoa.Visible = false;
            btn_sua.Visible = false;

            DataGridViewComboBoxColumn data_sach = new DataGridViewComboBoxColumn();
            data_sach.HeaderText = "Sách";
            data_sach.Name = "data_sach";

            sach_ctrl sach = new sach_ctrl();
            var data_source = sach.select_all_sachview();
            switch (data_source.errcode)
            {
                case ErrorCode.NaN:
                    break;
                case ErrorCode.sucess:
                    data_sach.DisplayMember = "tensach";
                    data_sach.ValueMember = "masach";
                    data_sach.DataSource = data_source.data;
                    data_sach.FlatStyle = FlatStyle.Flat;
                    break;
                case ErrorCode.fail:
                    break;
                default:
                    break;
            }
            dtgv_sachmuon.Columns.Add(data_sach);

            DataGridViewComboBoxColumn data_trangthai = new DataGridViewComboBoxColumn();
            data_trangthai.HeaderText = "Trạng thái sách";
            data_trangthai.Items.Add("Bình thường");
            data_trangthai.Items.Add("Rách nát");
            data_trangthai.Items.Add("Mất");
            data_trangthai.FlatStyle = FlatStyle.Flat;
            dtgv_sachmuon.Columns.Add(data_trangthai);
            dtgv_sachmuon.Font = new Font("Verdana", 9, FontStyle.Regular);
            dtgv_sachmuon.Columns[1].Width = 150;
            dtgv_sachmuon.Columns[0].Width = 150;
            
        }



        private void frm_capnhat_docgia_Load(object sender, EventArgs e)
        {
            load_data();
            List<how_to_search> dt_source = new List<how_to_search>();
            dt_source.Add(new how_to_search("Số phiếu mượn", "sophieumuon"));
            dt_source.Add(new how_to_search("Độc giả", "docgia"));

            cbx_option_search.DataSource = dt_source;
            cbx_option_search.DisplayMember = "value";
            cbx_option_search.ValueMember = "key";

            docgia_ctrl docgia = new docgia_ctrl();
            nhanvien_ctrl nhanvien = new nhanvien_ctrl();

            var data_docgia = docgia.select_all_docgia();
            switch (data_docgia.errcode)
            {
                case ErrorCode.NaN:
                    break;
                case ErrorCode.sucess:
                    cbx_docgia.DisplayMember = "tendocgia";
                    cbx_docgia.ValueMember = "madocgia";
                    cbx_docgia.DataSource = data_docgia.data;
                    cbx_docgia.SelectedIndex = -1;
                    break;
                case ErrorCode.fail:
                    break;
                default:
                    break;
            }
            var data_nhanvien = nhanvien.select_all_nhanvien();
            switch (data_nhanvien.errcode)
            {
                case ErrorCode.NaN:
                    break;
                case ErrorCode.sucess:
                    cbx_nhanvien.DisplayMember = "tennhanvien";
                    cbx_nhanvien.ValueMember = "manhanvien";
                    cbx_nhanvien.DataSource = data_nhanvien.data;
                    cbx_nhanvien.SelectedIndex = -1;
                    break;
                case ErrorCode.fail:
                    break;
                default:
                    break;
            }

            dtgv_sachmuon.Rows[0].Cells[0].Selected = true;
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
            Utils.erase_text_box(new List<TextBox> { txt_soPM, txt_ghichu});
            Utils.readOnly_text_box(new List<TextBox> { txt_ghichu}, true);
            Utils.enable_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien }, false);
            chbox_xacnhantra.Enabled = false;
            dtgv_sachmuon.Enabled = false;
            dtgv_sachmuon.Rows.Clear();
            dtpk_ngaytra.Enabled = false;
            dtpk_ngaymuon.Enabled = false;
            cbx_docgia.SelectedIndex = -1;
            cbx_nhanvien.SelectedIndex = -1;
            option = Option.Nodata;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Utils.erase_text_box(new List<TextBox> { txt_soPM, txt_ghichu });
            Utils.erase_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien });
            Utils.readOnly_text_box(new List<TextBox> { txt_ghichu }, false);
            Utils.enable_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien }, true);
            chbox_xacnhantra.Enabled = false;
            chbox_xacnhantra.Checked = false;
            dtgv_sachmuon.Enabled = true;
            dtgv_sachmuon.Rows.Clear();
            dtpk_ngaytra.Enabled = true;
            dtpk_ngaymuon.Enabled = true;
            txt_soPM.Focus();
            option = Option.Insert;
            btn_sua.Enabled = btn_xoa.Enabled = btn_luu.Enabled = true;
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow temp = dtgv.Rows[e.RowIndex];
                txt_soPM.Text = temp.Cells[0].Value.ToString();
                cbx_docgia.Text = temp.Cells[1].Value.ToString();
                cbx_nhanvien.Text = temp.Cells[2].Value.ToString();
                dtpk_ngaymuon.Value = DateTime.ParseExact(temp.Cells[3].Value.ToString(), "dd/MM/yyyy", null);
                dtpk_ngaytra.Value = DateTime.ParseExact(temp.Cells[4].Value.ToString(), "dd/MM/yyyy", null);
                chbox_xacnhantra.Checked = bool.Parse(temp.Cells[5].Value.ToString());
                txt_ghichu.Text = temp.Cells[6].Value.ToString();

                if (chbox_xacnhantra.Checked)
                {
                    btn_sua.Enabled = btn_xoa.Enabled = btn_luu.Enabled = false;
                    Utils.readOnly_text_box(new List<TextBox> { txt_ghichu }, true);
                    Utils.enable_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien }, false);
                    chbox_xacnhantra.Enabled = false;
                    dtgv_sachmuon.Enabled = false;
                    dtpk_ngaytra.Enabled = false;
                    dtpk_ngaymuon.Enabled = false;
                }
                else
                {
                   btn_sua.Enabled = btn_xoa.Enabled = btn_luu.Enabled = true;
                }

                chitietphieu_ctrl chitiet_ctrl = new chitietphieu_ctrl();
                var list_sach = chitiet_ctrl.select_all_chitietphieu_by_sopm(int.Parse(txt_soPM.Text));
                var data_sach = list_sach.data;
                if (data_sach != null)
                {
                    dtgv_sachmuon.RowCount = data_sach.Count() + 1;
                    for (int i = 0; i < data_sach.Count(); i++)
                    {
                        dtgv_sachmuon.Rows[i].Cells[0].Value = data_sach[i].masach;
                        dtgv_sachmuon.Rows[i].Cells[1].Value = data_sach[i].trangthaisach;
                    }
                }
                else
                {
                    dtgv_sachmuon.Rows.Clear();
                }
                
            }
        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            var select_cbx = cbx_option_search.SelectedValue.ToString();
            var temp = phieumuontra_ctrl.select_phieumuontra_fields(txt_timkiem.Text, select_cbx);
            switch (temp.errcode)
            {
                case ErrorCode.NaN:
                    dtgv.DataSource = temp.data;
                    break;
                case ErrorCode.sucess:
                    dtgv.DataSource = temp.data;
                    Utils.chang_title_datagridViewCell(dtgv, new List<string> { "Số PM", "Độc giả", "Nhân viên", "Ngày mượn", "Ngày trả", "Xác nhận trả", "ghi chú" });
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
            Utils.readOnly_text_box(new List<TextBox> { txt_ghichu }, false);
            Utils.enable_combobox(new List<ComboBox> { cbx_docgia, cbx_nhanvien }, true);
            chbox_xacnhantra.Enabled = true;
            dtgv_sachmuon.Enabled = true;
            dtpk_ngaytra.Enabled = true;
            dtpk_ngaymuon.Enabled = true;
        }

        private List<chitietphieu_ett> chitietphieu = new List<chitietphieu_ett>();

        private void get_ds_sach()
        {
            get_info();
            var data = dtgv_sachmuon.Rows;
            foreach (DataGridViewRow item in data)
            {
                chitietphieu_ett temp = new chitietphieu_ett();
                if (item.Cells[0].Value == null)
                {
                    break;
                }
                temp.sophieumuon = phieumuontra_ett.sophieumuon;
                temp.masach = int.Parse(item.Cells[0].Value.ToString());
                if (item.Cells[1].Value == null)
                {
                    temp.trangthaisach = "Bình thường";
                } else
                temp.trangthaisach = item.Cells[1].Value.ToString();

                chitietphieu.Add(temp);
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case Option.Nodata:

                    break;

                case Option.Insert:
                    get_ds_sach();
                    var check_docgia = Utils.err_null_data_cbx(cbx_docgia);
                    if (check_docgia != null)
                    {
                        MessageBox.Show(check_docgia);
                        break;
                    }
                    var check_nhanvien = Utils.err_null_data_cbx(cbx_nhanvien);
                    if (check_nhanvien != null)
                    {
                        MessageBox.Show(check_nhanvien);
                        break;
                    }
                    get_info();
                    //check if existing data
                    var check = true;
                    var data = dtgv.Rows;
                    foreach (DataGridViewRow item in data)
                    {
                        if (int.Parse(item.Cells[0].Value.ToString()) == phieumuontra_ett.sophieumuon)
                        {
                            check = false;
                        }
                    }
                    if (!check)
                    {
                        Utils.err_duplicate_data();
                        break;
                    }
                    List<string> lst_masach = new List<string>();
                    var sach_dataRows = dtgv_sachmuon.Rows;
                    foreach (DataGridViewRow item in sach_dataRows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            lst_masach.Add(item.Cells[0].Value.ToString());
                        }
                    }
                    var temp = phieumuontra_ctrl.insert_phieumuontra(phieumuontra_ett, lst_masach);
                    switch (temp.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_insert);
                            load_data();
                            Utils.erase_text_box(new List<TextBox> { txt_soPM, txt_ghichu });
                            Utils.erase_combobox(new List<ComboBox>() { cbx_docgia, cbx_nhanvien });
                            chbox_xacnhantra.Checked = false;
                            dtgv_sachmuon.Rows.Clear();
                            // insert chitietphieu
                            var sopm = temp.data.sophieumuon;
                            chitietphieu_ctrl chitiet_ctrl = new chitietphieu_ctrl();
                            get_ds_sach();
                            phieumuontra_ett.sophieumuon = sopm;
                            foreach (chitietphieu_ett item in chitietphieu)
                            {
                                item.sophieumuon = sopm;
                                chitiet_ctrl.insert_chitietphieu(item);
                            }
                            
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
                        if (int.Parse(item.Cells[0].Value.ToString()) == phieumuontra_ett.sophieumuon)
                        {
                            check1 = false;
                        }
                    }
                    if (check1)
                    {
                        Utils.err_no_duplicate_data();
                        break;
                    }
                    List<string> lst_masach1 = new List<string>();
                    var sach_dataRows1 = dtgv_sachmuon.Rows;
                    foreach (DataGridViewRow item in sach_dataRows1)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            lst_masach1.Add(item.Cells[0].Value.ToString());
                        }
                    }
                    var temp1 = phieumuontra_ctrl.edit_phieumuontra(phieumuontra_ett, lst_masach1);
                    var sopm1 = phieumuontra_ett.sophieumuon;

                    chitietphieu_ctrl chitiet_ctrl1 = new chitietphieu_ctrl();
                    var list_sach = chitiet_ctrl1.select_all_chitietphieu_by_sopm(phieumuontra_ett.sophieumuon);
                    if (list_sach.data != null)
                    {
                        foreach (chitietphieu_ett item in list_sach.data)
                        {
                            chitiet_ctrl1.delete_chitietphieu(item.sophieumuon, item.masach);
                        }
                    }
                    
                    switch (temp1.errcode)
                    {
                        case ErrorCode.NaN:
                            break;
                        case ErrorCode.sucess:
                            MessageBox.Show(Constants.success_edit);
                            load_data();
                            get_ds_sach();
                            foreach (chitietphieu_ett item in chitietphieu)
                            {
                                item.sophieumuon = sopm1;
                                chitiet_ctrl1.insert_chitietphieu(item);
                            }
                            Utils.erase_text_box(new List<TextBox> { txt_soPM, txt_ghichu});
                            Utils.readOnly_text_box(new List<TextBox> {txt_ghichu}, true);
                            Utils.erase_combobox(new List<ComboBox>() { cbx_nhanvien, cbx_docgia });
                            Utils.enable_combobox(new List<ComboBox>() { cbx_nhanvien, cbx_docgia }, false);
                            dtpk_ngaymuon.Enabled = false;
                            dtgv_sachmuon.Enabled = false;
                            dtgv_sachmuon.Rows.Clear();
                            dtpk_ngaytra.Enabled = false;
                            chbox_xacnhantra.Enabled = false;
                            chbox_xacnhantra.Checked = false;
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
                        var temp = phieumuontra_ctrl.delete_phieumuontra(int.Parse(item.Cells[0].Value.ToString()));
                        switch (temp.errcode)
                        {
                            case ErrorCode.NaN:
                                break;
                            case ErrorCode.sucess:
                                Utils.erase_text_box(new List<TextBox> { txt_soPM, txt_ghichu });
                                Utils.readOnly_text_box(new List<TextBox> { txt_ghichu }, true);
                                Utils.erase_combobox(new List<ComboBox>() { cbx_nhanvien, cbx_docgia });
                                Utils.enable_combobox(new List<ComboBox>() { cbx_nhanvien, cbx_docgia }, false);
                                dtpk_ngaymuon.Enabled = false;
                                dtgv_sachmuon.Enabled = false;
                                dtpk_ngaytra.Enabled = false;
                                chbox_xacnhantra.Enabled = false;
                                chbox_xacnhantra.Checked = false;
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

        private void txt_sophieumuon_TextChanged(object sender, EventArgs e)
        {
            if (txt_soPM.Text == null || txt_soPM.Text == "")
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

        private void cbx_docgia_Leave(object sender, EventArgs e)
        {
            List<docgia_ett> temp = cbx_docgia.Items.OfType<docgia_ett>().ToList();
            var x = temp.Where(o => o.tendocgia == cbx_docgia.Text);
            if (x.Count() == 0)
            {
                MessageBox.Show(Constants.error_not_list);
                cbx_docgia.Focus();
            }
        }

        private void cbx_nhanvien_Leave(object sender, EventArgs e)
        {
            List<nhanvien_ett> temp = cbx_nhanvien.Items.OfType<nhanvien_ett>().ToList();
            var x = temp.Where(o => o.tennhanvien == cbx_nhanvien.Text);
            if (x.Count() == 0)
            {
                MessageBox.Show(Constants.error_not_list);
                cbx_nhanvien.Focus();
            }
        }

        private void dtgv_sachmuon_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }

        private void dtgv_sachmuon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 )
            {
                var data_check = dtgv_sachmuon.Rows;
                var cur_cell = dtgv_sachmuon[e.ColumnIndex, e.RowIndex];
                var check = 0;

                //MessageBox.Show(cur_cell.Value.ToString());
                foreach (DataGridViewRow item in data_check)
                {
                    if (item.Cells[0].FormattedValue == cur_cell.FormattedValue)
                    {
                        check++;
                    }
                }
                if (check == 2)
                {
                    MessageBox.Show("Mã sách này đã được nhập mời bạn nhập mã sách khác");
                    dtgv_sachmuon.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    cur_cell.Value = null;

                    //var x = dtgv_sachmuon.CanFocus;
                    //var y = dtgv_sachmuon.CurrentCellAddress;

                    //dtgv_sachmuon.CurrentCell = dtgv_sachmuon.Rows[y.Y].Cells[y.X];
                    //var z = dtgv_sachmuon.SelectedCells;
                    //cur_cell.Selected = true;
                    //dtgv_sachmuon.BeginEdit(true);
                    //return;
                }
            }
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            
        }
    }
}
