using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScheduler.Models;
namespace WPFScheduler.ViewModels
{
    public class SchedulerItemViewModel : ViewModelBase
    {
        #region Fields
        private ISchedulerItemData model;
        #endregion

        #region Proxy properties

        public DateTime Date
        {
            get { return model.Date; }
            set { model.Date = value; OnPropertyChanged("Date"); }
        }

        public int Duration
        {
            get { return model.Duration; }
            set { model.Duration = value; OnPropertyChanged("Duration"); }
        }

        public ISchedulerRowData Row
        {
            get { return model.Row; }
            set { model.Row = value; OnPropertyChanged("Row"); }
        }
        
        #endregion

        public SchedulerItemViewModel(ISchedulerItemData model)
        {
            this.model = model;
            model.PropertyChanged += model_PropertyChanged;
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }


    }
}
