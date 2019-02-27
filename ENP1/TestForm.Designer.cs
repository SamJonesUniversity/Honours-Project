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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
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
            this.neuronsBar = new System.Windows.Forms.TrackBar();
            this.layersBar = new System.Windows.Forms.TrackBar();
            this.neuronsLbl = new System.Windows.Forms.Label();
            this.layersLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.advancedFileBtn = new System.Windows.Forms.Button();
            this.advancedFileLbl = new System.Windows.Forms.Label();
            this.valtypeLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fileBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.recommendedBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.loadedLbl = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.backBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trainingLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuronsBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layersBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // networkBtn
            // 
            this.networkBtn.Location = new System.Drawing.Point(445, 229);
            this.networkBtn.Name = "networkBtn";
            this.networkBtn.Size = new System.Drawing.Size(89, 22);
            this.networkBtn.TabIndex = 1;
            this.networkBtn.Text = "Continue";
            this.networkBtn.UseVisualStyleBackColor = true;
            this.networkBtn.Click += new System.EventHandler(this.NetworkBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sampleBar
            // 
            this.sampleBar.Location = new System.Drawing.Point(2, 134);
            this.sampleBar.Maximum = 100;
            this.sampleBar.Minimum = 10;
            this.sampleBar.Name = "sampleBar";
            this.sampleBar.Size = new System.Drawing.Size(104, 45);
            this.sampleBar.SmallChange = 10;
            this.sampleBar.TabIndex = 4;
            this.sampleBar.TickFrequency = 10;
            this.sampleBar.Value = 10;
            this.sampleBar.Visible = false;
            this.sampleBar.Scroll += new System.EventHandler(this.SampleBar_Scroll);
            // 
            // sampleLbl
            // 
            this.sampleLbl.AutoSize = true;
            this.sampleLbl.Location = new System.Drawing.Point(112, 134);
            this.sampleLbl.Name = "sampleLbl";
            this.sampleLbl.Size = new System.Drawing.Size(94, 13);
            this.sampleLbl.TabIndex = 7;
            this.sampleLbl.Text = "Sample Data: 10%";
            this.sampleLbl.Visible = false;
            // 
            // learningRateBar
            // 
            this.learningRateBar.LargeChange = 1;
            this.learningRateBar.Location = new System.Drawing.Point(109, 111);
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
            this.momentumBar.Location = new System.Drawing.Point(109, 150);
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
            this.learningRateLbl.Location = new System.Drawing.Point(219, 111);
            this.learningRateLbl.Name = "learningRateLbl";
            this.learningRateLbl.Size = new System.Drawing.Size(95, 13);
            this.learningRateLbl.TabIndex = 9;
            this.learningRateLbl.Text = "Learning Rate: 0.1";
            this.learningRateLbl.Visible = false;
            // 
            // momentumLbl
            // 
            this.momentumLbl.AutoSize = true;
            this.momentumLbl.Location = new System.Drawing.Point(219, 155);
            this.momentumLbl.Name = "momentumLbl";
            this.momentumLbl.Size = new System.Drawing.Size(80, 13);
            this.momentumLbl.TabIndex = 10;
            this.momentumLbl.Text = "Momentum: 0.1";
            this.momentumLbl.Visible = false;
            // 
            // rateTestBtn
            // 
            this.rateTestBtn.Location = new System.Drawing.Point(445, 206);
            this.rateTestBtn.Name = "rateTestBtn";
            this.rateTestBtn.Size = new System.Drawing.Size(89, 22);
            this.rateTestBtn.TabIndex = 11;
            this.rateTestBtn.Text = "Determine Best";
            this.rateTestBtn.UseVisualStyleBackColor = true;
            this.rateTestBtn.Click += new System.EventHandler(this.RateTestBtn_Click);
            // 
            // deepNetworkBox
            // 
            this.deepNetworkBox.AutoSize = true;
            this.deepNetworkBox.Location = new System.Drawing.Point(117, 197);
            this.deepNetworkBox.Name = "deepNetworkBox";
            this.deepNetworkBox.Size = new System.Drawing.Size(96, 17);
            this.deepNetworkBox.TabIndex = 12;
            this.deepNetworkBox.Text = "Deep Learning";
            this.deepNetworkBox.UseVisualStyleBackColor = true;
            this.deepNetworkBox.Visible = false;
            this.deepNetworkBox.CheckedChanged += new System.EventHandler(this.DeepNetworkBox_CheckedChanged);
            // 
            // advancedBtn
            // 
            this.advancedBtn.Location = new System.Drawing.Point(10, 185);
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
            this.advancedLbl.Location = new System.Drawing.Point(32, 190);
            this.advancedLbl.Name = "advancedLbl";
            this.advancedLbl.Size = new System.Drawing.Size(97, 13);
            this.advancedLbl.TabIndex = 15;
            this.advancedLbl.Text = "Advanced Settings";
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.White;
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Cursor = System.Windows.Forms.Cursors.Default;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(9, 10);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(1181, 534);
            this.output.TabIndex = 16;
            this.output.Text = "";
            // 
            // radBtnCrossVal
            // 
            this.radBtnCrossVal.AutoSize = true;
            this.radBtnCrossVal.Location = new System.Drawing.Point(118, 111);
            this.radBtnCrossVal.Name = "radBtnCrossVal";
            this.radBtnCrossVal.Size = new System.Drawing.Size(100, 17);
            this.radBtnCrossVal.TabIndex = 17;
            this.radBtnCrossVal.Text = "Cross-Validation";
            this.radBtnCrossVal.UseVisualStyleBackColor = true;
            this.radBtnCrossVal.Visible = false;
            this.radBtnCrossVal.CheckedChanged += new System.EventHandler(this.RadBtnCrossVal_CheckedChanged);
            // 
            // radBtnSplit
            // 
            this.radBtnSplit.AutoSize = true;
            this.radBtnSplit.Checked = true;
            this.radBtnSplit.Location = new System.Drawing.Point(9, 111);
            this.radBtnSplit.Name = "radBtnSplit";
            this.radBtnSplit.Size = new System.Drawing.Size(103, 17);
            this.radBtnSplit.TabIndex = 18;
            this.radBtnSplit.TabStop = true;
            this.radBtnSplit.Text = "Percentage Split";
            this.radBtnSplit.UseVisualStyleBackColor = true;
            this.radBtnSplit.Visible = false;
            this.radBtnSplit.CheckedChanged += new System.EventHandler(this.RadBtnSplit_CheckedChanged);
            // 
            // radBtnEncog
            // 
            this.radBtnEncog.AutoSize = true;
            this.radBtnEncog.Checked = true;
            this.radBtnEncog.Location = new System.Drawing.Point(10, 111);
            this.radBtnEncog.Name = "radBtnEncog";
            this.radBtnEncog.Size = new System.Drawing.Size(56, 17);
            this.radBtnEncog.TabIndex = 25;
            this.radBtnEncog.TabStop = true;
            this.radBtnEncog.Text = "Encog";
            this.radBtnEncog.UseVisualStyleBackColor = true;
            this.radBtnEncog.CheckedChanged += new System.EventHandler(this.RadBtnEncog_CheckedChanged);
            // 
            // radBtnAccord
            // 
            this.radBtnAccord.AutoSize = true;
            this.radBtnAccord.Location = new System.Drawing.Point(10, 130);
            this.radBtnAccord.Name = "radBtnAccord";
            this.radBtnAccord.Size = new System.Drawing.Size(59, 17);
            this.radBtnAccord.TabIndex = 26;
            this.radBtnAccord.Text = "Accord";
            this.radBtnAccord.UseVisualStyleBackColor = true;
            // 
            // outputsUpDown
            // 
            this.outputsUpDown.Location = new System.Drawing.Point(111, 27);
            this.outputsUpDown.Name = "outputsUpDown";
            this.outputsUpDown.Size = new System.Drawing.Size(36, 20);
            this.outputsUpDown.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Number of Outputs:";
            // 
            // networkSaveBtn
            // 
            this.networkSaveBtn.Location = new System.Drawing.Point(1104, 559);
            this.networkSaveBtn.Name = "networkSaveBtn";
            this.networkSaveBtn.Size = new System.Drawing.Size(83, 22);
            this.networkSaveBtn.TabIndex = 29;
            this.networkSaveBtn.Text = "Save Network";
            this.networkSaveBtn.UseVisualStyleBackColor = true;
            this.networkSaveBtn.Click += new System.EventHandler(this.NetworkSaveBtn_Click);
            // 
            // neuronsBar
            // 
            this.neuronsBar.Location = new System.Drawing.Point(320, 110);
            this.neuronsBar.Maximum = 50;
            this.neuronsBar.Minimum = 5;
            this.neuronsBar.Name = "neuronsBar";
            this.neuronsBar.Size = new System.Drawing.Size(104, 45);
            this.neuronsBar.SmallChange = 5;
            this.neuronsBar.TabIndex = 27;
            this.neuronsBar.TickFrequency = 5;
            this.neuronsBar.Value = 5;
            this.neuronsBar.Visible = false;
            this.neuronsBar.Scroll += new System.EventHandler(this.NeuronsBar_Scroll);
            // 
            // layersBar
            // 
            this.layersBar.LargeChange = 1;
            this.layersBar.Location = new System.Drawing.Point(320, 149);
            this.layersBar.Maximum = 5;
            this.layersBar.Minimum = 1;
            this.layersBar.Name = "layersBar";
            this.layersBar.Size = new System.Drawing.Size(104, 45);
            this.layersBar.TabIndex = 28;
            this.layersBar.Value = 1;
            this.layersBar.Visible = false;
            this.layersBar.Scroll += new System.EventHandler(this.LayersBar_Scroll);
            // 
            // neuronsLbl
            // 
            this.neuronsLbl.AutoSize = true;
            this.neuronsLbl.Location = new System.Drawing.Point(430, 110);
            this.neuronsLbl.Name = "neuronsLbl";
            this.neuronsLbl.Size = new System.Drawing.Size(59, 13);
            this.neuronsLbl.TabIndex = 29;
            this.neuronsLbl.Text = "Neurons: 1";
            this.neuronsLbl.Visible = false;
            // 
            // layersLbl
            // 
            this.layersLbl.AutoSize = true;
            this.layersLbl.Location = new System.Drawing.Point(430, 154);
            this.layersLbl.Name = "layersLbl";
            this.layersLbl.Size = new System.Drawing.Size(50, 13);
            this.layersLbl.TabIndex = 30;
            this.layersLbl.Text = "Layers: 1";
            this.layersLbl.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(930, 563);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Network Name:";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(1015, 560);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(82, 20);
            this.nameTxt.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.advancedFileBtn);
            this.panel1.Controls.Add(this.advancedFileLbl);
            this.panel1.Controls.Add(this.valtypeLbl);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.fileBtn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.outputsUpDown);
            this.panel1.Controls.Add(this.radBtnCrossVal);
            this.panel1.Controls.Add(this.sampleBar);
            this.panel1.Controls.Add(this.radBtnSplit);
            this.panel1.Controls.Add(this.sampleLbl);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 224);
            this.panel1.TabIndex = 33;
            // 
            // advancedFileBtn
            // 
            this.advancedFileBtn.Location = new System.Drawing.Point(9, 56);
            this.advancedFileBtn.Name = "advancedFileBtn";
            this.advancedFileBtn.Size = new System.Drawing.Size(20, 22);
            this.advancedFileBtn.TabIndex = 38;
            this.advancedFileBtn.Text = "+";
            this.advancedFileBtn.UseVisualStyleBackColor = true;
            this.advancedFileBtn.Click += new System.EventHandler(this.AdvancedFileBtn_Click);
            // 
            // advancedFileLbl
            // 
            this.advancedFileLbl.AutoSize = true;
            this.advancedFileLbl.Location = new System.Drawing.Point(34, 61);
            this.advancedFileLbl.Name = "advancedFileLbl";
            this.advancedFileLbl.Size = new System.Drawing.Size(97, 13);
            this.advancedFileLbl.TabIndex = 39;
            this.advancedFileLbl.Text = "Advanced Settings";
            // 
            // valtypeLbl
            // 
            this.valtypeLbl.AutoSize = true;
            this.valtypeLbl.Location = new System.Drawing.Point(6, 95);
            this.valtypeLbl.Name = "valtypeLbl";
            this.valtypeLbl.Size = new System.Drawing.Size(83, 13);
            this.valtypeLbl.TabIndex = 37;
            this.valtypeLbl.Text = "Validation Type:";
            this.valtypeLbl.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "File Set-up";
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(148, 190);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(75, 23);
            this.fileBtn.TabIndex = 29;
            this.fileBtn.Text = "Select File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.recommendedBtn);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.loadedLbl);
            this.panel2.Controls.Add(this.neuronsBar);
            this.panel2.Controls.Add(this.networkBtn);
            this.panel2.Controls.Add(this.rateTestBtn);
            this.panel2.Controls.Add(this.layersBar);
            this.panel2.Controls.Add(this.momentumLbl);
            this.panel2.Controls.Add(this.neuronsLbl);
            this.panel2.Controls.Add(this.deepNetworkBox);
            this.panel2.Controls.Add(this.layersLbl);
            this.panel2.Controls.Add(this.learningRateLbl);
            this.panel2.Controls.Add(this.radBtnAccord);
            this.panel2.Controls.Add(this.advancedBtn);
            this.panel2.Controls.Add(this.radBtnEncog);
            this.panel2.Controls.Add(this.momentumBar);
            this.panel2.Controls.Add(this.advancedLbl);
            this.panel2.Controls.Add(this.learningRateBar);
            this.panel2.Location = new System.Drawing.Point(251, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(546, 262);
            this.panel2.TabIndex = 34;
            // 
            // recommendedBtn
            // 
            this.recommendedBtn.Location = new System.Drawing.Point(445, 183);
            this.recommendedBtn.Name = "recommendedBtn";
            this.recommendedBtn.Size = new System.Drawing.Size(89, 22);
            this.recommendedBtn.TabIndex = 34;
            this.recommendedBtn.Text = "Recommended";
            this.recommendedBtn.UseVisualStyleBackColor = true;
            this.recommendedBtn.Click += new System.EventHandler(this.RecommendedBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(503, 52);
            this.label6.TabIndex = 33;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "Network Settings";
            // 
            // loadedLbl
            // 
            this.loadedLbl.AutoSize = true;
            this.loadedLbl.Location = new System.Drawing.Point(9, 92);
            this.loadedLbl.Name = "loadedLbl";
            this.loadedLbl.Size = new System.Drawing.Size(0, 13);
            this.loadedLbl.TabIndex = 31;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.nameTxt);
            this.panel3.Controls.Add(this.networkSaveBtn);
            this.panel3.Controls.Add(this.output);
            this.panel3.Location = new System.Drawing.Point(12, 280);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1199, 592);
            this.panel3.TabIndex = 35;
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(803, 12);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 36;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.trainingLbl);
            this.panel4.Location = new System.Drawing.Point(1217, 280);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(357, 211);
            this.panel4.TabIndex = 37;
            // 
            // trainingLbl
            // 
            this.trainingLbl.AutoSize = true;
            this.trainingLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trainingLbl.Location = new System.Drawing.Point(48, 87);
            this.trainingLbl.Name = "trainingLbl";
            this.trainingLbl.Size = new System.Drawing.Size(258, 31);
            this.trainingLbl.TabIndex = 0;
            this.trainingLbl.Text = "Training Network. . .";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 881);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TestForm";
            this.Text = "Network Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sampleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentumBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuronsBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layersBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TrackBar neuronsBar;
        private System.Windows.Forms.TrackBar layersBar;
        private System.Windows.Forms.Label neuronsLbl;
        private System.Windows.Forms.Label layersLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.Label loadedLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label valtypeLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label trainingLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button advancedFileBtn;
        private System.Windows.Forms.Label advancedFileLbl;
        private System.Windows.Forms.Button recommendedBtn;
    }
}

