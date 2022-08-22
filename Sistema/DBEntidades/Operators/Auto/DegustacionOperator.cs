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
    public partial class DegustacionOperator
    {

        public static Degustacion GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Degustacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Degustacion where Id = " + Id.ToString()).Tables[0];
            Degustacion degustacion = new Degustacion();
            foreach (PropertyInfo prop in typeof(Degustacion).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(degustacion, value, null); }
                catch (System.ArgumentException) { }
            }
            return degustacion;
        }

        public static List<Degustacion> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Degustacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Degustacion> lista = new List<Degustacion>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Degustacion").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Degustacion degustacion = new Degustacion();
                foreach (PropertyInfo prop in typeof(Degustacion).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(degustacion, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(degustacion);
            }
            return lista;
        }

		public static List<Degustacion> GetAllEstado1()
		{
			return GetAll().Where(x => x.EstadoId == 1).ToList();
		}
		public static List<Degustacion> GetAllEstadoNot1()
		{
			return GetAll().Where(x => x.EstadoId != 1).ToList();
		}
		public static List<Degustacion> GetAllEstadoN(int estado)
		{
			return GetAll().Where(x => x.EstadoId == estado).ToList();
		}
		public static List<Degustacion> GetAllEstadoNotN(int estado)
		{
			return GetAll().Where(x => x.EstadoId != estado).ToList();
		}


        public class MaxLength
        {
			public static int HoraCorporativo { get; set; } = 5;
			public static int HoraSocial { get; set; } = 5;


        }

        public static Degustacion Save(Degustacion degustacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionSave")) throw new PermisoException();
            if (degustacion.Id == -1) return Insert(degustacion);
            else return Update(degustacion);
        }

        public static Degustacion Insert(Degustacion degustacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionSave")) throw new PermisoException();
            string sql = "insert into Degustacion(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Degustacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(degustacion, null));
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
            degustacion.Id = Convert.ToInt32(resp);
            return degustacion;
        }

        public static Degustacion Update(Degustacion degustacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoDegustacionSave")) throw new PermisoException();
            string sql = "update Degustacion set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Degustacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(degustacion, null));
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
            sql += " where Id = " + degustacion.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return degustacion;
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
