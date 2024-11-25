using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDiemDaiHoc
{
    public partial class frmNhapDiem : Form
    {
        public frmNhapDiem()
        {
            InitializeComponent();
            // Thêm sự kiện CellValidating để kiểm tra giá trị nhập vào
            dgvDiem.CellValidating += dgvDiem_CellValidating;
            // Thêm sự kiện CellEndEdit để tính toán điểm tổng kết
            dgvDiem.CellEndEdit += dgvDiem_CellEndEdit;
        }

        private QLDDataContext db = new QLDDataContext();
        private bool isValidating = false; // Biến cờ để kiểm soát việc hiển thị thông báo lỗi

        private void frmNhapDiem_Load(object sender, EventArgs e)
        {
            cbKhoa.DataSource = db.Khoas;
            cbKhoa.DisplayMember = "TenKhoa";
            cbKhoa.ValueMember = "MaKhoa";

            var data = db.DIEM_SELECT();

            var distinctKhoaHoc = data.Select(c => c.KhoaHoc).Distinct().ToList();
            cbKhoas.DataSource = distinctKhoaHoc;

            cbLopHP.DataSource = db.LopHocPhans;
            cbLopHP.DisplayMember = "TenLopHocPhan";
            cbLopHP.ValueMember = "MaLopHocPhan";
            DisplayData();
        }

        private void DisplayData()
        {
            if (cbKhoa.Text == "" || cbKhoas.Text == "" || cbLopHP.Text == "")
            {
                return;
            }

            var data = db.Diem_Select_With_Condition(cbKhoa.SelectedValue.ToString(), cbKhoas.Text, cbLopHP.SelectedValue.ToString())
                         .Select(d => new Diem
                         {
                             MaSinhVien = d.MaSinhVien,
                             HoTen = d.HoTen,
                             MaLop = d.MaLop,
                             DiemQuaTrinh = d.DiemQuaTrinh,
                             DiemThiKTHP = d.DiemThiKTHP,
                             DiemTKHP = d.DiemTKHP,
                             DiemHe4 = d.DiemHe4,
                             DiemHeChu = d.DiemHeChu,
                             LanHoc = d.LanHoc,
                             TrongSoDiemQuaTrinh = d.TrongSoDiemQuaTrinh,
                             TrongSoDiemThiKTHP = d.TrongSoDiemThiKTHP,
                             MaHocPhan = d.MaHocPhan
                         }).Distinct().ToList();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = data;
            dgvDiem.DataSource = bindingSource;

            dgvDiem.AllowUserToAddRows = true;
            dgvDiem.EditMode = DataGridViewEditMode.EditOnEnter;

            // Đặt tên cột bằng tiếng Việt
            dgvDiem.Columns["MaSinhVien"].HeaderText = "Mã Sinh Viên";
            dgvDiem.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDiem.Columns["MaLop"].HeaderText = "Mã Lớp";
            dgvDiem.Columns["DiemQuaTrinh"].HeaderText = "Điểm Quá Trình";
            dgvDiem.Columns["DiemThiKTHP"].HeaderText = "Điểm Thi KTHP";
            dgvDiem.Columns["DiemTKHP"].HeaderText = "Điểm TKHP";
            dgvDiem.Columns["DiemHe4"].HeaderText = "Điểm Hệ 4";
            dgvDiem.Columns["DiemHeChu"].HeaderText = "Điểm Hệ Chữ";
            dgvDiem.Columns["LanHoc"].HeaderText = "Lần Học";

            // Không đặt tên cột cho các cột không chiếu lên DataGridView
            dgvDiem.Columns["TrongSoDiemQuaTrinh"].Visible = false;
            dgvDiem.Columns["TrongSoDiemThiKTHP"].Visible = false;
            dgvDiem.Columns["MaHocPhan"].Visible = false;

            // Đặt thuộc tính ReadOnly cho các cột khác
            foreach (DataGridViewColumn column in dgvDiem.Columns)
            {
                if (column.Name != "DiemQuaTrinh" && column.Name != "DiemThiKTHP")
                {
                    column.ReadOnly = true;
                }
            }


        }

        private void dgvDiem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvDiem.Columns[e.ColumnIndex].Name == "DiemQuaTrinh" || dgvDiem.Columns[e.ColumnIndex].Name == "DiemThiKTHP")
            {
                if (!double.TryParse(e.FormattedValue.ToString(), out double newValue) || newValue < 0.0 || newValue > 10.0)
                {
                    e.Cancel = true;
                    if (!isValidating)
                    {
                        isValidating = true;
                        MessageBox.Show("Giá trị phải là số từ 0.0 đến 10.0", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isValidating = false;
                    }
                }
            }
        }

        private void dgvDiem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDiem.Columns[e.ColumnIndex].Name == "DiemQuaTrinh" || dgvDiem.Columns[e.ColumnIndex].Name == "DiemThiKTHP")
            {
                var row = dgvDiem.Rows[e.RowIndex];
                if (double.TryParse(row.Cells["DiemQuaTrinh"].Value?.ToString(), out double diemQuaTrinh) &&
                    double.TryParse(row.Cells["DiemThiKTHP"].Value?.ToString(), out double diemThiKTHP) &&
                    double.TryParse(row.Cells["TrongSoDiemQuaTrinh"].Value?.ToString(), out double trongSoDiemQuaTrinh) &&
                    double.TryParse(row.Cells["TrongSoDiemThiKTHP"].Value?.ToString(), out double trongSoDiemThiKTHP))
                {
                    double diemTKHP = Math.Round((diemQuaTrinh * trongSoDiemQuaTrinh + diemThiKTHP * trongSoDiemThiKTHP), 1);
                    row.Cells["DiemTKHP"].Value = diemTKHP;

                    // Tính Điểm hệ 4
                    double diemHe4 = Math.Round(diemTKHP * 4 / 10, 1);
                    row.Cells["DiemHe4"].Value = diemHe4;

                    // Tính Điểm hệ chữ
                    string diemHeChu;
                    if (diemTKHP >= 9.5)
                    {
                        diemHeChu = "A+";
                    }
                    else if (diemTKHP >= 8.5)
                    {
                        diemHeChu = "A";
                    }
                    else if (diemTKHP >= 8)
                    {
                        diemHeChu = "B+";
                    }
                    else if (diemTKHP >= 7)
                    {
                        diemHeChu = "B";
                    }
                    else if (diemTKHP >= 6)
                    {
                        diemHeChu = "C+";
                    }
                    else if (diemTKHP >= 5.5)
                    {
                        diemHeChu = "C";
                    }
                    else if (diemTKHP >= 4.5)
                    {
                        diemHeChu = "D+";
                    }
                    else if (diemTKHP >= 4)
                    {
                        diemHeChu = "D";
                    }
                    else if (diemTKHP >= 2)
                    {
                        diemHeChu = "F+";
                    }
                    else
                    {
                        diemHeChu = "F";
                    }
                    row.Cells["DiemHeChu"].Value = diemHeChu;
                }
            }
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cbKhoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cbLopHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BindingSource bindingSource = (BindingSource)dgvDiem.DataSource;
            List<Diem> data = (List<Diem>)bindingSource.DataSource;

            foreach (var item in data)
            {
                db.DIEM_UPDATE(item.MaHocPhan, cbLopHP.SelectedValue.ToString(), item.MaSinhVien, item.DiemQuaTrinh, item.DiemThiKTHP, item.DiemTKHP, item.DiemHe4, item.DiemHeChu);
            }

            MessageBox.Show("Dữ liệu đã được lưu thành công!");
        }

        public class Diem
        {
            public string MaSinhVien { get; set; }
            public string HoTen { get; set; }
            public string MaLop { get; set; }
            public double? DiemQuaTrinh { get; set; }
            public double? DiemThiKTHP { get; set; }
            public double? DiemTKHP { get; set; }
            public double? DiemHe4 { get; set; }
            public string DiemHeChu { get; set; }
            public int? LanHoc { get; set; }
            public double? TrongSoDiemQuaTrinh { get; set; }
            public double? TrongSoDiemThiKTHP { get; set; }
            public string MaHocPhan { get; set; }
        }


        private void btnExcel_Click(object sender, EventArgs e)
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
                ws.Cells[1, 1] = "Bảng điểm lớp học phần";
                ws.Range["A1"].Font.Size = 16;
                ws.Range["A1"].Font.Bold = true;
                ws.Range["A1", "E1"].Merge();
                ws.Range["A1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Thêm thông tin Khoa, Lớp, Khóa
                ws.Cells[2, 1] = "Khoa:";
                ws.Cells[2, 2] = cbKhoa.Text;
                ws.Cells[3, 1] = "Lớp:";
                ws.Cells[3, 2] = cbLopHP.Text;
                ws.Cells[4, 1] = "Khóa:";
                ws.Cells[4, 2] = cbKhoas.Text;

                // Thêm tiêu đề cột
                int colIndex = 1;
                for (int i = 0; i < dgvDiem.Columns.Count; i++)
                {
                    if (dgvDiem.Columns[i].Name != "LanHoc" && dgvDiem.Columns[i].Name != "TrongSoDiemQuaTrinh" &&
                        dgvDiem.Columns[i].Name != "TrongSoDiemThiKTHP" && dgvDiem.Columns[i].Name != "MaHocPhan")
                    {
                        ws.Cells[6, colIndex] = dgvDiem.Columns[i].HeaderText;
                        ws.Cells[6, colIndex].Font.Bold = true;
                        ws.Cells[6, colIndex].Interior.Color = Color.LightGray;
                        ws.Cells[6, colIndex].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        colIndex++;
                    }
                }

                // Thêm dữ liệu từ DataGridView vào Excel
                for (int i = 0; i < dgvDiem.Rows.Count; i++)
                {
                    colIndex = 1;
                    for (int j = 0; j < dgvDiem.Columns.Count; j++)
                    {
                        if (dgvDiem.Columns[j].Name != "LanHoc" && dgvDiem.Columns[j].Name != "TrongSoDiemQuaTrinh" &&
                            dgvDiem.Columns[j].Name != "TrongSoDiemThiKTHP" && dgvDiem.Columns[j].Name != "MaHocPhan")
                        {
                            ws.Cells[i + 7, colIndex] = dgvDiem.Rows[i].Cells[j].Value?.ToString();
                            ws.Cells[i + 7, colIndex].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                            colIndex++;
                        }
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
}

