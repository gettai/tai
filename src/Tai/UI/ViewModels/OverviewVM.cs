using LancerUI.Controls.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tai.Common;
using Tai.Constants;
using Tai.UI.Common;
using Tai.UI.DataAccess;
using Tai.UI.Main;
using Tai.UI.Models;
using Tai.UI.Services;
using Tai.UI.Views;

namespace Tai.UI.ViewModels
{
    public struct SelectionBarItem
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
    public class OverviewVM : UINotifyProperty
    {
        #region 应用时长
        private int _appDuration = 0;
        public int AppDuration { get => _appDuration; set { _appDuration = value; OnPropertyChanged(); } }
        #endregion
        #region 网页时长
        private int _webDuration = 0;
        public int WebDuration { get => _webDuration; set { _webDuration = value; OnPropertyChanged(); } }
        #endregion
        #region 闲置时长
        private int _inactiveDuration = 0;
        public int InactiveDuration { get => _inactiveDuration; set { _inactiveDuration = value; OnPropertyChanged(); } }
        #endregion
        #region 选择器栏
        private SelectionBarItem _selectionBarSelectedItem;
        public SelectionBarItem SelectionBarSelectedItem { get => _selectionBarSelectedItem; set { _selectionBarSelectedItem = value; OnPropertyChanged(); } }
        public ObservableCollection<SelectionBarItem> SelectionBarItems { get; set; } = new ObservableCollection<SelectionBarItem>();
        #endregion
        #region 最为频繁APP
        public ObservableCollection<ProgressListItem> AppTopItems { get; set; } = new ObservableCollection<ProgressListItem>();
        #endregion
        #region 最为频繁Website
        public ObservableCollection<ProgressListItem> WebTopItems { get; set; } = new ObservableCollection<ProgressListItem>();
        #endregion
        //public UICommand TestCommand { get; set; }
        private readonly IRouter _router;
        private readonly IAppActiveTimeLogRepository _aatlRep;
        private readonly IAppRepository _appRep;
        private readonly IWebActiveTimeLogRepository _watlRep;
        private readonly IWebRepository _webRep;
        private readonly IAppInactiveTimeLogRepository _aitlRep;


        private DateTime[] _dayTimes = DateTimeHelper.GetTodayStartToEndTimes();
        private DateTime[] _weekTimes = DateTimeHelper.GetWeekTimes();

        private DateTime _startDateTime, _endDateTime;
        public OverviewVM(
            IRouter router_,
            IAppActiveTimeLogRepository aatlRep_,
            IAppRepository appRep_,
            IWebActiveTimeLogRepository watlRep_,
            IWebRepository webRep_,
            IAppInactiveTimeLogRepository aitlRep_)
        {
            _router = router_;
            _aatlRep = aatlRep_;
            _appRep = appRep_;
            _watlRep = watlRep_;
            _webRep = webRep_;
            _aitlRep = aitlRep_;

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //  初始化UI
            InitSelectionBarItems();

            //  数据读取
            LoadData();

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectionBarSelectedItem))
            {
                LoadData();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            Debug.WriteLine("加载数据");

            //  设置获取时间范围
            SetTimes();

            Task.Run(() =>
            {
                GetStatistics();
                GetTopData();
            });
        }

        private void InitSelectionBarItems()
        {
            SelectionBarItems.Clear();
            SelectionBarItems.Add(new SelectionBarItem()
            {
                Text = "今日",
                Name = "Day"
            });
            SelectionBarItems.Add(new SelectionBarItem()
            {
                Text = "本周",
                Name = "Week"
            });
            //  默认选择项
            SelectionBarSelectedItem = SelectionBarItems.FirstOrDefault();
        }

        private void SetTimes()
        {
            string type = SelectionBarSelectedItem.Name;

            if (type == "Day")
            {
                _startDateTime = _dayTimes[0];
                _endDateTime = _dayTimes[1];
            }
            else if (type == "Week")
            {
                _startDateTime = _weekTimes[0];
                _endDateTime = _weekTimes[1];
            }
        }

        /// <summary>
        /// 获取统计数据
        /// </summary>
        private void GetStatistics()
        {
            //  重置数据初始值
            AppDuration = 0;
            WebDuration = 0;
            InactiveDuration = 0;

            AppDuration = _aatlRep.GetTotalDuration(_startDateTime, _endDateTime);
            WebDuration = _watlRep.GetTotalDuration(_startDateTime, _endDateTime);
            InactiveDuration = _aitlRep.GetTotalDuration(_startDateTime, _endDateTime);
        }
        private void GetTopData()
        {
            //  重置数据初始值
            App.Current.Dispatcher.Invoke(() =>
            {
                AppTopItems.Clear();
                WebTopItems.Clear();
            });

            var appResult = _appRep.GetTopList(_startDateTime, _endDateTime, 5);
            var webResult = _webRep.GetTopList(_startDateTime, _endDateTime, 5);

            double appMaxValue = appResult.Max(s => s.ActiveDuration);
            double webMaxValue = webResult.Max(s => s.ActiveDuration);

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var log in appResult)
                {
                    var item = new ProgressListItem()
                    {
                        Source = log,
                        Description = DateTimeHelper.ConvertSecondsToTimeString(log.ActiveDuration),
                        Title = log.Description ?? log.Name,
                        Progress = log.ActiveDuration / appMaxValue,
                    };
                    if (!string.IsNullOrEmpty(log.Icon))
                    {
                        item.Image = new BitmapImage(new Uri(Path.Combine(FileHelper.GetAppRunDirectory(), log.Icon), UriKind.RelativeOrAbsolute));
                    }
                    AppTopItems.Add(item);
                }
                foreach (var log in webResult)
                {
                    var item = new ProgressListItem()
                    {
                        Source = log,
                        Description = DateTimeHelper.ConvertSecondsToTimeString(log.ActiveDuration),
                        Title = log.Name ?? log.Domain,
                        Progress = log.ActiveDuration / webMaxValue,
                    };
                    if (!string.IsNullOrEmpty(log.Icon))
                    {
                        item.Image = new BitmapImage(new Uri(Path.Combine(FileHelper.GetAppRunDirectory(), log.Icon), UriKind.RelativeOrAbsolute));
                    }
                    WebTopItems.Add(item);
                }
            });
        }
    }
}
