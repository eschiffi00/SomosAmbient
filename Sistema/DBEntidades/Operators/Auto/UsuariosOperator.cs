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
    public partial class UsuariosOperator
    {

        public static Usuarios GetOneByIdentity(int EmpleadoId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Usuarios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Usuarios where Id = " + EmpleadoId.ToString()).Tables[0];
            Usuarios usuarios = new Usuarios();
            foreach (PropertyInfo prop in typeof(Usuarios).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(usuarios, value, null); }
                catch (System.ArgumentException) { }
            }
            return usuarios;
        }

        public static List<Usuarios> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Usuarios).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Usuarios> lista = new List<Usuarios>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Usuarios").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Usuarios usuarios = new Usuarios();
                foreach (PropertyInfo prop in typeof(Usuarios).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(usuarios, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(usuarios);
            }
            return lista;
        }

		public static List<Usuarios> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Usuarios> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Usuarios> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Usuarios> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int UserName { get; set; } = 50;
			public static int Password { get; set; } = 50;
			public static int CodigoSeguridad { get; set; } = 20;
			public static int RutaCodigoSeguridad { get; set; } = 200;


        }

        public static Usuarios Save(Usuarios usuarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosSave")) throw new PermisoException();
            if (usuarios.EmpleadoId == -1) return Insert(usuarios);
            else return Update(usuarios);
        }

        public static Usuarios Insert(Usuarios usuarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosSave")) throw new PermisoException();
            string sql = "insert into Usuarios(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Usuarios).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarios, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted. values (" + valores + ")";
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
            usuarios.EmpleadoId = Convert.ToInt32(resp);
            return usuarios;
        }

        public static Usuarios Update(Usuarios usuarios)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoUsuariosSave")) throw new PermisoException();
            string sql = "update Usuarios set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Usuarios).GetProperties())
            {
                if (prop.Name == "") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(usuarios, null));
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
            sql += " where EmpleadoId = " + usuarios.EmpleadoId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return usuarios;
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
