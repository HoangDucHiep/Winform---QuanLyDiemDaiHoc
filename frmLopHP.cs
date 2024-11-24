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
    public partial class frmLopHP : Form
    {
        private QLDDataContext db = new QLDDataContext();

        public frmLopHP()
        {
            InitializeComponent();
        }

        private void frmLopHP_Load(object sender, EventArgs e)
        {
            cbKhoa.DataSource = db.Khoas;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            UpdateCTDT();
            UpdateListALlSV();
        }

        private void cbCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHPAndLopHP();
            UpdateListALlSV();
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCTDT();
            UpdateListALlSV();
        }

        private void cbHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLopHP();
        }

        private void UpdateCTDT()
        {
            if (cbKhoa.SelectedValue == null) return;

            var ctdtList = db.ChuongTrinhDaoTaos.Where(c => c.MaKhoa == cbKhoa.SelectedValue.ToString()).ToList();


            if (ctdtList.Count == 0)
            {
                cbCTDT.Text = string.Empty;
                cbHP.DataSource = null;
                cbLopHp.DataSource = null;
                txtBoxMaLopHP.DataBindings.Clear();
                txtBoxTenLopHP.DataBindings.Clear();
            }
            else
            {
                cbCTDT.DataSource = ctdtList;
                cbCTDT.DisplayMember = "TenCTDT";
                cbCTDT.ValueMember = "MaCTDT";


                UpdateHPAndLopHP();
                UpdateKhoaHoc();
                UpdateListALlSV();
            }
        }

        private void UpdateHPAndLopHP()
        {
            if (cbCTDT.SelectedValue == null) return;

            var hpList = db.HocPhan_SelectByMaCTDT(cbCTDT.SelectedValue.ToString()).ToList();

            if (hpList.Count == 0)
            {
                cbHP.DataSource = null;
                cbHP.Text = string.Empty;
                cbLopHp.DataSource = null;
                txtBoxMaLopHP.DataBindings.Clear();
                txtBoxTenLopHP.DataBindings.Clear();
                cbKhoas.DataSource = null;
                cbKhoas.Text = string.Empty;

                return;
            }


            cbKhoas.DataSource = db.Lops.Where(s => s.MaKhoa == cbKhoa.SelectedValue.ToString() && s.MaCTDT == cbCTDT.SelectedValue.ToString()).Select(c => c.KhoaHoc);

            cbHP.DataSource = hpList;
            cbHP.DisplayMember = "TenHocPhan";
            cbHP.ValueMember = "MaHocPhan";
            UpdateLopHP();
        }

        private void UpdateLopHP()
        {
            if (cbHP.SelectedValue == null) return;

            var lopHpList = db.LopHP_SelectByMaHP(cbHP.SelectedValue.ToString()).ToList();

            if (lopHpList.Count == 0)
            {
                cbLopHp.Text = string.Empty;
                txtBoxMaLopHP.DataBindings.Clear();
                txtBoxTenLopHP.DataBindings.Clear();
                txtBoxMaLopHP.Text = string.Empty;
                txtBoxTenLopHP.Text = string.Empty;
                cbGiangVien.DataSource = null;
                cbGiangVien.Text = string.Empty;
                txtBoxNamHoc.DataBindings.Clear();
                txtBoxNamHoc.Text = string.Empty;
                txtBoxHocKy.DataBindings.Clear();
                txtBoxHocKy.Text = string.Empty;
            }
            else
            {
                cbLopHp.DataSource = lopHpList;
                cbLopHp.DisplayMember = "TenLopHocPhan";
                cbLopHp.ValueMember = "MaLopHocPhan";
                txtBoxMaLopHP.DataBindings.Clear();
                txtBoxMaLopHP.DataBindings.Add("Text", cbLopHp.DataSource, "MaLopHocPhan");

                txtBoxTenLopHP.DataBindings.Clear();
                txtBoxTenLopHP.DataBindings.Add("Text", cbLopHp.DataSource, "TenLopHocPhan");

                cbGiangVien.DataSource = db.GiangViens;
                cbGiangVien.DisplayMember = "FullNameWithID";
                cbGiangVien.ValueMember = "MaGiangVien";

                cbGiangVien.SelectedValue = lopHpList
                    .Where(c => c.MaLopHocPhan == cbLopHp.SelectedValue.ToString())
                    .Select(c => c.MaGiangVien).FirstOrDefault().ToString();

                txtBoxNamHoc.DataBindings.Clear();
                txtBoxNamHoc.DataBindings.Add("Text", cbLopHp.DataSource, "NamHoc");

                txtBoxHocKy.DataBindings.Clear();
                txtBoxHocKy.DataBindings.Add("Text", cbLopHp.DataSource, "HocKy");
            }

        }

        private void UpdateKhoaHoc()
        {
            if (cbKhoas.SelectedValue == null) return;

            var lopList = db.Lops_By(cbKhoa.SelectedValue.ToString(), cbCTDT.SelectedValue.ToString(), cbKhoas.SelectedValue.ToString()).ToList();

            if (lopList.Count == 0)
            {
                cbLop.DataSource = null;
                cbLop.Text = string.Empty;
            }
            else
            {
                cbLop.DataSource = lopList;
                cbLop.DisplayMember = "TenLop";
                cbLop.ValueMember = "MaLop";
            }
        }

        private void cbKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateKhoaHoc();
            UpdateListALlSV();
        }


        private void UpdateListALlSV()
        {
            if (cbKhoa.SelectedValue == null || cbCTDT.SelectedValue == null || cbKhoas.SelectedValue == null)
            {
                dgvAllSV.DataSource = null;
                return;
            };

            var sinhViens = db.SinhViens.Where(sv => sv.MaLop == cbLop.SelectedValue.ToString())
                            .Select(sv => new
                            {
                                MaSinhVien = sv.MaSinhVien,
                                HoDem = sv.HoDem,
                                Ten = sv.Ten,
                                Lop = sv.MaLop
                            }).OrderBy(c => c.Ten).ThenBy(c => c.HoDem).ToList();

            if (sinhViens.Count == 0)
            {
                dgvAllSV.DataSource = null;
            }
            else
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = sinhViens;
                this.dgvAllSV.DataSource = bindingSource;

                this.dgvAllSV.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
                this.dgvAllSV.Columns["HoDem"].HeaderText = "Họ Đệm";
                this.dgvAllSV.Columns["Ten"].HeaderText = "Tên";
                this.dgvAllSV.Columns["Lop"].HeaderText = "Lớp";
            }

        }


        private void UpdateListSelectedSV()
        {
            if (cbKhoa.SelectedValue == null || cbCTDT.SelectedValue == null || cbKhoas.SelectedValue == null)
            {
                dgvSelectedSV.DataSource = null;
                return;
            };

            var sinhViens = db.SinhVien_ByLopHP(cbLopHp.SelectedValue.ToString())
                            .Select(sv => new
                            {
                                MaSinhVien = sv.MaSinhVien,
                                HoDem = sv.HoDem,
                                Ten = sv.Ten,
                                Lop = sv.MaLop,
                                LanHoc = sv.LanHoc
                            }).OrderBy(c => c.Ten).ThenBy(c => c.HoDem).ToList();


            if (sinhViens.Count == 0)
            {
                dgvSelectedSV.DataSource = null;
            }
            else
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = sinhViens;
                this.dgvSelectedSV.DataSource = bindingSource;

                this.dgvSelectedSV.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
                this.dgvSelectedSV.Columns["HoDem"].HeaderText = "Họ Đệm";
                this.dgvSelectedSV.Columns["Ten"].HeaderText = "Tên";
                this.dgvSelectedSV.Columns["Lop"].HeaderText = "Lớp";
                this.dgvSelectedSV.Columns["LanHoc"].HeaderText = "Lần Học";
            }

        }

        private void cbLopHp_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListSelectedSV();
        }

        private void btnAddSV_Click(object sender, EventArgs e)
        {
            var selectedList = dgvAllSV.SelectedRows;

            if (selectedList.Count == 0) return;

            foreach (DataGridViewRow row in selectedList)
            {
                var maSinhVien = row.Cells["MaSinhVien"].Value.ToString();
                var maLopHocPhan = cbLopHp.SelectedValue.ToString();
                var maHP = cbHP.SelectedValue.ToString();
                try
                {
                    db.LopHP_SV_Add(maHP, maLopHocPhan, maSinhVien);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            UpdateListSelectedSV();


        }

        private void btnRemoveSV_Click(object sender, EventArgs e)
        {
            var selectedList = dgvSelectedSV.SelectedRows;

            if (selectedList.Count == 0) return;

            foreach (DataGridViewRow row in selectedList)
            {
                var maSinhVien = row.Cells["MaSinhVien"].Value.ToString();
                var maLopHocPhan = cbLopHp.SelectedValue.ToString();
                var maHP = cbHP.SelectedValue.ToString();
                db.LopHP_SV_Remove(maHP, maLopHocPhan, maSinhVien);

            }

            UpdateListSelectedSV();
        }

        bool adHP = false;
        private void btnAddLopHP_Click(object sender, EventArgs e)
        {
            adHP = true;
            cbLopHp.Enabled = false;
            txtBoxMaLopHP.Text = string.Empty;
            txtBoxTenLopHP.Text = string.Empty;
            txtBoxNamHoc.Text = string.Empty;
            txtBoxHocKy.Text = string.Empty;
            cbGiangVien.SelectedIndex = -1;

            btnXoaLopHP.Text = "Hủy";
            btnAddLopHP.Enabled = false;
        }

        private void btnXoaLopHP_Click(object sender, EventArgs e)
        {
            if (adHP)
            {
                adHP = false;
                btnAddLopHP.Enabled = true;
                cbLopHp.Enabled = true;
                btnXoaLopHP.Text = "Xóa";
                UpdateLopHP();
            }
            else
            {
                if (cbLopHp.SelectedValue == null) return;
                var maLopHocPhan = cbLopHp.SelectedValue.ToString();
                var maHP = cbHP.SelectedValue.ToString();
                UpdateLopHP();
            }
        }
    }





    public partial class GiangVien
    {
        public string FullNameWithID
        {
            get { return $"{HoDem} {Ten} - {MaGiangVien}"; }
        }
    }
}
