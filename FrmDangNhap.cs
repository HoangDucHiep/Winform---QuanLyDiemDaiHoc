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
    public partial class FrmDangNhap : Form
    {

        QLDDataContext db = new QLDDataContext();

        public FrmDangNhap()
        {
            InitializeComponent();
        }



        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtBoxEmail.Text;
            string password = Utilities.PasswordHasher.HashPassword(txtBoxMK.Text);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            var user = db.TKs.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng");
                return;
            }

            if (user.MaRole == "ad")
            {
                frmMain frmMain = new frmMain(this);
                frmMain.Show();
                this.Hide();
            }
            else if (user.MaRole == "sv")
            {
                string result = user.MaTK.Substring(3);
                frmChiTietSV frmChiTietSV = new frmChiTietSV(result);
                frmChiTietSV.Show();
                this.Hide();
            }
            else if (user.MaRole == "gv")
            {
                frmMainGV frmMainGV = new frmMainGV(this, user.MaTK.Substring(3));
                frmMainGV.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản không có quyền truy cập");
            }

            txtBoxEmail.Text = "";
            txtBoxMK.Text = "";
        }
    }
}
