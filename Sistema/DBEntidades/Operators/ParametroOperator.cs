using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using DbEntidades.Entities;
using System.Data.SqlClient;
using LibDB2;
using System.Runtime.Caching;

namespace DbEntidades.Operators
{
    public partial class ParametroOperator
    {
        protected static MemoryCache cache = MemoryCache.Default;
        public static string GetParametroString(string name)
        {
            string cacheName = System.Web.HttpContext.Current.Session["WebApplication"].ToString() + "_Parametro_" + name;
            if (cache[cacheName] != null)
            {
                return cache[cacheName].ToString();
            }
            else
            {
                string valor = ParametroOperator.GetAll().Where(x => x.Name == name).FirstOrDefault().Value;
                cache[cacheName] = valor;
                return valor;
            }
        }

        public static int GetParametroInt(string name)
        {
            return Convert.ToInt32(GetParametroString(name));
        }

        public static decimal GetParametroDecimal(string name)
        {
            return Convert.ToDecimal(GetParametroString(name));
        }
        public static void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }
    }
}
