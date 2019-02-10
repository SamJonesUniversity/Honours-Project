namespace ENP1
{
    partial class EntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
            this.testBtn = new System.Windows.Forms.Button();
            this.inputBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(159, 329);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(96, 23);
            this.testBtn.TabIndex = 0;
            this.testBtn.Text = "Network Creater";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // inputBtn
            // 
            this.inputBtn.Location = new System.Drawing.Point(499, 329);
            this.inputBtn.Name = "inputBtn";
            this.inputBtn.Size = new System.Drawing.Size(98, 23);
            this.inputBtn.TabIndex = 1;
            this.inputBtn.Text = "Output Predicter";
            this.inputBtn.UseVisualStyleBackColor = true;
            this.inputBtn.Click += new System.EventHandler(this.InputBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(252, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "CSV Neural Network Predicter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(32, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 117);
            this.label2.TabIndex = 3;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(385, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 65);
            this.label3.TabIndex = 4;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 413);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputBtn);
            this.Controls.Add(this.testBtn);
            this.Name = "EntryForm";
            this.Text = "EntryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button inputBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}