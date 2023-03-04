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
            groupBox = new GroupBox();
            helpText = new Label();
            trayIcon = new NotifyIcon(components);
            trayIconContextMenu = new ContextMenuStrip(components);
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox.Controls.Add(helpText);
            groupBox.Location = new Point(15, 15);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(425, 425);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Settings && Help";
            // 
            // helpText
            // 
            helpText.AutoSize = true;
            helpText.Location = new Point(6, 22);
            helpText.Name = "helpText";
            helpText.Size = new Size(390, 64);
            helpText.TabIndex = 0;
            helpText.Text = "In this section we'll go over the specific hotkeys and what they do:";
            // 
            // notifyIcon1
            // 
            trayIcon.ContextMenuStrip = trayIconContextMenu;
            trayIcon.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            trayIcon.Text = "Rectangle";
            trayIcon.Visible = true;
            // 
            // contextMenuStrip1
            // 
            trayIconContextMenu.Name = "notifyIconContextMenu";
            trayIconContextMenu.Size = new Size(64, 4);
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 450);
            Controls.Add(groupBox);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Settings";
            Text = "Rectangle Settings & Help";
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox;
        private Label helpText;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayIconContextMenu;
    }
}