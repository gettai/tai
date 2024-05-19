using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tai.Controls.ProgressList
{
    public class ProgressListItem : Control
    {
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ProgressListItem), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ProgressListItem), new PropertyMetadata(null));
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ProgressListItem), new PropertyMetadata(null));
        /// <summary>
        /// 当前进度值，取值范围[0, 1]
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(ProgressListItem), new PropertyMetadata(0.0, new PropertyChangedCallback(OnProgressChanged)));

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProgressListItem;
            if (e.NewValue != e.OldValue)
            {
                control.UpdateProgressBar();
            }
        }

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(ProgressListItem), new PropertyMetadata(false));
        static ProgressListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressListItem), new FrameworkPropertyMetadata(typeof(ProgressListItem)));
        }
        private Rectangle _progressBar;
        private TextBlock _progressText;
        private Grid _progressGrid;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _progressBar = GetTemplateChild("PART_ProgressBar") as Rectangle;
            _progressText = GetTemplateChild("PART_ProgressText") as TextBlock;
            _progressGrid = GetTemplateChild("PART_ProgressGrid") as Grid;

            Loaded += ProgressListItem_Loaded;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateProgressBar();
        }

        private void ProgressListItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {
            if (_progressBar == null || _progressText == null) return;

            double p = Progress < 0 ? 0 : Progress;
            if (Progress >= 100)
            {
                p = 1;
            }
            else if (Progress > 1)
            {
                p = Progress / 100;
            }

            _progressBar.Width = (0.95 * _progressGrid.ActualWidth - _progressText.ActualWidth - _progressText.Margin.Left) * p;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            IsPressed = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            IsPressed = false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            IsPressed = false;
        }
    }
}
