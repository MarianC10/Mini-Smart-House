namespace UnitTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.number_tested = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.good_tests = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Press to do unit testing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // number_tested
            // 
            this.number_tested.AutoSize = true;
            this.number_tested.Location = new System.Drawing.Point(196, 168);
            this.number_tested.Name = "number_tested";
            this.number_tested.Size = new System.Drawing.Size(36, 17);
            this.number_tested.TabIndex = 1;
            this.number_tested.Text = "0/15";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Successful:";
            // 
            // good_tests
            // 
            this.good_tests.AutoSize = true;
            this.good_tests.Location = new System.Drawing.Point(505, 168);
            this.good_tests.Name = "good_tests";
            this.good_tests.Size = new System.Drawing.Size(16, 17);
            this.good_tests.TabIndex = 3;
            this.good_tests.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.good_tests);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.number_tested);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label number_tested;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label good_tests;
        private System.Windows.Forms.Label label2;
    }
}

