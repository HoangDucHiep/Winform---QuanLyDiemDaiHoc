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
        IQueryable<DiemHocPhan> datas;
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

            datas = from lhp_sv in db.LopHocPhan_SinhViens
                        join lhp in db.LopHocPhans on lhp_sv.MaLopHocPhan equals lhp.MaLopHocPhan
                        join hp in db.HocPhans on lhp.MaHocPhan equals hp.MaHocPhan
                        where lhp_sv.MaSinhVien == sv.MaSinhVien
                        select new DiemHocPhan
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

            if (cbHocKy.Text == "Toàn khóa")
            {
               dgvDiem.DataSource = datas.ToList();
            }
            else
            {
                int hk = int.Parse(cbHocKy.Text);
                dgvDiem.DataSource = datas.Where(d => d.HocKy == hk).ToList();
            }



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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet ws = null;

                ws = wb.Sheets[1];
                ws = wb.ActiveSheet;
                ws.Name = "Danh sách điểm";
                app.Visible = true;

                // Thêm tiêu đề
                ws.Cells[1, 1] = $"Bảng điểm sinh viên {(cbHocKy.Text == "Toàn khóa" ? "toàn khóa" : "học kì " + cbHocKy.Text)}";
                ws.Range["A1"].Font.Size = 20;

                ws.Range["A1"].RowHeight = 25;
                ws.Range["A1"].Font.Bold = true;
                ws.Range["A1", "I1"].Merge();
                ws.Range["A1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Thêm thông tin sinh viên
                ws.Cells[2, 1] = "Mã sinh viên:";
                ws.Cells[2, 2] = txtBoxMaSV.Text;
                ws.Cells[3, 1] = "Họ và tên:";
                ws.Cells[3, 2] = txtBoxHoVaTen.Text;
                ws.Cells[4, 1] = "Ngày sinh:";
                ws.Cells[4, 2] = txtBoxNgaySinh.Text;
                ws.Cells[5, 1] = "Lớp:";
                ws.Cells[5, 2] = txtBoxLop.Text;
                ws.Cells[6, 1] = "Quê quán:";
                ws.Cells[6, 2] = txtBoxQue.Text;
                ws.Cells[7, 1] = "Số điện thoại:";
                ws.Cells[7, 2] = txtBoxSDT.Text;
                ws.Cells[8, 1] = "Email:";
                ws.Cells[8, 2] = txtBoxEmail.Text;

                // Căn chỉnh văn bản sang trái cho các phần thông tin sinh viên
                ws.Range["B2", "B8"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                // Thêm thông tin GPA và xếp loại
                ws.Cells[2, 5] = "GPA hệ 10:";
                ws.Cells[2, 6] = txtBoxGPA10.Text;
                ws.Cells[3, 5] = "GPA hệ 4:";
                ws.Cells[3, 6] = txtBoxGPA4.Text;
                ws.Cells[4, 5] = "GPA hệ chữ:";
                ws.Cells[4, 6] = txtBoxGPAChu.Text;
                ws.Cells[5, 5] = "Xếp loại:";
                ws.Cells[5, 6] = txtBoxXepLoai.Text;

                // Căn chỉnh văn bản sang trái cho các phần thông tin GPA và xếp loại
                ws.Range["F2", "F8"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                ws.Range["F2", "F8"].RowHeight = 20;

                // Thêm tiêu đề cột cho bảng điểm
                int rowIndex = 10; // Bắt đầu từ hàng 10
                ws.Cells[rowIndex, 1] = "Mã học phần";
                ws.Cells[rowIndex, 2] = "Tên học phần";
                ws.Cells[rowIndex, 3] = "Số tín chỉ";
                ws.Cells[rowIndex, 4] = "Học kỳ";
                ws.Cells[rowIndex, 5] = "Điểm quá trình";
                ws.Cells[rowIndex, 6] = "Điểm thi";
                ws.Cells[rowIndex, 7] = "Điểm tổng kết";
                ws.Cells[rowIndex, 8] = "Điểm hệ 4";
                ws.Cells[rowIndex, 9] = "Điểm hệ chữ";

                for (int i = 0; i < 9; i++)
                {
                    ws.Cells[rowIndex, i + 1].Font.Bold = true;
                    ws.Cells[rowIndex, i + 1].Interior.Color = Color.LightGray;
                    ws.Cells[rowIndex, i + 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }

                // Thêm dữ liệu từ DataGridView vào Excel
                for (int i = 0; i < dgvDiem.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvDiem.Columns.Count; j++)
                    {
                        ws.Cells[i + rowIndex + 1, j + 1] = dgvDiem.Rows[i].Cells[j].Value?.ToString();
                        ws.Cells[i + rowIndex + 1, j + 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }
                }

                // Định dạng lại cột
                ws.Columns.AutoFit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xuất dữ liệu ra Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi không xác định xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






    }
    public class DiemHocPhan
    {
        public string MaHocPhan { get; set; }
        public string TenHocPhan { get; set; }
        public int? SoTinChi { get; set; }
        public int? HocKy { get; set; }
        public double? DiemQuaTrinh { get; set; }
        public double? DiemThi { get; set; }
        public double? DiemTK { get; set; }
        public double? DiemHe4 { get; set; }
        public string DiemChu { get; set; }
    }
}
