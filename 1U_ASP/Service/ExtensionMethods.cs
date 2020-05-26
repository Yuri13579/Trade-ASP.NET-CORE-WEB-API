using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _1U_ASP.Service
{
    public static class ExtensionMethods
    {
        public static void TrimAllStrings<T>(this T obj)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public
                                              | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            foreach (var p in obj.GetType().GetProperties(flags))
            {
                var currentNodeType = p.PropertyType;
                if (currentNodeType == typeof(string))
                {
                    var currentValue = (string)p.GetValue(obj, null);
                    if (currentValue != null) p.SetValue(obj, currentValue.Trim(), null);
                }
                else if (currentNodeType == typeof(List<string>))
                {
                    var source = (List<string>)p.GetValue(obj, null);
                    var dest = new List<string>();

                    if (source != null)
                    {
                        source.ForEach(x => dest.Add(x.Trim()));

                        p.SetValue(obj, dest, null);
                    }
                }
            }
        }

        public static void TrimAllStringsWhithNull<T>(this T obj, bool replaceNullWithEmptyString = true)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public
                                              | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            if (obj is IEnumerable)
                foreach (var item in obj as IEnumerable)
                    item.TrimAllStringsWhithNull();

            var t = obj.GetType().GetProperties(flags);
            foreach (var p in t)
            {
                var currentNodeType = p.PropertyType;
                if (currentNodeType == typeof(string))
                {
                    var currentValue = (string)p.GetValue(obj);
                    if (currentValue != null)
                        p.SetValue(obj, currentValue.Trim());
                    else if (replaceNullWithEmptyString)
                        p.SetValue(obj, "");
                }
                else if (currentNodeType == typeof(List<string>))
                {
                    var source = (List<string>)p.GetValue(obj);
                    var dest = new List<string>();

                    if (source != null)
                    {
                        source.ForEach(x =>
                        {
                            if (x != null)
                                dest.Add(x.Trim());
                        });

                        p.SetValue(obj, dest);
                    }
                }
            }
        }
    }
}
