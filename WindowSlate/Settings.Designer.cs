using WindowSlate.Properties;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            groupBox = new GroupBox();
            runOnStartCheckbox = new CheckBox();
            trayIcon = new NotifyIcon(components);
            trayIconContextMenu = new ContextMenuStrip(components);
            startMinimizedCheckbox = new CheckBox();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox
            // 
            groupBox.Controls.Add(startMinimizedCheckbox);
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
            runOnStartCheckbox.Location = new Point(140, 573);
            runOnStartCheckbox.Name = "runOnStartCheckbox";
            runOnStartCheckbox.Size = new Size(105, 20);
            runOnStartCheckbox.TabIndex = 1;
            runOnStartCheckbox.Text = "Run on start?";
            runOnStartCheckbox.UseVisualStyleBackColor = true;
            runOnStartCheckbox.CheckedChanged += runOnStartCheckbox_CheckedChanged;
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayIconContextMenu;
            trayIcon.Text = "Window Slate";
            trayIcon.Icon = (Icon)resources.GetObject("notifyIcon");
            trayIcon.Visible = false;
            //
            // trayIconContextMenu
            // 
            trayIconContextMenu.Name = "notifyIconContextMenu";
            trayIconContextMenu.Size = new Size(60, 5);
            // 
            // startMinimizedCheckbox
            // 
            startMinimizedCheckbox.AutoSize = true;
            startMinimizedCheckbox.Location = new Point(250, 573);
            startMinimizedCheckbox.Name = "startMinimizedCheckbox";
            startMinimizedCheckbox.Size = new Size(125, 20);
            startMinimizedCheckbox.TabIndex = 2;
            startMinimizedCheckbox.Text = "Start Minimized?";
            startMinimizedCheckbox.UseVisualStyleBackColor = true;
            startMinimizedCheckbox.CheckedChanged += startMinimizedCheckbox_CheckedChanged;
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
        private CheckBox startMinimizedCheckbox;
    }
}
