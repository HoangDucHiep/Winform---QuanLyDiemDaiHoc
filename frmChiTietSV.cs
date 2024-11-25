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
    public partial class frmChiTietSV : Form
    {
        SinhVien sv;
        QLDDataContext db;

        public frmChiTietSV(string MaSV)
        {
            InitializeComponent();
            db = new QLDDataContext();

            sv = db.SinhViens.FirstOrDefault(s => s.MaSinhVien == MaSV);


            txtBoxMaSV.Text = sv.MaSinhVien;
            txtBoxHoVaTen.Text = sv.HoDem + " " + sv.Ten;
            txtBoxNgaySinh.Text = sv.NgaySinh?.ToString("dd/MM/yyyy");
            txtBoxLop.Text = sv.MaLop;
            txtBoxQue.Text = sv.DiaChi;
            txtBoxSDT.Text = sv.DienThoai;
            txtBoxEmail.Text = sv.Email;

            cbHocKy.Text = "Toàn khóa";
        }

        private void frmChiTietSV_Load(object sender, EventArgs e)
        {



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbHocKy_TextChanged(object sender, EventArgs e)
        {
            var hocKy = 0;
            if (cbHocKy.Text != "Toàn khóa" && !int.TryParse(cbHocKy.Text, out hocKy))
            {
                MessageBox.Show("Học kỳ không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //select *
            var datas = from lhp_sv in db.LopHocPhan_SinhViens
                        join lhp in db.LopHocPhans on lhp_sv.MaLopHocPhan equals lhp.MaLopHocPhan
                        join hp in db.HocPhans on lhp.MaHocPhan equals hp.MaHocPhan
                        where lhp_sv.MaSinhVien == sv.MaSinhVien
                        select new
                        {
                            MaHocPhan = hp.MaHocPhan,
                            TenHocPhan = hp.TenHocPhan,
                            SoTinChi = hp.SoTinChi,
                            HocKy = lhp.HocKy,
                            DiemQuaTrinh = lhp_sv.DiemQuaTrinh,
                            DiemThi = lhp_sv.DiemThiKTHP,
                            DiemTK = lhp_sv.DiemTKHP,
                            DiemHe4 = lhp_sv.DiemHe4,
                            DiemChu = lhp_sv.DiemHeChu
                        };

            dgvDiem.DataSource = datas.ToList();

            dgvDiem.Columns[0].HeaderText = "Mã học phần";
            dgvDiem.Columns[1].HeaderText = "Tên học phần";
            dgvDiem.Columns[2].HeaderText = "Số tín chỉ";
            dgvDiem.Columns[3].HeaderText = "Học kỳ";
            dgvDiem.Columns[4].HeaderText = "Điểm quá trình";
            dgvDiem.Columns[5].HeaderText = "Điểm thi";
            dgvDiem.Columns[6].HeaderText = "Điểm tổng kết";
            dgvDiem.Columns[7].HeaderText = "Điểm hệ 4";
            dgvDiem.Columns[8].HeaderText = "Điểm hệ chữ";



            int tongTinChi = 0;
            double tongDiem = 0;

            foreach (DataGridViewRow row in dgvDiem.Rows)
            {
                tongTinChi += int.Parse(row.Cells[2].Value.ToString());
                tongDiem += double.Parse(row.Cells[6].Value.ToString()) * int.Parse(row.Cells[2].Value.ToString());
            }

            float gpa10 = (float)(tongDiem / tongTinChi);


            txtBoxGPA10.Text = gpa10.ToString("0.00");
            txtBoxGPA4.Text = (gpa10 * 4 / 10).ToString("0.00");
            txtBoxGPAChu.Text = Utilities.GPAConverter.ConvertToGPA(gpa10);
           
            float gpa4 = gpa10 * 4 / 10;
            if (gpa4 >= 3.6)
            {
                txtBoxXepLoai.Text = "Xuất sắc";
            }
            else if (gpa4 >= 3.2)
            {
                txtBoxXepLoai.Text = "Giỏi";
            }
            else if (gpa4 >= 2.5)
            {
                txtBoxXepLoai.Text = "Khá";
            }
            else if (gpa4 >= 2)
            {
                txtBoxXepLoai.Text = "Trung bình";
            }
            else
            {
                txtBoxXepLoai.Text = "Yếu";
            }

        }
    }
}
