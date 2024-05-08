using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tai.UI.Common;

namespace Tai.UI.Servicer
{
    public class Router : IRouter
    {
        private Dictionary<string, object> _pageCache = new Dictionary<string, object>();

        private Type _currentPage;
        public Type CurrentPage => _currentPage;
        private object _extraData;
        public object ExtraData => _extraData;
        private Frame _frame;

        public event CommonEventHandler InitCompleted;
        public event CommonEventHandler Navigated;

        public void Init(Frame frame_)
        {
            _frame = frame_;
            _frame.Navigated += _frame_Navigated;
            InitCompleted?.Invoke(this, null);
        }

        private void _frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            _currentPage = e.Content.GetType();
            Navigated?.Invoke(this, null);
        }

        public void Navigate(Type page_, bool isCache_ = false)
        {
            LoadPage(page_, isCache_);
        }

        public void Navigate(Type page_, object extraData_, bool isCache_ = false)
        {
            _extraData = extraData_;
            Navigate(page_, isCache_);
        }

        private void LoadPage(Type page_, bool isCache = false)
        {
            //_frame.Content = null;

            if (_frame == null || page_ == null) return;
            if(page_== _currentPage)
            {
                _pageCache.Remove(_currentPage.Name);
            }

            if (_pageCache.ContainsKey(page_.Name))
            {
                _frame.Content = _pageCache[page_.Name];
            }
            else
            {
                var page = App.AppHost.Services.GetRequiredService(page_);
                _frame.Content = page;
                if (isCache)
                {
                    _pageCache.Add(page_.Name, page);
                }
            }
            _currentPage = page_;
        }

        public void Dispose()
        {
            _pageCache.Clear();
            _frame.Navigated -= _frame_Navigated;
            _frame.Content = null;
            _frame = null;
            _currentPage = null;
            _extraData = null;
        }

        public void GoBack()
        {
            if(_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}
