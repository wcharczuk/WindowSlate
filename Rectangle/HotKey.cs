using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangle.Lib
{
    public class HotKey
    {
        HotKey(Keys k, KeyModifiers m)
        {
            this.Key = k;
            this.KeyModifiers = m;
        }
        
        HotKey(string serialized)
        {
            // iterate through the string
            // do some thaaangs

            var parts = serialized.Split('+');

            Keys k = 0;
            KeyModifiers m = 0;

            foreach(var part in parts)
            {
                switch (part)
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
                        m = m | KeyModifiers.Windows;
                        break;
                    default:
                        if (part.Trim() == "")
                        {
                            break;
                        }
                        if (Char.IsLetterOrDigit(part, 0))
                        {
                            k = (Keys)Char.ToUpper(part[0]);
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

            this.Key = k;
            this.KeyModifiers = m;
        }
    
        public Keys Key { get; set; }
        public KeyModifiers KeyModifiers { get; set; }
    }
}
