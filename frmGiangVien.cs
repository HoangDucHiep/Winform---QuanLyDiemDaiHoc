using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemDaiHoc
{
    public partial class frmGiangVien : Form
    {

        QLDDataContext db = new QLDDataContext();

        string initialDirectory = Application.StartupPath + "\\Images\\";
        private string loadedImagePath = "";

        public frmGiangVien()
        {
            InitializeComponent();
        }



        private void frmGiangVien_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();

        }

        private void RefreshDataGridView()
        {
            dgvGV.DataBindings.Clear();
            dgvGV.DataSource = db.GiangViens;

            dgvGV.Columns["FullNameWithId"].Visible = false;
            dgvGV.Columns["Anh"].Visible = false;

            dgvGV.Columns["MaGiangVien"].HeaderText = "Mã giảng viên";
            dgvGV.Columns["HoDem"].HeaderText = "Họ đệm";
            dgvGV.Columns["Ten"].HeaderText = "Tên";
            dgvGV.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvGV.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvGV.Columns["HocHam"].HeaderText = "Học hàm";
            dgvGV.Columns["HocVi"].HeaderText = "Học vị";
            dgvGV.Columns["ChucDanh"].HeaderText = "Chức danh";
            dgvGV.Columns["DienThoai"].HeaderText = "Điện thoại";
            dgvGV.Columns["Email"].HeaderText = "Email";
        }

        private bool isUpdatingFromGrid = false;

        private void dgvGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                isUpdatingFromGrid = true;

                txtBoxMaGV.Text = dgvGV.Rows[e.RowIndex].Cells["MaGiangVien"].Value?.ToString() ?? string.Empty;
                txtBoxHoDem.Text = dgvGV.Rows[e.RowIndex].Cells["HoDem"].Value?.ToString() ?? string.Empty;
                txtBoxTen.Text = dgvGV.Rows[e.RowIndex].Cells["Ten"].Value?.ToString() ?? string.Empty;
                dtpNgaySinh.Value = dgvGV.Rows[e.RowIndex].Cells["NgaySinh"].Value != null ? (DateTime)dgvGV.Rows[e.RowIndex].Cells["NgaySinh"].Value : DateTime.Now;
                cbGioiTinh.Text = dgvGV.Rows[e.RowIndex].Cells["GioiTinh"].Value?.ToString() ?? string.Empty;
                cbHocHam.Text = dgvGV.Rows[e.RowIndex].Cells["HocHam"].Value?.ToString() ?? string.Empty;
                cbHocVi.Text = dgvGV.Rows[e.RowIndex].Cells["HocVi"].Value?.ToString() ?? string.Empty;
                cbChucDanh.Text = dgvGV.Rows[e.RowIndex].Cells["ChucDanh"].Value?.ToString() ?? string.Empty;
                txtBoxSDT.Text = dgvGV.Rows[e.RowIndex].Cells["DienThoai"].Value?.ToString() ?? string.Empty;
                txtBoxEmail.Text = dgvGV.Rows[e.RowIndex].Cells["Email"].Value?.ToString() ?? string.Empty;

                string imagePath = string.IsNullOrEmpty(dgvGV.Rows[e.RowIndex].Cells["Anh"].Value?.ToString()) ? "default_avatar.png" : dgvGV.Rows[e.RowIndex].Cells["Anh"].Value?.ToString();

                pictureBox1.Image = Image.FromFile(initialDirectory + imagePath);

                isUpdatingFromGrid = false;
                loadedImagePath = imagePath;
            }
        }


        bool adGV = false;
        bool updateGV = false;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            adGV = true;
            txtBoxMaGV.Text = Utilities.IdGenerator.GetGvID();
            txtBoxHoDem.Text = "";
            txtBoxTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            cbGioiTinh.Text = "";
            cbHocHam.Text = "";
            cbHocVi.Text = "";
            cbChucDanh.Text = "";
            txtBoxSDT.Text = "";
            txtBoxEmail.Text = "";

            btnDelete.Text = "Hủy";
            btnSave.Enabled = true;

            btnAdd.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (adGV || updateGV)
            {
                isUpdatingFromGrid = true;
                adGV = false;
                updateGV = false;

                txtBoxMaGV.Text = "";
                txtBoxHoDem.Text = "";
                txtBoxTen.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                cbGioiTinh.Text = "";
                cbHocHam.Text = "";
                cbHocVi.Text = "";
                cbChucDanh.Text = "";
                txtBoxSDT.Text = "";
                txtBoxEmail.Text = "";
                btnSave.Enabled = false;

                btnDelete.Text = "Xóa";

                pictureBox1.Image = Image.FromFile(initialDirectory + "default_avatar.png");

                btnAdd.Enabled = true;
                isUpdatingFromGrid = false;



            }
            else
            {
                string maGV = txtBoxMaGV.Text;

                if (string.IsNullOrEmpty(maGV))
                {
                    MessageBox.Show("Vui lòng chọn giảng viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có muốn xóa giảng viên này? Chỉ giảng viên chưa có lớp học phần hoặc bộ môn mới có thể xóa", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var gv = db.GiangViens.Single(g => g.MaGiangVien == maGV);
                        db.GiangViens.DeleteOnSubmit(gv);
                        db.SubmitChanges();
                        MessageBox.Show("Xóa giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa giảng viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                RefreshDataContext();
                RefreshDataGridView();

                if (dgvGV.Rows.Count > 0 && dgvGV.Rows[0].Cells[0].Visible)
                {
                    dgvGV.CurrentCell = dgvGV.Rows[0].Cells[0];
                }
            }
        }


        private void activeUpdateGV()
        {
            updateGV = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnDelete.Text = "Hủy";
        }

        private void txtBoxHoDem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                activeUpdateGV();
            }
        }

        private void cbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isUpdatingFromGrid)
            {
                activeUpdateGV();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (adGV)
            {
                var maGV = txtBoxMaGV.Text;
                var hoDem = txtBoxHoDem.Text;
                var ten = txtBoxTen.Text;
                var ngaySinh = dtpNgaySinh.Value;
                var gioiTinh = cbGioiTinh.Text;
                var hocHam = cbHocHam.Text;
                var hocVi = cbHocVi.Text;
                var chucDanh = cbChucDanh.Text;
                var sdt = txtBoxSDT.Text;
                var email = txtBoxEmail.Text;

                if (string.IsNullOrEmpty(maGV) || string.IsNullOrEmpty(hoDem) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(hocHam) || string.IsNullOrEmpty(hocVi) || string.IsNullOrEmpty(chucDanh) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    db.GiangViens.InsertOnSubmit(new GiangVien
                    {
                        MaGiangVien = maGV,
                        HoDem = hoDem,
                        Ten = ten,
                        NgaySinh = ngaySinh,
                        GioiTinh = gioiTinh,
                        HocHam = hocHam,
                        HocVi = hocVi,
                        ChucDanh = chucDanh,
                        DienThoai = sdt,
                        Email = email,
                        Anh = loadedImagePath
                    });

                    db.TKs.InsertOnSubmit(new TK
                    {
                        MaTK = "TK_" + maGV,
                        Password = Utilities.PasswordHasher.HashPassword(maGV),
                        Email = email,
                        MaRole = "gv"
                    });

                    db.SubmitChanges();

                    MessageBox.Show("Thêm giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm giảng viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (updateGV)
            {
                var maGV = txtBoxMaGV.Text;
                var hoDem = txtBoxHoDem.Text;
                var ten = txtBoxTen.Text;
                var ngaySinh = dtpNgaySinh.Value;
                var gioiTinh = cbGioiTinh.Text;
                var hocHam = cbHocHam.Text;
                var hocVi = cbHocVi.Text;
                var chucDanh = cbChucDanh.Text;
                var sdt = txtBoxSDT.Text;
                var email = txtBoxEmail.Text;
                if (string.IsNullOrEmpty(maGV) || string.IsNullOrEmpty(hoDem) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(hocHam) || string.IsNullOrEmpty(hocVi) || string.IsNullOrEmpty(chucDanh) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    var gv = db.GiangViens.Single(g => g.MaGiangVien == maGV);
                    gv.HoDem = hoDem;
                    gv.Ten = ten;
                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;
                    gv.HocHam = hocHam;
                    gv.HocVi = hocVi;
                    gv.ChucDanh = chucDanh;
                    gv.DienThoai = sdt;
                    gv.Email = email;
                    gv.Anh = loadedImagePath;


                    db.SubmitChanges();

                    MessageBox.Show("Cập nhật giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cập nhật giảng viên thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            RefreshDataContext();
            RefreshDataGridView();
            btnDelete_Click(sender, e);
        }
        private void createEmail()
        {
            string hoDem = txtBoxHoDem.Text;
            string ten = txtBoxTen.Text;
            string id = txtBoxMaGV.Text;
            string domain = "edu.vn";

            if (!string.IsNullOrEmpty(hoDem) && !string.IsNullOrEmpty(ten) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(domain))
            {
                string fullName = txtBoxHoDem.Text + " " + txtBoxTen.Text;
                txtBoxEmail.Text = Utilities.EmailGenerator.GenerateEmail(fullName, id, domain);
            }

        }

        private void txtBoxMaGV_TextChanged(object sender, EventArgs e)
        {
            createEmail();
        }

        private void txtBoxHoDem_TextChanged(object sender, EventArgs e)
        {
            createEmail();
        }

        private void txtBoxTen_TextChanged(object sender, EventArgs e)
        {
            createEmail();
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {
            if (!isUpdatingFromGrid)
            {
                activeUpdateGV();
            }
        }

        private void RefreshDataContext()
        {
            db = new QLDDataContext();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.Title = "Chọn ảnh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                string initialDirectory = openFileDialog.InitialDirectory;
                string fileName = Path.GetFileName(selectedFilePath);
                string destinationPath = Path.Combine(initialDirectory, fileName);


                if (!selectedFilePath.StartsWith(initialDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(selectedFilePath, destinationPath, true);
                }

                pictureBox1.Image = new Bitmap(destinationPath);

                loadedImagePath = fileName;
                updateGV = true;

                activeUpdateGV();
            }
        }

        private void txtBoxSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    activeUpdateGV();
                }
            }
        }
    }

}
