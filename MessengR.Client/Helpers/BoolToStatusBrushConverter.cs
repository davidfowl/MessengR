using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace MessengR.Client.Helpers
{
    class BoolToStatusBrushConverter : IValueConverter
    {
        public SolidColorBrush OnlineBrush { get; set; }
        public SolidColorBrush OfflineBrush { get; set; }

        public BoolToStatusBrushConverter()
        {
            OnlineBrush = new SolidColorBrush(Colors.Green);
            OfflineBrush = new SolidColorBrush(Colors.Red);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return OnlineBrush;
            return OfflineBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }

    class BoolToStatusStringConverter : IValueConverter
    {
        public string OnlineString { get; set; }
        public string OfflineString { get; set; }

        public BoolToStatusStringConverter()
        {
            OnlineString = "Online";
            OfflineString = "Offline";
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return OnlineString;
            return OfflineString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
