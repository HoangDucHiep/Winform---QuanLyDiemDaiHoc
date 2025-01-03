﻿using System;
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
        FrmDangNhap FrmDangNhap;
        public frmMain(FrmDangNhap frm)
        {
            InitializeComponent();
            FrmDangNhap = frm;

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

        private void quảnLýLớpHọcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLopHP frmLopHP = new frmLopHP();
            frmLopHP.ShowDialog();
        }

        private void quảnLýKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhoa frmKhoa = new frmKhoa();
            frmKhoa.ShowDialog();
        }

        private void quảnLýMônHọcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhoaBoMonHP frmKhoaBoMonHP = new frmKhoaBoMonHP();
            frmKhoaBoMonHP.ShowDialog();
        }

        private void quảnLýCTDTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhoa_CTDT frmKhoa_CTDT = new frmKhoa_CTDT();
            frmKhoa_CTDT.ShowDialog();
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

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
