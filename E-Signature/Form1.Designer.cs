﻿namespace E_Signature
{
    partial class Form1
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
            this.Sign = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.controlSignature = new System.Windows.Forms.Button();
            this.passwordArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.bckWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Sign
            // 
            this.Sign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Sign.Location = new System.Drawing.Point(151, 294);
            this.Sign.Name = "Sign";
            this.Sign.Size = new System.Drawing.Size(530, 34);
            this.Sign.TabIndex = 0;
            this.Sign.Text = "Sign Document";
            this.Sign.UseVisualStyleBackColor = true;
            this.Sign.Click += new System.EventHandler(this.Sign_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(530, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // controlSignature
            // 
            this.controlSignature.Location = new System.Drawing.Point(151, 334);
            this.controlSignature.Name = "controlSignature";
            this.controlSignature.Size = new System.Drawing.Size(530, 34);
            this.controlSignature.TabIndex = 2;
            this.controlSignature.Text = "Control E-Signature Existance";
            this.controlSignature.UseVisualStyleBackColor = true;
            this.controlSignature.Click += new System.EventHandler(this.controlSignature_Click);
            // 
            // passwordArea
            // 
            this.passwordArea.Location = new System.Drawing.Point(269, 112);
            this.passwordArea.Name = "passwordArea";
            this.passwordArea.Size = new System.Drawing.Size(412, 22);
            this.passwordArea.TabIndex = 3;
            this.passwordArea.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "E-Sign Password";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(148, 225);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(158, 16);
            this.fileLabel.TabIndex = 5;
            this.fileLabel.Text = "There is no selected file...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 461);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordArea);
            this.Controls.Add(this.controlSignature);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Sign);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "E-Signature Software";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Sign;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button controlSignature;
        private System.Windows.Forms.TextBox passwordArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileLabel;
        private System.ComponentModel.BackgroundWorker bckWorker;

    }
}

