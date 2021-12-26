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
using WPFScheduler.Models;

namespace WPFScheduler.Views
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFScheduler"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFScheduler;assembly=WPFScheduler"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SchedulerItemsPanel/>
    ///
    /// </summary>
    public class SchedulerItemsPanel : Panel
    {

        #region Attached properties
        public static DateTime GetDate(DependencyObject obj)
        {
            return (DateTime)obj.GetValue(DateProperty);
        }

        public static void SetDate(DependencyObject obj, DateTime value)
        {
            obj.SetValue(DateProperty, value);
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.RegisterAttached("Date", typeof(DateTime), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(DateTime.MinValue, 
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                new PropertyChangedCallback(OnDateChanged)));

        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(d)) as SchedulerItemsPanel;
            panel.InvalidateMeasure();
        }



        public static int GetDuration(DependencyObject obj)
        {
            return (int)obj.GetValue(DurationProperty);
        }

        public static void SetDuration(DependencyObject obj, int value)
        {
            obj.SetValue(DurationProperty, value);
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(int), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));






        public static ISchedulerRowData GetRow(DependencyObject obj)
        {
            return (ISchedulerRowData)obj.GetValue(RowProperty);
        }

        public static void SetRow(DependencyObject obj, ISchedulerRowData value)
        {
            obj.SetValue(RowProperty, value);
        }

        // Using a DependencyProperty as the backing store for Room.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached("Row", typeof(ISchedulerRowData), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        
        #endregion

        #region DependencyProperty



        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));




        public double ItemWidthUnit
        {
            get { return (double)GetValue(ItemWidthUnitProperty); }
            set { SetValue(ItemWidthUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemWidthUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemWidthUnitProperty =
            DependencyProperty.Register("ItemWidthUnit", typeof(double), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(
                0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        


        public DateTime InternalCurrentDate
        {
            get { return (DateTime)GetValue(InternalCurrentDateProperty); }
            set { SetValue(InternalCurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalStartDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalCurrentDateProperty =
            DependencyProperty.Register("InternalCurrentDate", typeof(DateTime), typeof(SchedulerItemsPanel),
            new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.AffectsMeasure));



        public int InternalRange
        {
            get { return (int)GetValue(InternalRangeProperty); }
            set { SetValue(InternalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalRangeProperty =
            DependencyProperty.Register("InternalRange", typeof(int), typeof(SchedulerItemsPanel),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));



        public ISchedulerRowData InternalRow
        {
            get { return (ISchedulerRowData)GetValue(InternalRowProperty); }
            set { SetValue(InternalRowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InternalRoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalRowProperty =
            DependencyProperty.Register("InternalRow", typeof(ISchedulerRowData), typeof(SchedulerItemsPanel), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        
        #endregion

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in base.InternalChildren)
            {
               // DependencyObject o = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(element, 0), 0), 0);
                var schedulerItem = VisualTreeHelper.GetChild(element, 0) as SchedulerItem;
                if (schedulerItem != null)
                {
                    var room = GetRow(schedulerItem);
                    if (room == InternalRow)
                    {
                        var date = GetDate(schedulerItem);
                        var range = GetDuration(schedulerItem);
                        var shift = date.Date.Subtract(InternalCurrentDate.Date).Days;
                        var rect = new Rect(shift * ItemWidthUnit, 0, ItemWidthUnit * range + 1, ItemHeight);
                        element.Arrange(rect);
                    }
                }
            }
            return finalSize;
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement item in base.InternalChildren)
            {
                item.Measure(availableSize);
            }
            return new Size(0, 0);
        }
        static SchedulerItemsPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SchedulerItemsPanel), new FrameworkPropertyMetadata(typeof(SchedulerItemsPanel)));
        }
        public SchedulerItemsPanel()
        {
            Background = Brushes.Transparent;
        }
    }
}
