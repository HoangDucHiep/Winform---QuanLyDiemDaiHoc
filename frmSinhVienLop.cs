using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemDaiHoc
{
    public partial class frmSinhVienLop : Form
    {
        private QLDDataContext db = new QLDDataContext();
        public frmSinhVienLop()
        {
            InitializeComponent();
        }

        private void frmSinhVienLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyDiemTruongDaiHocDataSet.SinhVien' table. You can move, or remove it, as needed.
            var khoas = db.Khoas.ToList();
            this.cbKhoa.DataSource = khoas;
            this.cbKhoa.DisplayMember = "TenKhoa";
            this.cbKhoa.ValueMember = "MaKhoa";

            updateCTDT();
        }

        private void updateCTDT()
        {
            var ctdt = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString()).ToList();
            if (ctdt.Count() == 0)
            {
                cbCTDT.DataSource = null;
                cbCTDT.Text = string.Empty;
            }
            else
            {
                cbCTDT.DataSource = ctdt;

                this.cbCTDT.DataSource = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString());
                this.cbCTDT.DisplayMember = "TenCTDT";
                this.cbCTDT.ValueMember = "MaCTDT";
            }
            updateKhoaHoc();
            updateLops();
        }

        private void updateKhoaHoc()
        {
            if (cbCTDT.SelectedValue == null)
            {
                cbKhoas.DataSource = null;
                cbKhoas.Text = string.Empty;
            }
            else
            {
                var khoas = db.Lops.Where(s => s.MaKhoa == cbKhoa.SelectedValue.ToString() && s.MaCTDT == cbCTDT.SelectedValue.ToString()).Select(l => l.KhoaHoc).Distinct();

                if (khoas.Count() == 0)
                {
                    cbKhoas.DataSource = null;
                    cbKhoas.Text = string.Empty;
                }
                else
                {
                    cbKhoas.DataSource = khoas;
                }
            }
            updateLops();
        }

        private void updateLops()
        {
            if (cbKhoa.SelectedValue == null || cbCTDT.SelectedValue == null || string.IsNullOrEmpty(cbKhoas.Text))
            {
                cbLop.DataSource = null;
                cbLop.Text = string.Empty;
                return;
            }
            string khoa = cbKhoa.SelectedValue.ToString();
            string ctdt = cbCTDT.SelectedValue.ToString();
            string khoahoc = cbKhoas.Text;

            if (string.IsNullOrEmpty(khoa) || string.IsNullOrEmpty(ctdt) || string.IsNullOrEmpty(khoahoc))
            {
                cbLop.DataSource = null;
                cbLop.Text = "";
            }
            else
            {
                var lops = db.Lops.Where(l => l.MaKhoa == khoa && l.MaCTDT == ctdt && l.KhoaHoc == khoahoc).ToList();
                if (lops.Count() == 0)
                {
                    cbLop.DataSource = null;
                    cbLop.Text = "";
                }
                else
                {
                    cbLop.DataSource = lops;
                    cbLop.DisplayMember = "TenLop";
                    cbLop.ValueMember = "MaLop";
                }
            }
        }

        private void cbCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateKhoaHoc();
        }

        private void cbKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLops();
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCTDT();
        }

        bool adLop = false;

        private void btnAddLop_Click(object sender, EventArgs e)
        {
            adLop = true;
            txtBoxMaLop.Enabled = true;
            cbLop.Enabled = false;
            btnAddLop.Enabled = false;
            cbCTDT.Enabled = false;
            cbKhoa.Enabled = false;
            btnXoaHuy.Text = "Hủy";

            txtBoxMaLop.Text = string.Empty;
            txtBoxTenLop.Text = string.Empty;
        }

        bool lopUpdate = false;
        private void btnLuuLop_Click(object sender, EventArgs e)
        {
            if (adLop == true)
            {
                db.Lops_Insert(txtBoxMaLop.Text, txtBoxTenLop.Text, cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.Text);
                adLop = false;
                txtBoxMaLop.Enabled = false;
                cbLop.Enabled = true;
                btnAddLop.Enabled = true;

                cbCTDT.Enabled = true;
                cbKhoa.Enabled = true;
                updateKhoaHoc();
                updateLops();
            }

            if (lopUpdate == true)
            {
                lopUpdate = false;
                btnAddLop.Enabled = true;
                db.Lops_Update(txtBoxMaLop.Text, txtBoxTenLop.Text, cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.Text);

                updateKhoaHoc();
                updateLops();
            }
            btnXoaHuy.Text = "Xóa lớp";
        }

        private void btnXoaHuy_Click(object sender, EventArgs e)
        {
            if (adLop == true || lopUpdate == true)
            {
                adLop = false;
                txtBoxMaLop.Enabled = false;
                cbLop.Enabled = true;
                btnAddLop.Enabled = true;
                btnXoaHuy.Text = "Xóa lớp";
            }
            else
            {
                MessageBox.Show("Bạn có chắc muốn xóa lớp không, chỉ lớp chưa có sinh viên mới có thể xóa", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                try
                {
                    db.Lops_Delete(txtBoxMaLop.Text);
                    MessageBox.Show("Xóa lớp thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa lớp này, lớp này đã có sinh viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cantTypeIn(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtBoxTenLop_KeyPress(object sender, KeyPressEventArgs e)
        {
            lopUpdate = true;
            btnAddLop.Enabled = false;
            btnXoaHuy.Text = "Hủy";
        }

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLop.SelectedValue == null)
            {
                txtBoxMaLop.Text = string.Empty;
                txtBoxTenLop.Text = string.Empty;
            }
            else
            {
                txtBoxMaLop.Text = cbLop.SelectedValue.ToString();
                txtBoxTenLop.Text = cbLop.Text;
            }
            updatedgv();
        }

        private void updatedgv()
        {
            if (cbLop.SelectedValue == null)
            {
                dgvSV.DataSource = null;
                return;
            }

            string maLop = cbLop.SelectedValue.ToString();
            var svList = db.SinhViens.Where(s => s.MaLop == maLop).ToList();

            var bindingList = new BindingList<SinhVien>(svList);
            var bindingSource = new BindingSource(bindingList, null);

            dgvSV.DataSource = bindingSource;

            dgvSV.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
            dgvSV.Columns["HoDem"].HeaderText = "Họ Đệm";
            dgvSV.Columns["Ten"].HeaderText = "Tên";
            dgvSV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvSV.Columns["DiaChi"].HeaderText = "Quê quán";
            dgvSV.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvSV.Columns["Email"].HeaderText = "Email";

            dgvSV.Columns["MaLop"].Visible = false;
            dgvSV.Columns["Lop"].Visible = false;

            // Set the date format for the "NgaySinh" column
            dgvSV.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvSV.AllowUserToAddRows = true; // Allow adding new rows
        }


        private void dgvSV_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Xử lý sự kiện khi người dùng thêm hàng mới
            var newRow = e.Row;
            if (newRow != null && newRow.Index >= 0 && newRow.Index < dgvSV.Rows.Count - 1)
            {
                var sinhVien = new SinhVien
                {
                    MaSinhVien = newRow.Cells["MaSinhVien"].Value?.ToString(),
                    HoDem = newRow.Cells["HoDem"].Value?.ToString(),
                    Ten = newRow.Cells["Ten"].Value?.ToString(),
                    NgaySinh = DateTime.TryParse(newRow.Cells["NgaySinh"].Value?.ToString(), out DateTime ngaySinh) ? ngaySinh : (DateTime?)null,
                    DiaChi = newRow.Cells["DiaChi"].Value?.ToString(),
                    DienThoai = newRow.Cells["DienThoai"].Value?.ToString(),
                    Email = newRow.Cells["Email"].Value?.ToString(),
                    MaLop = cbLop.SelectedValue.ToString()
                };

                db.SinhViens.InsertOnSubmit(sinhVien);
                db.SubmitChanges();
            }
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            // Cập nhật dữ liệu từ DataGridView vào cơ sở dữ liệu
            foreach (DataGridViewRow row in dgvSV.Rows)
            {
                if (row.IsNewRow) continue;

                string maSV = row.Cells["MaSinhVien"].Value?.ToString();
                var sinhVien = db.SinhViens.SingleOrDefault(s => s.MaSinhVien == maSV);
                if (sinhVien != null)
                {
                    sinhVien.HoDem = row.Cells["HoDem"].Value?.ToString();
                    sinhVien.Ten = row.Cells["Ten"].Value?.ToString();
                    sinhVien.NgaySinh = DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ngaySinh) ? ngaySinh : (DateTime?)null;
                    sinhVien.DiaChi = row.Cells["DiaChi"].Value?.ToString();
                    sinhVien.DienThoai = row.Cells["DienThoai"].Value?.ToString();
                    sinhVien.Email = row.Cells["Email"].Value?.ToString();
                }
            }
            db.SubmitChanges();
            MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
