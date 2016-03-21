using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Qwikiwiki.DataLayer
{

    /// <summary>
    /// ReflectionHelper
    /// </summary>
    /// <remarks>
    /// Contains reflection helper methods
    /// </remarks>
    [DebuggerStepThrough]
    public sealed class ReflectionHelper
    {
        /// <summary>
        /// Constructor marked as private to inhibit instance creation
        /// </summary>

        private ReflectionHelper()
        {
        }

        public static TEntity ReflectType<TEntity>(DataRow dataRow) where TEntity : class, new()
        {
            TEntity instanceToPopulate = GetNewEntity<TEntity>();

            //get propertyinfo array from intended type
            PropertyInfo[] pis = GetPropertiesFromObject<TEntity>();

            //for each public property on the original
            foreach (PropertyInfo pi in pis)
            {
                //get the DataFieldInfoAttribute array from the propertyinfo instance
                DataFieldInfoAttribute[] datafieldAttributeArray = GetDataFieldInfoAttributeArrayFromPropertyInfo(pi);
                //this attribute is marked with AllowMultiple=false, sp we should only have one
                //if <> 1 then not interested in it
                if (datafieldAttributeArray.Length == 1)
                {
                    DataFieldInfoAttribute dfa = datafieldAttributeArray[0];
                    //only attempt to set value if data row has column
                    object rowItem = dataRow[dfa.FieldName];
                    if (rowItem != null)
                    {
                        object dbValue = ConvertValue(dfa, rowItem);

                        SetProperty<TEntity>(instanceToPopulate, pi, dbValue);
                    }
                }
                //If the object fauils to have teh attribute try and match on property name
                else if (datafieldAttributeArray.Length == 0)
                {
                    string propertyName = pi.Name;
                    object rowItem = dataRow[propertyName];
                    if (rowItem != null)
                    {
                        //object dbValue = ConvertValue(dfa, rowItem);
                        SetProperty<TEntity>(instanceToPopulate, pi, rowItem);
                    }
                }
            }

            return instanceToPopulate;
        }

        public static TEntity ReflectType<TEntity>(IDataReader dataReader) where TEntity : class, new()
        {
            TEntity instanceToPopulate = GetNewEntity<TEntity>();

            //get propertyinfo array from intended type
            PropertyInfo[] pis = GetPropertiesFromObject<TEntity>();

            //for each public property on the original
            foreach (PropertyInfo pi in pis)
            {
                //get the DataFieldInfoAttribute array from the propertyinfo instance
                DataFieldInfoAttribute[] datafieldAttributeArray = GetDataFieldInfoAttributeArrayFromPropertyInfo(pi);
                //this attribute is marked with AllowMultiple=false, sp we should only have one
                //if <> 1 then not interested in it
                if (datafieldAttributeArray.Length == 1)
                {
                    DataFieldInfoAttribute dfa = datafieldAttributeArray[0];
                    //only attempt to set value if data reader has column
                    if (dataReader.HasColumn(dfa.FieldName))
                    {
                        object dbValue = ConvertValue(dfa, dataReader[dfa.FieldName]);

                        SetProperty<TEntity>(instanceToPopulate, pi, dbValue);
                    }
                }

                //If the object fauils to have teh attribute try and match on property name
                else if (datafieldAttributeArray.Length == 0)
                {
                    string propertyName = pi.Name;
                    if (dataReader.HasColumn(propertyName))
                    {
                        object dbValue = dataReader[propertyName];
                        //object dbValue = ConvertValue(dfa, rowItem);
                        SetProperty<TEntity>(instanceToPopulate, pi, dbValue);
                    }
                }
            }

            return instanceToPopulate;
        }

        private static object ConvertValue(DataFieldInfoAttribute dataFieldInfoAttribute, object dbValue)
        {
            object result = dbValue;
            //if the attribute has a convertor definexd 
            //get an instance and attempt the defined conversion
            if (dataFieldInfoAttribute.HasConvertor)
            {
                IConverter converterInstance = GetConvertorInstanceByType(dataFieldInfoAttribute.Convertor);

                result = converterInstance.ConvertValue(dbValue);
            }
            return result;
        }

        private static IConverter GetConvertorInstanceByType(Type type)
        {
            return ConverterCache.GetConvertorInstanceByType(type);
        }

        static internal DataFieldInfoAttribute[] GetDataFieldInfoAttributeArrayFromPropertyInfo(PropertyInfo propertyInfo)
        {
            return (DataFieldInfoAttribute[])propertyInfo.GetCustomAttributes(typeof(DataFieldInfoAttribute), false);
        }

        static internal TEntity GetNewEntity<TEntity>() where TEntity : class, new()
        {
            return new TEntity();
        }

        static internal PropertyInfo[] GetPropertiesFromObject<TEntity>() where TEntity : class
        {
            return typeof(TEntity).GetProperties(BindingFlagsToUse());
        }

        static internal PropertyInfo GetPropertyFromObject<TEntity>(string propertyName) where TEntity : class
        {
            return typeof(TEntity).GetProperty(propertyName, BindingFlagsToUse());
        }

        static internal void SetProperty<TEntity>(TEntity instance, PropertyInfo propertyInfo, object value)
        {

            if (!object.ReferenceEquals(value, DBNull.Value) && value != null)
            {
                IConvertible temp = value as IConvertible;

                if (temp != null)
                {
                    propertyInfo.SetValue(instance, ((IConvertible)value).ToType(propertyInfo.PropertyType, null), null);
                }
                else
                {
                    propertyInfo.SetValue(instance, value, null);
                }
            }
        }

        static internal BindingFlags BindingFlagsToUse()
        {
            return BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
        }
    }
}
