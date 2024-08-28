namespace Defendo_Blue.Forms
{
    partial class RegistryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistryForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.localMachineRun = new System.Windows.Forms.Button();
            this.bit32 = new System.Windows.Forms.Button();
            this.AllUSer = new System.Windows.Forms.Button();
            this.ValidUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 282);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1199, 283);
            this.dataGridView1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Defendo_Blue.Properties.Resources.bg;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1199, 326);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(389, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(381, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Başlangıç Uygulamaları";
            // 
            // localMachineRun
            // 
            this.localMachineRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.localMachineRun.FlatAppearance.BorderSize = 0;
            this.localMachineRun.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localMachineRun.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.localMachineRun.Location = new System.Drawing.Point(38, 36);
            this.localMachineRun.Name = "localMachineRun";
            this.localMachineRun.Size = new System.Drawing.Size(283, 67);
            this.localMachineRun.TabIndex = 5;
            this.localMachineRun.Text = "Local Machine Üzerindeki Başlangıç Programlarını Görüntüle";
            this.localMachineRun.UseVisualStyleBackColor = false;
            this.localMachineRun.Click += new System.EventHandler(this.button4_Click);
            // 
            // bit32
            // 
            this.bit32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.bit32.FlatAppearance.BorderSize = 0;
            this.bit32.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bit32.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bit32.Location = new System.Drawing.Point(871, 36);
            this.bit32.Name = "bit32";
            this.bit32.Size = new System.Drawing.Size(283, 67);
            this.bit32.TabIndex = 6;
            this.bit32.Text = "32-Bit Programlar için Başlangıç Programlarını Görüntüle";
            this.bit32.UseVisualStyleBackColor = false;
            this.bit32.Click += new System.EventHandler(this.bit32_Click);
            // 
            // AllUSer
            // 
            this.AllUSer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AllUSer.FlatAppearance.BorderSize = 0;
            this.AllUSer.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllUSer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AllUSer.Location = new System.Drawing.Point(38, 233);
            this.AllUSer.Name = "AllUSer";
            this.AllUSer.Size = new System.Drawing.Size(283, 67);
            this.AllUSer.TabIndex = 7;
            this.AllUSer.Text = "Tüm Kullanıcılar İçin Başlangıç Programlarını Görüntüle";
            this.AllUSer.UseVisualStyleBackColor = false;
            this.AllUSer.Click += new System.EventHandler(this.AllUSer_Click);
            // 
            // ValidUser
            // 
            this.ValidUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ValidUser.FlatAppearance.BorderSize = 0;
            this.ValidUser.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValidUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ValidUser.Location = new System.Drawing.Point(871, 233);
            this.ValidUser.Name = "ValidUser";
            this.ValidUser.Size = new System.Drawing.Size(283, 67);
            this.ValidUser.TabIndex = 8;
            this.ValidUser.Text = "Mevcut Kullanıcı İçin Başlangıç Programlarını Görüntüle";
            this.ValidUser.UseVisualStyleBackColor = false;
            this.ValidUser.Click += new System.EventHandler(this.ValidUser_Click);
            // 
            // RegistryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 546);
            this.Controls.Add(this.ValidUser);
            this.Controls.Add(this.AllUSer);
            this.Controls.Add(this.bit32);
            this.Controls.Add(this.localMachineRun);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RegistryForm";
            this.Text = "Registry";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button localMachineRun;
        private System.Windows.Forms.Button bit32;
        private System.Windows.Forms.Button AllUSer;
        private System.Windows.Forms.Button ValidUser;
    }
}