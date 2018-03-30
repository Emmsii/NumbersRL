using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersRL.Input
{
    public class InputEventArgs
    {
        public Keys Key { get; set; }

        public InputEventArgs(Keys key)
        {
            Key = key;
        }
    }
}
