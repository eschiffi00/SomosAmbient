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
    public partial class RolOperator
    {

        public static Rol GetOneByIdentity(int RolId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRolBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rol).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rol where RolId = " + RolId.ToString()).Tables[0];
            Rol rol = new Rol();
            foreach (PropertyInfo prop in typeof(Rol).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(rol, value, null); }
                catch (System.ArgumentException) { }
            }
            return rol;
        }

        public static List<Rol> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRolBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Rol).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Rol> lista = new List<Rol>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Rol").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Rol rol = new Rol();
                foreach (PropertyInfo prop in typeof(Rol).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(rol, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(rol);
            }
            return lista;
        }

		public static List<Rol> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Rol> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Rol> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Rol> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Nombre { get; set; } = 40;


        }

        public static Rol Save(Rol rol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRolSave")) throw new PermisoException();
            if (rol.RolId == -1) return Insert(rol);
            else return Update(rol);
        }

        public static Rol Insert(Rol rol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRolSave")) throw new PermisoException();
            string sql = "insert into Rol(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rol).GetProperties())
            {
                if (prop.Name == "RolId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rol, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.RolId values (" + valores + ")";
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
            rol.RolId = Convert.ToInt32(resp);
            return rol;
        }

        public static Rol Update(Rol rol)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRolSave")) throw new PermisoException();
            string sql = "update Rol set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Rol).GetProperties())
            {
                if (prop.Name == "RolId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(rol, null));
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
            sql += " where RolId = " + rol.RolId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return rol;
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
