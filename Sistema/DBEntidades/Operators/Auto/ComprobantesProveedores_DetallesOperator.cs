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
    public partial class ComprobantesProveedores_DetallesOperator
    {

        public static ComprobantesProveedores_Detalles GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedores_DetallesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantesProveedores_Detalles where Id = " + Id.ToString()).Tables[0];
            ComprobantesProveedores_Detalles comprobantesProveedores_Detalles = new ComprobantesProveedores_Detalles();
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(comprobantesProveedores_Detalles, value, null); }
                catch (System.ArgumentException) { }
            }
            return comprobantesProveedores_Detalles;
        }

        public static List<ComprobantesProveedores_Detalles> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedores_DetallesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ComprobantesProveedores_Detalles> lista = new List<ComprobantesProveedores_Detalles>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantesProveedores_Detalles").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ComprobantesProveedores_Detalles comprobantesProveedores_Detalles = new ComprobantesProveedores_Detalles();
                foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(comprobantesProveedores_Detalles, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(comprobantesProveedores_Detalles);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;


        }

        public static ComprobantesProveedores_Detalles Save(ComprobantesProveedores_Detalles comprobantesProveedores_Detalles)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedores_DetallesSave")) throw new PermisoException();
            if (comprobantesProveedores_Detalles.Id == -1) return Insert(comprobantesProveedores_Detalles);
            else return Update(comprobantesProveedores_Detalles);
        }

        public static ComprobantesProveedores_Detalles Insert(ComprobantesProveedores_Detalles comprobantesProveedores_Detalles)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedores_DetallesSave")) throw new PermisoException();
            string sql = "insert into ComprobantesProveedores_Detalles(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantesProveedores_Detalles, null));
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
            comprobantesProveedores_Detalles.Id = Convert.ToInt32(resp);
            return comprobantesProveedores_Detalles;
        }

        public static ComprobantesProveedores_Detalles Update(ComprobantesProveedores_Detalles comprobantesProveedores_Detalles)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedores_DetallesSave")) throw new PermisoException();
            string sql = "update ComprobantesProveedores_Detalles set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantesProveedores_Detalles).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantesProveedores_Detalles, null));
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
            sql += " where Id = " + comprobantesProveedores_Detalles.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return comprobantesProveedores_Detalles;
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
