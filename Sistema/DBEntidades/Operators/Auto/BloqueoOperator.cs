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
    public partial class BloqueoOperator
    {

        public static Bloqueo GetOneByIdentity(int BloqueoId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBloqueoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Bloqueo where BloqueoId = " + BloqueoId.ToString()).Tables[0];
            Bloqueo bloqueo = new Bloqueo();
            foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(bloqueo, value, null); }
                catch (System.ArgumentException) { }
            }
            return bloqueo;
        }

        public static List<Bloqueo> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBloqueoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Bloqueo> lista = new List<Bloqueo>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Bloqueo").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Bloqueo bloqueo = new Bloqueo();
                foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(bloqueo, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(bloqueo);
            }
            return lista;
        }

		public static List<Bloqueo> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Bloqueo> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Bloqueo> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Bloqueo> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Control { get; set; } = 40;


        }

        public static Bloqueo Save(Bloqueo bloqueo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBloqueoSave")) throw new PermisoException();
            if (bloqueo.BloqueoId == -1) return Insert(bloqueo);
            else return Update(bloqueo);
        }

        public static Bloqueo Insert(Bloqueo bloqueo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBloqueoSave")) throw new PermisoException();
            string sql = "insert into Bloqueo(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties())
            {
                if (prop.Name == "BloqueoId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(bloqueo, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.BloqueoId values (" + valores + ")";
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
            bloqueo.BloqueoId = Convert.ToInt32(resp);
            return bloqueo;
        }

        public static Bloqueo Update(Bloqueo bloqueo)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoBloqueoSave")) throw new PermisoException();
            string sql = "update Bloqueo set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Bloqueo).GetProperties())
            {
                if (prop.Name == "BloqueoId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(bloqueo, null));
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
            sql += " where BloqueoId = " + bloqueo.BloqueoId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return bloqueo;
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
