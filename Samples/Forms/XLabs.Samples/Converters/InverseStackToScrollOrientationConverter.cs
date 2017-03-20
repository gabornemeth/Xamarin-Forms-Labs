using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace XLabs.Samples.Converters
{
    public class InverseStackToScrollOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((StackOrientation)value == StackOrientation.Horizontal)
                return ScrollOrientation.Vertical;
            else
                return ScrollOrientation.Horizontal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
