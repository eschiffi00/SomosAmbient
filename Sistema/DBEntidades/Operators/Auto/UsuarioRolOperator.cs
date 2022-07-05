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
    public partial class UsuarioRolOperator
    {

        public static UsuarioRol GetOneByIdentity(int UsuarioRolId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioRolBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuarioRol where UsuarioRolId = " + UsuarioRolId.ToString()).Tables[0];
            UsuarioRol usuarioRol = new UsuarioRol();
            foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(usuarioRol, value, null); }
                catch (System.ArgumentException) { }
            }
            return usuarioRol;
        }

        public static List<UsuarioRol> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioRolBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<UsuarioRol> lista = new List<UsuarioRol>();
            DataTable dt = db.GetDataSet("select " + columnas + " from UsuarioRol").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                UsuarioRol usuarioRol = new UsuarioRol();
                foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(usuarioRol, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(usuarioRol);
            }
            return lista;
        }

		public static List<UsuarioRol> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<UsuarioRol> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<UsuarioRol> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<UsuarioRol> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {


        }

        public static UsuarioRol Save(UsuarioRol usuarioRol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioRolSave")) throw new PermisoException();
            if (usuarioRol.UsuarioRolId == -1) return Insert(usuarioRol);
            else return Update(usuarioRol);
        }

        public static UsuarioRol Insert(UsuarioRol usuarioRol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioRolSave")) throw new PermisoException();
            string sql = "insert into UsuarioRol(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties())
            {
                if (prop.Name == "UsuarioRolId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarioRol, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.UsuarioRolId values (" + valores + ")";
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
            usuarioRol.UsuarioRolId = Convert.ToInt32(resp);
            return usuarioRol;
        }

        public static UsuarioRol Update(UsuarioRol usuarioRol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioRolSave")) throw new PermisoException();
            string sql = "update UsuarioRol set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(UsuarioRol).GetProperties())
            {
                if (prop.Name == "UsuarioRolId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarioRol, null));
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
            sql += " where UsuarioRolId = " + usuarioRol.UsuarioRolId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return usuarioRol;
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
