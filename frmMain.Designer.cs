namespace QuanLyDiemDaiHoc
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.xửLýDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLíSinhViênLớpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýĐiểmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýLớpHọcPhầnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýKhoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýMônHọcPhầnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xửLýDữLiệuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1471, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // xửLýDữLiệuToolStripMenuItem
            // 
            this.xửLýDữLiệuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLíSinhViênLớpToolStripMenuItem,
            this.quảnLýĐiểmToolStripMenuItem,
            this.quảnLýLớpHọcPhầnToolStripMenuItem,
            this.quảnLýKhoaToolStripMenuItem,
            this.quảnLýMônHọcPhầnToolStripMenuItem});
            this.xửLýDữLiệuToolStripMenuItem.Name = "xửLýDữLiệuToolStripMenuItem";
            this.xửLýDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.xửLýDữLiệuToolStripMenuItem.Text = "Xử lý dữ liệu";
            // 
            // quảnLíSinhViênLớpToolStripMenuItem
            // 
            this.quảnLíSinhViênLớpToolStripMenuItem.Name = "quảnLíSinhViênLớpToolStripMenuItem";
            this.quảnLíSinhViênLớpToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.quảnLíSinhViênLớpToolStripMenuItem.Text = "Quản lí sinh viên, lớp";
            this.quảnLíSinhViênLớpToolStripMenuItem.Click += new System.EventHandler(this.quảnLíSinhViênLớpToolStripMenuItem_Click);
            // 
            // quảnLýĐiểmToolStripMenuItem
            // 
            this.quảnLýĐiểmToolStripMenuItem.Name = "quảnLýĐiểmToolStripMenuItem";
            this.quảnLýĐiểmToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.quảnLýĐiểmToolStripMenuItem.Text = "Quản lý điểm";
            this.quảnLýĐiểmToolStripMenuItem.Click += new System.EventHandler(this.quảnLýĐiểmToolStripMenuItem_Click);
            // 
            // quảnLýLớpHọcPhầnToolStripMenuItem
            // 
            this.quảnLýLớpHọcPhầnToolStripMenuItem.Name = "quảnLýLớpHọcPhầnToolStripMenuItem";
            this.quảnLýLớpHọcPhầnToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.quảnLýLớpHọcPhầnToolStripMenuItem.Text = "Quản lý lớp học phần";
            this.quảnLýLớpHọcPhầnToolStripMenuItem.Click += new System.EventHandler(this.quảnLýLớpHọcPhầnToolStripMenuItem_Click);
            // 
            // quảnLýKhoaToolStripMenuItem
            // 
            this.quảnLýKhoaToolStripMenuItem.Name = "quảnLýKhoaToolStripMenuItem";
            this.quảnLýKhoaToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.quảnLýKhoaToolStripMenuItem.Text = "Quản lý khoa";
            this.quảnLýKhoaToolStripMenuItem.Click += new System.EventHandler(this.quảnLýKhoaToolStripMenuItem_Click);
            // 
            // quảnLýMônHọcPhầnToolStripMenuItem
            // 
            this.quảnLýMônHọcPhầnToolStripMenuItem.Name = "quảnLýMônHọcPhầnToolStripMenuItem";
            this.quảnLýMônHọcPhầnToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.quảnLýMônHọcPhầnToolStripMenuItem.Text = "Quản lý môn, học phần";
            this.quảnLýMônHọcPhầnToolStripMenuItem.Click += new System.EventHandler(this.quảnLýMônHọcPhầnToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 771);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Hệ thống quản lý điểm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xửLýDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLíSinhViênLớpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýĐiểmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýLớpHọcPhầnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýKhoaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýMônHọcPhầnToolStripMenuItem;
    }
}

