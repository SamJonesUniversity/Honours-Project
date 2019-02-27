namespace ENP1
{
    partial class InputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            this.fileBtn = new System.Windows.Forms.Button();
            this.inputListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileLbl = new System.Windows.Forms.Label();
            this.inputTxt = new System.Windows.Forms.TextBox();
            this.networkListBox = new System.Windows.Forms.ListBox();
            this.networkBtn = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.networkSelectedBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.manualBtn = new System.Windows.Forms.Button();
            this.csvBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(262, 70);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(86, 23);
            this.fileBtn.TabIndex = 0;
            this.fileBtn.Text = "Select File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.Location = new System.Drawing.Point(11, 29);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(216, 264);
            this.inputListBox.TabIndex = 1;
            this.inputListBox.SelectedIndexChanged += new System.EventHandler(this.InputListBox_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fileLbl
            // 
            this.fileLbl.AutoSize = true;
            this.fileLbl.Location = new System.Drawing.Point(206, 294);
            this.fileLbl.Name = "fileLbl";
            this.fileLbl.Size = new System.Drawing.Size(0, 13);
            this.fileLbl.TabIndex = 2;
            // 
            // inputTxt
            // 
            this.inputTxt.Location = new System.Drawing.Point(240, 29);
            this.inputTxt.Name = "inputTxt";
            this.inputTxt.Size = new System.Drawing.Size(201, 20);
            this.inputTxt.TabIndex = 5;
            this.inputTxt.TextChanged += new System.EventHandler(this.InputTxt_TextChanged);
            // 
            // networkListBox
            // 
            this.networkListBox.FormattingEnabled = true;
            this.networkListBox.Location = new System.Drawing.Point(11, 32);
            this.networkListBox.Name = "networkListBox";
            this.networkListBox.Size = new System.Drawing.Size(255, 173);
            this.networkListBox.TabIndex = 6;
            this.networkListBox.SelectedIndexChanged += new System.EventHandler(this.NetworkListBox_SelectedIndexChanged);
            // 
            // networkBtn
            // 
            this.networkBtn.Location = new System.Drawing.Point(354, 298);
            this.networkBtn.Name = "networkBtn";
            this.networkBtn.Size = new System.Drawing.Size(87, 23);
            this.networkBtn.TabIndex = 7;
            this.networkBtn.Text = "Predict Output";
            this.networkBtn.UseVisualStyleBackColor = true;
            this.networkBtn.Click += new System.EventHandler(this.NetworkBtn_Click);
            // 
            // output
            // 
            this.output.BackColor = System.Drawing.Color.White;
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.output.Location = new System.Drawing.Point(11, 28);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(517, 274);
            this.output.TabIndex = 9;
            this.output.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Enter your input here:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Predicted outputs:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(444, 307);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(85, 23);
            this.saveBtn.TabIndex = 12;
            this.saveBtn.Text = "Save Output";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.fileBtn);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 104);
            this.panel1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 39);
            this.label4.TabIndex = 1;
            this.label4.Text = "Please select the JSON file used to store the network you have created. \r\nThis fi" +
    "le will be stored in the \"networks\" folder in the same directory \r\nas the CSV yo" +
    "u used for creating a network.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.networkSelectedBtn);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.networkListBox);
            this.panel2.Location = new System.Drawing.Point(393, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 242);
            this.panel2.TabIndex = 14;
            // 
            // networkSelectedBtn
            // 
            this.networkSelectedBtn.Location = new System.Drawing.Point(192, 209);
            this.networkSelectedBtn.Name = "networkSelectedBtn";
            this.networkSelectedBtn.Size = new System.Drawing.Size(75, 23);
            this.networkSelectedBtn.TabIndex = 8;
            this.networkSelectedBtn.Text = "Next";
            this.networkSelectedBtn.UseVisualStyleBackColor = true;
            this.networkSelectedBtn.Click += new System.EventHandler(this.NetworkSelectedBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Your Desired Network:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.inputListBox);
            this.panel4.Controls.Add(this.networkBtn);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.inputTxt);
            this.panel4.Location = new System.Drawing.Point(12, 294);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(450, 332);
            this.panel4.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input headings:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.output);
            this.panel5.Controls.Add(this.saveBtn);
            this.panel5.Location = new System.Drawing.Point(468, 295);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(540, 340);
            this.panel5.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.manualBtn);
            this.panel3.Controls.Add(this.csvBtn);
            this.panel3.Location = new System.Drawing.Point(699, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 113);
            this.panel3.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(286, 52);
            this.label6.TabIndex = 11;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // manualBtn
            // 
            this.manualBtn.Location = new System.Drawing.Point(131, 80);
            this.manualBtn.Name = "manualBtn";
            this.manualBtn.Size = new System.Drawing.Size(78, 23);
            this.manualBtn.TabIndex = 10;
            this.manualBtn.Text = "Manual Input";
            this.manualBtn.UseVisualStyleBackColor = true;
            this.manualBtn.Click += new System.EventHandler(this.ManualBtn_Click);
            // 
            // csvBtn
            // 
            this.csvBtn.Location = new System.Drawing.Point(215, 80);
            this.csvBtn.Name = "csvBtn";
            this.csvBtn.Size = new System.Drawing.Size(78, 23);
            this.csvBtn.TabIndex = 9;
            this.csvBtn.Text = "CSV Input";
            this.csvBtn.UseVisualStyleBackColor = true;
            this.csvBtn.Click += new System.EventHandler(this.CsvBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(1003, 12);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 18;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 651);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fileLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InputForm";
            this.Text = "Output Predictor";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.ListBox inputListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label fileLbl;
        private System.Windows.Forms.TextBox inputTxt;
        private System.Windows.Forms.ListBox networkListBox;
        private System.Windows.Forms.Button networkBtn;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button networkSelectedBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button manualBtn;
        private System.Windows.Forms.Button csvBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}