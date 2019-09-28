namespace School_Management_System.Student
{
    partial class LCDetails
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
            this.ExitButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ComboBox();
            this.ReasonForLeaving = new System.Windows.Forms.ComboBox();
            this.Remark = new System.Windows.Forms.ComboBox();
            this.Conduct = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(318, 160);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(97, 33);
            this.ExitButton.TabIndex = 78;
            this.ExitButton.Text = "&Cancel";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(117, 160);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(97, 33);
            this.DeleteButton.TabIndex = 77;
            this.DeleteButton.Text = "&Proceed";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 80;
            this.label2.Text = "Cunduct : ";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(30, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 19);
            this.label20.TabIndex = 79;
            this.label20.Text = "Progress : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 19);
            this.label1.TabIndex = 81;
            this.label1.Text = "Reason For Leaving School : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 19);
            this.label3.TabIndex = 81;
            this.label3.Text = "Remark : ";
            // 
            // Progress
            // 
            this.Progress.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Progress.FormattingEnabled = true;
            this.Progress.Items.AddRange(new object[] {
            "Excellent",
            "Very Good",
            "Good",
            "Average",
            "Poor",
            "Very Poor"});
            this.Progress.Location = new System.Drawing.Point(224, 21);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(278, 27);
            this.Progress.TabIndex = 82;
            // 
            // ReasonForLeaving
            // 
            this.ReasonForLeaving.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonForLeaving.FormattingEnabled = true;
            this.ReasonForLeaving.Items.AddRange(new object[] {
            "Going Else Where",
            "Taking a Educational Gap"});
            this.ReasonForLeaving.Location = new System.Drawing.Point(224, 87);
            this.ReasonForLeaving.Name = "ReasonForLeaving";
            this.ReasonForLeaving.Size = new System.Drawing.Size(278, 27);
            this.ReasonForLeaving.TabIndex = 83;
            // 
            // Remark
            // 
            this.Remark.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remark.FormattingEnabled = true;
            this.Remark.Items.AddRange(new object[] {
            "PASSED S.S.C. EXAM "});
            this.Remark.Location = new System.Drawing.Point(224, 120);
            this.Remark.Name = "Remark";
            this.Remark.Size = new System.Drawing.Size(278, 27);
            this.Remark.TabIndex = 84;
            // 
            // Conduct
            // 
            this.Conduct.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Conduct.FormattingEnabled = true;
            this.Conduct.Items.AddRange(new object[] {
            "Excellent",
            "Very Good",
            "Good",
            "Average",
            "Poor",
            "Very Poor"});
            this.Conduct.Location = new System.Drawing.Point(224, 54);
            this.Conduct.Name = "Conduct";
            this.Conduct.Size = new System.Drawing.Size(278, 27);
            this.Conduct.TabIndex = 85;
            // 
            // LCDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 202);
            this.Controls.Add(this.Conduct);
            this.Controls.Add(this.Remark);
            this.Controls.Add(this.ReasonForLeaving);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label20);
            this.Name = "LCDetails";
            this.Text = "LCDetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Progress;
        private System.Windows.Forms.ComboBox ReasonForLeaving;
        private System.Windows.Forms.ComboBox Remark;
        private System.Windows.Forms.ComboBox Conduct;

    }
}