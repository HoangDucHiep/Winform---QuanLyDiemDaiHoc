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
                updateHPs();
            }
            else
            {
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


                    // select cbMon and cbHp to dgvHP selected row
                    if (dgvHP.Rows.Count > 0)
                    {
                        cbMon.SelectedValue = db.HocPhans.FirstOrDefault(h => h.MaHocPhan == dgvHP.Rows[0].Cells["MaHocPhan"].Value.ToString()).MaBoMon;
                        cbHp.SelectedValue = dgvHP.Rows[0].Cells["MaHocPhan"].Value.ToString();
                    }
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

        private void dgvHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Cell Clicked");
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
