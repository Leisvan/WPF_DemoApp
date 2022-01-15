using Demo.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public static class EnumUtils
    {
        public static string TranslateEnumValue(Type enumType, object value)
        {
            string enumerationValueName = Enum.GetName(enumType, value);
            string translation = Resources.ResourceManager.GetString(enumerationValueName, CultureInfo.CurrentCulture);
            return translation;
        }
    }
}
