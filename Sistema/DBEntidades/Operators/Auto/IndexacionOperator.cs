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
    public partial class IndexacionOperator
    {

        public static Indexacion GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIndexacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Indexacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Indexacion where Id = " + Id.ToString()).Tables[0];
            Indexacion indexacion = new Indexacion();
            foreach (PropertyInfo prop in typeof(Indexacion).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(indexacion, value, null); }
                catch (System.ArgumentException) { }
            }
            return indexacion;
        }

        public static List<Indexacion> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIndexacionBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Indexacion).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Indexacion> lista = new List<Indexacion>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Indexacion").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Indexacion indexacion = new Indexacion();
                foreach (PropertyInfo prop in typeof(Indexacion).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(indexacion, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(indexacion);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int TipoIndexacion { get; set; } = 1;


        }

        public static Indexacion Save(Indexacion indexacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIndexacionSave")) throw new PermisoException();
            if (indexacion.Id == -1) return Insert(indexacion);
            else return Update(indexacion);
        }

        public static Indexacion Insert(Indexacion indexacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIndexacionSave")) throw new PermisoException();
            string sql = "insert into Indexacion(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Indexacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(indexacion, null));
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
            indexacion.Id = Convert.ToInt32(resp);
            return indexacion;
        }

        public static Indexacion Update(Indexacion indexacion)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoIndexacionSave")) throw new PermisoException();
            string sql = "update Indexacion set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Indexacion).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(indexacion, null));
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
            sql += " where Id = " + indexacion.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return indexacion;
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
