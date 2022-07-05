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
    public partial class ProvinciaOperator
    {

        public static Provincia GetOneByIdentity(int ProvinciaId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Provincia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Provincia where ProvinciaId = " + ProvinciaId.ToString()).Tables[0];
            Provincia provincia = new Provincia();
            foreach (PropertyInfo prop in typeof(Provincia).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(provincia, value, null); }
                catch (System.ArgumentException) { }
            }
            return provincia;
        }

        public static List<Provincia> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Provincia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Provincia> lista = new List<Provincia>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Provincia").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Provincia provincia = new Provincia();
                foreach (PropertyInfo prop in typeof(Provincia).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(provincia, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(provincia);
            }
            return lista;
        }

		public static List<Provincia> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Provincia> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Provincia> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Provincia> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Nombre { get; set; } = 60;


        }

        public static Provincia Save(Provincia provincia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciaSave")) throw new PermisoException();
            if (provincia.ProvinciaId == -1) return Insert(provincia);
            else return Update(provincia);
        }

        public static Provincia Insert(Provincia provincia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciaSave")) throw new PermisoException();
            string sql = "insert into Provincia(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Provincia).GetProperties())
            {
                if (prop.Name == "ProvinciaId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(provincia, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ProvinciaId values (" + valores + ")";
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
            provincia.ProvinciaId = Convert.ToInt32(resp);
            return provincia;
        }

        public static Provincia Update(Provincia provincia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProvinciaSave")) throw new PermisoException();
            string sql = "update Provincia set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Provincia).GetProperties())
            {
                if (prop.Name == "ProvinciaId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(provincia, null));
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
            sql += " where ProvinciaId = " + provincia.ProvinciaId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return provincia;
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
