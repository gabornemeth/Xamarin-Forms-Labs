using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace XLabs.Samples.Converters
{
    public class InverseStackOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((StackOrientation)value == StackOrientation.Horizontal)
                return StackOrientation.Vertical;
            else
                return StackOrientation.Horizontal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
