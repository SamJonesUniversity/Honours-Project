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
            this.SuspendLayout();
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(12, 503);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(44, 23);
            this.fileBtn.TabIndex = 0;
            this.fileBtn.Text = "File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.FileBtn_Click);
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.Location = new System.Drawing.Point(366, 12);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(397, 485);
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
            this.inputTxt.Location = new System.Drawing.Point(769, 12);
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
            this.networkBtn.Location = new System.Drawing.Point(152, 502);
            this.networkBtn.Name = "networkBtn";
            this.networkBtn.Size = new System.Drawing.Size(75, 23);
            this.networkBtn.TabIndex = 7;
            this.networkBtn.Text = "Compute";
            this.networkBtn.UseVisualStyleBackColor = true;
            this.networkBtn.Click += new System.EventHandler(this.networkBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 537);
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
    }
}