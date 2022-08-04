using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using DbEntidades.Entities;
using System.Data.SqlClient;
using LibDB2;

namespace DbEntidades.Operators
{
    public partial class StockOperator
    {

        public static Stock GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoStockBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Stock).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Stock where ID = " + ID.ToString()).Tables[0];
            Stock stock = new Stock();
            foreach (PropertyInfo prop in typeof(Stock).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(stock, value, null); }
                catch (System.ArgumentException) { }
            }
            return stock;
        }

        public static List<Stock> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoStockBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Stock).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Stock> lista = new List<Stock>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Stock").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Stock stock = new Stock();
                foreach (PropertyInfo prop in typeof(Stock).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(stock, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(stock);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Stock Save(Stock stock)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoStockSave")) throw new PermisoException();
            if (stock.ID == -1) return Insert(stock);
            else return Update(stock);
        }

        public static Stock Insert(Stock stock)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoStockSave")) throw new PermisoException();
            string sql = "insert into Stock(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Stock).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(stock, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ID values (" + valores + ")";
            DB db = new DB();
            List<object> parametros = new List<object>();
            for (int i = 0; i < param.Count; i++)
            {
                parametros.Add(param[i]);
                parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
            }
            //object resp = db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            stock.ID = Convert.ToInt32(resp);
            return stock;
        }

        public static Stock Update(Stock stock)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoStockSave")) throw new PermisoException();
            string sql = "update Stock set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Stock).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(stock, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            sql += columnas;
            List<object> parametros = new List<object>();
            for (int i = 0; i<param.Count; i++)
            {
                parametros.Add(param[i]);
                parametros.Add(valor[i]);
                SqlParameter p = new SqlParameter(param[i].ToString(), valor[i]);
                sqlParams.Add(p);
        }
            sql += " where ID = " + stock.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return stock;
    }

        private static string GetComilla(string tipo)
        {
            switch (tipo) //son tipos de c#
            {
                case "Int32": return "";
                case "String": return "'";
                case "DateTime": return "'";
                case "Nullable`1": return "'";
            }
            return "";
        }

        public static string VerificaStringNull(string v)
        {
            return v == string.Empty ? null : v;
        }
    }
}
