namespace WindowSlate
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
            groupBox = new GroupBox();
            runOnStartCheckbox = new CheckBox();
            trayIcon = new NotifyIcon(components);
            trayIconContextMenu = new ContextMenuStrip(components);
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Controls.Add(runOnStartCheckbox);
            groupBox.Location = new Point(10, 10);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(380, 600);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Input Settings";
            // 
            // runOnStartCheckbox
            // 
            runOnStartCheckbox.AutoSize = true;
            runOnStartCheckbox.Location = new Point(270, 573);
            runOnStartCheckbox.Name = "runOnStartCheckbox";
            runOnStartCheckbox.Size = new Size(104, 21);
            runOnStartCheckbox.TabIndex = 1;
            runOnStartCheckbox.Text = "Run on start?";
            runOnStartCheckbox.UseVisualStyleBackColor = true;
            runOnStartCheckbox.CheckedChanged += RunOnStartCheckbox_CheckedChanged;
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayIconContextMenu;
            trayIcon.Text = "WindowSlate";
            trayIcon.Visible = true;
            // 
            // trayIconContextMenu
            // 
            trayIconContextMenu.Name = "notifyIconContextMenu";
            trayIconContextMenu.Size = new Size(61, 4);
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 620);
            Controls.Add(groupBox);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Settings";
            Text = "WindowSlate Settings & Help";
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayIconContextMenu;
        private CheckBox runOnStartCheckbox;
    }
}
