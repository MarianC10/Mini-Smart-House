namespace Voice
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
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.Text1 = new System.Windows.Forms.RichTextBox();
            this.stopmusic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(47, 250);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(144, 23);
            this.btnEnable.TabIndex = 0;
            this.btnEnable.Text = "Voice enabled";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(474, 250);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(127, 23);
            this.btnDisable.TabIndex = 1;
            this.btnDisable.Text = "Voice disabled";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // Text1
            // 
            this.Text1.Location = new System.Drawing.Point(62, 39);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(307, 182);
            this.Text1.TabIndex = 2;
            this.Text1.Text = "Log";
            // 
            // stopmusic
            // 
            this.stopmusic.Location = new System.Drawing.Point(244, 348);
            this.stopmusic.Name = "stopmusic";
            this.stopmusic.Size = new System.Drawing.Size(245, 23);
            this.stopmusic.TabIndex = 3;
            this.stopmusic.Text = "Stop the music";
            this.stopmusic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stopmusic.UseVisualStyleBackColor = true;
            this.stopmusic.Visible = false;
            this.stopmusic.Click += new System.EventHandler(this.stopmusic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stopmusic);
            this.Controls.Add(this.Text1);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnEnable);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.RichTextBox Text1;
        private System.Windows.Forms.Button stopmusic;
    }
}

