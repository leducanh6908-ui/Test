namespace GUI_QLQN
{
    partial class ucTinhTrangMay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMay = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btnMay
            // 
            this.btnMay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMay.ForeColor = System.Drawing.Color.White;
            this.btnMay.Location = new System.Drawing.Point(0, 0);
            this.btnMay.Name = "btnMay";
            this.btnMay.Size = new System.Drawing.Size(268, 179);
            this.btnMay.TabIndex = 0;
            this.btnMay.Text = "Máy";
            this.btnMay.Click += new System.EventHandler(this.btnMay_Click);
            // 
            // ucTinhTrangMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMay);
            this.Name = "ucTinhTrangMay";
            this.Size = new System.Drawing.Size(268, 179);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnMay;
    }
}
