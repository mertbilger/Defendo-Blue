﻿using System;
using System.Windows.Forms;

namespace Defendo_Blue.Forms
{
    partial class FastScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastScan));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTespit = new System.Windows.Forms.Button();
            this.content1 = new System.Windows.Forms.Label();
            this.addFile = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFile)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Defendo_Blue.Properties.Resources.bg;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1193, 546);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnTespit
            // 
            this.btnTespit.BackColor = System.Drawing.Color.DimGray;
            this.btnTespit.Font = new System.Drawing.Font("Lucida Bright", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTespit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTespit.Image = global::Defendo_Blue.Properties.Resources.bg;
            this.btnTespit.Location = new System.Drawing.Point(354, 64);
            this.btnTespit.Name = "btnTespit";
            this.btnTespit.Size = new System.Drawing.Size(418, 63);
            this.btnTespit.TabIndex = 2;
            this.btnTespit.Text = "Zararlı Yazılım Kontrolü";
            this.btnTespit.UseVisualStyleBackColor = false;
            // 
            // content1
            // 
            this.content1.AutoSize = true;
            this.content1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.content1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.content1.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.content1.Location = new System.Drawing.Point(397, 376);
            this.content1.Name = "content1";
            this.content1.Size = new System.Drawing.Size(333, 24);
            this.content1.TabIndex = 3;
            this.content1.Text = "Taramak İstediğiniz Dosyayı Seçin.";
            // 
            // addFile
            // 
            this.addFile.Image = global::Defendo_Blue.Properties.Resources._8725957_file_plus_alt_icon;
            this.addFile.Location = new System.Drawing.Point(471, 230);
            this.addFile.Name = "addFile";
            this.addFile.Size = new System.Drawing.Size(181, 85);
            this.addFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.addFile.TabIndex = 4;
            this.addFile.TabStop = false;
            this.addFile.Click += new System.EventHandler(this.addFile_Click);
            // 
            // FastScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 546);
            this.Controls.Add(this.addFile);
            this.Controls.Add(this.content1);
            this.Controls.Add(this.btnTespit);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FastScan";
            this.Text = "Fast Scan";
            this.TransparencyKey = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private PictureBox pictureBox1;
        private Button btnTespit;
        private Label content1;
        private PictureBox addFile;
    }
}