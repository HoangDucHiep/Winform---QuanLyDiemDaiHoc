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
    public partial class frmMainGV : Form
    {
        FrmDangNhap FrmDangNhap;
        string MaGV;
        public frmMainGV(FrmDangNhap frm, string maGV)
        {
            InitializeComponent();
            FrmDangNhap = frm;
            MaGV = maGV;
        }

        private void quảnLíSinhViênLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSinhVienLop frm = new frmSinhVienLop();
            frm.ShowDialog();

        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhapDiem frmNhapDiem = new frmNhapDiem(MaGV);
            frmNhapDiem.ShowDialog();
        }

        private void quảnLýGiảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGiangVien frmGiangVien = new frmGiangVien();
            frmGiangVien.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close from, show login form
            FrmDangNhap.Show();
            this.Close();
        }

        private void frmMainGV_Load(object sender, EventArgs e)
        {

        }



    }
}
