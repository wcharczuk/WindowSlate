namespace Rectangle
{
    partial class HotKeyInput
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotKeyInput));
            set = new Button();
            setTooltip = new ToolTip(components);
            clear = new Button();
            clearTooltip = new ToolTip(components);
            hotKeyLabel = new Label();
            description = new Label();
            inputGroup = new GroupBox();
            setDone = new Button();
            setDoneTooltip = new ToolTip(components);
            cancel = new Button();
            cancelTooltip = new ToolTip(components);
            inputGroup.SuspendLayout();
            SuspendLayout();
            // 
            // set
            // 
            set.Image = (Image)resources.GetObject("set.Image");
            set.Location = new Point(260, 14);
            set.Margin = new Padding(3, 4, 3, 4);
            set.Name = "set";
            set.Size = new Size(40, 25);
            set.TabIndex = 5;
            setTooltip.SetToolTip(set, "Set the hotkey for this motion");
            set.UseVisualStyleBackColor = true;
            set.Click += set_Click;
            // 
            // clear
            // 
            clear.Image = (Image)resources.GetObject("clear.Image");
            clear.Location = new Point(300, 14);
            clear.Margin = new Padding(3, 4, 3, 4);
            clear.Name = "clear";
            clear.Size = new Size(40, 25);
            clear.TabIndex = 6;
            clearTooltip.SetToolTip(clear, "Clear the hotkey for this motion");
            clear.UseVisualStyleBackColor = true;
            clear.Click += clear_Click;
            // 
            // hotKeyLabel
            // 
            hotKeyLabel.Location = new Point(105, 14);
            hotKeyLabel.Name = "hotKeyLabel";
            hotKeyLabel.Size = new Size(150, 25);
            hotKeyLabel.TabIndex = 7;
            hotKeyLabel.Text = "win+alt+shift+F";
            hotKeyLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // description
            // 
            description.Location = new Point(5, 14);
            description.Name = "description";
            description.Size = new Size(95, 25);
            description.TabIndex = 6;
            description.Text = "Top Left";
            description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // inputGroup
            // 
            inputGroup.Controls.Add(cancel);
            inputGroup.Controls.Add(setDone);
            inputGroup.Controls.Add(description);
            inputGroup.Controls.Add(hotKeyLabel);
            inputGroup.Controls.Add(clear);
            inputGroup.Controls.Add(set);
            inputGroup.Location = new Point(1, 1);
            inputGroup.Name = "inputGroup";
            inputGroup.Size = new Size(348, 48);
            inputGroup.TabIndex = 0;
            inputGroup.TabStop = false;
            // 
            // setDone
            // 
            setDone.Image = (Image)resources.GetObject("setDone.Image");
            setDone.Location = new Point(260, 14);
            setDone.Margin = new Padding(3, 4, 3, 4);
            setDone.Name = "setDone";
            setDone.Size = new Size(40, 25);
            setDone.TabIndex = 8;
            setDoneTooltip.SetToolTip(setDone, "Finalize setting the hotkey for this motion");
            setDone.UseVisualStyleBackColor = true;
            setDone.Visible = false;
            setDone.Click += setDone_Click;
            // 
            // button1
            // 
            cancel.Image = (Image)resources.GetObject("button1.Image");
            cancel.Location = new Point(300, 14);
            cancel.Margin = new Padding(3, 4, 3, 4);
            cancel.Name = "cancel";
            cancel.Size = new Size(40, 25);
            cancel.TabIndex = 9;
            cancelTooltip.SetToolTip(cancel, "Cancel setting the hotkey for this motion");
            cancel.UseVisualStyleBackColor = true;
            cancel.Visible = false;
            cancel.Click += cancel_Click;
            // 
            // HotKeyInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(inputGroup);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "HotKeyInput";
            Size = new Size(350, 50);
            inputGroup.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox inputGroup;
        private Label description;
        private Label hotKeyLabel;
        private Button set;
        private ToolTip setTooltip;
        private Button clear;
        private ToolTip clearTooltip;
        private Button setDone;
        private ToolTip setDoneTooltip;
        private Button cancel;
        private ToolTip cancelTooltip;
    }
}
