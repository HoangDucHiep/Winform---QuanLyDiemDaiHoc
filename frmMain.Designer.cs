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
            this.quảnLíSinhViênLớpToolStripMenuItem});
            this.xửLýDữLiệuToolStripMenuItem.Name = "xửLýDữLiệuToolStripMenuItem";
            this.xửLýDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.xửLýDữLiệuToolStripMenuItem.Text = "Xử lý dữ liệu";
            // 
            // quảnLíSinhViênLớpToolStripMenuItem
            // 
            this.quảnLíSinhViênLớpToolStripMenuItem.Name = "quảnLíSinhViênLớpToolStripMenuItem";
            this.quảnLíSinhViênLớpToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.quảnLíSinhViênLớpToolStripMenuItem.Text = "Quản lí sinh viên, lớp";
            this.quảnLíSinhViênLớpToolStripMenuItem.Click += new System.EventHandler(this.quảnLíSinhViênLớpToolStripMenuItem_Click);
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
    }
}

