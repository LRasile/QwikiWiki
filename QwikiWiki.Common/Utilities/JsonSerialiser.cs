using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utilities
{
    public class JsonSerialiser
    {
        public static string ToJson<T>(T instance)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            //' TODO: DAB - need to complete this when time permits and do the same change in the web store
            //'settings.Converters.Add(New generaljsonenumconvertor())
            return ToJson<T>(instance, settings);
        }

        public static string ToJson<T>(T instance, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(instance, settings);
        }

        public static T FromJson<T>(string value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            return FromJson<T>(value, settings);
        }

        public static T FromJson<T>(string value, JsonSerializerSettings settings)
        {
            T instance = default(T);
            if (!String.IsNullOrEmpty(value))
            {
                instance = JsonConvert.DeserializeObject<T>(value, settings);
            }
            return instance;
        }

        public static string PullPropertyFromJson(string json, string propName)
        {
            int indexOfProp = json.IndexOf(propName);

            if (indexOfProp < 0) return "";

            string temp = json.Substring(indexOfProp);
            int indexOfPropColon = temp.IndexOf(":");
            temp = temp.Substring(indexOfPropColon + 1);
            string objValue = temp.Split(Convert.ToChar("}"))[0];
            string propValue = objValue.Split(Convert.ToChar(","))[0];
            propValue = propValue.Replace("\"", "");
            return propValue;
        }

        public static string PullPropertyDependentOnAnotherFromJson(string json, string dependProp, string dependPropValue, string propName)
        {
            string[] jsonObjs = json.Split(Convert.ToChar("}"));
            string temp;
            foreach (string jsonObj in jsonObjs)
            {
                temp = PullPropertyFromJson(jsonObj, dependProp);
                if (String.Compare(temp, dependPropValue, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    string propValue = PullPropertyFromJson(jsonObj, propName);
                    propValue = propValue.Replace("\"", "");
                    return propValue;
                }
            }
            return "";
        }

        public static string[] PullListPropertyFromJson(string json, string propName)
        {
            List<String> propValues = new List<string>();
            string[] jsonObjs = json.Split(Convert.ToChar("{"));
            foreach (string jsonObj in jsonObjs)
            {
                string temp = PullPropertyFromJson(jsonObj, propName);
                if (String.Compare(temp, String.Empty, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    propValues.Add(temp);
                }
            }
            return propValues.ToArray();
        }
    }
}
