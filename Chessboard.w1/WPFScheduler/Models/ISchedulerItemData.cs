using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFScheduler.Models
{
    public interface ISchedulerItemData : IComparable, INotifyPropertyChanged
    {
        DateTime Date { get; set; }
        int Duration { get; set; }
        ISchedulerRowData Row { get; set; }
    }
}
