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
            this.fileBtn = new System.Windows.Forms.Button();
            this.inputListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fileLbl = new System.Windows.Forms.Label();
            this.inputTxt = new System.Windows.Forms.TextBox();
            this.networkListBox = new System.Windows.Forms.ListBox();
            this.networkBtn = new System.Windows.Forms.Button();
            this.csvBox = new System.Windows.Forms.CheckBox();
            this.output = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(12, 503);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(86, 23);
            this.fileBtn.TabIndex = 0;
            this.fileBtn.Text = "Network File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.Location = new System.Drawing.Point(366, 12);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(366, 485);
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
            this.inputTxt.Location = new System.Drawing.Point(769, 28);
            this.inputTxt.Name = "inputTxt";
            this.inputTxt.Size = new System.Drawing.Size(261, 20);
            this.inputTxt.TabIndex = 5;
            this.inputTxt.TextChanged += new System.EventHandler(this.InputTxt_TextChanged);
            // 
            // networkListBox
            // 
            this.networkListBox.FormattingEnabled = true;
            this.networkListBox.Location = new System.Drawing.Point(12, 12);
            this.networkListBox.Name = "networkListBox";
            this.networkListBox.Size = new System.Drawing.Size(348, 485);
            this.networkListBox.TabIndex = 6;
            this.networkListBox.SelectedIndexChanged += new System.EventHandler(this.NetworkListBox_SelectedIndexChanged);
            // 
            // networkBtn
            // 
            this.networkBtn.Location = new System.Drawing.Point(852, 503);
            this.networkBtn.Name = "networkBtn";
            this.networkBtn.Size = new System.Drawing.Size(87, 23);
            this.networkBtn.TabIndex = 7;
            this.networkBtn.Text = "Predict Output";
            this.networkBtn.UseVisualStyleBackColor = true;
            this.networkBtn.Click += new System.EventHandler(this.NetworkBtn_Click);
            // 
            // csvBox
            // 
            this.csvBox.AutoSize = true;
            this.csvBox.Location = new System.Drawing.Point(104, 507);
            this.csvBox.Name = "csvBox";
            this.csvBox.Size = new System.Drawing.Size(69, 17);
            this.csvBox.TabIndex = 8;
            this.csvBox.Text = "csv input";
            this.csvBox.UseVisualStyleBackColor = true;
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(769, 73);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(261, 424);
            this.output.TabIndex = 9;
            this.output.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(766, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Input:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(769, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Output:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(945, 503);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(85, 23);
            this.saveBtn.TabIndex = 12;
            this.saveBtn.Text = "Save Output";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 537);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.output);
            this.Controls.Add(this.csvBox);
            this.Controls.Add(this.networkBtn);
            this.Controls.Add(this.networkListBox);
            this.Controls.Add(this.inputTxt);
            this.Controls.Add(this.fileLbl);
            this.Controls.Add(this.inputListBox);
            this.Controls.Add(this.fileBtn);
            this.Name = "InputForm";
            this.Text = "InputForm";
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
        private System.Windows.Forms.CheckBox csvBox;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveBtn;
    }
}