using Demo.Common;
using Demo.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Demo.Presentation.Extensions.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        private static EnumToStringConverter _default;
        public static EnumToStringConverter Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new EnumToStringConverter();
                }
                return _default;
            }
        }
        private readonly Dictionary<string, object> _translations = new Dictionary<string, object>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type enumerationType = (parameter is Type) ? parameter as Type : value?.GetType();
            if (enumerationType != null)
            {
                try
                {
                    string valueName = Enum.GetName(enumerationType, value) ?? string.Empty;
                    return Resources.ResourceManager.GetString(valueName, culture);
                }
                catch (ArgumentException)
                {
                    Array values = Enum.GetValues(enumerationType);
                    var translationList = new List<string>();

                    for (int i = 0; i < values.Length; i++)
                    {
                        object enumerationValue = values.GetValue(i);
                        string translation = EnumUtils.TranslateEnumValue(enumerationType, enumerationValue);

                        if (!_translations.ContainsKey(translation))
                        {
                            _translations.Add(translation, enumerationValue);
                        }
                        translationList.Add(translation);
                    }

                    return translationList.ToArray();
                }
            }

            string message = "\"parameter\" or must be of enumeration type";
            throw new ArgumentException(message, nameof(parameter));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _translations[(string)value];
        }
    }
}
