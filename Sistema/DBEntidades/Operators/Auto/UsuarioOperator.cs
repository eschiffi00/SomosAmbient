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
    public partial class UsuarioOperator
    {

        public static Usuario GetOneByIdentity(int UsuarioId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Usuario).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Usuario where UsuarioId = " + UsuarioId.ToString()).Tables[0];
            Usuario usuario = new Usuario();
            foreach (PropertyInfo prop in typeof(Usuario).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(usuario, value, null); }
                catch (System.ArgumentException) { }
            }
            return usuario;
        }

        public static List<Usuario> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Usuario).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Usuario> lista = new List<Usuario>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Usuario").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Usuario usuario = new Usuario();
                foreach (PropertyInfo prop in typeof(Usuario).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(usuario, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(usuario);
            }
            return lista;
        }

		public static List<Usuario> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Usuario> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Usuario> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Usuario> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int LoginName { get; set; } = 40;
			public static int Nombre { get; set; } = 60;
			public static int Email { get; set; } = 50;
			public static int Password { get; set; } = 40;


        }

        public static Usuario Save(Usuario usuario)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioSave")) throw new PermisoException();
            if (usuario.UsuarioId == -1) return Insert(usuario);
            else return Update(usuario);
        }

        public static Usuario Insert(Usuario usuario)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioSave")) throw new PermisoException();
            string sql = "insert into Usuario(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Usuario).GetProperties())
            {
                if (prop.Name == "UsuarioId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuario, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.UsuarioId values (" + valores + ")";
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
            usuario.UsuarioId = Convert.ToInt32(resp);
            return usuario;
        }

        public static Usuario Update(Usuario usuario)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuarioSave")) throw new PermisoException();
            string sql = "update Usuario set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Usuario).GetProperties())
            {
                if (prop.Name == "UsuarioId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuario, null));
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
            sql += " where UsuarioId = " + usuario.UsuarioId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return usuario;
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
