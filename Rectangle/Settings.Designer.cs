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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            groupBox1 = new GroupBox();
            label9 = new Label();
            label10 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            helpText = new Label();
            notifyIcon1 = new NotifyIcon(components);
            label11 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(helpText);
            groupBox1.Location = new Point(12, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(426, 483);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Help";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 340);
            label9.Name = "label9";
            label9.Size = new Size(183, 17);
            label9.TabIndex = 8;
            label9.Text = "Next Display - shift+ctrl+alt+L";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 323);
            label10.Name = "label10";
            label10.Size = new Size(208, 17);
            label10.TabIndex = 7;
            label10.Text = "Previous Display - shift+ctrl+alt+H";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 273);
            label8.Name = "label8";
            label8.Size = new Size(378, 17);
            label8.TabIndex = 8;
            label8.Text = "The half sizes will alternate between 1/3rd, 1/2, and 2/3rd sizes.";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 192);
            label7.Name = "label7";
            label7.Size = new Size(134, 17);
            label7.TabIndex = 7;
            label7.Text = "Normal - ctrl+alt+J";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 226);
            label6.Name = "label6";
            label6.Size = new Size(134, 17);
            label6.TabIndex = 6;
            label6.Text = "Right Half - ctrl+alt+L";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 209);
            label5.Name = "label5";
            label5.Size = new Size(128, 17);
            label5.TabIndex = 5;
            label5.Text = "Left Half - ctrl+alt+H";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 135);
            label4.Name = "label4";
            label4.Size = new Size(191, 17);
            label4.TabIndex = 4;
            label4.Text = "Bottom Right - ctrl+windows+K";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 118);
            label3.Name = "label3";
            label3.Size = new Size(179, 17);
            label3.TabIndex = 3;
            label3.Text = "Bottom Left - ctrl+windows+J";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 101);
            label2.Name = "label2";
            label2.Size = new Size(169, 17);
            label2.TabIndex = 2;
            label2.Text = "Top Right - ctrl+windows+L";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 84);
            label1.Name = "label1";
            label1.Size = new Size(163, 17);
            label1.TabIndex = 1;
            label1.Text = "Top Left - ctrl+windows+H";
            // 
            // helpText
            // 
            helpText.AutoSize = true;
            helpText.Location = new Point(6, 22);
            helpText.Name = "helpText";
            helpText.Size = new Size(391, 17);
            helpText.TabIndex = 0;
            helpText.Text = "In this section we'll go over the specific hotkeys and what they do:";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Rectangle";
            notifyIcon1.Visible = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 175);
            label11.Name = "label11";
            label11.Size = new Size(134, 17);
            label11.TabIndex = 9;
            label11.Text = "Maximize - ctrl+alt+K";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 510);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
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
        private Label label9;
        private Label label10;
        private NotifyIcon notifyIcon1;
        private Label label11;
    }
}