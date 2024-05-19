using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.UI.Common;
using Tai.UI.Services;
using Tai.UI.Views;

namespace Tai.UI.ViewModels
{
    public class Page2VM
    {
        public UICommand test { get; set; }
        public Page2VM(IRouter router)
        {

            test = new UICommand(new Action<object>((obj) =>
            {
                router.Navigate(typeof(Page2));
                Debug.WriteLine("Page2VM test");
            }));
        }
    }
}
