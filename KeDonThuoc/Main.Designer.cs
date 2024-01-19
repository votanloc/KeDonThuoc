namespace KeDonThuoc
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnKeDonThuoc = new Button();
            btnDoanhThu = new Button();
            SuspendLayout();
            // 
            // btnKeDonThuoc
            // 
            btnKeDonThuoc.Location = new Point(234, 179);
            btnKeDonThuoc.Margin = new Padding(4, 4, 4, 4);
            btnKeDonThuoc.Name = "btnKeDonThuoc";
            btnKeDonThuoc.Size = new Size(153, 102);
            btnKeDonThuoc.TabIndex = 0;
            btnKeDonThuoc.Text = "Kê Đơn Thuốc";
            btnKeDonThuoc.UseVisualStyleBackColor = true;
            // 
            // btnDoanhThu
            // 
            btnDoanhThu.Location = new Point(490, 192);
            btnDoanhThu.Margin = new Padding(4, 4, 4, 4);
            btnDoanhThu.Name = "btnDoanhThu";
            btnDoanhThu.Size = new Size(152, 76);
            btnDoanhThu.TabIndex = 1;
            btnDoanhThu.Text = "Doanh Thu";
            btnDoanhThu.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 630);
            Controls.Add(btnDoanhThu);
            Controls.Add(btnKeDonThuoc);
            Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Main";
            Text = "Main";
            ResumeLayout(false);
        }

        #endregion

        private Button btnKeDonThuoc;
        private Button btnDoanhThu;
    }
}
