using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFScheduler
{
    [ValueConversion(typeof(object), typeof(string))]
    public class DateShiftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            return string.Format("{0:dd.MM}", ((DateTime)value).AddDays((int)parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(DateTime), typeof(double))]
    public class DateScrollValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.Subtract(DateTime.MinValue).Days;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return DateTime.MinValue.AddDays((double)value);
        }
    }

    [ValueConversion(typeof(DateTime), typeof(double))]
    public class DateThreshholdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.Subtract(DateTime.MinValue).Days;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(DateTime), typeof(Brush))]
    public class DateBrushConverter : IValueConverter
    {
        Brush gridCellBrush;
        Brush gridCellAlternativeBrush;
        public DateBrushConverter()
        {
            Uri resourceLocater = new Uri("/WPFScheduler;component/Themes/Generic.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            gridCellBrush = resourceDictionary["gridCellBrush"] as SolidColorBrush;
            gridCellAlternativeBrush = resourceDictionary["gridCellAlternativeBrush"] as SolidColorBrush;
        }
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            var date = ((DateTime)value).AddDays((int)parameter);

            var weekDayBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBBB5E5"));
            var  dayOffBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF026676"));
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ?
                gridCellAlternativeBrush : gridCellBrush;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(DateTime), typeof(Brush))]
    public class DateHeaderBrushConverter : IValueConverter
    {
        Brush columnHeadeBackgroundBrush;
        Brush columnHeadeBackgroundAlternativeBrush;
        public DateHeaderBrushConverter()
        {
            Uri resourceLocater = new Uri("/WPFScheduler;component/Themes/Generic.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            columnHeadeBackgroundBrush = resourceDictionary["columnHeadeBackgroundBrush"] as SolidColorBrush;
            columnHeadeBackgroundAlternativeBrush = resourceDictionary["columnHeadeBackgroundAlternativeBrush"] as SolidColorBrush;
        }
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            var date = ((DateTime)value).AddDays((int)parameter);
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ?
                columnHeadeBackgroundAlternativeBrush : columnHeadeBackgroundBrush;

        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(int), typeof(double))]
    public class DoubleAddParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            var add = double.Parse((string)parameter);
            var baseValue = (int)value;
            return baseValue + add;

        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}