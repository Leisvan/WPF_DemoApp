using Demo.Abstractions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo.Presentation.Extensions.Converters
{
    public class RequestStateToBrushConverter : IValueConverter
    {
        private static readonly Color RejectedColor = Color.FromRgb(119, 9, 5);
        private static readonly Color AcceptedColor = Color.FromRgb(15, 128, 54);
        private static readonly Color PendingColor = Color.FromRgb(194, 133, 10);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RequestState state)
            {
                switch (state)
                {
                    default:
                    case RequestState.Pending: return new SolidColorBrush(PendingColor);
                    case RequestState.Accepted: return new SolidColorBrush(AcceptedColor);
                    case RequestState.Rejected: return new SolidColorBrush(RejectedColor);
                }
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return RequestState.Pending;
        }
    }
}
