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
        Table<Khoa> khoas;
        public frmSinhVienLop()
        {
            InitializeComponent();
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd-MM-yyyy";
            khoas= db.Khoas;
        }

        private void frmSinhVienLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyDiemTruongDaiHocDataSet.SinhVien' table. You can move, or remove it, as needed.
            this.cbKhoa.DataSource = khoas;
            this.cbKhoa.DisplayMember = "TenKhoa";
            this.cbKhoa.ValueMember = "MaKhoa";

            this.cbCTDT.DataSource = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString());
            this.cbCTDT.DisplayMember = "TenCTDT";
            this.cbCTDT.ValueMember = "MaCTDT";

            this.cbKhoas.DataSource = db.Lops.Select(l => l.KhoaHoc).Distinct();

            reloadCbLop();
        }


        private void cbCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbLop.DataSource = db.Lops_By(cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.Text);
            reloadCbLop();
        }

        private void cbKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadCbLop();
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadCbLop();
        }

        private void reloadCbLop()
        {
            if (cbKhoa.SelectedValue != null && cbCTDT.SelectedValue != null && !string.IsNullOrEmpty(cbKhoas.Text))
            {
                var lops = db.Lops_By(cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.Text).ToList();
                if (lops != null && lops.Any())
                {
                    BindingSource lopBinding = new BindingSource();
                    lopBinding.DataSource = lops;
                    cbLop.DataSource = lopBinding;
                    cbLop.DisplayMember = "TenLop";
                    cbLop.ValueMember = "MaLop";

                    txtBoxMaLop.DataBindings.Clear();
                    txtBoxTenLop.DataBindings.Clear();
                    txtBoxMaLop.DataBindings.Add("Text", lopBinding, "MaLop");
                    txtBoxTenLop.DataBindings.Add("Text", lopBinding, "TenLop");

                    LoadDGV();
                }
                else
                {
                    ClearLopBindings();
                    dgvSV.DataSource = null;
                }
            }
            else
            {
                ClearLopBindings();
            }
        }

        private void ClearLopBindings()
        {
            cbLop.DataSource = null;
            txtBoxMaLop.DataBindings.Clear();
            txtBoxTenLop.DataBindings.Clear();
            txtBoxMaLop.Text = string.Empty;
            txtBoxTenLop.Text = string.Empty;
        }

        bool adLop = false;

        private void btnAddLop_Click(object sender, EventArgs e)
        {
            adLop = true;
            txtBoxMaLop.Enabled = true;
            cbLop.Enabled = false;
            btnAddLop.Enabled = false;
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
                frmSinhVienLop_Load(sender, e);
            }

            if (lopUpdate == true) {
                lopUpdate = false;
                btnAddLop.Enabled = true;
                db.Lops_Update(txtBoxMaLop.Text, txtBoxTenLop.Text, cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.Text);
                frmSinhVienLop_Load(sender, e);
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
                reloadCbLop();
            }
            else
            {
                MessageBox.Show("Bạn có chắc muốn xóa lớp không, chỉ lớp chưa có sinh viên mới có thể xóa", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                try
                {
                    db.Lops_Delete(txtBoxMaLop.Text);
                    reloadCbLop();
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

        private void dgvSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nhanh các điều kiện không hợp lệ và thoát sớm
            if (e.RowIndex < 0 || e.RowIndex >= dgvSV.Rows.Count) return;

            var row = dgvSV.Rows[e.RowIndex];
            if (row?.Cells[0]?.Value == null) return;

            // Sử dụng BeginInvoke để cập nhật UI không đồng bộ
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    // Cache row để tránh truy cập nhiều lần
                    var cells = row.Cells;

                    // Gom nhóm các thao tác cập nhật UI
                    SuspendLayout();

                    txtBoxMaSV.Text = cells[0].Value?.ToString() ?? string.Empty;
                    txtBoxHoDem.Text = cells[1].Value?.ToString() ?? string.Empty;
                    txtBoxTenSV.Text = cells[2].Value?.ToString() ?? string.Empty;
                    txtBoxQue.Text = cells[4].Value?.ToString() ?? string.Empty;
                    txtBoxDT.Text = cells[5].Value?.ToString() ?? string.Empty;
                    txtBoxEmail.Text = cells[6].Value?.ToString() ?? string.Empty;

                    // Xử lý ngày sinh riêng để tránh lỗi parsing
                    if (cells[3].Value != null && DateTime.TryParse(cells[3].Value.ToString(), out DateTime ngaySinh))
                    {
                        dtpNgaySinh.Value = ngaySinh;
                    }

                    ResumeLayout(true);
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật dữ liệu", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }


        // Thêm thuộc tính để tránh việc xử lý nhiều click liên tiếp
        private DateTime lastClickTime = DateTime.MinValue;
        private const int MIN_CLICK_INTERVAL = 200; // milliseconds

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra thời gian giữa các lần click
            var now = DateTime.Now;
            if ((now - lastClickTime).TotalMilliseconds < MIN_CLICK_INTERVAL)
            {
                return;
            }
            lastClickTime = now;

            // Gọi xử lý click bình thường
            dgvSV_CellContentClick(sender, e);
        }


        bool adSV = false;
        private void btnAddSV_Click(object sender, EventArgs e)
        {
            adSV = true;
            txtBoxMaSV.Enabled = true;
            btnAddSV.Enabled = false;

            // clear textboxes
            txtBoxMaSV.Text = string.Empty;
            txtBoxHoDem.Text = string.Empty;
            txtBoxTenSV.Text = string.Empty;
            txtBoxQue.Text = string.Empty;
            txtBoxDT.Text = string.Empty;
            txtBoxEmail.Text = string.Empty;

            dtpNgaySinh.Text = string.Empty;

            btnXoaSV.Text = "Hủy";
        }
        bool updateSV = false;

        private void btnUpdateSV_Click(object sender, EventArgs e)
        {
            if (adSV == true)
            {
                adSV = false;
                btnAddSV.Enabled = true;
                txtBoxMaSV.Enabled = false;
                db.SinhViens_Insert(txtBoxMaSV.Text, txtBoxHoDem.Text, txtBoxTenSV.Text, txtBoxMaLop.Text, dtpNgaySinh.Value, txtBoxQue.Text, txtBoxDT.Text, txtBoxEmail.Text);
                frmSinhVienLop_Load(sender, e);
            }
            
            if (updateSV == true)
            {
                updateSV = false;
                btnAddSV.Enabled = true;
                db.SinhViens_Update(txtBoxMaSV.Text, txtBoxHoDem.Text, txtBoxTenSV.Text, dtpNgaySinh.Value, txtBoxQue.Text, txtBoxDT.Text, txtBoxEmail.Text);
                frmSinhVienLop_Load(sender, e);
            }
            btnXoaSV.Text = "Xóa sinh viên";
        }

        private void txtBoxHoDem_KeyPress(object sender, KeyPressEventArgs e)
        {
            updateSV = true;
            btnXoaSV.Text = "Hủy";
            btnAddSV.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            if (adSV == true || updateSV == true)
            {
                adSV = false;
                btnAddSV.Enabled = true;
                txtBoxMaSV.Enabled = false;
                // clear textboxes
                txtBoxMaSV.Text = string.Empty;
                txtBoxHoDem.Text = string.Empty;
                txtBoxTenSV.Text = string.Empty;
                txtBoxQue.Text = string.Empty;
                txtBoxDT.Text = string.Empty;
                txtBoxEmail.Text = string.Empty;

                dtpNgaySinh.Text = string.Empty;

                btnXoaSV.Text = "Xóa sinh viên";
            }
        }

        private void dtpNgaySinh_CloseUp(object sender, EventArgs e)
        {
            updateSV = true;
            btnXoaSV.Text = "Hủy";
            btnAddSV.Enabled = false;
        }



        private void LoadDGV()
        {
            var sinhViens = db.SinhViens.Where(sv => sv.MaLop == txtBoxMaLop.Text)
                            .Select(sv => new
                            {
                                MaSinhVien = sv.MaSinhVien,
                                HoDem = sv.HoDem,
                                Ten = sv.Ten,
                                NgaySinh = sv.NgaySinh,
                                DiaChi = sv.DiaChi,
                                DienThoai = sv.DienThoai,
                                Email = sv.Email
                            }).ToList();

            if (sinhViens == null || sinhViens.Count == 0)
            {
                dgvSV.DataSource = null;
            }

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = sinhViens;
            this.dgvSV.DataSource = bindingSource;

            this.dgvSV.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
            this.dgvSV.Columns["HoDem"].HeaderText = "Họ Đệm";
            this.dgvSV.Columns["Ten"].HeaderText = "Tên";
            this.dgvSV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            this.dgvSV.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            this.dgvSV.Columns["DienThoai"].HeaderText = "Điện Thoại";
            this.dgvSV.Columns["Email"].HeaderText = "Email";
        }
    }
}
