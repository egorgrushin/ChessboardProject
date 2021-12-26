using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScheduler;
using WPFScheduler.Models;

namespace Chessboard.w1
{
    class MyRow : ISchedulerRowData
    {
        public int Number { get; set; }

        public int CompareTo(object obj)
        {
            return Number.CompareTo(((MyRow)obj).Number);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
