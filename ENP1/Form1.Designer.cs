namespace ENP1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.engocBtn = new System.Windows.Forms.Button();
            this.accordBtn = new System.Windows.Forms.Button();
            this.fileBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sampleBar = new System.Windows.Forms.TrackBar();
            this.sampleLbl = new System.Windows.Forms.Label();
            this.learningRateBar = new System.Windows.Forms.TrackBar();
            this.momentumBar = new System.Windows.Forms.TrackBar();
            this.learningRateLbl = new System.Windows.Forms.Label();
            this.momentumLbl = new System.Windows.Forms.Label();
            this.rateTestBtn = new System.Windows.Forms.Button();
            this.deepNetworkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.advancedBtn = new System.Windows.Forms.Button();
            this.advancedLbl = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).BeginInit();
            this.SuspendLayout();
            // 
            // engocBtn
            // 
            this.engocBtn.Location = new System.Drawing.Point(255, 438);
            this.engocBtn.Name = "engocBtn";
            this.engocBtn.Size = new System.Drawing.Size(75, 23);
            this.engocBtn.TabIndex = 1;
            this.engocBtn.Text = "Encog";
            this.engocBtn.UseVisualStyleBackColor = true;
            this.engocBtn.Click += new System.EventHandler(this.engocBtn_Click);
            // 
            // accordBtn
            // 
            this.accordBtn.Location = new System.Drawing.Point(255, 477);
            this.accordBtn.Name = "accordBtn";
            this.accordBtn.Size = new System.Drawing.Size(75, 23);
            this.accordBtn.TabIndex = 2;
            this.accordBtn.Text = "Accord.NET";
            this.accordBtn.UseVisualStyleBackColor = true;
            this.accordBtn.Click += new System.EventHandler(this.accordBtn_Click);
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(12, 438);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(75, 23);
            this.fileBtn.TabIndex = 3;
            this.fileBtn.Text = "File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sampleBar
            // 
            this.sampleBar.LargeChange = 1;
            this.sampleBar.Location = new System.Drawing.Point(5, 482);
            this.sampleBar.Name = "sampleBar";
            this.sampleBar.Size = new System.Drawing.Size(104, 45);
            this.sampleBar.TabIndex = 4;
            this.sampleBar.Scroll += new System.EventHandler(this.sampleBar_Scroll);
            // 
            // sampleLbl
            // 
            this.sampleLbl.AutoSize = true;
            this.sampleLbl.Location = new System.Drawing.Point(113, 482);
            this.sampleLbl.Name = "sampleLbl";
            this.sampleLbl.Size = new System.Drawing.Size(88, 13);
            this.sampleLbl.TabIndex = 7;
            this.sampleLbl.Text = "Sample Data: 0%";
            // 
            // learningRateBar
            // 
            this.learningRateBar.LargeChange = 1;
            this.learningRateBar.Location = new System.Drawing.Point(355, 438);
            this.learningRateBar.Minimum = 1;
            this.learningRateBar.Name = "learningRateBar";
            this.learningRateBar.Size = new System.Drawing.Size(104, 45);
            this.learningRateBar.TabIndex = 5;
            this.learningRateBar.Value = 1;
            this.learningRateBar.Visible = false;
            this.learningRateBar.Scroll += new System.EventHandler(this.learningRateBar_Scroll);
            // 
            // momentumBar
            // 
            this.momentumBar.LargeChange = 1;
            this.momentumBar.Location = new System.Drawing.Point(355, 477);
            this.momentumBar.Minimum = 1;
            this.momentumBar.Name = "momentumBar";
            this.momentumBar.Size = new System.Drawing.Size(104, 45);
            this.momentumBar.TabIndex = 6;
            this.momentumBar.Value = 1;
            this.momentumBar.Visible = false;
            this.momentumBar.Scroll += new System.EventHandler(this.momentumBar_Scroll);
            // 
            // learningRateLbl
            // 
            this.learningRateLbl.AutoSize = true;
            this.learningRateLbl.Location = new System.Drawing.Point(465, 438);
            this.learningRateLbl.Name = "learningRateLbl";
            this.learningRateLbl.Size = new System.Drawing.Size(95, 13);
            this.learningRateLbl.TabIndex = 9;
            this.learningRateLbl.Text = "Learning Rate: 0.1";
            this.learningRateLbl.Visible = false;
            // 
            // momentumLbl
            // 
            this.momentumLbl.AutoSize = true;
            this.momentumLbl.Location = new System.Drawing.Point(465, 482);
            this.momentumLbl.Name = "momentumLbl";
            this.momentumLbl.Size = new System.Drawing.Size(80, 13);
            this.momentumLbl.TabIndex = 10;
            this.momentumLbl.Text = "Momentum: 0.1";
            this.momentumLbl.Visible = false;
            // 
            // rateTestBtn
            // 
            this.rateTestBtn.Location = new System.Drawing.Point(869, 438);
            this.rateTestBtn.Name = "rateTestBtn";
            this.rateTestBtn.Size = new System.Drawing.Size(75, 23);
            this.rateTestBtn.TabIndex = 11;
            this.rateTestBtn.Text = "Test Rates";
            this.rateTestBtn.UseVisualStyleBackColor = true;
            this.rateTestBtn.Click += new System.EventHandler(this.rateTestBtn_Click);
            // 
            // deepNetworkBox
            // 
            this.deepNetworkBox.AutoSize = true;
            this.deepNetworkBox.Location = new System.Drawing.Point(363, 513);
            this.deepNetworkBox.Name = "deepNetworkBox";
            this.deepNetworkBox.Size = new System.Drawing.Size(96, 17);
            this.deepNetworkBox.TabIndex = 12;
            this.deepNetworkBox.Text = "Deep Learning";
            this.deepNetworkBox.UseVisualStyleBackColor = true;
            this.deepNetworkBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(938, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // advancedBtn
            // 
            this.advancedBtn.Location = new System.Drawing.Point(255, 512);
            this.advancedBtn.Name = "advancedBtn";
            this.advancedBtn.Size = new System.Drawing.Size(20, 22);
            this.advancedBtn.TabIndex = 14;
            this.advancedBtn.Text = "+";
            this.advancedBtn.UseVisualStyleBackColor = true;
            this.advancedBtn.Click += new System.EventHandler(this.advancedBtn_Click);
            // 
            // advancedLbl
            // 
            this.advancedLbl.AutoSize = true;
            this.advancedLbl.Location = new System.Drawing.Point(276, 517);
            this.advancedLbl.Name = "advancedLbl";
            this.advancedLbl.Size = new System.Drawing.Size(97, 13);
            this.advancedLbl.TabIndex = 15;
            this.advancedLbl.Text = "Advanced Settings";
            // 
            // Output
            // 
            this.Output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Output.Cursor = System.Windows.Forms.Cursors.Default;
            this.Output.Font = new System.Drawing.Font("Adobe Fangsong Std R", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Output.Location = new System.Drawing.Point(12, 12);
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.Size = new System.Drawing.Size(932, 390);
            this.Output.TabIndex = 16;
            this.Output.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 570);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.advancedLbl);
            this.Controls.Add(this.advancedBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deepNetworkBox);
            this.Controls.Add(this.rateTestBtn);
            this.Controls.Add(this.momentumLbl);
            this.Controls.Add(this.learningRateLbl);
            this.Controls.Add(this.momentumBar);
            this.Controls.Add(this.learningRateBar);
            this.Controls.Add(this.sampleLbl);
            this.Controls.Add(this.sampleBar);
            this.Controls.Add(this.fileBtn);
            this.Controls.Add(this.accordBtn);
            this.Controls.Add(this.engocBtn);
            this.Name = "Form1";
            this.Text = "Test Window";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button engocBtn;
        private System.Windows.Forms.Button accordBtn;
		private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TrackBar sampleBar;
        private System.Windows.Forms.Label sampleLbl;
        private System.Windows.Forms.TrackBar learningRateBar;
        private System.Windows.Forms.TrackBar momentumBar;
        private System.Windows.Forms.Label learningRateLbl;
        private System.Windows.Forms.Label momentumLbl;
        private System.Windows.Forms.Button rateTestBtn;
        private System.Windows.Forms.CheckBox deepNetworkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button advancedBtn;
        private System.Windows.Forms.Label advancedLbl;
        private System.Windows.Forms.RichTextBox Output;
    }
}

