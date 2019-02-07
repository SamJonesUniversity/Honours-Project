namespace ENP1
{
    partial class TestForm
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
            this.networkBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sampleBar = new System.Windows.Forms.TrackBar();
            this.sampleLbl = new System.Windows.Forms.Label();
            this.learningRateBar = new System.Windows.Forms.TrackBar();
            this.momentumBar = new System.Windows.Forms.TrackBar();
            this.learningRateLbl = new System.Windows.Forms.Label();
            this.momentumLbl = new System.Windows.Forms.Label();
            this.rateTestBtn = new System.Windows.Forms.Button();
            this.deepNetworkBox = new System.Windows.Forms.CheckBox();
            this.advancedBtn = new System.Windows.Forms.Button();
            this.advancedLbl = new System.Windows.Forms.Label();
            this.output = new System.Windows.Forms.RichTextBox();
            this.radBtnCrossVal = new System.Windows.Forms.RadioButton();
            this.radBtnSplit = new System.Windows.Forms.RadioButton();
            this.radBtnEncog = new System.Windows.Forms.RadioButton();
            this.radBtnAccord = new System.Windows.Forms.RadioButton();
            this.outputsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.networkSaveBtn = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.GroupBox();
            this.networkBox = new System.Windows.Forms.GroupBox();
            this.additionalBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsUpDown)).BeginInit();
            this.fileBox.SuspendLayout();
            this.networkBox.SuspendLayout();
            this.additionalBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // networkBtn
            // 
            this.networkBtn.Location = new System.Drawing.Point(6, 62);
            this.networkBtn.Name = "networkBtn";
            this.networkBtn.Size = new System.Drawing.Size(75, 22);
            this.networkBtn.TabIndex = 1;
            this.networkBtn.Text = "Test";
            this.networkBtn.UseVisualStyleBackColor = true;
            this.networkBtn.Click += new System.EventHandler(this.NetworkBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sampleBar
            // 
            this.sampleBar.Location = new System.Drawing.Point(3, 78);
            this.sampleBar.Maximum = 100;
            this.sampleBar.Minimum = 10;
            this.sampleBar.Name = "sampleBar";
            this.sampleBar.Size = new System.Drawing.Size(104, 45);
            this.sampleBar.SmallChange = 10;
            this.sampleBar.TabIndex = 4;
            this.sampleBar.TickFrequency = 10;
            this.sampleBar.Value = 10;
            this.sampleBar.Scroll += new System.EventHandler(this.SampleBar_Scroll);
            // 
            // sampleLbl
            // 
            this.sampleLbl.AutoSize = true;
            this.sampleLbl.Location = new System.Drawing.Point(112, 78);
            this.sampleLbl.Name = "sampleLbl";
            this.sampleLbl.Size = new System.Drawing.Size(94, 13);
            this.sampleLbl.TabIndex = 7;
            this.sampleLbl.Text = "Sample Data: 10%";
            // 
            // learningRateBar
            // 
            this.learningRateBar.LargeChange = 1;
            this.learningRateBar.Location = new System.Drawing.Point(106, 20);
            this.learningRateBar.Minimum = 1;
            this.learningRateBar.Name = "learningRateBar";
            this.learningRateBar.Size = new System.Drawing.Size(104, 45);
            this.learningRateBar.TabIndex = 5;
            this.learningRateBar.Value = 1;
            this.learningRateBar.Visible = false;
            this.learningRateBar.Scroll += new System.EventHandler(this.LearningRateBar_Scroll);
            // 
            // momentumBar
            // 
            this.momentumBar.LargeChange = 1;
            this.momentumBar.Location = new System.Drawing.Point(106, 59);
            this.momentumBar.Minimum = 1;
            this.momentumBar.Name = "momentumBar";
            this.momentumBar.Size = new System.Drawing.Size(104, 45);
            this.momentumBar.TabIndex = 6;
            this.momentumBar.Value = 1;
            this.momentumBar.Visible = false;
            this.momentumBar.Scroll += new System.EventHandler(this.MomentumBar_Scroll);
            // 
            // learningRateLbl
            // 
            this.learningRateLbl.AutoSize = true;
            this.learningRateLbl.Location = new System.Drawing.Point(216, 20);
            this.learningRateLbl.Name = "learningRateLbl";
            this.learningRateLbl.Size = new System.Drawing.Size(95, 13);
            this.learningRateLbl.TabIndex = 9;
            this.learningRateLbl.Text = "Learning Rate: 0.1";
            this.learningRateLbl.Visible = false;
            // 
            // momentumLbl
            // 
            this.momentumLbl.AutoSize = true;
            this.momentumLbl.Location = new System.Drawing.Point(216, 64);
            this.momentumLbl.Name = "momentumLbl";
            this.momentumLbl.Size = new System.Drawing.Size(80, 13);
            this.momentumLbl.TabIndex = 10;
            this.momentumLbl.Text = "Momentum: 0.1";
            this.momentumLbl.Visible = false;
            // 
            // rateTestBtn
            // 
            this.rateTestBtn.Location = new System.Drawing.Point(285, 112);
            this.rateTestBtn.Name = "rateTestBtn";
            this.rateTestBtn.Size = new System.Drawing.Size(83, 22);
            this.rateTestBtn.TabIndex = 11;
            this.rateTestBtn.Text = "Test Rates";
            this.rateTestBtn.UseVisualStyleBackColor = true;
            this.rateTestBtn.Click += new System.EventHandler(this.RateTestBtn_Click);
            // 
            // deepNetworkBox
            // 
            this.deepNetworkBox.AutoSize = true;
            this.deepNetworkBox.Location = new System.Drawing.Point(114, 106);
            this.deepNetworkBox.Name = "deepNetworkBox";
            this.deepNetworkBox.Size = new System.Drawing.Size(96, 17);
            this.deepNetworkBox.TabIndex = 12;
            this.deepNetworkBox.Text = "Deep Learning";
            this.deepNetworkBox.UseVisualStyleBackColor = true;
            this.deepNetworkBox.Visible = false;
            // 
            // advancedBtn
            // 
            this.advancedBtn.Location = new System.Drawing.Point(6, 94);
            this.advancedBtn.Name = "advancedBtn";
            this.advancedBtn.Size = new System.Drawing.Size(20, 22);
            this.advancedBtn.TabIndex = 14;
            this.advancedBtn.Text = "+";
            this.advancedBtn.UseVisualStyleBackColor = true;
            this.advancedBtn.Click += new System.EventHandler(this.AdvancedBtn_Click);
            // 
            // advancedLbl
            // 
            this.advancedLbl.AutoSize = true;
            this.advancedLbl.Location = new System.Drawing.Point(27, 99);
            this.advancedLbl.Name = "advancedLbl";
            this.advancedLbl.Size = new System.Drawing.Size(97, 13);
            this.advancedLbl.TabIndex = 15;
            this.advancedLbl.Text = "Advanced Settings";
            // 
            // output
            // 
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Cursor = System.Windows.Forms.Cursors.Default;
            this.output.Font = new System.Drawing.Font("Adobe Fangsong Std R", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(12, 12);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(932, 399);
            this.output.TabIndex = 16;
            this.output.Text = "";
            // 
            // radBtnCrossVal
            // 
            this.radBtnCrossVal.AutoSize = true;
            this.radBtnCrossVal.Location = new System.Drawing.Point(114, 55);
            this.radBtnCrossVal.Name = "radBtnCrossVal";
            this.radBtnCrossVal.Size = new System.Drawing.Size(100, 17);
            this.radBtnCrossVal.TabIndex = 17;
            this.radBtnCrossVal.Text = "Cross-Validation";
            this.radBtnCrossVal.UseVisualStyleBackColor = true;
            this.radBtnCrossVal.CheckedChanged += new System.EventHandler(this.RadBtnCrossVal_CheckedChanged);
            // 
            // radBtnSplit
            // 
            this.radBtnSplit.AutoSize = true;
            this.radBtnSplit.Checked = true;
            this.radBtnSplit.Location = new System.Drawing.Point(9, 55);
            this.radBtnSplit.Name = "radBtnSplit";
            this.radBtnSplit.Size = new System.Drawing.Size(103, 17);
            this.radBtnSplit.TabIndex = 18;
            this.radBtnSplit.TabStop = true;
            this.radBtnSplit.Text = "Percentage Split";
            this.radBtnSplit.UseVisualStyleBackColor = true;
            this.radBtnSplit.CheckedChanged += new System.EventHandler(this.RadBtnSplit_CheckedChanged);
            // 
            // radBtnEncog
            // 
            this.radBtnEncog.AutoSize = true;
            this.radBtnEncog.Checked = true;
            this.radBtnEncog.Location = new System.Drawing.Point(6, 20);
            this.radBtnEncog.Name = "radBtnEncog";
            this.radBtnEncog.Size = new System.Drawing.Size(56, 17);
            this.radBtnEncog.TabIndex = 25;
            this.radBtnEncog.TabStop = true;
            this.radBtnEncog.Text = "Encog";
            this.radBtnEncog.UseVisualStyleBackColor = true;
            // 
            // radBtnAccord
            // 
            this.radBtnAccord.AutoSize = true;
            this.radBtnAccord.Location = new System.Drawing.Point(6, 39);
            this.radBtnAccord.Name = "radBtnAccord";
            this.radBtnAccord.Size = new System.Drawing.Size(59, 17);
            this.radBtnAccord.TabIndex = 26;
            this.radBtnAccord.Text = "Accord";
            this.radBtnAccord.UseVisualStyleBackColor = true;
            // 
            // outputsUpDown
            // 
            this.outputsUpDown.Location = new System.Drawing.Point(101, 25);
            this.outputsUpDown.Name = "outputsUpDown";
            this.outputsUpDown.Size = new System.Drawing.Size(36, 20);
            this.outputsUpDown.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Network Outputs:";
            // 
            // networkSaveBtn
            // 
            this.networkSaveBtn.Location = new System.Drawing.Point(182, 20);
            this.networkSaveBtn.Name = "networkSaveBtn";
            this.networkSaveBtn.Size = new System.Drawing.Size(83, 22);
            this.networkSaveBtn.TabIndex = 29;
            this.networkSaveBtn.Text = "Save Network";
            this.networkSaveBtn.UseVisualStyleBackColor = true;
            this.networkSaveBtn.Click += new System.EventHandler(this.NetworkSaveBtn_Click);
            // 
            // fileBox
            // 
            this.fileBox.Controls.Add(this.label2);
            this.fileBox.Controls.Add(this.outputsUpDown);
            this.fileBox.Controls.Add(this.sampleBar);
            this.fileBox.Controls.Add(this.sampleLbl);
            this.fileBox.Controls.Add(this.radBtnSplit);
            this.fileBox.Controls.Add(this.radBtnCrossVal);
            this.fileBox.Location = new System.Drawing.Point(5, 417);
            this.fileBox.Name = "fileBox";
            this.fileBox.Size = new System.Drawing.Size(231, 141);
            this.fileBox.TabIndex = 30;
            this.fileBox.TabStop = false;
            this.fileBox.Text = "File Setup:";
            // 
            // networkBox
            // 
            this.networkBox.Controls.Add(this.radBtnAccord);
            this.networkBox.Controls.Add(this.radBtnEncog);
            this.networkBox.Controls.Add(this.networkBtn);
            this.networkBox.Controls.Add(this.learningRateBar);
            this.networkBox.Controls.Add(this.advancedLbl);
            this.networkBox.Controls.Add(this.momentumBar);
            this.networkBox.Controls.Add(this.advancedBtn);
            this.networkBox.Controls.Add(this.learningRateLbl);
            this.networkBox.Controls.Add(this.deepNetworkBox);
            this.networkBox.Controls.Add(this.momentumLbl);
            this.networkBox.Location = new System.Drawing.Point(242, 417);
            this.networkBox.Name = "networkBox";
            this.networkBox.Size = new System.Drawing.Size(323, 141);
            this.networkBox.TabIndex = 31;
            this.networkBox.TabStop = false;
            this.networkBox.Text = "Network Settings:";
            // 
            // additionalBox
            // 
            this.additionalBox.Controls.Add(this.label1);
            this.additionalBox.Controls.Add(this.nameTxt);
            this.additionalBox.Controls.Add(this.networkSaveBtn);
            this.additionalBox.Controls.Add(this.rateTestBtn);
            this.additionalBox.Location = new System.Drawing.Point(571, 417);
            this.additionalBox.Name = "additionalBox";
            this.additionalBox.Size = new System.Drawing.Size(374, 141);
            this.additionalBox.TabIndex = 32;
            this.additionalBox.TabStop = false;
            this.additionalBox.Text = "Additional Settings:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Network Name:";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(94, 21);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(82, 20);
            this.nameTxt.TabIndex = 30;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 563);
            this.Controls.Add(this.output);
            this.Controls.Add(this.fileBox);
            this.Controls.Add(this.networkBox);
            this.Controls.Add(this.additionalBox);
            this.Name = "TestForm";
            this.Text = "Test Window";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsUpDown)).EndInit();
            this.fileBox.ResumeLayout(false);
            this.fileBox.PerformLayout();
            this.networkBox.ResumeLayout(false);
            this.networkBox.PerformLayout();
            this.additionalBox.ResumeLayout(false);
            this.additionalBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button networkBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TrackBar sampleBar;
        private System.Windows.Forms.Label sampleLbl;
        private System.Windows.Forms.TrackBar learningRateBar;
        private System.Windows.Forms.TrackBar momentumBar;
        private System.Windows.Forms.Label learningRateLbl;
        private System.Windows.Forms.Label momentumLbl;
        private System.Windows.Forms.Button rateTestBtn;
        private System.Windows.Forms.CheckBox deepNetworkBox;
        private System.Windows.Forms.Button advancedBtn;
        private System.Windows.Forms.Label advancedLbl;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.RadioButton radBtnCrossVal;
        private System.Windows.Forms.RadioButton radBtnSplit;
        private System.Windows.Forms.RadioButton radBtnEncog;
        private System.Windows.Forms.RadioButton radBtnAccord;
        private System.Windows.Forms.NumericUpDown outputsUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button networkSaveBtn;
        private System.Windows.Forms.GroupBox fileBox;
        private System.Windows.Forms.GroupBox networkBox;
        private System.Windows.Forms.GroupBox additionalBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTxt;
    }
}

