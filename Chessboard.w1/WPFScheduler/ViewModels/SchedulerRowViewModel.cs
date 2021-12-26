using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFScheduler.Models;
using WPFScheduler.Views;
namespace WPFScheduler.ViewModels
{
    public class SchedulerRowViewModel : ViewModelBase
    {

        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                var oldDate = currentDate;
                currentDate = value;
                DetermineAlternativeCells();
                OnPropertyChanged("CurrentDate");

            }
        }
        private int range;
        public int Range
        {
            get { return range; }
            set { range = value; OnPropertyChanged("Range"); CreateCells(); }
        }
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

        private List<ISchedulerItemData> selectedItemsData;
        private string header;
        public string Header
        {
            get { return header; }
            set { header = value; OnPropertyChanged("Header"); }
        }

        public void DetermineAlternativeCells()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                var currentCell = Cells[i];
                if (CurrentDate.Date.AddDays(i).DayOfWeek == DayOfWeek.Saturday ||
                    CurrentDate.Date.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    if (!currentCell.IsAlternative)
                        currentCell.IsAlternative = true;
                }
                else
                {
                    if (currentCell.IsAlternative)
                        currentCell.IsAlternative = false;
                }
            }
        }

        private ObservableCollection<SchedulerItemViewModel> selectedItems;
        public ObservableCollection<SchedulerItemViewModel> SelectedItems
        {
            get { return selectedItems; }
            set { selectedItems = value; OnPropertyChanged("SelectedItems"); }
        }


        private List<SchedulerCell> cells;
        public List<SchedulerCell> Cells
        {
            get { return cells; }
            set { cells = value; OnPropertyChanged("Cells"); }
        }
        
        #endregion


        public SchedulerRowViewModel(ISchedulerRowData model)
        {
            IsLoaded = false;
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

        private void CreateCells()
        {
            var newCells = new List<SchedulerCell>();
            for (int i = 0; i < Range; i++)
            {
                var cell = new SchedulerCell();
                newCells.Add(cell);
            }
            Cells = newCells;
        }
        

        public void SetSelectedItems(DateTime beginDate, int availableRange)
        {

            if (true)
            {
                selectedItemsData = loader.FirstLoadData(Items, beginDate, availableRange);
                ObservableCollection<SchedulerItemViewModel> newSelectedData = null;
                if (selectedItemsData != null)
                {
                    newSelectedData = new ObservableCollection<SchedulerItemViewModel>();
                    foreach (var item in selectedItemsData)
                    {
                        newSelectedData.Add(new SchedulerItemViewModel(item));
                    }
                }
                SelectedItems = newSelectedData;
            }
        }

        public void UpdateSelectedItems(DateTime oldDate, DateTime newDate, int availableRange)
        {
            
            if (IsLoaded)
            {
                selectedItemsData = loader.FilterData(selectedItemsData, oldDate, newDate, availableRange);
                ObservableCollection<SchedulerItemViewModel> newSelectedData = null;
                if (selectedItemsData != null)
                {
                    newSelectedData = new ObservableCollection<SchedulerItemViewModel>();
                    foreach (var item in selectedItemsData)
                    {
                        newSelectedData.Add(new SchedulerItemViewModel(item));
                    }
                }
                SelectedItems = newSelectedData;
            }
            else
            {

            }
        }

        #endregion


        public bool IsLoaded { get; set; }
    }
}
