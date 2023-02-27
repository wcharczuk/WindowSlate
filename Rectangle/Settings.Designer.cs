namespace Rectangle
{
    partial class Settings
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
            groupBox1 = new GroupBox();
            helpText = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(helpText);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(426, 426);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Help";
            // 
            // helpText
            // 
            helpText.AutoSize = true;
            helpText.Location = new Point(6, 19);
            helpText.Name = "helpText";
            helpText.Size = new Size(355, 15);
            helpText.TabIndex = 0;
            helpText.Text = "In this section we'll go over the specific hotkeys and what they do:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 74);
            label1.Name = "label1";
            label1.Size = new Size(149, 15);
            label1.TabIndex = 1;
            label1.Text = "Top Left - ctrl+windows+H\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 89);
            label2.Name = "label2";
            label2.Size = new Size(154, 15);
            label2.TabIndex = 2;
            label2.Text = "Top Right - ctrl+windows+L";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 104);
            label3.Name = "label3";
            label3.Size = new Size(165, 15);
            label3.TabIndex = 3;
            label3.Text = "Bottom Left - ctrl+windows+J";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 119);
            label4.Name = "label4";
            label4.Size = new Size(176, 15);
            label4.TabIndex = 4;
            label4.Text = "Bottom Right - ctrl+windows+K";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 184);
            label5.Name = "label5";
            label5.Size = new Size(118, 15);
            label5.TabIndex = 5;
            label5.Text = "Left Half - ctrl+alt+H\r\n";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 199);
            label6.Name = "label6";
            label6.Size = new Size(123, 15);
            label6.TabIndex = 6;
            label6.Text = "Right Half - ctrl+alt+L\r\n";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 169);
            label7.Name = "label7";
            label7.Size = new Size(122, 15);
            label7.TabIndex = 7;
            label7.Text = "Maximize - ctrl+alt+K";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 241);
            label8.Name = "label8";
            label8.Size = new Size(376, 15);
            label8.TabIndex = 8;
            label8.Text = "The half sized modes will alternate between 1/3rd, 1/2, and 2/3rd sizes.";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 450);
            Controls.Add(groupBox1);
            Name = "Settings";
            Text = "Settings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label helpText;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}