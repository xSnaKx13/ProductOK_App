using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductOK_App.Classes
{
    public class TabPageHide
    {
        private readonly TabControl _tabControl;
        public TabPageHide(TabControl tabControl)
        {
            _tabControl = tabControl;
        }
        public void HideTabControl()
        {
            _tabControl.Appearance = TabAppearance.FlatButtons;
            _tabControl.ItemSize = new Size(0, 1);
            _tabControl.SizeMode = TabSizeMode.Fixed;
        }
    }
}
