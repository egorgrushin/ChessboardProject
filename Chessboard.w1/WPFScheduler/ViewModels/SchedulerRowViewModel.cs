using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScheduler.Models;
namespace WPFScheduler.ViewModels
{
    public class SchedulerRowViewModel : ViewModelBase
    {

        private Loader loader;
        #region Properties
        private ISchedulerRowData model;
        public ISchedulerRowData Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged("Model"); }
        }

        private List<ISchedulerItemData> items;
        public List<ISchedulerItemData> Items
        {
            get { return items; }
            set { items = value;  }
        }

        public void SetSelectedItems(DateTime beginDate, int availableRange)
        {
            items.Sort();
            var newSelectedData = new ObservableCollection<SchedulerItemViewModel>();
            var filteredData = loader.FirstLoadData(items, beginDate, availableRange);
            foreach (var item in filteredData)
            {
                newSelectedData.Add(new SchedulerItemViewModel(item));
            }
            SelectedItems = newSelectedData;
        }

        private string header;
        public string Header
        {
            get { return header; }
            set { header = value; OnPropertyChanged("Header"); }
        }

        private ObservableCollection<SchedulerItemViewModel> selectedItems;
        public ObservableCollection<SchedulerItemViewModel> SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value; OnPropertyChanged("SelectedItems"); }
        }
        

        
        #endregion


        public SchedulerRowViewModel(ISchedulerRowData model)
        {
            Model = model;
            Header = string.Format("Row {0}", model.Number);
            Items = new List<ISchedulerItemData>();
            loader = new Loader();
            Model.PropertyChanged += Model_PropertyChanged;
        }

        void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Header = string.Format("Row {0}", model.Number);
        }

        #region Methods

        public void UpdateSelectedItems(DateTime oldDate, DateTime newDate, int availableRange)
        {
            var filteredData = loader.FilterData(Items, oldDate, newDate, availableRange);
            var newSelectedData = new ObservableCollection<SchedulerItemViewModel>();
            foreach (var item in filteredData)
            {
                newSelectedData.Add(new SchedulerItemViewModel(item));
            }
            SelectedItems = newSelectedData;
        }

        #endregion
        
    }
}
