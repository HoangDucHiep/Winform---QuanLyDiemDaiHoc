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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void quảnLíSinhViênLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSinhVienLop frm = new frmSinhVienLop();
            frm.ShowDialog();

        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhapDiem frmNhapDiem = new frmNhapDiem();
            frmNhapDiem.ShowDialog();
        }
    }
}
