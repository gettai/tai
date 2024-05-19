using LancerUI.Controls.Types;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tai.Controls.Card
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tai.Controls.Card"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tai.Controls.Card;assembly=Tai.Controls.Card"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:Card/>
    ///
    /// </summary>
    public class Card : ContentControl
    {
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(Card), new PropertyMetadata(null));
        public IconSymbol Icon
        {
            get { return (IconSymbol)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconSymbol), typeof(Card), new PropertyMetadata(IconSymbol.None));
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(Card), new PropertyMetadata(16.0));
        public SolidColorBrush IconColorBrush
        {
            get { return (SolidColorBrush)GetValue(IconColorBrushProperty); }
            set { SetValue(IconColorBrushProperty, value); }
        }
        public static readonly DependencyProperty IconColorBrushProperty = DependencyProperty.Register("IconColorBrush", typeof(SolidColorBrush), typeof(Card), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        public double IconOpacity
        {
            get { return (double)GetValue(IconOpacityProperty); }
            set { SetValue(IconOpacityProperty, value); }
        }
        public static readonly DependencyProperty IconOpacityProperty = DependencyProperty.Register("IconOpacity", typeof(double), typeof(Card), new PropertyMetadata(1.0));
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }
    }
}
