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
    public partial class UnidadesNegocios_ProveedoresOperator
    {

        public static UnidadesNegocios_Proveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegocios_ProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from UnidadesNegocios_Proveedores where Id = " + Id.ToString()).Tables[0];
            UnidadesNegocios_Proveedores unidadesNegocios_Proveedores = new UnidadesNegocios_Proveedores();
            foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(unidadesNegocios_Proveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return unidadesNegocios_Proveedores;
        }

        public static List<UnidadesNegocios_Proveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegocios_ProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<UnidadesNegocios_Proveedores> lista = new List<UnidadesNegocios_Proveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from UnidadesNegocios_Proveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                UnidadesNegocios_Proveedores unidadesNegocios_Proveedores = new UnidadesNegocios_Proveedores();
                foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(unidadesNegocios_Proveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(unidadesNegocios_Proveedores);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static UnidadesNegocios_Proveedores Save(UnidadesNegocios_Proveedores unidadesNegocios_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegocios_ProveedoresSave")) throw new PermisoException();
            if (unidadesNegocios_Proveedores.Id == -1) return Insert(unidadesNegocios_Proveedores);
            else return Update(unidadesNegocios_Proveedores);
        }

        public static UnidadesNegocios_Proveedores Insert(UnidadesNegocios_Proveedores unidadesNegocios_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegocios_ProveedoresSave")) throw new PermisoException();
            string sql = "insert into UnidadesNegocios_Proveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(unidadesNegocios_Proveedores, null));
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
            unidadesNegocios_Proveedores.Id = Convert.ToInt32(resp);
            return unidadesNegocios_Proveedores;
        }

        public static UnidadesNegocios_Proveedores Update(UnidadesNegocios_Proveedores unidadesNegocios_Proveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUnidadesNegocios_ProveedoresSave")) throw new PermisoException();
            string sql = "update UnidadesNegocios_Proveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UnidadesNegocios_Proveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(unidadesNegocios_Proveedores, null));
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
            sql += " where Id = " + unidadesNegocios_Proveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return unidadesNegocios_Proveedores;
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
