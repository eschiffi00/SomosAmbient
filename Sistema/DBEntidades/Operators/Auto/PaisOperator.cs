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
    public partial class PaisOperator
    {

        public static Pais GetOneByIdentity(int PaisId)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPaisBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Pais).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Pais where PaisId = " + PaisId.ToString()).Tables[0];
            Pais pais = new Pais();
            foreach (PropertyInfo prop in typeof(Pais).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(pais, value, null); }
                catch (System.ArgumentException) { }
            }
            return pais;
        }

        public static List<Pais> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPaisBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Pais).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Pais> lista = new List<Pais>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Pais").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Pais pais = new Pais();
                foreach (PropertyInfo prop in typeof(Pais).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(pais, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(pais);
            }
            return lista;
        }

		public static List<Pais> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Pais> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Pais> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Pais> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int Nombre { get; set; } = 40;


        }

        public static Pais Save(Pais pais)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPaisSave")) throw new PermisoException();
            if (pais.PaisId == -1) return Insert(pais);
            else return Update(pais);
        }

        public static Pais Insert(Pais pais)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPaisSave")) throw new PermisoException();
            string sql = "insert into Pais(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Pais).GetProperties())
            {
                if (prop.Name == "PaisId") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pais, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.PaisId values (" + valores + ")";
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
            pais.PaisId = Convert.ToInt32(resp);
            return pais;
        }

        public static Pais Update(Pais pais)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoPaisSave")) throw new PermisoException();
            string sql = "update Pais set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Pais).GetProperties())
            {
                if (prop.Name == "PaisId") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(pais, null));
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
            sql += " where PaisId = " + pais.PaisId;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return pais;
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
