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
    public partial class frmKhoaBoMonHP : Form
    {

        QLDDataContext db = new QLDDataContext();
        public frmKhoaBoMonHP()
        {
            InitializeComponent();
        }

        private void frmKhoaBoMonHP_Load(object sender, EventArgs e)
        {
            cbKhoa.DataSource = db.Khoas;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            updateBoMon();

            var list = db.GiangViens;
            if (list == null || list.Count() == 0)
            {
                cbGV.DataSource = null;
                cbGV.Items.Clear();
                return;
            }
            cbGV.DataSource = list;
            cbGV.DisplayMember = "FullNameWithID";
            cbGV.ValueMember = "MaGiangVien";

            updategridiew();
        }

        private void updategridiew()
        {

            dgvHP.DataSource = null;

            if (cbMon.SelectedValue == null)
            {
                dgvHP.DataSource = null;

                txtBoxMaHP.DataBindings.Clear();
                txtBoxMaHP.Text = "";

                txtBoxTenHP.DataBindings.Clear();
                txtBoxTenHP.Text = "";

                txtBoxSoTC.DataBindings.Clear();
                txtBoxSoTC.Text = "";

                txtBoxTrongSoDiemQT.DataBindings.Clear();
                txtBoxTrongSoDiemQT.Text = "";

                txtBoxTrongSoDiemThi.DataBindings.Clear();
                txtBoxTrongSoDiemThi.Text = "";
                return;
            }

            var list = db.HocPhans.Where(m => m.MaBoMon == cbMon.SelectedValue.ToString());

            if (list == null || list.Count() == 0)
            {
                dgvHP.DataSource = null;

                txtBoxMaHP.DataBindings.Clear();
                txtBoxMaHP.Text = "";

                txtBoxTenHP.DataBindings.Clear();
                txtBoxTenHP.Text = "";

                txtBoxSoTC.DataBindings.Clear();
                txtBoxSoTC.Text = "";

                txtBoxTrongSoDiemQT.DataBindings.Clear();
                txtBoxTrongSoDiemQT.Text = "";

                txtBoxTrongSoDiemThi.DataBindings.Clear();
                txtBoxTrongSoDiemThi.Text = "";
                return;
            }

            dgvHP.DataSource = list;

            // Ẩn cột MaBoMon
            if (dgvHP.Columns["BoMon"] != null)
            {
                dgvHP.Columns["BoMon"].Visible = false;
            }

            // Đặt tên các cột thành tiếng Việt
            if (dgvHP.Columns["MaHocPhan"] != null)
            {
                dgvHP.Columns["MaHocPhan"].HeaderText = "Mã Học Phần";
            }
            if (dgvHP.Columns["TenHocPhan"] != null)
            {
                dgvHP.Columns["TenHocPhan"].HeaderText = "Tên Học Phần";
            }
            if (dgvHP.Columns["SoTinChi"] != null)
            {
                dgvHP.Columns["SoTinChi"].HeaderText = "Số Tín Chỉ";
            }
            if (dgvHP.Columns["TrongSoDiemQuaTrinh"] != null)
            {
                dgvHP.Columns["TrongSoDiemQuaTrinh"].HeaderText = "Trọng Số Điểm Quá Trình";
            }
            if (dgvHP.Columns["TrongSoDiemThiKTHP"] != null)
            {
                dgvHP.Columns["TrongSoDiemThiKTHP"].HeaderText = "Trọng Số Điểm Thi KTHP";
            }

            txtBoxMaHP.DataBindings.Clear();
            txtBoxMaHP.DataBindings.Add("Text", dgvHP.DataSource, "MaHocPhan");
            txtBoxTenHP.DataBindings.Clear();
            txtBoxTenHP.DataBindings.Add("Text", dgvHP.DataSource, "TenHocPhan");
            txtBoxSoTC.DataBindings.Clear();
            txtBoxSoTC.DataBindings.Add("Text", dgvHP.DataSource, "SoTinChi");
            txtBoxTrongSoDiemQT.DataBindings.Clear();
            txtBoxTrongSoDiemQT.DataBindings.Add("Text", dgvHP.DataSource, "TrongSoDiemQuaTrinh");
            txtBoxTrongSoDiemThi.DataBindings.Clear();
            txtBoxTrongSoDiemThi.DataBindings.Add("Text", dgvHP.DataSource, "TrongSoDiemThiKTHP");
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateBoMon();
        }

        private void updateBoMon()
        {
            var list = db.BoMons.Where(m => m.MaKhoa == cbKhoa.SelectedValue.ToString());

            if (list == null || list.Count() == 0)
            {
                cbMon.DataSource = null;
                cbMon.Items.Clear();

                txtBoxMaMon.DataBindings.Clear();
                txtBoxMaMon.Text = "";

                txtBoxTenMon.DataBindings.Clear();
                txtBoxTenMon.Text = "";
                updateGiangVien();

                cbGV.SelectedIndex = -1;
                cbGV.Text = "";
                updategridiew();
                return;
            }
            cbMon.DataSource = list;
            cbMon.DisplayMember = "TenBoMon";
            cbMon.ValueMember = "MaBoMon";

            txtBoxMaMon.DataBindings.Clear();
            txtBoxMaMon.DataBindings.Add("Text", cbMon.DataSource, "MaBoMon");
            txtBoxTenMon.DataBindings.Clear();
            txtBoxTenMon.DataBindings.Add("Text", cbMon.DataSource, "TenBoMon");
            updateGiangVien();

        }

        private void updateGiangVien()
        {
            if (!string.IsNullOrEmpty(cbMon?.SelectedValue?.ToString()))
            {
                var boMon = db.BoMons.Where(c => c.MaBoMon == cbMon.SelectedValue.ToString()).FirstOrDefault();
                if (boMon != null)
                {
                    var res = boMon.TruongBoMon;
                    if (res != null)
                    {
                        cbGV.SelectedValue = res;
                    }
                    else
                    {
                        cbGV.SelectedIndex = -1;
                        cbGV.Text = "";
                    }
                }
                else
                {
                    cbGV.SelectedIndex = -1;
                    cbGV.Text = "";
                }
                updategridiew();
            }
        }

        private void cbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateBoMon();
        }

        private void cbMon_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbMon.SelectedValue != null)
            {
                txtBoxMaMon.DataBindings.Clear();
                txtBoxMaMon.DataBindings.Add("Text", cbMon.DataSource, "MaBoMon");
                txtBoxTenMon.DataBindings.Clear();
                txtBoxTenMon.DataBindings.Add("Text", cbMon.DataSource, "TenBoMon");
                updateGiangVien();
            }
        }

        bool adMon = false;
        bool updateMon = false;
        bool click = false;

        private void btnAddMon_Click(object sender, EventArgs e)
        {
            adMon = true;
            updateMon = false;
            btnAddMon.Enabled = false;
            btnRemoveMon.Text = "Hủy";
            btnSaveMon.Enabled = true;

            txtBoxMaMon.Enabled = true;

            txtBoxMaMon.DataBindings.Clear();
            txtBoxTenMon.DataBindings.Clear();
            txtBoxMaMon.Text = "";
            txtBoxTenMon.Text = "";
            cbGV.SelectedIndex = -1;
            cbGV.Text = "";

            cbMon.Enabled = false;

            dgvHP.DataSource = null;

            txtBoxMaHP.DataBindings.Clear();
            txtBoxMaHP.Text = "";

            txtBoxTenHP.DataBindings.Clear();
            txtBoxTenHP.Text = "";

            txtBoxSoTC.DataBindings.Clear();
            txtBoxSoTC.Text = "";

            txtBoxTrongSoDiemQT.DataBindings.Clear();
            txtBoxTrongSoDiemQT.Text = "";

            txtBoxTrongSoDiemThi.DataBindings.Clear();
            txtBoxTrongSoDiemThi.Text = "";

            txtBoxMaHP.Enabled = false;
            txtBoxTenHP.Enabled = false;
            txtBoxSoTC.Enabled = false;
            txtBoxTrongSoDiemQT.Enabled = false;
            txtBoxTrongSoDiemThi.Enabled = false;
            btnAddHP.Enabled = false;
            btnRemoveHP.Enabled = false;
            cbKhoa.Enabled = false;

        }

        private void btnRemoveMon_Click(object sender, EventArgs e)
        {
            if (adMon || updateMon)
            {
                adMon = false;
                updateMon = false;
                btnAddMon.Enabled = true;
                btnRemoveMon.Text = "Xóa";
                btnSaveMon.Enabled = false;

                txtBoxMaMon.Enabled = false;

                txtBoxMaHP.Enabled = true;
                txtBoxTenHP.Enabled = true;
                txtBoxSoTC.Enabled = true;
                txtBoxTrongSoDiemQT.Enabled = true;
                txtBoxTrongSoDiemThi.Enabled = true;
                btnAddHP.Enabled = true;
                btnRemoveHP.Enabled = true;
                cbKhoa.Enabled = true;

                cbKhoa.Enabled = true;
                cbMon.Enabled = true;

                frmKhoaBoMonHP_Load(sender, e);


            }
            else
            {
                if (cbMon.SelectedValue == null)
                {
                    return;
                }
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa bộ môn này không? Bạn chỉ có thể xóa môn nếu nó chưa có học phần nào.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        db.BoMon_Delete(cbMon.SelectedValue.ToString());
                        MessageBox.Show("Xóa bộ môn thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    frmKhoaBoMonHP_Load(sender, e);
                }
            }
        }

        private void btnSaveMon_Click(object sender, EventArgs e)
        {
            if (adMon)
            {
                if (string.IsNullOrEmpty(txtBoxMaMon.Text) || string.IsNullOrEmpty(txtBoxTenMon.Text) || string.IsNullOrEmpty(cbGV.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    db.BoMon_Insert(txtBoxMaMon.Text, txtBoxTenMon.Text, cbKhoa.SelectedValue.ToString(), cbGV.SelectedValue.ToString());
                    MessageBox.Show("Thêm bộ môn thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                adMon = false;
                updateMon = false;
                btnAddMon.Enabled = true;
                btnRemoveMon.Text = "Xóa";
                btnSaveMon.Enabled = false;

                txtBoxMaMon.Enabled = false;

                txtBoxMaHP.Enabled = true;
                txtBoxTenHP.Enabled = true;
                txtBoxSoTC.Enabled = true;
                txtBoxTrongSoDiemQT.Enabled = true;
                txtBoxTrongSoDiemThi.Enabled = true;
                btnAddHP.Enabled = true;
                btnRemoveHP.Enabled = true;
                cbKhoa.Enabled = true;

                cbKhoa.Enabled = true;
                cbMon.Enabled = true;

                frmKhoaBoMonHP_Load(sender, e);

            }
            else if (updateMon)
            {
                if (string.IsNullOrEmpty(txtBoxMaMon.Text) || string.IsNullOrEmpty(txtBoxTenMon.Text) || string.IsNullOrEmpty(cbGV.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    db.BoMon_Update(txtBoxMaMon.Text, txtBoxTenMon.Text, cbKhoa.SelectedValue.ToString(), cbGV.SelectedValue.ToString());
                    MessageBox.Show("Cập nhật bộ môn thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                adMon = false;
                updateMon = false;
                btnAddMon.Enabled = true;
                btnRemoveMon.Text = "Xóa";
                btnSaveMon.Enabled = false;
            }
        }

        private void txtBoxTenMon_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                updateMon = true;
                btnAddMon.Enabled = false;
                btnSaveMon.Enabled = true;
                btnRemoveMon.Text = "Hủy";
            }
        }

        private void cbGV_MouseEnter(object sender, EventArgs e)
        {
            click = true;

        }

        private void cbGV_MouseLeave(object sender, EventArgs e)
        {
            click = false;
        }

        private void cbGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (click == true)
            {
                updateMon = true;
                btnAddMon.Enabled = false;
                btnSaveMon.Enabled = true;
                btnRemoveMon.Text = "Hủy";
                
            }
        }

        bool adHP = false;
        bool updateHP = false;

        private void btnAddHP_Click(object sender, EventArgs e)
        {
            adHP = true;
            updateHP = false;
            btnAddHP.Enabled = false;
            btnRemoveHP.Text = "Hủy";
            btnSaveHP.Enabled = true;

            txtBoxMaHP.Enabled = true;

            txtBoxMaHP.DataBindings.Clear();
            txtBoxTenHP.DataBindings.Clear();
            txtBoxSoTC.DataBindings.Clear();
            txtBoxTrongSoDiemQT.DataBindings.Clear();
            txtBoxTrongSoDiemThi.DataBindings.Clear();
            txtBoxMaHP.Text = "";
            txtBoxTenHP.Text = "";
            txtBoxSoTC.Text = "";
            txtBoxTrongSoDiemQT.Text = "";
            txtBoxTrongSoDiemThi.Text = "";

            dgvHP.Enabled = false;
            cbMon.Enabled = false;
            cbKhoa.Enabled = false;

            txtBoxMaMon.Enabled = false;
            txtBoxTenMon.Enabled = false;
            cbGV.Enabled = false;
            btnAddMon.Enabled = false;
            btnRemoveMon.Enabled = false;


        }

        private void btnRemoveHP_Click(object sender, EventArgs e)
        {
            if (adHP || updateHP)
            {
                toggleInput();
                RefreshDataContext();
                updategridiew();
            }
            else
            {
                if (dgvHP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn học phần cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa học những phần này không? Chỉ có thể xóa học phần chưa có lớp học phần", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    foreach (DataGridViewRow row in dgvHP.SelectedRows)
                    {
                        string maHP = row.Cells["MaHocPhan"].Value.ToString();
                        try
                        {
                            db.HocPhan_Delete(maHP);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    toggleInput();
                    RefreshDataContext();
                    updategridiew();
                }
            }
        }


        private void toggleInput()
        {
            adHP = false;
            updateHP = false;
            btnAddHP.Enabled = true;
            btnRemoveHP.Text = "Xóa";
            btnSaveHP.Enabled = false;

            txtBoxMaHP.Enabled = false;


            dgvHP.Enabled = true;
            cbMon.Enabled = true;
            cbKhoa.Enabled = true;

            txtBoxMaMon.Enabled = false;
            txtBoxTenMon.Enabled = true;
            cbGV.Enabled = true;
            btnAddMon.Enabled = true;
            btnRemoveMon.Enabled = true;
        }

        private void btnSaveHP_Click(object sender, EventArgs e)
        {
            if (adHP)
            {
                if (string.IsNullOrEmpty(txtBoxMaHP.Text) || string.IsNullOrEmpty(txtBoxTenHP.Text) || string.IsNullOrEmpty(txtBoxSoTC.Text) || string.IsNullOrEmpty(txtBoxTrongSoDiemQT.Text) || string.IsNullOrEmpty(txtBoxTrongSoDiemThi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    decimal trongSoDiemQT = Math.Round(decimal.Parse(txtBoxTrongSoDiemQT.Text), 1);
                    decimal trongSoDiemThi = Math.Round(decimal.Parse(txtBoxTrongSoDiemThi.Text), 1);

                    db.HocPhan_Insert(txtBoxMaHP.Text, txtBoxTenHP.Text, cbMon.SelectedValue.ToString(), int.Parse(txtBoxSoTC.Text), (double?)trongSoDiemQT, (double?)trongSoDiemThi);
                    MessageBox.Show("Thêm học phần thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                toggleInput();
                RefreshDataContext();
                updategridiew();
            }
            else if (updateHP)
            {
                if (string.IsNullOrEmpty(txtBoxMaHP.Text) || string.IsNullOrEmpty(txtBoxTenHP.Text) || string.IsNullOrEmpty(txtBoxSoTC.Text) || string.IsNullOrEmpty(txtBoxTrongSoDiemQT.Text) || string.IsNullOrEmpty(txtBoxTrongSoDiemThi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    decimal trongSoDiemQT = Math.Round(decimal.Parse(txtBoxTrongSoDiemQT.Text), 1);
                    decimal trongSoDiemThi = Math.Round(decimal.Parse(txtBoxTrongSoDiemThi.Text), 1);

                    db.HocPhan_Update(txtBoxMaHP.Text, txtBoxTenHP.Text, cbMon.SelectedValue.ToString(), int.Parse(txtBoxSoTC.Text), (double?)trongSoDiemQT, (double?)trongSoDiemThi);
                    MessageBox.Show("Cập nhật học phần thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                toggleInput();
                RefreshDataContext();
                updategridiew();
            }
        }


        private void RefreshDataContext()
        {
            db = new QLDDataContext();
        }

        private void txtBoxTenHP_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                updateHP = true;
                btnAddHP.Enabled = false;
                btnSaveHP.Enabled = true;
                btnRemoveHP.Text = "Hủy";

                txtBoxMaHP.DataBindings.Clear();
                txtBoxTenHP.DataBindings.Clear();
                txtBoxSoTC.DataBindings.Clear();
                txtBoxTrongSoDiemQT.DataBindings.Clear();
                txtBoxTrongSoDiemThi.DataBindings.Clear();
            }
        }

        private void txtBoxTrongSoDiemQT_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxTrongSoDiemQT.Text))
            {
                return;
            }

            try
            {
                decimal trongSoDiemQT = Math.Round(decimal.Parse(txtBoxTrongSoDiemQT.Text), 1);
                if (trongSoDiemQT < 0 || trongSoDiemQT > 1)
                {
                    MessageBox.Show("Trọng số điểm quá trình phải nằm trong khoảng từ 0 đến 1", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBoxTrongSoDiemQT.Text = "";
                }
                else
                {
                    txtBoxTrongSoDiemThi.Text = (1 - trongSoDiemQT).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Trọng số điểm quá trình phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxTrongSoDiemQT.Text = "";
            }
        }

        private void txtBoxTrongSoDiemThi_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxTrongSoDiemThi.Text))
            {
                return;
            }

            try
            {
                decimal trongSoDiemQT = Math.Round(decimal.Parse(txtBoxTrongSoDiemThi.Text), 1);
                if (trongSoDiemQT < 0 || trongSoDiemQT > 1)
                {
                    MessageBox.Show("Trọng số điểm quá trình phải nằm trong khoảng từ 0 đến 1", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBoxTrongSoDiemThi.Text = "";
                }
                else
                {
                    txtBoxTrongSoDiemQT.Text = (1 - trongSoDiemQT).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Trọng số điểm quá trình phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxTrongSoDiemThi.Text = "";
            }
        }

        private void CantPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtBoxTrongSoDiemQT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtBoxSoTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
