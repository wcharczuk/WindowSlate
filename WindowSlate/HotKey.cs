using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowSlate
{
    public class HotKey
    {
        private static List<KeyModifiers> standardModifiers = new() { 
            KeyModifiers.Windows,
            KeyModifiers.Control, 
            KeyModifiers.Alt, 
            KeyModifiers.Shift, 
        };

        public HotKey() { }

        public HotKey(Keys k, KeyModifiers m)
        {
            Key = k;
            KeyModifiers = m;
        }

        public HotKey(string key, bool windows, bool control, bool alt, bool shift)
        {
            Key = (Keys)char.ToUpper(key[0]);

            KeyModifiers m = 0;
            if (windows)
            {
                m = m | KeyModifiers.Windows;
            }
            if (control)
            {
                m = m | KeyModifiers.Control;
            }
            if (alt)
            {
                m = m | KeyModifiers.Alt;
            }
            if (shift)
            {
                m = m | KeyModifiers.Shift;
            }
            KeyModifiers = m;
        }

        public HotKey(string serialized)
        {
            var parts = serialized.Split('+');

            Keys k = 0;
            KeyModifiers m = 0;

            foreach (var part in parts)
            {
                switch (part.ToLower())
                {
                    case "control":
                        m = m | KeyModifiers.Control;
                        break;
                    case "alt":
                        m = m | KeyModifiers.Alt;
                        break;
                    case "shift":
                        m = m | KeyModifiers.Shift;
                        break;
                    case "win":
                    case "windows":
                        m = m | KeyModifiers.Windows;
                        break;
                    default:
                        if (part.Trim() == "")
                        {
                            break;
                        }
                        if (char.IsLetterOrDigit(part, 0))
                        {
                            k = (Keys)char.ToUpper(part[0]);
                        }
                        else
                        {
                            throw new Exception($"Cannot parse HotKey; invalid token: {part}");
                        }
                        break;
                }
            }
            if (k == 0)
            {
                throw new Exception($"Cannot parse HotKey; must have a letter or digit chord component");
            }
            if (m == 0)
            {
                throw new Exception($"Cannot parse HotKey; must have a modifier chord component");
            }

            Key = k;
            KeyModifiers = m;
        }

        public Keys Key { get; set; }
        public KeyModifiers KeyModifiers { get; set; }

        public bool ModifierEnabled(KeyModifiers modifier)
        {
            return (this.KeyModifiers & modifier) > 0;
        }

        public override string ToString() 
        {
            var output = "";
            foreach (var modifier in standardModifiers)
            {
                if (this.ModifierEnabled(modifier))
                {
                    var modifierToken = modifier.ToString().ToLower();
                    if (modifierToken == "windows")
                    {
                        modifierToken = "win";
                    }
                    output += modifierToken;
                    output += "+";
                }
            }
            if (this.Key != Keys.None)
            {
                output += this.Key.ToString();
            }
            return output;
        }
    }
}
