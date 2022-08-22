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
    public partial class ComprobantesProveedoresOperator
    {

        public static ComprobantesProveedores GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantesProveedores where Id = " + Id.ToString()).Tables[0];
            ComprobantesProveedores comprobantesProveedores = new ComprobantesProveedores();
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(comprobantesProveedores, value, null); }
                catch (System.ArgumentException) { }
            }
            return comprobantesProveedores;
        }

        public static List<ComprobantesProveedores> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedoresBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ComprobantesProveedores> lista = new List<ComprobantesProveedores>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ComprobantesProveedores").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ComprobantesProveedores comprobantesProveedores = new ComprobantesProveedores();
                foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(comprobantesProveedores, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(comprobantesProveedores);
            }
            return lista;
        }

		public static List<ComprobantesProveedores> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<ComprobantesProveedores> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<ComprobantesProveedores> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<ComprobantesProveedores> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int GeneraOP { get; set; } = 10;


        }

        public static ComprobantesProveedores Save(ComprobantesProveedores comprobantesProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedoresSave")) throw new PermisoException();
            if (comprobantesProveedores.Id == -1) return Insert(comprobantesProveedores);
            else return Update(comprobantesProveedores);
        }

        public static ComprobantesProveedores Insert(ComprobantesProveedores comprobantesProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedoresSave")) throw new PermisoException();
            string sql = "insert into ComprobantesProveedores(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantesProveedores, null));
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
            comprobantesProveedores.Id = Convert.ToInt32(resp);
            return comprobantesProveedores;
        }

        public static ComprobantesProveedores Update(ComprobantesProveedores comprobantesProveedores)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoComprobantesProveedoresSave")) throw new PermisoException();
            string sql = "update ComprobantesProveedores set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ComprobantesProveedores).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(comprobantesProveedores, null));
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
            sql += " where Id = " + comprobantesProveedores.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return comprobantesProveedores;
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
