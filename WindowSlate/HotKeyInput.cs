using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowSlate
{
    public partial class HotKeyInput : UserControl
    {
        public HotKeyInput()
        {
            InitializeComponent();
        }

        private HotKey? _previousHotKey;
        private HotKey? _hotKey;
        public HotKey? HotKey
        {
            get => this._hotKey;
            set
            {
                this._hotKey = value;
                this.UpdateHotKey(value);
            }
        }

        public int? HotKeyIndex { get; set; }

        public Action? RegisterHotKeysHandler { get; set; }
        public Action? UnregisterHotKeysHandler { get; set; }
        public Action? SaveSettingsHandler { get; set; }

        public bool IsCapturing { get; set; }

        public string Description
        {
            get => this.description.Text;
            set => this.description.Text = value;
        }

        public void Unregister()
        {
            if (this.HotKeyIndex != null)
            {
                HotKeyManager.UnregisterHotKey((int)this.HotKeyIndex);
                this.HotKeyIndex = null;
            }
        }

        public void Register()
        {
            if (this._hotKey != null)
            {
                this.HotKeyIndex = HotKeyManager.RegisterHotKey(this._hotKey.Key, this._hotKey.KeyModifiers);
            }
        }

        private void UpdateHotKey(HotKey? hotKey)
        {
            this.Unregister();
            if (hotKey != null)
            {

                this.hotKeyLabel.Text = hotKey.ToString();
                this.Register();
            }
            else
            {
                this.hotKeyLabel.Text = "";
            }
        }

        private void set_Click(object sender, EventArgs e)
        {
            if (this.UnregisterHotKeysHandler != null)
            {
                this.UnregisterHotKeysHandler();
            }
            _previousHotKey = _hotKey;
            this.HotKey = null; // also reflects in the text
            this.IsCapturing = true;
            this.set.Visible = false;
            this.clear.Visible = false;
            this.setDone.Visible = true;
            this.cancel.Visible = true;
        }

        private void setDone_Click(object sender, EventArgs e)
        {
            if (_hotKey != null)
            {
                try
                {
                    _hotKey.Validate();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Settings Error");
                    return;
                }

                this.hotKeyLabel.Text = _hotKey.ToString();
            }
            else
            {
                this.hotKeyLabel.Text = "";
            }
            
            this.IsCapturing = false;

            this.set.Visible = true;
            this.clear.Visible = true;
            this.setDone.Visible = false;
            this.cancel.Visible = false;

            if (this.RegisterHotKeysHandler != null)
            {
                this.RegisterHotKeysHandler();
            }
            if (this.SaveSettingsHandler != null)
            {
                this.SaveSettingsHandler();
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            this.HotKey = null;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.IsCapturing = false;

            this.set.Visible = true;
            this.clear.Visible = true;
            this.setDone.Visible = false;
            this.cancel.Visible = false;

            _hotKey = _previousHotKey;
            _previousHotKey = null;

            if (_hotKey != null)
            {
                this.hotKeyLabel.Text = _hotKey.ToString();
            }
            else
            {
                this.hotKeyLabel.Text = "";
            }

            if (this.RegisterHotKeysHandler != null)
            {
                this.RegisterHotKeysHandler();
            }
        }

        public void CaptureKeyDown(object? sender, KeyEventArgs e)
        {
            if (this.IsCapturing)
            {
                if (this.HotKey == null)
                {
                    this.HotKey = new HotKey();
                }
                switch (e.KeyCode)
                {
                    case Keys.LWin:
                    case Keys.RWin:
                        this.HotKey.KeyModifiers = this.HotKey.KeyModifiers | KeyModifiers.Windows;
                        break;
                    case Keys.ControlKey:
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                    case Keys.Control:
                        this.HotKey.KeyModifiers = this.HotKey.KeyModifiers | KeyModifiers.Control;
                        break;
                    case Keys.Menu:
                    case Keys.LMenu:
                    case Keys.RMenu:
                    case Keys.Alt:
                        this.HotKey.KeyModifiers = this.HotKey.KeyModifiers | KeyModifiers.Alt;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                    case Keys.ShiftKey:
                    case Keys.Shift:
                        this.HotKey.KeyModifiers = this.HotKey.KeyModifiers | KeyModifiers.Shift;
                        break;
                    default:
                        this.HotKey.Key = e.KeyCode;
                        break;
                }
                this.hotKeyLabel.Text = this.HotKey.ToString();
            }
        }

    }
}
