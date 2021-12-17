using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFScheduler.Models;

namespace WPFScheduler
{
    public class Loader
    {
        private int maxDuration;
        private int currentOutsideIndex;

        public Loader() { }


        private List<ISchedulerItemData> originalSource;

        public ObservableCollection<ISchedulerItemData> FilterData(List<ISchedulerItemData> sortableSource, DateTime oldDate, DateTime newDate, int availableRange)
        {
			var isPositiveDirection = oldDate.Date < newDate.Date;
			var selectedData = new ObservableCollection<ISchedulerItemData>();

            if (originalSource != null && originalSource.Count != 0)
            {
				int i = 0;
				var newEndDate = newDate.AddDays(availableRange);
				var leftBorderDate = newDate.AddDays(-maxDuration).Date;

				if(isPositiveDirection)
				{
					//Start from oldDate - maxDuration stop at newDate + availableRange, going forwards

					//May be somehow change into newDate - maxDuration to newDate + availableRange
					//if necessary
					var rightBorderDate = oldDate.AddDays(-maxDuration).Date;

					i = sortableSource.Count != 0 ? originalSource.IndexOf(sortableSource[sortableSource.Count - 1]) : currentOutsideIndex;
					while(originalSource[i].Date > rightBorderDate)
					{
						i--;
						if(i < 0)
						{
							i = 0;
							break;
						}
					}
					var currentItemDate = originalSource[i].Date;

					while(currentItemDate < newEndDate)
					{
						if(currentItemDate > leftBorderDate && currentItemDate <= newEndDate)
						{
							selectedData.Add(originalSource[i]);
						}

						i++;
						if(i > originalSource.Count - 1)
						{
							i = originalSource.Count - 1;
							break;
						}
						currentItemDate = originalSource[i].Date;
					}
				}
				else
				{
					//Start from oldDate + availableRange stop at newDate - maxDuration, going backwards

					//May be somehow change into newDate + availableRange to newDate - maxDuration
					//if necessary
					i = sortableSource.Count != 0 ? originalSource.IndexOf(sortableSource[0]) : currentOutsideIndex;
					var oldEndDate = oldDate.AddDays(availableRange);
					while(originalSource[i].Date < oldEndDate)
					{
						i++;
						if(i > originalSource.Count - 1)
						{
							i = originalSource.Count - 1;
							break;
						}
					}

					var currentItemDate = originalSource[i].Date;

					while(currentItemDate > leftBorderDate)
					{
						if(currentItemDate > leftBorderDate && currentItemDate < newEndDate)
						{
							selectedData.Add(originalSource[i]);
						}

						i--;
						if(i < 0)
						{
							i = 0;
							break;
						}
						currentItemDate = originalSource[i].Date;
					}
				}
			}
            return selectedData;
        }

        public ObservableCollection<ISchedulerItemData> FirstLoadData(List<ISchedulerItemData> source, DateTime beginDate, int availableRange)
        {
            originalSource = source;
			DateTime endDate = beginDate.AddDays(availableRange - 1);			
            ObservableCollection<ISchedulerItemData> selectedData = null;

            if (source != null && source.Count != 0)
            {
                bool isFounded = false;
                currentOutsideIndex = 0;
                selectedData = new ObservableCollection<ISchedulerItemData>();
                maxDuration = source[0].Duration;

                foreach (var item in source)
                {
					var currentItemDate = item.Date.Date;

					if(currentItemDate.AddDays(item.Duration) > beginDate.Date && currentItemDate < endDate)
					{
						selectedData.Add(item);
					}

                    if (!isFounded && currentItemDate > beginDate.AddDays(availableRange - 1).Date)
					{
						currentOutsideIndex = source.IndexOf(item);
						isFounded = true;
					}

					if (item.Duration > maxDuration)
					{
                        maxDuration = item.Duration;
					}
                }
            }
            return selectedData;
        }

    }
}
