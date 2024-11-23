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
        }

        private void cbCTDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHPAndLopHP();
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCTDT();
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


            cbKhoas.DataSource = db.Lops.Where(s => s.MaKhoa == cbKhoa.SelectedValue.ToString() && s.MaCTDT == cbCTDT.SelectedValue.ToString()).Select(c=>c.KhoaHoc);

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
                    .Where(c=>c.MaLopHocPhan == cbLopHp.SelectedValue.ToString())
                    .Select(c=>c.MaGiangVien).FirstOrDefault().ToString();

                txtBoxNamHoc.DataBindings.Clear();
                txtBoxNamHoc.DataBindings.Add("Text", cbLopHp.DataSource, "NamHoc");

                txtBoxHocKy.DataBindings.Clear();
                txtBoxHocKy.DataBindings.Add("Text", cbLopHp.DataSource, "HocKy");
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
