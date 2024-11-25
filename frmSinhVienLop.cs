using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyDiemDaiHoc
{
    public partial class frmSinhVienLop : Form
    {
        private QLDDataContext db = new QLDDataContext();
        private CultureInfo culture = new CultureInfo("vi-VN");
        private List<SinhVien> sinhVienToDelete = new List<SinhVien>();

        public frmSinhVienLop()
        {
            InitializeComponent();
            // Đăng ký các sự kiện
            dgvSV.UserAddedRow += dgvSV_UserAddedRow;
            dgvSV.DataError += dgvSV_DataError;
            dgvSV.CellParsing += dgvSV_CellParsing;
            dgvSV.CellFormatting += dgvSV_CellFormatting;
            dgvSV.UserDeletingRow += dgvSV_UserDeletingRow;
            dgvSV.DefaultValuesNeeded += dgvSV_DefaultValuesNeeded; // Thêm sự kiện này
            dgvSV.CellValueChanged += dgvSV_CellValueChanged; // Thêm sự kiện này
            dgvSV.CellValidating += dgvSV_CellValidating; // Thêm sự kiện này
        }


        private void dgvSV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var columnName = dgvSV.Columns[e.ColumnIndex].Name;
            var cellValue = e.FormattedValue?.ToString().Trim();

            // Kiểm tra các cột bắt buộc
            if (columnName == "HoDem" || columnName == "Ten" || columnName == "NgaySinh" || columnName == "DiaChi" || columnName == "DienThoai" || columnName == "Email")
            {
                if (string.IsNullOrEmpty(cellValue))
                {
                    MessageBox.Show(Text = "Dữ liệu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);        
                    e.Cancel = true;
                }
            }

            // Kiểm tra cột số điện thoại chỉ chứa số
            if (columnName == "DienThoai")
            {
                if (!long.TryParse(cellValue, out _))
                {
                    MessageBox.Show("Số điện thoại chỉ chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void dgvSV_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["MaSinhVien"].Value = Utilities.IdGenerator.GetSvID();
        }

        private void dgvSV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvSV.Columns[e.ColumnIndex].Name == "HoDem" || dgvSV.Columns[e.ColumnIndex].Name == "Ten"))
            {
                var row = dgvSV.Rows[e.RowIndex];
                string hoDem = row.Cells["HoDem"].Value?.ToString();
                string ten = row.Cells["Ten"].Value?.ToString();
                string maSinhVien = row.Cells["MaSinhVien"].Value?.ToString();

                if (!string.IsNullOrEmpty(hoDem) && !string.IsNullOrEmpty(ten) && !string.IsNullOrEmpty(maSinhVien))
                {
                    string fullName = $"{hoDem} {ten}";
                    string email = Utilities.EmailGenerator.GenerateEmail(fullName, maSinhVien, "utc");
                    row.Cells["Email"].Value = email;
                }
            }
        }

        private void frmSinhVienLop_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu vào combobox Khoa
            var khoas = db.Khoas.ToList();
            cbKhoa.DataSource = khoas;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            updateCTDT();
        }

        private void updateCTDT()
        {
            var ctdt = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString()).ToList();
            if (ctdt.Count == 0)
            {
                cbCTDT.DataSource = null;
                cbCTDT.Text = string.Empty;
            }
            else
            {
                cbCTDT.DataSource = ctdt;
                cbCTDT.DisplayMember = "TenCTDT";
                cbCTDT.ValueMember = "MaCTDT";
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
                var khoas = db.Lops
                    .Where(s => s.MaKhoa == cbKhoa.SelectedValue.ToString() && s.MaCTDT == cbCTDT.SelectedValue.ToString())
                    .Select(l => l.KhoaHoc)
                    .Distinct()
                    .ToList();

                if (khoas.Count == 0)
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

            var lops = db.Lops.Where(l => l.MaKhoa == khoa && l.MaCTDT == ctdt && l.KhoaHoc == khoahoc).ToList();
            if (lops.Count == 0)
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
            if (adLop)
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

            if (lopUpdate)
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
            if (adLop || lopUpdate)
            {
                adLop = false;
                lopUpdate = false;
                txtBoxMaLop.Enabled = false;
                cbLop.Enabled = true;
                btnAddLop.Enabled = true;
                btnXoaHuy.Text = "Xóa lớp";
            }
            else
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa lớp không? Chỉ lớp chưa có sinh viên mới có thể xóa.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        db.Lops_Delete(txtBoxMaLop.Text);
                        MessageBox.Show("Xóa lớp thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateKhoaHoc();
                        updateLops();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xóa lớp này, lớp này đã có sinh viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            // Xử lý khi người dùng click vào ô trong DataGridView (nếu cần)
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

            // Định dạng ngày tháng cho cột "NgaySinh"
            dgvSV.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvSV.Columns["NgaySinh"].DefaultCellStyle.FormatProvider = culture;

            dgvSV.AllowUserToAddRows = true; // Cho phép thêm hàng mới

            // Đặt cột "MaSinhVien" thành ReadOnly
            dgvSV.Columns["MaSinhVien"].ReadOnly = true;
            dgvSV.Columns["Email"].ReadOnly = true;
        }

        private void dgvSV_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // Không cần xử lý thao tác thêm mới ở đây vì chúng ta sẽ xử lý trong btnUpdateData_Click
        }

        private void dgvSV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Hiển thị MessageBox xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không? Bạn chỉ có thể xóa các sinh viên chưa tham gia lớp học phần nào", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy bỏ việc xóa nếu người dùng chọn "No"
                return;
            }

            // Đánh dấu sinh viên để xóa
            string maSV = e.Row.Cells["MaSinhVien"].Value?.ToString();
            var sinhVien = db.SinhViens.SingleOrDefault(s => s.MaSinhVien == maSV);
            if (sinhVien != null)
            {
                sinhVienToDelete.Add(sinhVien);
            }
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa sinh viên đã được đánh dấu
                foreach (var sinhVien in sinhVienToDelete)
                {
                    // Xóa tài khoản tương ứng
                    var tk = db.TKs.SingleOrDefault(t => t.MaTK == "TK_" + sinhVien.MaSinhVien);
                    if (tk != null)
                    {
                        db.TKs.DeleteOnSubmit(tk);
                    }
                    db.SinhViens.DeleteOnSubmit(sinhVien);
                }
                sinhVienToDelete.Clear();

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

                        string ngaySinhStr = row.Cells["NgaySinh"].Value?.ToString().Trim();

                        sinhVien.NgaySinh = DateTime.Parse(ngaySinhStr);

                        sinhVien.DiaChi = row.Cells["DiaChi"].Value?.ToString();
                        sinhVien.DienThoai = row.Cells["DienThoai"].Value?.ToString();
                        sinhVien.Email = row.Cells["Email"].Value?.ToString();

                        var newTK = new TK
                        {
                            MaTK = "TK_" + maSV,
                            Email = sinhVien.Email,
                            Password = Utilities.PasswordHasher.HashPassword(maSV),
                            MaRole = "sv"
                        };
                        db.TKs.InsertOnSubmit(newTK);
                    }
                    else
                    {
                        // Thêm sinh viên mới
                        var newSinhVien = new SinhVien
                        {
                            MaSinhVien = maSV,
                            HoDem = row.Cells["HoDem"].Value?.ToString(),
                            Ten = row.Cells["Ten"].Value?.ToString(),
                            MaLop = cbLop.SelectedValue.ToString(),
                            DiaChi = row.Cells["DiaChi"].Value?.ToString(),
                            DienThoai = row.Cells["DienThoai"].Value?.ToString(),
                            Email = row.Cells["Email"].Value?.ToString(),
                        };

                        string ngaySinhStr = row.Cells["NgaySinh"].Value?.ToString().Trim();
                        newSinhVien.NgaySinh = DateTime.Parse(ngaySinhStr);

                        db.SinhViens.InsertOnSubmit(newSinhVien);

                        var newTK = new TK
                        {
                            MaTK = "TK_" + maSV,
                            Email = newSinhVien.Email,
                            Password = Utilities.PasswordHasher.HashPassword(maSV),
                            MaRole = "sv"
                        };
                        db.TKs.InsertOnSubmit(newTK);
                    }
                }
                db.SubmitChanges();
                MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updatedgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý lỗi dữ liệu trong DataGridView
        private void dgvSV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = false;
        }

        // Sự kiện CellParsing để phân tích dữ liệu nhập vào
        private void dgvSV_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (dgvSV.Columns[e.ColumnIndex].Name == "NgaySinh" && e.Value != null)
            {
                string ngaySinhStr = e.Value.ToString().Trim();

                if (DateTime.TryParseExact(ngaySinhStr, "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    e.Value = ngaySinh;
                    e.ParsingApplied = true;
                }
                else
                {
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.ParsingApplied = false;
                }
            }
        }

        // Sự kiện CellFormatting để định dạng dữ liệu hiển thị
        private void dgvSV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSV.Columns[e.ColumnIndex].Name == "NgaySinh" && e.Value != null)
            {
                if (e.Value is DateTime dateValue)
                {
                    e.Value = dateValue.ToString("dd/MM/yyyy", culture);
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // ignore changes
            db = new QLDDataContext();
            updatedgv();

        }
    }
}
