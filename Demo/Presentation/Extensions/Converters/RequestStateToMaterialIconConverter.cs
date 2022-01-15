using Demo.Abstractions.Domain.Entities;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Demo.Presentation.Extensions.Converters
{
    public class RequestStateToMaterialIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RequestState state)
            {
                switch (state)
                {
                    default:
                    case RequestState.Pending: return PackIconKind.CircleSlice5;
                    case RequestState.Accepted: return PackIconKind.CheckCircle;
                    case RequestState.Rejected: return PackIconKind.CloseThick;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
