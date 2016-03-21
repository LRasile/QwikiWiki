using System;

namespace Qwikiwiki.DataLayer
{
    public interface IConverter
    {
        object ConvertValue(object value);
        object ConvertValue(object value, IFormatProvider provider);
    }
}
