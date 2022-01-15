using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    [ExcludeFromCodeCoverage]
    public static class Check
    {
        public static void AssertIsNotEmptyString(this string value, string argumentName)
        {
            if (value == string.Empty)
                throw new ArgumentException("String cannot be empty.", argumentName);
        }
        public static void AssertIsNotNull(this object value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);
        }
        private static void AssertIsOfType(this object value, Type type, string argumentName)
        {
            value.AssertIsNotNull("value");
            type.AssertIsNotNull("type");
            argumentName.AssertIsNotNull("argumentName");

            Type valueType = value.GetType();

            while (valueType != null)
            {
                if (valueType == type)
                    return;

                valueType = valueType.BaseType;
            }

            throw new ArgumentException($"Expected a type implementing: {type.FullName}.", argumentName);
        }
        public static void AssertIsOfType<T>(this object value, string argumentName)
        {
            AssertIsOfType(value, typeof(T), argumentName);
        }
    }
}