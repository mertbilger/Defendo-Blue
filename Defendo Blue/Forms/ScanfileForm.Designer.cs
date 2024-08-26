namespace Defendo_Blue.Forms
{
    partial class ScanfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanfileForm));
            this.content1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.addFile = new System.Windows.Forms.PictureBox();
            this.bgFileBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgFileBack)).BeginInit();
            this.SuspendLayout();
            // 
            // content1
            // 
            this.content1.AutoSize = true;
            this.content1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.content1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.content1.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.content1.Location = new System.Drawing.Point(253, 380);
            this.content1.Name = "content1";
            this.content1.Size = new System.Drawing.Size(489, 24);
            this.content1.TabIndex = 2;
            this.content1.Text = "Taramak İstediğiniz Dosyayı Sürükleyin. veya Seçin.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(44)))), ((int)(((byte)(65)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Lucida Bright", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(76, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Defendo Blue";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(44)))), ((int)(((byte)(65)))));
            this.picLogo.Image = global::Defendo_Blue.Properties.Resources._4527369_avatar_character_dragon_game_ice_icon;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(70, 61);
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(44)))), ((int)(((byte)(65)))));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(936, 61);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // addFile
            // 
            this.addFile.Image = global::Defendo_Blue.Properties.Resources._8725957_file_plus_alt_icon;
            this.addFile.Location = new System.Drawing.Point(388, 179);
            this.addFile.Name = "addFile";
            this.addFile.Size = new System.Drawing.Size(171, 75);
            this.addFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addFile.TabIndex = 1;
            this.addFile.TabStop = false;
            // 
            // bgFileBack
            // 
            this.bgFileBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bgFileBack.Image = global::Defendo_Blue.Properties.Resources.bg;
            this.bgFileBack.Location = new System.Drawing.Point(0, 0);
            this.bgFileBack.Name = "bgFileBack";
            this.bgFileBack.Size = new System.Drawing.Size(936, 562);
            this.bgFileBack.TabIndex = 0;
            this.bgFileBack.TabStop = false;
            // 
            // ScanfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 562);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.content1);
            this.Controls.Add(this.addFile);
            this.Controls.Add(this.bgFileBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanfileForm";
            this.Text = "ScanfileForm";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgFileBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox bgFileBack;
        private System.Windows.Forms.PictureBox addFile;
        private System.Windows.Forms.Label content1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label2;
    }
}