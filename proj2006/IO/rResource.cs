using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.IO;
using project2006.Util;

namespace project2006.IO
{
    class RResource
    {
        ResourceManager resourceManager = null;
        static Dictionary<string, Assembly> assemblyDictionary = new Dictionary<string, Assembly>();
        object lockObject = new object();

        internal RResource(string name, string assemblyName)
        {
            Assembly assembly = null;
            if (name == null)
                return;
            if (assemblyName == null)
                return;
            lock (lockObject)
            {
                if (assemblyDictionary.ContainsKey(assemblyName))
                {
                    assembly = assemblyDictionary[assemblyName];
                }
                else
                {
                    if (!File.Exists(assemblyName))
                        return;
                    try
                    {
                        assembly = Assembly.LoadFile(assemblyName);
                    }
                    catch (Exception ex)
                    {
                        //TODO:异常处理
                    }
                }
            }
            if (assembly == null)
                return;
            resourceManager = new System.Resources.ResourceManager(name, assembly);
        }

        internal object GetResource(string resourceName)
        {
            return GetResource(resourceName, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal object GetResource(string resourceName, System.Globalization.CultureInfo cultureInfo)
        {
            object obj = null;
            try
            {
                obj = resourceManager.GetObject(resourceName, cultureInfo);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        internal string GetString(string resourceName)
        {
            return GetString(resourceName, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal string GetString(string resourceName, System.Globalization.CultureInfo cultureInfo)
        {
            string obj = null;
            try
            {
                obj = resourceManager.GetString(resourceName, cultureInfo);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        internal UnmanagedMemoryStream GetStream(string resourceName)
        {
            return GetStream(resourceName, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal UnmanagedMemoryStream GetStream(string resourceName, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                return resourceManager.GetStream(resourceName, cultureInfo);
            }
            catch (Exception)
            {
            }
            return null;
        }

        internal T GetResource<T>(string resourceName)
        {
            return GetResource<T>(resourceName, System.Globalization.CultureInfo.CurrentCulture);
        }

        internal T GetResource<T>(string resourceName, System.Globalization.CultureInfo cultureInfo)
        {
            object obj = null;
            try
            {
                obj = resourceManager.GetObject(resourceName, cultureInfo);
            }
            catch (Exception)
            {
            }
            if (obj is T)
                return (T)obj;
            return default(T);
        }
    }
}
