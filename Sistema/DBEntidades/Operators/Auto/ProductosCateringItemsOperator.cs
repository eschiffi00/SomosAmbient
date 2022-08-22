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
    public partial class ProductosCateringItemsOperator
    {

        public static ProductosCateringItems GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductosCateringItemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProductosCateringItems where Id = " + Id.ToString()).Tables[0];
            ProductosCateringItems productosCateringItems = new ProductosCateringItems();
            foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(productosCateringItems, value, null); }
                catch (System.ArgumentException) { }
            }
            return productosCateringItems;
        }

        public static List<ProductosCateringItems> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductosCateringItemsBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ProductosCateringItems> lista = new List<ProductosCateringItems>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ProductosCateringItems").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ProductosCateringItems productosCateringItems = new ProductosCateringItems();
                foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(productosCateringItems, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(productosCateringItems);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ProductosCateringItems Save(ProductosCateringItems productosCateringItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductosCateringItemsSave")) throw new PermisoException();
            if (productosCateringItems.Id == -1) return Insert(productosCateringItems);
            else return Update(productosCateringItems);
        }

        public static ProductosCateringItems Insert(ProductosCateringItems productosCateringItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductosCateringItemsSave")) throw new PermisoException();
            string sql = "insert into ProductosCateringItems(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(productosCateringItems, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.Id values (" + valores + ")";
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
            productosCateringItems.Id = Convert.ToInt32(resp);
            return productosCateringItems;
        }

        public static ProductosCateringItems Update(ProductosCateringItems productosCateringItems)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductosCateringItemsSave")) throw new PermisoException();
            string sql = "update ProductosCateringItems set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ProductosCateringItems).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(productosCateringItems, null));
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
            sql += " where Id = " + productosCateringItems.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return productosCateringItems;
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
