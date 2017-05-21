using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace lab_3
{
    public static class EnumHelper<T>
    {
        public static string GetEnumDescription(string value, Type type)
        {
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static List<string> GetAllDescriptions()
        {
            Type type = typeof(T);
            string[] temp = Enum.GetNames(type);
            List<string> result = new List<string>();
            for (int i = 0; i < temp.Length; i++)
            {
                result.Add(GetEnumDescription(temp[i], type));
            }
            return result;
        }

        public static List<string> GetEnumValues()
        {
            Type type = typeof(T);
            string[] values = Enum.GetNames(type);
            List<string> result = new List<string>(); 
            foreach (string someValue in values)
            {
                result.Add(someValue);
            }
            return result;
        }
    }

}
