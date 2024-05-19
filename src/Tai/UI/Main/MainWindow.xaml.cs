using LancerUI.Controls.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tai.UI.Services;

namespace Tai.UI.Main
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : LUWindow
    {
        private readonly IRouter _router;
        private readonly MainVM _vm;
        public MainWindow(IRouter router_, MainVM vm_)
        {
            InitializeComponent();
            _router = router_;
            _vm = vm_;
            DataContext = vm_;
            _router.Init(PageFrame);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _vm.Dispose();
            _router.Dispose();
        }
    }
}
