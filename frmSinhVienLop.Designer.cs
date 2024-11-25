namespace QuanLyDiemDaiHoc
{
    partial class frmSinhVienLop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBoxTenLop = new System.Windows.Forms.TextBox();
            this.cbCTDT = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLop = new System.Windows.Forms.ComboBox();
            this.cbKhoas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbKhoa = new System.Windows.Forms.ComboBox();
            this.txtBoxMaLop = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnXoaHuy = new System.Windows.Forms.Button();
            this.btnLuuLop = new System.Windows.Forms.Button();
            this.btnAddLop = new System.Windows.Forms.Button();
            this.dgvSV = new System.Windows.Forms.DataGridView();
            this.quanLyDiemTruongDaiHocDataSet = new QuanLyDiemDaiHoc.QuanLyDiemTruongDaiHocDataSet();
            this.sinhVienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sinhVienTableAdapter = new QuanLyDiemDaiHoc.QuanLyDiemTruongDaiHocDataSetTableAdapters.SinhVienTableAdapter();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLyDiemTruongDaiHocDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinhVienBindingSource)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvSV, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.221426F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.71396F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.16705F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1450, 874);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(148, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1154, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản lý lớp, sinh viên";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.00346F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.99653F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(148, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.37799F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.62201F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1154, 209);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.79085F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.29412F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.91503F));
            this.tableLayoutPanel3.Controls.Add(this.txtBoxTenLop, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbCTDT, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbLop, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbKhoas, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbKhoa, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtBoxMaLop, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.92453F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.13208F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.69811F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.24528F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(848, 139);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // txtBoxTenLop
            // 
            this.txtBoxTenLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxTenLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTenLop.Location = new System.Drawing.Point(503, 100);
            this.txtBoxTenLop.Name = "txtBoxTenLop";
            this.txtBoxTenLop.Size = new System.Drawing.Size(342, 26);
            this.txtBoxTenLop.TabIndex = 11;
            this.txtBoxTenLop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTenLop_KeyPress);
            // 
            // cbCTDT
            // 
            this.cbCTDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCTDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCTDT.FormattingEnabled = true;
            this.cbCTDT.Location = new System.Drawing.Point(3, 100);
            this.cbCTDT.Name = "cbCTDT";
            this.cbCTDT.Size = new System.Drawing.Size(365, 28);
            this.cbCTDT.TabIndex = 9;
            this.cbCTDT.SelectedIndexChanged += new System.EventHandler(this.cbCTDT_SelectedIndexChanged);
            this.cbCTDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantTypeIn);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(503, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(342, 30);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tên lớp";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(374, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 30);
            this.label6.TabIndex = 7;
            this.label6.Text = "Mã lớp";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(365, 30);
            this.label5.TabIndex = 6;
            this.label5.Text = "Chương trình đào tạo";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbLop
            // 
            this.cbLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.FormattingEnabled = true;
            this.cbLop.Location = new System.Drawing.Point(503, 27);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(342, 28);
            this.cbLop.TabIndex = 5;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.cbLop_SelectedIndexChanged);
            // 
            // cbKhoas
            // 
            this.cbKhoas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbKhoas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhoas.FormattingEnabled = true;
            this.cbKhoas.Location = new System.Drawing.Point(374, 27);
            this.cbKhoas.Name = "cbKhoas";
            this.cbKhoas.Size = new System.Drawing.Size(123, 28);
            this.cbKhoas.TabIndex = 4;
            this.cbKhoas.SelectedIndexChanged += new System.EventHandler(this.cbKhoas_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(503, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(342, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Lớp";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(374, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Khóa";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Khoa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbKhoa
            // 
            this.cbKhoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbKhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhoa.FormattingEnabled = true;
            this.cbKhoa.Location = new System.Drawing.Point(3, 27);
            this.cbKhoa.Name = "cbKhoa";
            this.cbKhoa.Size = new System.Drawing.Size(365, 28);
            this.cbKhoa.TabIndex = 3;
            this.cbKhoa.SelectedIndexChanged += new System.EventHandler(this.cbKhoa_SelectedIndexChanged);
            this.cbKhoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantTypeIn);
            // 
            // txtBoxMaLop
            // 
            this.txtBoxMaLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxMaLop.Enabled = false;
            this.txtBoxMaLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxMaLop.Location = new System.Drawing.Point(374, 100);
            this.txtBoxMaLop.Name = "txtBoxMaLop";
            this.txtBoxMaLop.Size = new System.Drawing.Size(123, 26);
            this.txtBoxMaLop.TabIndex = 10;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.626F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.122016F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.29973F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.122016F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.56499F));
            this.tableLayoutPanel4.Controls.Add(this.btnXoaHuy, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnLuuLop, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnAddLop, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(857, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.2F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.8F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(294, 139);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // btnXoaHuy
            // 
            this.btnXoaHuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXoaHuy.Location = new System.Drawing.Point(203, 85);
            this.btnXoaHuy.Name = "btnXoaHuy";
            this.btnXoaHuy.Size = new System.Drawing.Size(88, 51);
            this.btnXoaHuy.TabIndex = 12;
            this.btnXoaHuy.Text = "Xóa lớp";
            this.btnXoaHuy.UseVisualStyleBackColor = true;
            this.btnXoaHuy.Click += new System.EventHandler(this.btnXoaHuy_Click);
            // 
            // btnLuuLop
            // 
            this.btnLuuLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLuuLop.Location = new System.Drawing.Point(105, 85);
            this.btnLuuLop.Name = "btnLuuLop";
            this.btnLuuLop.Size = new System.Drawing.Size(86, 51);
            this.btnLuuLop.TabIndex = 10;
            this.btnLuuLop.Text = "Lưu";
            this.btnLuuLop.UseVisualStyleBackColor = true;
            this.btnLuuLop.Click += new System.EventHandler(this.btnLuuLop_Click);
            // 
            // btnAddLop
            // 
            this.btnAddLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddLop.Location = new System.Drawing.Point(3, 85);
            this.btnAddLop.Name = "btnAddLop";
            this.btnAddLop.Size = new System.Drawing.Size(90, 51);
            this.btnAddLop.TabIndex = 8;
            this.btnAddLop.Text = "Thêm lớp";
            this.btnAddLop.UseVisualStyleBackColor = true;
            this.btnAddLop.Click += new System.EventHandler(this.btnAddLop_Click);
            // 
            // dgvSV
            // 
            this.dgvSV.AllowUserToOrderColumns = true;
            this.dgvSV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSV.Location = new System.Drawing.Point(148, 254);
            this.dgvSV.Name = "dgvSV";
            this.dgvSV.RowHeadersWidth = 51;
            this.dgvSV.RowTemplate.Height = 24;
            this.dgvSV.Size = new System.Drawing.Size(1154, 617);
            this.dgvSV.TabIndex = 2;
            this.dgvSV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSV_CellClick);
            // 
            // quanLyDiemTruongDaiHocDataSet
            // 
            this.quanLyDiemTruongDaiHocDataSet.DataSetName = "QuanLyDiemTruongDaiHocDataSet";
            this.quanLyDiemTruongDaiHocDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sinhVienBindingSource
            // 
            this.sinhVienBindingSource.DataMember = "SinhVien";
            this.sinhVienBindingSource.DataSource = this.quanLyDiemTruongDaiHocDataSet;
            // 
            // sinhVienTableAdapter
            // 
            this.sinhVienTableAdapter.ClearBeforeFill = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(848, 64);
            this.label8.TabIndex = 4;
            this.label8.Text = "Danh sách sinh viên của lớp";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnUpdateData, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(857, 148);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.24138F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.75862F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(294, 58);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateData.Location = new System.Drawing.Point(3, 13);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(288, 42);
            this.btnUpdateData.TabIndex = 0;
            this.btnUpdateData.Text = "Cập nhật danh sách sinh viên";
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // frmSinhVienLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 874);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1468, 921);
            this.Name = "frmSinhVienLop";
            this.Text = "frmSinhVienLop";
            this.Load += new System.EventHandler(this.frmSinhVienLop_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLyDiemTruongDaiHocDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinhVienBindingSource)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnXoaHuy;
        private System.Windows.Forms.Button btnLuuLop;
        private System.Windows.Forms.Button btnAddLop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCTDT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbLop;
        private System.Windows.Forms.ComboBox cbKhoas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbKhoa;
        private System.Windows.Forms.TextBox txtBoxMaLop;
        private System.Windows.Forms.TextBox txtBoxTenLop;
        private System.Windows.Forms.DataGridView dgvSV;
        private QuanLyDiemTruongDaiHocDataSet quanLyDiemTruongDaiHocDataSet;
        private System.Windows.Forms.BindingSource sinhVienBindingSource;
        private QuanLyDiemTruongDaiHocDataSetTableAdapters.SinhVienTableAdapter sinhVienTableAdapter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnUpdateData;
    }
}