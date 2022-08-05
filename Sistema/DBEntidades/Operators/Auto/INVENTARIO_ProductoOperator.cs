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
    public partial class INVENTARIO_ProductoOperator
    {

        public static INVENTARIO_Producto GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Producto where Id = " + Id.ToString()).Tables[0];
            INVENTARIO_Producto iNVENTARIO_Producto = new INVENTARIO_Producto();
            foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(iNVENTARIO_Producto, value, null); }
                catch (System.ArgumentException) { }
            }
            return iNVENTARIO_Producto;
        }

        public static List<INVENTARIO_Producto> GetAll() 
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<INVENTARIO_Producto> lista = new List<INVENTARIO_Producto>();
            DataTable dt = db.GetDataSet("select " + columnas + " from INVENTARIO_Producto").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                INVENTARIO_Producto iNVENTARIO_Producto = new INVENTARIO_Producto();
                foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(iNVENTARIO_Producto, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(iNVENTARIO_Producto);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Codigo { get; set; } = 50;
			public static int CodigoBarra { get; set; } = 50;
			public static int Descripcion { get; set; } = 500;


        }

        public static INVENTARIO_Producto Save(INVENTARIO_Producto iNVENTARIO_Producto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoSave")) throw new PermisoException();
            if (iNVENTARIO_Producto.Id == -1) return Insert(iNVENTARIO_Producto);
            else return Update(iNVENTARIO_Producto);
        }

        public static INVENTARIO_Producto Insert(INVENTARIO_Producto iNVENTARIO_Producto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoSave")) throw new PermisoException();
            string sql = "insert into INVENTARIO_Producto(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Producto, null));
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
            iNVENTARIO_Producto.Id = Convert.ToInt32(resp);
            return iNVENTARIO_Producto;
        }

        public static INVENTARIO_Producto Update(INVENTARIO_Producto iNVENTARIO_Producto)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoINVENTARIO_ProductoSave")) throw new PermisoException();
            string sql = "update INVENTARIO_Producto set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(INVENTARIO_Producto).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(iNVENTARIO_Producto, null));
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
            sql += " where Id = " + iNVENTARIO_Producto.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return iNVENTARIO_Producto;
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
