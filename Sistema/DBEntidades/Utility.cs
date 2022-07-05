using LibDB2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DbEntidades
{
    public static class Utility
    {
        public static DataTable ClassToDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
        public static string GetPlural(string s)
        {
            if (s.Last() == 's') return s;
            if (s.Last() == 'a' || s.Last() == 'e' || s.Last() == 'i' || s.Last() == 'o' || s.Last() == 'u')
                return s + "s";
            else return s + "es";
        }
        public static DataTable GetDatabaseColumnInfo(string tablename, string columnname)
        {
            string query = @"
                select DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION, NUMERIC_SCALE
                from INFORMATION_SCHEMA.COLUMNS
                where TABLE_NAME = @tablename
                  and COLUMN_NAME = @columnname
            ";

            DB db = new DB();
            return db.GetDataTable(query, CommandType.Text, 
                new SqlParameter("@tablename", tablename), 
                new SqlParameter("@columnname", columnname));
        }
    }
}
