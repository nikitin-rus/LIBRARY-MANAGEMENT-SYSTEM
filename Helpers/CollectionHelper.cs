using System;
using System.Collections.Generic;
using System.Reflection;

namespace LIBRARY_MANAGEMENT_SYSTEM.Helpers
{
    public class CollectionHelper
    {
        public static List<T> GetFilteredByPropertySubstr<T>
            (List<T> collection, string propName, string substring) 
        {
            PropertyInfo? pInfo = typeof(T).GetProperty(propName) ?? throw new Exception("No such property.");

            if (pInfo.PropertyType != typeof(string))
            {
                throw new Exception("Property type is not string.");
            }

            List<T> result = [];

            foreach (T item in collection)
            {
                if (pInfo.GetValue(item) is string propValue)
                {
                    if (propValue.Contains(substring, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}
