using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemDaiHoc
{
    public partial class frmKhoa : Form
    {
        public frmKhoa()
        {
            InitializeComponent();
        }

        QLDDataContext db = new QLDDataContext();

        private void frmKhoa_Load(object sender, EventArgs e)
        {

            dgvKhoa.DataSource = db.Khoas;

            updateTextBox();

            txtBoxTenKhoa.TabStop = true;

        }

        bool adKhoa = false;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtBoxMaKhoa.Enabled = true;
            btnSave.Enabled = true;
            txtBoxMaKhoa.DataBindings.Clear();
            txtBoxTenKhoa.DataBindings.Clear();
            txtBoxMaKhoa.Text = "";
            txtBoxTenKhoa.Text = "";

            adKhoa = true;
            btnAdd.Enabled = false;

            btnRemove.Text = "Hủy";
        }


        bool updateKhoa = false;
        private void txtBoxTenKhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if press enter
            if (e.KeyChar == (char)13)
            {
                btnSave_Click(sender, e);
                return;
            }
            updateKhoa = true;
            btnAdd.Enabled = false;
            btnRemove.Text = "Hủy";
            btnSave.Enabled = true;

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (adKhoa || updateKhoa)
            {
                adKhoa = false;
                updateKhoa = false;
                btnAdd.Enabled = true;
                btnRemove.Text = "Xóa";
                txtBoxMaKhoa.Enabled = false;
                btnSave.Enabled = false;

                updateTextBox();

            }
            else
            {
                if (MessageBox.Show("Bạn xó muốn xóa khoa này?, Chỉ có thể xóa khoa nếu chưa có các lớp học phần trong khoa này", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string maKhoa = txtBoxMaKhoa.Text;
                    try
                    {
                        db.Khoa_Delete(maKhoa);
                        MessageBox.Show("Xóa khoa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvKhoa.DataSource = null;
                        dgvKhoa.DataSource = db.Khoas.ToList();

                        updateTextBox();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void updateTextBox()
        {
            txtBoxMaKhoa.DataBindings.Clear();
            txtBoxMaKhoa.DataBindings.Add("Text", dgvKhoa.DataSource, "MaKhoa");

            txtBoxTenKhoa.DataBindings.Clear();
            txtBoxTenKhoa.DataBindings.Add("Text", dgvKhoa.DataSource, "TenKhoa");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (adKhoa)
            {
                string maKhoa = txtBoxMaKhoa.Text.ToUpper();
                string tenKhoa = txtBoxTenKhoa.Text.ToUpper();

                if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                try
                {
                    db.Khoa_Insert(maKhoa, tenKhoa);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mã khoa đã được sử dụng");
                    return;
                }

                MessageBox.Show("Thêm khoa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại DataGridView
                dgvKhoa.DataSource = null;
                dgvKhoa.DataSource = db.Khoas.ToList(); // Chuyển đổi sang danh sách để làm mới dữ liệu
                updateTextBox();

                adKhoa = false;
            }

            else if (updateKhoa)
            {
                string maKhoa = txtBoxMaKhoa.Text.ToUpper();
                string tenKhoa = txtBoxTenKhoa.Text.ToUpper();

                if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                try
                {
                    db.Khoa_Update(maKhoa, tenKhoa);
                    MessageBox.Show("Cập nhật khoa thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvKhoa.DataSource = null;
                    dgvKhoa.DataSource = db.Khoas.ToList();

                    updateKhoa = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            btnAdd.Enabled = true;
            btnRemove.Text = "Xóa";
            txtBoxMaKhoa.Enabled = false;
            btnSave.Enabled = false;
        }

        private void txtBoxMaKhoa_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if press enter
            if (e.KeyChar == (char)13)
            {
                btnSave_Click(sender, e);
            }
        }
    }
}
