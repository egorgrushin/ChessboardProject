using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFScheduler;
using WPFScheduler.Models;

namespace Chessboard.w1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rd = new Random();
        public ObservableCollection<ISchedulerItemData> DebugList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var list = new List<ISchedulerRowData>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(new MyRow() { Number = i });
            }
            scheduler.Positions= list;

            var items = new List<ISchedulerItemData>();
            var lastDur = -1;
            int j = 0;
            int z = 1;
            foreach (var row in list)
            {
                var lastDate = new DateTime(2014, 8, 27);
                for (int i = 0; i < 100; i++)
                {
                    lastDate = lastDate.AddDays(1);
                    items.Add(new MyClass() { Name = i.ToString(), Duration = 1, Date = lastDate, Row = row });
                }

            }
            scheduler.Items = items;

            /*items.Add(new MyClass() { Name = "Name", Duration = 3, Date = new DateTime(2014, 9, 3), Row = list[0] });
            items.Add(new MyClass() { Name = "Name", Duration = 3, Date = new DateTime(2014, 9, 3), Row = list[1] });*/
            //DebugList = new ObservableCollection<ISchedulerItemData>();
            //foreach (var item in items)
            //    DebugList.Add(item);
            //var binding = new Binding("DebugList");
            //binding.Mode = BindingMode.TwoWay;
            //listbox.DataContext = this;
            //listbox.SetBinding(ListBox.ItemsSourceProperty, binding);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
                scheduler.CurrentDate = scheduler.CurrentDate.AddDays(1);
            if (e.Key == Key.Left)
                scheduler.CurrentDate = scheduler.CurrentDate.AddDays(-1);
        }
    }
}
