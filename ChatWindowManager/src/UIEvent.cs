using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindowManager.src
{
    public class UIEvent : EventArgs
    {
        public String window { get; private set; }

        public UIEvent(string window)
        {
            this.window = window;
        }
    }
}
