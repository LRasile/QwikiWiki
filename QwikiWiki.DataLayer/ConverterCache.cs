using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Qwikiwiki.DataLayer
{
    [DebuggerStepThrough]
    public class ConverterDictionary : Dictionary<string, IConverter>
    { }

    public sealed class ConverterCache
    {


        private ConverterCache()
        {
        }

        public static IConverter GetConvertorInstanceByType(Type type)
        {
            IConverter result = null;

            if (_converterDictionary == null)
            {
                _converterDictionary = new ConverterDictionary();
            }

            if (type != null)
            {
                string itemKey = type.FullName;
                if (_converterDictionary.Count > 0 && _converterDictionary.ContainsKey(itemKey))
                {
                    result = _converterDictionary[itemKey];
                }
                else
                {
                    result = (IConverter)Activator.CreateInstance(type);

                    _converterDictionary.Add(itemKey, result);
                }
            }

            return result;
        }

#if DEBUG
        static ConverterDictionary Get
        {
            [DebuggerStepThrough()]
            get { return _converterDictionary; }
        }
#endif

        [Conditional("DEBUG")]
        static internal void DestroyCache()
        {
            _converterDictionary = null;
        }

        private static ConverterDictionary _converterDictionary;
    }
}
