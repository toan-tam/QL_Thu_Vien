using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCNPM.Shareds
{
    class Utils
    {
        public static void add_form_to_panel(Form f, Panel p)
        {
            p.Controls.Clear();
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            p.Controls.Add(f);
            f.Show();
        }

        public static void chang_title_datagridViewCell(DataGridView dtgv, List<string> title_cell)
        {
            dtgv.ColumnHeadersVisible = true;
            int i = 0;
            foreach (string item in title_cell)
            {
                dtgv.Columns[i].HeaderText = item;
                i++;
            }
        }

        #region "dialog"
        public static bool switch_false()
        {
            return MessageBox.Show(Constants.error, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public static bool confirm_delete()
        {
            return MessageBox.Show(Constants.confirm_delete, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public static bool confirm_exit()
        {
            return MessageBox.Show(Constants.confirm_exit, Constants.warning_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        #endregion

        public static void readOnly_text_box(List<TextBox> t, bool check)
        {
            if (check)
            {
                foreach (TextBox item in t)
                {
                    item.ReadOnly = true;
                }
            }
            else
            {
                foreach (TextBox item in t)
                {
                    item.ReadOnly = false;
                }
            }
           
        }

        public static void erase_text_box(List<TextBox> t)
        {
            foreach (TextBox item in t)
            {
                item.Text = "";
            }
        }

        public static void erase_combobox(List<ComboBox> t)
        {
            foreach (ComboBox item in t)
            {
                item.ResetText();
                item.SelectedIndex = -1;
            }
        }

        public static void enable_combobox(List<ComboBox> t, bool check)
        {
            if (check)
            {
                foreach (ComboBox item in t)
                {
                    item.Enabled = true;
                }
            } else
            {
                foreach (ComboBox item in t)
                {
                    item.Enabled = false;
                }
            }
        }

        public static DialogResult err_duplicate_data() {
            return MessageBox.Show("Mã đã tồn tại! bạn không thể thêm cơ sở dữ liệu! ");
        }

        public static DialogResult err_no_duplicate_data()
        {
            return MessageBox.Show("Mã không tồn tại! bạn không thể sửa thông tin ở 1 bản ghi không tồn tại! ");
        }

        public static string err_null_data(TextBox temp)
        {
            if (temp.Text == null || temp.Text == "")
            {
                temp.Focus();
                return "Trường này không được để trống!";
            }
            else return null;
        }
        public static string err_null_data_cbx(ComboBox temp)
        {
            if (temp.Text == null || temp.Text == "")
            {
                temp.Focus();
                return "Trường này không được để trống!";
            }
            else return null;
        }
    }
}
