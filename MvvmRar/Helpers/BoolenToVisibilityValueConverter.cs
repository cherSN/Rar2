using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MvvmRar.Helpers
{
    public class BoolenToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object convertedValue = null;
            if (targetType == typeof(System.Windows.Visibility))
            {
                Visibility invisibleValue = Visibility.Hidden;
                try
                {
                    invisibleValue = (Visibility)parameter;
                }
                catch
                {
                    invisibleValue = Visibility.Hidden;
                }
                try
                {
                    bool sourceBoolean = (bool)value;
                    convertedValue = sourceBoolean ? Visibility.Visible : invisibleValue;
                }
                catch
                {
                    convertedValue = Visibility.Visible;
                }
            }
            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

