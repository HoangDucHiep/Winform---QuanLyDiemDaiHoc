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
    public partial class frmKhoa_CTDT : Form
    {

        QLDDataContext db = new QLDDataContext();

        public frmKhoa_CTDT()
        {
            InitializeComponent();
        }

        private void frmKhoa_CTDT_Load(object sender, EventArgs e)
        {
            var khoas = db.Khoas.ToList();

            if (khoas == null || khoas.Count == 0)
            {
                cbKhoa.DataSource = null;
            }
            else
            {
                cbKhoa.DataSource = khoas;
                cbKhoa.DisplayMember = "TenKhoa";
                cbKhoa.ValueMember = "MaKhoa";
            }


            updateCbCTDT();
            updateMons();

        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCbCTDT();
            updateMons();
        }

        void updateCbCTDT()
        {
            if (cbKhoa.SelectedValue == null)
            {
                cbCTDT.DataSource = null;

                txtBoxMaCTDT.DataBindings.Clear();
                txtBoxTenCTDT.DataBindings.Clear();

                txtBoxMaCTDT.Text = "";
                txtBoxTenCTDT.Text = "";
            }
            else
            {
                var ctdts = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString()).ToList();
                if (ctdts == null || ctdts.Count == 0)
                {
                    cbCTDT.DataSource = null;
                    txtBoxMaCTDT.DataBindings.Clear();
                    txtBoxTenCTDT.DataBindings.Clear();

                    txtBoxMaCTDT.Text = "";
                    txtBoxTenCTDT.Text = "";
                }
                else
                {
                    cbCTDT.DataSource = ctdts;
                    cbCTDT.DisplayMember = "TenCTDT";
                    cbCTDT.ValueMember = "MaCTDT";

                    txtBoxMaCTDT.DataBindings.Clear();
                    txtBoxTenCTDT.DataBindings.Clear();

                    txtBoxMaCTDT.DataBindings.Add("Text", cbCTDT.DataSource, "MaCTDT");
                    txtBoxTenCTDT.DataBindings.Add("Text", cbCTDT.DataSource, "TenCTDT");
                }
            }
            updateDGV();
        }


        private void updateMons()
        {
            if (cbKhoa.SelectedValue == null)
            {
                cbMon.DataSource = null;
                
            }
            else
            {
                if (adHP || updateHP)
                {
                    return;
                }
                var mons = db.BoMons.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString()).ToList();

                if (mons == null || mons.Count == 0)
                {
                    cbMon.DataSource = null;
                }
                else
                {
                    cbMon.DataSource = mons;
                    cbMon.DisplayMember = "TenBoMon";
                    cbMon.ValueMember = "MaBoMon";
                }
            }
            updateHPs();

            if (!cellClick)
                updateDGV();

            cellClick = false;
        }


        private void updateHPs()
        {
            if (cbMon.SelectedValue == null)
            {
                cbHp.DataSource = null;
            }
            else
            {

                var hps = db.HocPhans.Where(c => c.MaBoMon == cbMon.SelectedValue.ToString()).ToList();
                if (hps == null || hps.Count == 0)
                {
                    cbHp.DataSource = null;
                }
                else
                {
                    cbHp.DataSource = hps;
                    cbHp.DisplayMember = "TenHocPhan";
                    cbHp.ValueMember = "MaHocPhan";
                }
            }
            if (adHP || updateHP)
            {
                return;
            }

            if (!cellClick)
                updateDGV();

            cellClick = false;
        }

        private void updateDGV()
        {
            if (cbCTDT.SelectedValue == null)
            {
                dgvHP.DataSource = null;
            }
            else
            {
                var ctdt_hps = db.CTDT_HocPhans
                    .Where(c => c.MaCTDT == cbCTDT.SelectedValue.ToString())
                    .Join(db.HocPhans,
                          c => c.MaHocPhan,
                          h => h.MaHocPhan,
                          (c, h) => new
                          {
                              MaCTDT = c.MaCTDT,
                              MaHocPhan = c.MaHocPhan,
                              TenHocPhan = h.TenHocPhan,
                              KyHoc = c.KyHoc
                          })
                    .ToList();

                if (ctdt_hps == null || ctdt_hps.Count == 0)
                {
                    dgvHP.DataSource = null;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MaCTDT", typeof(string));
                    dt.Columns.Add("MaHocPhan", typeof(string));
                    dt.Columns.Add("TenHocPhan", typeof(string));
                    dt.Columns.Add("KyHoc", typeof(int));

                    foreach (var item in ctdt_hps)
                    {
                        dt.Rows.Add(item.MaCTDT, item.MaHocPhan, item.TenHocPhan, item.KyHoc);
                    }

                    dgvHP.DataSource = dt;


                    dgvHP.Columns["MaCTDT"].HeaderText = "Mã CTĐT";
                    dgvHP.Columns["MaHocPhan"].HeaderText = "Mã học phần";
                    dgvHP.Columns["TenHocPhan"].HeaderText = "Tên học phần";
                    dgvHP.Columns["KyHoc"].HeaderText = "Kỳ học";

                }
            }
        }

        bool adCTDT = false;
        bool updateCTDT = false;

        private void btnAddCTDT_Click(object sender, EventArgs e)
        {
            adCTDT = true;
            updateCTDT = false;

            txtBoxMaCTDT.Enabled = true;

            txtBoxMaCTDT.DataBindings.Clear();
            txtBoxTenCTDT.DataBindings.Clear();

            txtBoxMaCTDT.Text = "";
            txtBoxTenCTDT.Text = "";

            cbKhoa.Enabled = false;
            cbCTDT.Enabled = false;
            cbMon.Enabled = false;
            cbHp.Enabled = false;
            txtBoxHK.Enabled = false;

            btnAddCTDT.Enabled = false;
            btnAddHP.Enabled = false;

            btnRemoveCTDT.Text = "Hủy";
            btnSaveCTDT.Enabled = true;

            btnDeleteHP.Enabled = false;
            dgvHP.Enabled = false;
        }

        private void btnRemoveCTDT_Click(object sender, EventArgs e)
        {
            if (adCTDT || updateCTDT)
            {
                adCTDT = false;
                updateCTDT = false;

                txtBoxMaCTDT.Enabled = false;

                cbKhoa.Enabled = true;
                cbCTDT.Enabled = true;
                cbMon.Enabled = true;
                cbHp.Enabled = true;
                txtBoxHK.Enabled = true;

                btnAddCTDT.Enabled = true;
                btnAddHP.Enabled = true;

                btnRemoveCTDT.Text = "Xóa";
                btnSaveCTDT.Enabled = false;

                btnDeleteHP.Enabled = true;
                dgvHP.Enabled = true;

                updateCbCTDT();
                updateMons();
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa chương trình đào tạo này không? Bạn chỉ có thể xóa nếu CTDT này chưa có học phần nào.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    db.ChuongTrinhDaoTaos.DeleteOnSubmit(db.ChuongTrinhDaoTaos.FirstOrDefault(c => c.MaCTDT == txtBoxMaCTDT.Text));
                    db.SubmitChanges();
                    updateCbCTDT();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa chương trình đào tạo này");
                }
            }
        }

        private void btnSaveCTDT_Click(object sender, EventArgs e)
        {
            if (adCTDT)
            {
                if (string.IsNullOrEmpty(txtBoxMaCTDT.Text) || string.IsNullOrEmpty(txtBoxTenCTDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                var existingCTDT = db.ChuongTrinhDaoTaos.FirstOrDefault(c => c.MaCTDT == txtBoxMaCTDT.Text);
                if (existingCTDT != null)
                {
                    MessageBox.Show("Mã CTDT đã tồn tại");
                    return;
                }

                var ctdt = new ChuongTrinhDaoTao
                {
                    MaCTDT = txtBoxMaCTDT.Text,
                    TenCTDT = txtBoxTenCTDT.Text,
                    MaKhoa = cbKhoa.SelectedValue.ToString()
                };
                db.ChuongTrinhDaoTaos.InsertOnSubmit(ctdt);
                db.SubmitChanges();

                btnRemoveCTDT_Click(sender, e);
            }
            else if (updateCTDT)
            {
                if (string.IsNullOrEmpty(txtBoxMaCTDT.Text) || string.IsNullOrEmpty(txtBoxTenCTDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                var ctdt = db.ChuongTrinhDaoTaos.FirstOrDefault(c => c.MaCTDT == txtBoxMaCTDT.Text);
                if (ctdt == null)
                {
                    MessageBox.Show("Không tìm thấy chương trình đào tạo");
                    return;
                }

                ctdt.TenCTDT = txtBoxTenCTDT.Text;
                db.SubmitChanges();


                btnRemoveCTDT_Click(sender, e);
            }
        }

        private void txtBoxTenCTDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            updateCTDT = true;

            txtBoxTenCTDT.DataBindings.Clear();

            cbKhoa.Enabled = false;
            cbCTDT.Enabled = false;
            cbMon.Enabled = false;
            cbHp.Enabled = false;
            txtBoxHK.Enabled = false;

            btnAddCTDT.Enabled = false;
            btnAddHP.Enabled = false;

            btnRemoveCTDT.Text = "Hủy";
            btnSaveCTDT.Enabled = true;

            btnDeleteHP.Enabled = false;
            dgvHP.Enabled = false;
        }

        private void cbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateHPs();
        }

        private void cbCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDGV();
        }

        bool cellClick = false;

        private void dgvHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvHP.Rows[e.RowIndex];

                // Retrieve values from the cells
                string maCTDT = selectedRow.Cells["MaCTDT"].Value.ToString();
                string maHocPhan = selectedRow.Cells["MaHocPhan"].Value.ToString();
                string tenHocPhan = selectedRow.Cells["TenHocPhan"].Value.ToString();
                string kyHoc = selectedRow.Cells["KyHoc"].Value.ToString();

                // Bind the values to the text boxes
                txtBoxHK.Text = kyHoc;

                cellClick = true;

                // Update the Mon (Department) ComboBox
                var hocPhan = db.HocPhans.FirstOrDefault(hp => hp.MaHocPhan == maHocPhan);
                if (hocPhan != null)
                {
                    cbMon.SelectedValue = hocPhan.MaBoMon;
                    cbHp.SelectedValue = maHocPhan;
                }
            }
        }

        bool adHP = false;
        bool updateHP = false;

        private void btnAddHP_Click(object sender, EventArgs e)
        {
            adHP = true;
            updateHP = false;

            cbMon.Enabled = true;
            cbHp.Enabled = true;

            btnAddHP.Enabled = false;
            btnSaveHP.Enabled = true;

            btnDeleteHP.Text = "Hủy";
            txtBoxHK.Text = "";
            dgvHP.Enabled = false;

            cbKhoa.Enabled = false;
            cbCTDT.Enabled = false;
            txtBoxTenCTDT.Enabled = false;

            btnAddCTDT.Enabled = false;
            btnRemoveCTDT.Enabled = false;


        }

        private void btnDeleteHP_Click(object sender, EventArgs e)
        {
            if (adHP || updateHP)
            {
                adHP = false;
                updateHP = false;

                cbMon.Enabled = false;
                cbHp.Enabled = false;

                btnAddHP.Enabled = true;
                btnSaveHP.Enabled = false;

                btnDeleteHP.Text = "Xóa";
                dgvHP.Enabled = true;

                cbKhoa.Enabled = true;
                cbCTDT.Enabled = true;
                txtBoxTenCTDT.Enabled = true;

                btnAddCTDT.Enabled = true;
                btnRemoveCTDT.Enabled = true;

                updateMons();
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa học phần này không? Bạn chỉ có thể xóa nếu chưa có lớp học phần cho học phần này", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    db.CTDT_HocPhans.DeleteOnSubmit(db.CTDT_HocPhans.FirstOrDefault(c => c.MaCTDT == cbCTDT.SelectedValue.ToString() && c.MaHocPhan == cbHp.SelectedValue.ToString()));
                    db.SubmitChanges();
                    updateDGV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa học phần này");
                }
            }
        }

        private void btnSaveHP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbMon.Text) || string.IsNullOrEmpty(cbHp.Text) || string.IsNullOrEmpty(txtBoxHK.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }

            if (adHP)
            {
                var existingHP = db.CTDT_HocPhans.FirstOrDefault(c => c.MaCTDT == cbCTDT.SelectedValue.ToString() && c.MaHocPhan == cbHp.SelectedValue.ToString());
                if (existingHP != null)
                {
                    MessageBox.Show("Học phần đã tồn tại");
                    return;
                }
                var ctdt_hp = new CTDT_HocPhan
                {
                    MaCTDT = cbCTDT.SelectedValue.ToString(),
                    MaHocPhan = cbHp.SelectedValue.ToString(),
                    KyHoc = int.Parse(txtBoxHK.Text)
                };
                db.CTDT_HocPhans.InsertOnSubmit(ctdt_hp);
                db.SubmitChanges();
                btnDeleteHP_Click(sender, e);
            }
            else if (updateHP)
            {
                var ctdt_hp = db.CTDT_HocPhans.FirstOrDefault(c => c.MaCTDT == cbCTDT.SelectedValue.ToString() && c.MaHocPhan == cbHp.SelectedValue.ToString());
                if (ctdt_hp == null)
                {
                    MessageBox.Show("Không tìm thấy học phần");
                    return;
                }
                ctdt_hp.KyHoc = int.Parse(txtBoxHK.Text);
                db.SubmitChanges();
                btnDeleteHP_Click(sender, e);
            }

            updateDGV();


        }

        private void txtBoxHK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBoxHK_KeyDown(object sender, KeyEventArgs e)
        {
            // if not arrow key
            if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                adHP = false;
                updateHP = true;

                cbMon.Enabled = true;
                cbHp.Enabled = true;

                btnAddHP.Enabled = false;
                btnSaveHP.Enabled = true;

                btnDeleteHP.Text = "Hủy";
                dgvHP.Enabled = false;

                cbKhoa.Enabled = false;
                cbCTDT.Enabled = false;
                txtBoxTenCTDT.Enabled = false;

                btnAddCTDT.Enabled = false;
                btnRemoveCTDT.Enabled = false;
            }
        }

        private void cantType(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }

    public class CTDT_HocPhanDTO
    {
        public string MaCTDT { get; set; }
        public string MaHocPhan { get; set; }
        public string TenHocPhan { get; set; }
        public int? KyHoc { get; set; }
    }
}
