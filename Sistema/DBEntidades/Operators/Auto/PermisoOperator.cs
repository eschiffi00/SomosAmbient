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
    public partial class PermisoOperator
    {

        public static Permiso GetOneByIdentity(int PermisoId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPermisoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Permiso).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Permiso where PermisoId = " + PermisoId.ToString()).Tables[0];
            Permiso permiso = new Permiso();
            foreach (PropertyInfo prop in typeof(Permiso).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(permiso, value, null); }
                catch (System.ArgumentException) { }
            }
            return permiso;
        }

        public static List<Permiso> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPermisoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Permiso).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Permiso> lista = new List<Permiso>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Permiso").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Permiso permiso = new Permiso();
                foreach (PropertyInfo prop in typeof(Permiso).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(permiso, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(permiso);
            }
            return lista;
        }

		public static List<Permiso> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Permiso> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Permiso> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Permiso> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {


        }

        public static Permiso Save(Permiso permiso)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPermisoSave")) throw new PermisoException();
            if (permiso.PermisoId == -1) return Insert(permiso);
            else return Update(permiso);
        }

        public static Permiso Insert(Permiso permiso)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPermisoSave")) throw new PermisoException();
            string sql = "insert into Permiso(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Permiso).GetProperties())
            {
                if (prop.Name == "PermisoId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(permiso, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.PermisoId values (" + valores + ")";
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
            permiso.PermisoId = Convert.ToInt32(resp);
            return permiso;
        }

        public static Permiso Update(Permiso permiso)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPermisoSave")) throw new PermisoException();
            string sql = "update Permiso set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Permiso).GetProperties())
            {
                if (prop.Name == "PermisoId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(permiso, null));
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
            sql += " where PermisoId = " + permiso.PermisoId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return permiso;
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
