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
    public partial class UsuariosLocacionesOperator
    {

        public static UsuariosLocaciones GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosLocacionesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuariosLocaciones where Id = " + Id.ToString()).Tables[0];
            UsuariosLocaciones usuariosLocaciones = new UsuariosLocaciones();
            foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(usuariosLocaciones, value, null); }
                catch (System.ArgumentException) { }
            }
            return usuariosLocaciones;
        }

        public static List<UsuariosLocaciones> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosLocacionesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<UsuariosLocaciones> lista = new List<UsuariosLocaciones>();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuariosLocaciones").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                UsuariosLocaciones usuariosLocaciones = new UsuariosLocaciones();
                foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(usuariosLocaciones, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(usuariosLocaciones);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static UsuariosLocaciones Save(UsuariosLocaciones usuariosLocaciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosLocacionesSave")) throw new PermisoException();
            if (usuariosLocaciones.Id == -1) return Insert(usuariosLocaciones);
            else return Update(usuariosLocaciones);
        }

        public static UsuariosLocaciones Insert(UsuariosLocaciones usuariosLocaciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosLocacionesSave")) throw new PermisoException();
            string sql = "insert into UsuariosLocaciones(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuariosLocaciones, null));
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
            usuariosLocaciones.Id = Convert.ToInt32(resp);
            return usuariosLocaciones;
        }

        public static UsuariosLocaciones Update(UsuariosLocaciones usuariosLocaciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosLocacionesSave")) throw new PermisoException();
            string sql = "update UsuariosLocaciones set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuariosLocaciones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuariosLocaciones, null));
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
            sql += " where Id = " + usuariosLocaciones.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return usuariosLocaciones;
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
