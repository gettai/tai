using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.UI.Common;
using Tai.UI.Main;
using Tai.UI.Servicer;
using Tai.UI.Views;

namespace Tai.UI.ViewModels
{
    public class OverviewVM : UINotifyProperty
    {
        private string _content;
        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged(); }
        }
        public UICommand TestCommand { get; set; }
        private readonly IRouter _router;
        public OverviewVM(IRouter router_)
        {
            _router = router_;
            TestCommand = new UICommand(OnTestCommandExectued);

            Debug.WriteLine("overview vm is be created!");
        }

        private void OnTestCommandExectued(object obj)
        {
            _router.Navigate(typeof(Page1));
            //mainVM.NavigateTo(typeof(Page1));
        }

        ~OverviewVM()
        {
            Debug.WriteLine("overview vm is be destroyed!");
        }
    }
}
