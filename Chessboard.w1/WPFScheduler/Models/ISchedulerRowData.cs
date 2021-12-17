using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFScheduler.Models
{
    public interface ISchedulerRowData : IComparable, INotifyPropertyChanged
    {
        int Number { get; set; }
    }
}
