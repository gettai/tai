using LancerUI.Controls.Chart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tai.UI.Common;

namespace Tai.UI.ViewModels
{
    public class StatisticsVM : UINotifyProperty
    {
        public List<ChartItem> ChartData { get; set; } = new List<ChartItem>();
        public string[] ChartLabels { get; set; } = new string[] { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };
        public StatisticsVM()
        {
            //ChartData.Add(new ChartItem()
            //{
            //    ColorBrush = new SolidColorBrush(Colors.SkyBlue),
            //    Label = "使用时长",
            //    Values = new double[] { 50, 3, 3, 4, 15, 2, 8 }
            //});
            //ChartData.Add(new ChartItem()
            //{
            //    Label = "空闲",
            //    Values = new double[] { 132, 2, 3, 4, 5, 6, 150 }
            //});
            var chartItem = new ChartItem();
            var values = new double[31];
            var labels = new string[31];
            for (int i = 0; i < 31; i++)
            {
                values[i] = i;
                labels[i] = i.ToString();
            }
            chartItem.Values = values;
            chartItem.Label = "使用时长";
            chartItem.ColorBrush = new SolidColorBrush(Colors.SkyBlue);
            ChartData.Add(chartItem);
            ChartLabels = labels;
        }
    }
}
