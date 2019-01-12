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
            this.Flowers = new System.Windows.Forms.ListBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Flowers
            // 
            this.Flowers.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Flowers.FormattingEnabled = true;
            this.Flowers.ItemHeight = 31;
            this.Flowers.Location = new System.Drawing.Point(12, 12);
            this.Flowers.Name = "Flowers";
            this.Flowers.Size = new System.Drawing.Size(1133, 376);
            this.Flowers.TabIndex = 8;
            // 
            // engocBtn
            // 
            this.engocBtn.Location = new System.Drawing.Point(12, 406);
            this.engocBtn.Name = "engocBtn";
            this.engocBtn.Size = new System.Drawing.Size(75, 23);
            this.engocBtn.TabIndex = 1;
            this.engocBtn.Text = "Encog";
            this.engocBtn.UseVisualStyleBackColor = true;
            this.engocBtn.Click += new System.EventHandler(this.engocBtn_Click);
            // 
            // accordBtn
            // 
            this.accordBtn.Location = new System.Drawing.Point(93, 406);
            this.accordBtn.Name = "accordBtn";
            this.accordBtn.Size = new System.Drawing.Size(75, 23);
            this.accordBtn.TabIndex = 2;
            this.accordBtn.Text = "Accord.NET";
            this.accordBtn.UseVisualStyleBackColor = true;
            this.accordBtn.Click += new System.EventHandler(this.accordBtn_Click);
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(175, 406);
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
            this.sampleBar.Location = new System.Drawing.Point(256, 406);
            this.sampleBar.Name = "sampleBar";
            this.sampleBar.Size = new System.Drawing.Size(104, 45);
            this.sampleBar.TabIndex = 4;
            this.sampleBar.Scroll += new System.EventHandler(this.sampleBar_Scroll);
            // 
            // sampleLbl
            // 
            this.sampleLbl.AutoSize = true;
            this.sampleLbl.Location = new System.Drawing.Point(366, 411);
            this.sampleLbl.Name = "sampleLbl";
            this.sampleLbl.Size = new System.Drawing.Size(21, 13);
            this.sampleLbl.TabIndex = 7;
            this.sampleLbl.Text = "0%";
            // 
            // learningRateBar
            // 
            this.learningRateBar.LargeChange = 1;
            this.learningRateBar.Location = new System.Drawing.Point(410, 406);
            this.learningRateBar.Name = "learningRateBar";
            this.learningRateBar.Size = new System.Drawing.Size(104, 45);
            this.learningRateBar.TabIndex = 5;
            this.learningRateBar.Scroll += new System.EventHandler(this.learningRateBar_Scroll);
            // 
            // momentumBar
            // 
            this.momentumBar.LargeChange = 1;
            this.momentumBar.Location = new System.Drawing.Point(556, 406);
            this.momentumBar.Name = "momentumBar";
            this.momentumBar.Size = new System.Drawing.Size(104, 45);
            this.momentumBar.TabIndex = 6;
            this.momentumBar.Scroll += new System.EventHandler(this.momentumBar_Scroll);
            // 
            // learningRateLbl
            // 
            this.learningRateLbl.AutoSize = true;
            this.learningRateLbl.Location = new System.Drawing.Point(515, 411);
            this.learningRateLbl.Name = "learningRateLbl";
            this.learningRateLbl.Size = new System.Drawing.Size(22, 13);
            this.learningRateLbl.TabIndex = 9;
            this.learningRateLbl.Text = "0.0";
            // 
            // momentumLbl
            // 
            this.momentumLbl.AutoSize = true;
            this.momentumLbl.Location = new System.Drawing.Point(666, 411);
            this.momentumLbl.Name = "momentumLbl";
            this.momentumLbl.Size = new System.Drawing.Size(22, 13);
            this.momentumLbl.TabIndex = 10;
            this.momentumLbl.Text = "0.0";
            // 
            // rateTestBtn
            // 
            this.rateTestBtn.Location = new System.Drawing.Point(712, 406);
            this.rateTestBtn.Name = "rateTestBtn";
            this.rateTestBtn.Size = new System.Drawing.Size(75, 23);
            this.rateTestBtn.TabIndex = 11;
            this.rateTestBtn.Text = "Test Rates";
            this.rateTestBtn.UseVisualStyleBackColor = true;
            this.rateTestBtn.Click += new System.EventHandler(this.rateTestBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 438);
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
            this.Controls.Add(this.Flowers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Flowers;
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
    }
}

