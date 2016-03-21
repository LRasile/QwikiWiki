using System;
using System.Data;

namespace Qwikiwiki.DataLayer
{
    public static class IDataReaderExtensions
    {
        public static bool HasColumn(this IDataReader instance, string columnName)
        {
            bool found = false;

            if (instance != null && !string.IsNullOrEmpty(columnName))
            {
                Int32 counter = 0;

                while (counter < instance.FieldCount && !found)
                {
                    found = (instance.GetName(counter).Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
                    counter += 1;
                }
            }

            return found;
        }
    }
}
