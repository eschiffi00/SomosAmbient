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
    public partial class Cat_ProductosOperator
    {

        public static Cat_Productos GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCat_ProductosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cat_Productos where ID = " + ID.ToString()).Tables[0];
            Cat_Productos cat_Productos = new Cat_Productos();
            foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(cat_Productos, value, null); }
                catch (System.ArgumentException) { }
            }
            return cat_Productos;
        }

        public static List<Cat_Productos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCat_ProductosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Cat_Productos> lista = new List<Cat_Productos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Cat_Productos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Cat_Productos cat_Productos = new Cat_Productos();
                foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(cat_Productos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(cat_Productos);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static Cat_Productos Save(Cat_Productos cat_Productos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCat_ProductosSave")) throw new PermisoException();
            if (cat_Productos.ID == -1) return Insert(cat_Productos);
            else return Update(cat_Productos);
        }

        public static Cat_Productos Insert(Cat_Productos cat_Productos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCat_ProductosSave")) throw new PermisoException();
            string sql = "insert into Cat_Productos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cat_Productos, null));
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
            cat_Productos.ID = Convert.ToInt32(resp);
            return cat_Productos;
        }

        public static Cat_Productos Update(Cat_Productos cat_Productos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCat_ProductosSave")) throw new PermisoException();
            string sql = "update Cat_Productos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Cat_Productos).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(cat_Productos, null));
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
            sql += " where ID = " + cat_Productos.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return cat_Productos;
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
