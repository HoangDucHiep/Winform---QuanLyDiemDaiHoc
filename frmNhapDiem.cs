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
    public partial class frmNhapDiem : Form
    {
        public frmNhapDiem()
        {
            InitializeComponent();
        }

        private QLDDataContext db = new QLDDataContext();

        private void frmNhapDiem_Load(object sender, EventArgs e)
        {
            cbKhoa.DataSource = db.Khoas;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            var data = db.DIEM_SELECT();

            var distinctKhoaHoc = data.Select(c => c.KhoaHoc).Distinct().ToList();
            cbKhoas.DataSource = distinctKhoaHoc;

            cbLopHP.DataSource = db.LopHocPhans;
            cbLopHP.DisplayMember = "TenLopHocPhan";
            cbLopHP.ValueMember = "MaLopHocPhan";
            DisplayData();
        }

        private void DisplayData()
        {
            if (cbKhoa.Text == "" || cbKhoas.Text == "" || cbLopHP.Text == "")
            {
                return;
            }

            var data = db.Diem_Select_With_Condition(cbKhoa.SelectedValue.ToString(), cbKhoas.Text, cbLopHP.SelectedValue.ToString())
                         .Select(d => new
                         {
                             MaSinhVien = d.MaSinhVien,
                             HoTen = d.HoTen,
                             MaLop = d.MaLop,
                             DiemQuaTrinh = d.DiemQuaTrinh,
                             DiemThiKTHP = d.DiemThiKTHP,
                             DiemTKHP = d.DiemTKHP,
                             DiemHe4 = d.DiemHe4,
                             DiemHeChu = d.DiemHeChu,
                             LanHoc = d.LanHoc,
                             // Thêm các cột khác mà không chiếu lên DataGridView
                             TrongSoDiemQuaTrinh = d.TrongSoDiemQuaTrinh,
                             TrongSoDiemThiKTHP = d.TrongSoDiemThiKTHP,
                             MaHocPhan = d.MaHocPhan
                         }).ToList();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = data;
            this.dgvDiem.DataSource = bindingSource;

            // Đặt tên cột bằng tiếng Việt
            dgvDiem.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
            dgvDiem.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDiem.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvDiem.Columns["DiemQuaTrinh"].HeaderText = "Điểm Quá Trình";
            dgvDiem.Columns["DiemThiKTHP"].HeaderText = "Điểm Thi KTHP";
            dgvDiem.Columns["DiemTKHP"].HeaderText = "Điểm TKHP";
            dgvDiem.Columns["DiemHe4"].HeaderText = "Điểm Hệ 4";
            dgvDiem.Columns["DiemHeChu"].HeaderText = "Điểm Hệ Chữ";
            dgvDiem.Columns["LanHoc"].HeaderText = "Lần Học";

            // Không đặt tên cột cho các cột không chiếu lên DataGridView
            dgvDiem.Columns["TrongSoDiemQuaTrinh"].Visible = false;
            dgvDiem.Columns["TrongSoDiemThiKTHP"].Visible = false;
            dgvDiem.Columns["MaHocPhan"].Visible = false;

            txtBoxDiemHP.DataBindings.Clear();
            txtBoxDiemHP.DataBindings.Add("Text", bindingSource, "DiemQuaTrinh");
            txtBoxDiemThi.DataBindings.Clear();
            txtBoxDiemThi.DataBindings.Add("Text", bindingSource, "DiemThiKTHP");
            txtBoxDiemTK.DataBindings.Clear();
            txtBoxDiemTK.DataBindings.Add("Text", bindingSource, "DiemTKHP");
            txtBoxDiemHe4.DataBindings.Clear();
            txtBoxDiemHe4.DataBindings.Add("Text", bindingSource, "DiemHe4");
            txtBoxDiemChu.DataBindings.Clear();
            txtBoxDiemChu.DataBindings.Add("Text", bindingSource, "DiemHeChu");
            txtBoxTrongSoDiemQT.DataBindings.Clear();
            txtBoxTrongSoDiemQT.DataBindings.Add("Text", bindingSource, "TrongSoDiemQuaTrinh");
            txtBoxTrongSoDiemThi.DataBindings.Clear();
            txtBoxTrongSoDiemThi.DataBindings.Add("Text", bindingSource, "TrongSoDiemThiKTHP");
            txtBoxMaHP.DataBindings.Clear();
            txtBoxMaHP.DataBindings.Add("Text", bindingSource, "MaHocPhan");
        }


        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cbKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cbLopHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }


        private void txtBoxDiemHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            NhapDiem(sender, e);
        }

        private void txtBoxDiemThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            NhapDiem(sender, e);
        }

        private void NhapDiem(object sender, KeyPressEventArgs e)
        {
            // if press enter
            if (e.KeyChar == (char)13)
            {
                btnSave_Click(sender, e);
                return;
            }
            // can only input number
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }


            txtBoxDiemHP.DataBindings.Clear();
            txtBoxDiemThi.DataBindings.Clear();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                double diemHP, diemThi, diemTK, diemHe4;

                if (!double.TryParse(txtBoxDiemHP.Text, out diemHP) ||
                    !double.TryParse(txtBoxDiemThi.Text, out diemThi) ||
                    !double.TryParse(txtBoxDiemTK.Text, out diemTK) ||
                    !double.TryParse(txtBoxDiemHe4.Text, out diemHe4))
                {
                    MessageBox.Show("Hãy nhập đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                db.DIEM_UPDATE(
                    dgvDiem.CurrentRow.Cells["MaHocPhan"].Value.ToString(),
                    cbLopHP.SelectedValue.ToString(),
                    dgvDiem.CurrentRow.Cells["MaSinhVien"].Value.ToString(),
                    diemHP,
                    diemThi,
                    diemTK,
                    diemHe4,
                    txtBoxDiemChu.Text
                );
                MessageBox.Show("Cập nhật điểm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //reset data
            DisplayData();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void txtBoxDiemHP_TextChanged(object sender, EventArgs e)
        {

            CalculateDiemTK();
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }


        private void CalculateDiemTK()
        {
            string tsDiemQT = dgvDiem.CurrentRow.Cells["TrongSoDiemQuaTrinh"].Value.ToString();
            string tsDiemThi = dgvDiem.CurrentRow.Cells["TrongSoDiemThiKTHP"].Value.ToString();
            if (string.IsNullOrEmpty(txtBoxDiemHP.Text) || string.IsNullOrEmpty(txtBoxDiemThi.Text)
                || string.IsNullOrEmpty(tsDiemQT) || string.IsNullOrEmpty(tsDiemThi))
            {
                txtBoxDiemTK.Text = "_";
                txtBoxDiemChu.Text = "_";
                txtBoxDiemHe4.Text = "_";
                return;
            }
            double diemHP, diemThi, TrongSoDiemQT, TrongSoDiemThi;
            try
            {
                diemHP = double.Parse(txtBoxDiemHP.Text);
                diemThi = double.Parse(txtBoxDiemThi.Text);
                TrongSoDiemQT = double.Parse(tsDiemQT);
                TrongSoDiemThi = double.Parse(tsDiemThi);
            }
            catch (Exception ex)
            {
                return;
            }


            double diemTK = diemHP * TrongSoDiemQT + diemThi * TrongSoDiemThi;
            txtBoxDiemTK.Text = diemTK.ToString();

            txtBoxDiemHe4.Text = diemTK * 4 / 10 + "";

            if (diemTK >= 9.5)
            {
                txtBoxDiemChu.Text = "A+";
            }
            else if (diemTK >= 8.5)
            {
                txtBoxDiemChu.Text = "A";
            }
            else if (diemTK >= 8)
            {
                txtBoxDiemChu.Text = "B+";
            }
            else if (diemTK >= 7)
            {
                txtBoxDiemChu.Text = "B";
            }
            else if (diemTK >= 6)
            {
                txtBoxDiemChu.Text = "C+";
            }
            else if (diemTK >= 5.5)
            {
                txtBoxDiemChu.Text = "C";
            }
            else if (diemTK >= 4.5)
            {
                txtBoxDiemChu.Text = "D+";
            }
            else if (diemTK >= 4)
            {
                txtBoxDiemChu.Text = "D";
            }
            else if (diemTK >= 2)
            {
                txtBoxDiemChu.Text = "F+";
            }
            else
            {
                txtBoxDiemChu.Text = "F";
            }
        }


    }
}
