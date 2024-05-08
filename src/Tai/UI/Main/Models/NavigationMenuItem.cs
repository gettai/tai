using LancerUI.Controls.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.Main.Models
{
    /// <summary>
    /// 导航栏菜单项模型
    /// </summary>
    public class NavigationMenuItem
    {
        public string Title { get; set; }
        public IconSymbol Icon { get; set; }
        public IconSymbol SelectedIcon { get; set; }
        public Type PageType { get; set; }
    }
}
