using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tai.Common;

namespace Tai.Controls.DurationTextBlock
{
    public class DurationTextBlock : Control
    {
        /// <summary>
        /// 时长（秒）
        /// </summary>
        public int Duration { get => (int)GetValue(DurationProperty); set => SetValue(DurationProperty, value); }
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(int), typeof(DurationTextBlock), new PropertyMetadata(0, new PropertyChangedCallback(OnDurationChanged)));

        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DurationTextBlock;
            if (e.NewValue != e.OldValue)
            {
                control.Update();
            }
        }

        /// <summary>
        /// 显示的小时文本
        /// </summary>
        public string HourText { get => (string)GetValue(HourTextProperty); set => SetValue(HourTextProperty, value); }
        public static readonly DependencyProperty HourTextProperty = DependencyProperty.Register("HourText", typeof(string), typeof(DurationTextBlock), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 显示的分钟文本
        /// </summary>
        public string MinuteText { get => (string)GetValue(MinuteTextProperty); set => SetValue(MinuteTextProperty, value); }
        public static readonly DependencyProperty MinuteTextProperty = DependencyProperty.Register("MinuteText", typeof(string), typeof(DurationTextBlock), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 显示的秒文本
        /// </summary>
        public string SecondText { get => (string)GetValue(SecondTextProperty); set => SetValue(SecondTextProperty, value); }
        public static readonly DependencyProperty SecondTextProperty = DependencyProperty.Register("SecondText", typeof(string), typeof(DurationTextBlock), new PropertyMetadata(string.Empty));

        #region 数值字体样式
        /// <summary>
        /// 数值字体颜色
        /// </summary>
        public SolidColorBrush ValueForeground{ get => (SolidColorBrush)GetValue(ValueForegroundProperty); set => SetValue(ValueForegroundProperty, value); }
        public static readonly DependencyProperty ValueForegroundProperty = DependencyProperty.Register("ValueForeground", typeof(SolidColorBrush), typeof(DurationTextBlock));
        /// <summary>
        /// 数值字体大小
        /// </summary>
        public double ValueFontSize { get => (double)GetValue(ValueFontSizeProperty); set => SetValue(ValueFontSizeProperty, value); }
        public static readonly DependencyProperty ValueFontSizeProperty = DependencyProperty.Register("ValueFontSize", typeof(double), typeof(DurationTextBlock), new PropertyMetadata(12.0));
        /// <summary>
        /// 数值字体
        /// </summary>
        public FontFamily ValueFontFamily { get => (FontFamily)GetValue(ValueFontFamilyProperty); set => SetValue(ValueFontFamilyProperty, value); }
        public static readonly DependencyProperty ValueFontFamilyProperty = DependencyProperty.Register("ValueFontFamily", typeof(FontFamily), typeof(DurationTextBlock));
        /// <summary>
        /// 数值字体粗细
        /// </summary>
        public FontWeight ValueFontWeight { get => (FontWeight)GetValue(ValueFontWeightProperty); set => SetValue(ValueFontWeightProperty, value); }
        public static readonly DependencyProperty ValueFontWeightProperty = DependencyProperty.Register("ValueFontWeight", typeof(FontWeight), typeof(DurationTextBlock));
        public FontStretch ValueFontStretch { get => (FontStretch)GetValue(ValueFontStretchProperty); set => SetValue(ValueFontStretchProperty, value); }
        public static readonly DependencyProperty ValueFontStretchProperty = DependencyProperty.Register("ValueFontStretch", typeof(FontStretch), typeof(DurationTextBlock));
        /// <summary>
        /// 数值字体样式
        /// </summary>
        public FontStyle ValueFontStyle { get => (FontStyle)GetValue(ValueFontStyleProperty); set => SetValue(ValueFontStyleProperty, value); }
        public static readonly DependencyProperty ValueFontStyleProperty = DependencyProperty.Register("ValueFontStyle", typeof(FontStyle), typeof(DurationTextBlock));
        public VerticalAlignment ValueVerticalAlignment { get => (VerticalAlignment)GetValue(ValueVerticalAlignmentProperty); set => SetValue(ValueVerticalAlignmentProperty, value); }
        public static readonly DependencyProperty ValueVerticalAlignmentProperty = DependencyProperty.Register("ValueVerticalAlignment", typeof(VerticalAlignment), typeof(DurationTextBlock));
        #endregion
        #region 文本字体样式
        /// <summary>
        /// 文本字体颜色
        /// </summary>
        public SolidColorBrush TextForeground { get => (SolidColorBrush)GetValue(TextForegroundProperty); set => SetValue(TextForegroundProperty, value); }
        public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.Register("TextForeground", typeof(SolidColorBrush), typeof(DurationTextBlock));
        /// <summary>
        /// 文本字体大小
        /// </summary>
        public double TextFontSize { get => (double)GetValue(TextFontSizeProperty); set => SetValue(TextFontSizeProperty, value); }
        public static readonly DependencyProperty TextFontSizeProperty = DependencyProperty.Register("TextFontSize", typeof(double), typeof(DurationTextBlock), new PropertyMetadata(12.0));
        /// <summary>
        /// 文本字体
        /// </summary>
        public FontFamily TextFontFamily { get => (FontFamily)GetValue(TextFontFamilyProperty); set => SetValue(TextFontFamilyProperty, value); }
        public static readonly DependencyProperty TextFontFamilyProperty = DependencyProperty.Register("TextFontFamily", typeof(FontFamily), typeof(DurationTextBlock));
        /// <summary>
        /// 文本字体粗细
        /// </summary>
        public FontWeight TextFontWeight { get => (FontWeight)GetValue(TextFontWeightProperty); set => SetValue(TextFontWeightProperty, value); }
        public static readonly DependencyProperty TextFontWeightProperty = DependencyProperty.Register("TextFontWeight", typeof(FontWeight), typeof(DurationTextBlock));
        public FontStretch TextFontStretch { get => (FontStretch)GetValue(TextFontStretchProperty); set => SetValue(TextFontStretchProperty, value); }
        public static readonly DependencyProperty TextFontStretchProperty = DependencyProperty.Register("TextFontStretch", typeof(FontStretch), typeof(DurationTextBlock));
        /// <summary>
        /// 文本字体样式
        /// </summary>
        public FontStyle TextFontStyle { get => (FontStyle)GetValue(TextFontStyleProperty); set => SetValue(TextFontStyleProperty, value); }
        public static readonly DependencyProperty TextFontStyleProperty = DependencyProperty.Register("TextFontStyle", typeof(FontStyle), typeof(DurationTextBlock));
        public VerticalAlignment TextVerticalAlignment { get => (VerticalAlignment)GetValue(TextVerticalAlignmentProperty); set => SetValue(TextVerticalAlignmentProperty, value); }
        public static readonly DependencyProperty TextVerticalAlignmentProperty = DependencyProperty.Register("TextVerticalAlignment", typeof(VerticalAlignment), typeof(DurationTextBlock));
        #endregion

        private TextBlock _hoursValueText, _hoursText, _minutesValueText, _minutesText, _secondsValueText, _secondsText;
        public DurationTextBlock()
        {
            DefaultStyleKey = typeof(DurationTextBlock);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _hoursValueText = GetTemplateChild("PART_Hours") as TextBlock;
            _hoursText = GetTemplateChild("PART_HoursText") as TextBlock;
            _minutesValueText = GetTemplateChild("PART_Minutes") as TextBlock;
            _minutesText = GetTemplateChild("PART_MinutesText") as TextBlock;
            _secondsValueText = GetTemplateChild("PART_Seconds") as TextBlock;
            _secondsText = GetTemplateChild("PART_SecondsText") as TextBlock;

            Update();
        }


        private void Update()
        {
            if (_hoursValueText == null) return;

            HiddleAll();

            var times = DateTimeHelper.ConvertSecondsToTimeArray(Duration);

            if (times[0] > 0)
            {
                _hoursValueText.Text = times[0].ToString();
                _hoursText.Visibility = Visibility.Visible;
                _hoursValueText.Visibility = Visibility.Visible;
                if (times[1] > 0)
                {
                    _minutesValueText.Text = times[1].ToString();
                    _minutesText.Visibility = Visibility.Visible;
                    _minutesValueText.Visibility = Visibility.Visible;
                }
            }
            else if (times[1] > 0)
            {
                _minutesValueText.Text = times[1].ToString();
                _minutesText.Visibility = Visibility.Visible;
                _minutesValueText.Visibility = Visibility.Visible;
                if (times[2] > 0)
                {
                    _secondsValueText.Text = times[2].ToString();
                    _secondsText.Visibility = Visibility.Visible;
                    _secondsValueText.Visibility = Visibility.Visible;
                }
            }
            else
            {
                _secondsValueText.Text = times[2].ToString();
                _secondsText.Visibility = Visibility.Visible;
                _secondsValueText.Visibility = Visibility.Visible;
            }

            _hoursText.Text = HourText;
            _minutesText.Text = MinuteText;
            _secondsText.Text = SecondText;
        }

        private void HiddleAll()
        {
            _hoursValueText.Visibility = Visibility.Collapsed;
            _hoursText.Visibility = Visibility.Collapsed;
            _minutesValueText.Visibility = Visibility.Collapsed;
            _minutesText.Visibility = Visibility.Collapsed;
            _secondsValueText.Visibility = Visibility.Collapsed;
            _secondsText.Visibility = Visibility.Collapsed;
        }

    }
}
