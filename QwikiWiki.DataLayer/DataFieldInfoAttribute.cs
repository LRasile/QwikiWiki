using System;
using System.Diagnostics;

namespace Qwikiwiki.DataLayer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataFieldInfoAttribute : Attribute
    {

        [DebuggerStepThrough()]
        public DataFieldInfoAttribute(string fieldName)
            : this(fieldName, null)
        {
        }

        [DebuggerStepThrough()]
        public DataFieldInfoAttribute(string fieldName, Type convertor)
        {
            _fieldName = fieldName;
            _convertor = convertor;
        }

        public string FieldName
        {
            [DebuggerStepThrough()]
            get { return _fieldName; }
        }

        public Type Convertor
        {
            [DebuggerStepThrough()]
            get { return _convertor; }
        }

        public bool HasConvertor
        {
            [DebuggerStepThrough()]
            get { return (Convertor != null); }
        }

        private string _fieldName;
        private Type _convertor = null;
    }
}
