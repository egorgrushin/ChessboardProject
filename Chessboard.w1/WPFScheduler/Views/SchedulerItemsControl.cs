using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFScheduler.Models;
using WPFScheduler.ViewModels;

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
    ///     <MyNamespace:SchedulerItemsControl/>
    ///
    /// </summary>
    public class SchedulerItemsControl : ItemsControl
    {

        private Point startDragPosition;
        static SchedulerItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SchedulerItemsControl), new FrameworkPropertyMetadata(typeof(SchedulerItemsControl)));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && SelectedItem != null)
            {
                var offset = startDragPosition - e.GetPosition(null);
                if (Math.Abs(offset.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(offset.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var dragObject = new DataObject("dragData", (SelectedItem.DataContext));
                    DragDrop.DoDragDrop(this, dragObject, DragDropEffects.Move);
                }
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var hitTest = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            var hitTestResult =
                VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(hitTest.VisualHit))) as SchedulerItem;
            if (hitTestResult != null)
            {
                SelectedItem = hitTestResult;
                startDragPosition = e.GetPosition(null);
            }

        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
        }
        public SchedulerItemsControl()
        {
        }

        void SchedulerItemsControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && SelectedItem != null)
            {
                var offset = startDragPosition - e.GetPosition(null);
                if (Math.Abs(offset.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(offset.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var dragObject = new DataObject("d", SelectedItem.DataContext);
                    DragDrop.DoDragDrop(SelectedItem, dragObject, DragDropEffects.Move);
                }
            }
        }

        void SchedulerItemsControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var hitTest = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            var hitTestResult =
                VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(hitTest.VisualHit))) as SchedulerItem;
            if (hitTestResult != null)
            {
                SelectedItem = hitTestResult;
                startDragPosition = e.GetPosition(null);
            }
        }

        void SchedulerItemsControl_PreviewDragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }
        


        void SchedulerItemsControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        void SchedulerItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            
        }



        void SchedulerItemsControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var hitTest = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            var hitTestResult = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(hitTest.VisualHit))) as SchedulerItem;
            if (hitTestResult != null)
            {
                SelectedItem = hitTestResult;
            }
        }

        /*void SchedulerItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex != -1)
            {

                var data = SelectedItems[0] as ISchedulerItemData;
                data.Date = data.Date.AddDays(1);

            }
        }*/

        /*void SchedulerItemsControl_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var date = SchedulerItemsPanel.GetDate(SelectedItem);
                MessageBox.Show(date.ToShortDateString());
                SchedulerItemsPanel.SetDate(SelectedItem, date.AddDays(1));
            }
        }*/

        public event SchedulerSelectionChangedEventHandler SelectionChanged;
        public delegate void SchedulerSelectionChangedEventHandler(DependencyObject sender, SchedulerSelectionChangedEventArgs e);
        private void OnSelectionChanged(SchedulerItem selectedItem)
        {
            var handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, new SchedulerSelectionChangedEventArgs(selectedItem));
            }
        }
        private SchedulerItem selectedItem;

        public SchedulerItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnSelectionChanged(value); }
        }
        
    }
    public class SchedulerSelectionChangedEventArgs : RoutedEventArgs
    {
        public SchedulerItem SelectedItem { get; set; }
        public SchedulerSelectionChangedEventArgs(SchedulerItem selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
