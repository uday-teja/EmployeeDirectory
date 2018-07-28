using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace EmployeeDirectory
{
    public class ColorChangeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (System.Convert.ToDouble(value) > 10000.00)
            {
                return Brushes.Brown;
            }
            else if(System.Convert.ToDouble(value) > 20000.00)
            {
                return Brushes.YellowGreen;

            }
            else if(System.Convert.ToDouble(value) > 30000.00)
            {
                return Brushes.Tomato;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}