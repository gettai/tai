using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Navigation;
using Tai.UI.Common;
using Tai.UI.Main.Models;
using Tai.UI.Services;
using Tai.UI.Views;

namespace Tai.UI.Main
{
    public class MainVM : UINotifyProperty
    {
        public ObservableCollection<NavigationMenuItem> NavigationItems { get; set; }
        private NavigationMenuItem _selectedNavigationItem;
        public NavigationMenuItem SelectedNavigationItem
        {
            get => _selectedNavigationItem;
            set { _selectedNavigationItem = value; OnPropertyChanged(); }
        }
        private Visibility _goBackVisibility = Visibility.Collapsed;
        public Visibility GoBackVisibility
        {
            get => _goBackVisibility;
            set { _goBackVisibility = value; OnPropertyChanged(); }
        }

        public UICommand GoBackCommand { get; set; }

        private readonly IRouter _router;
        public MainVM(IRouter router_)
        {
            _router = router_;
            _router.InitCompleted += _router_InitCompleted;
            _router.Navigated += _router_Navigated;
        }

        private void _router_Navigated(object sender, object data)
        {
            GoBackVisibility = NavigationItems.Where(s => s.PageType == _router.CurrentPage).FirstOrDefault() == null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void _router_InitCompleted(object sender, object data)
        {
            Init();
        }

        private void Init()
        {
            PropertyChanged += MainVM_PropertyChanged;
            InitNavigationItems();
            SetDefaultPage();
            GoBackCommand = new UICommand(OnGoBackCommandExectued);
        }

        private void OnGoBackCommandExectued(object obj)
        {
            _router.GoBack();
        }

        private void MainVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedNavigationItem))
            {
                _router.Navigate(SelectedNavigationItem.PageType, true);
            }
        }

        private void InitNavigationItems()
        {
            NavigationItems = new ObservableCollection<NavigationMenuItem>
            {
                new NavigationMenuItem
                {
                    Title = "概览",
                    Icon= LancerUI.Controls.Types.IconSymbol.Home,
                    SelectedIcon = LancerUI.Controls.Types.IconSymbol.HomeFilled,
                    PageType = typeof(Overview)
                },
                new NavigationMenuItem
                {
                    Title = "统计",
                    Icon= LancerUI.Controls.Types.IconSymbol.DataHistogram,
                    SelectedIcon= LancerUI.Controls.Types.IconSymbol.DataHistogramFilled,
                    PageType = typeof(Statistics)
                },
                new NavigationMenuItem
                {
                    Title = "记录",
                    Icon= LancerUI.Controls.Types.IconSymbol.CalendarAgenda,
                    SelectedIcon= LancerUI.Controls.Types.IconSymbol.CalendarAgendaFilled,
                    PageType = typeof(Statistics)
                },
                new NavigationMenuItem
                {
                    Title = "分类",
                    Icon= LancerUI.Controls.Types.IconSymbol.TagMultiple,
                    SelectedIcon= LancerUI.Controls.Types.IconSymbol.TagMultipleFilled,
                    PageType = typeof(Statistics)
                },
                //new NavigationMenuItem
                //{
                //    Title = "插件",
                //    Icon= LancerUI.Controls.Types.IconSymbol.PuzzlePiece,
                //    SelectedIcon= LancerUI.Controls.Types.IconSymbol.PuzzlePieceFilled,
                //    PageType = typeof(Statistics)
                //},
            };
        }

        private void SetDefaultPage()
        {
            SelectedNavigationItem = NavigationItems[0];
        }


        public void Dispose()
        {
            _router.InitCompleted -= _router_InitCompleted;
            _router.Navigated -= _router_Navigated;
            PropertyChanged -= MainVM_PropertyChanged;
        }

        ~MainVM()
        {
            Debug.WriteLine("MainVM disposed");
        }
    }
}
