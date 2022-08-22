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
    public partial class RetencionesOperator
    {

        public static Retenciones GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRetencionesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Retenciones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Retenciones where Id = " + Id.ToString()).Tables[0];
            Retenciones retenciones = new Retenciones();
            foreach (PropertyInfo prop in typeof(Retenciones).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(retenciones, value, null); }
                catch (System.ArgumentException) { }
            }
            return retenciones;
        }

        public static List<Retenciones> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRetencionesBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Retenciones).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Retenciones> lista = new List<Retenciones>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Retenciones").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Retenciones retenciones = new Retenciones();
                foreach (PropertyInfo prop in typeof(Retenciones).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(retenciones, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(retenciones);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int NroCertificado { get; set; } = 50;


        }

        public static Retenciones Save(Retenciones retenciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRetencionesSave")) throw new PermisoException();
            if (retenciones.Id == -1) return Insert(retenciones);
            else return Update(retenciones);
        }

        public static Retenciones Insert(Retenciones retenciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRetencionesSave")) throw new PermisoException();
            string sql = "insert into Retenciones(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Retenciones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(retenciones, null));
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
            retenciones.Id = Convert.ToInt32(resp);
            return retenciones;
        }

        public static Retenciones Update(Retenciones retenciones)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRetencionesSave")) throw new PermisoException();
            string sql = "update Retenciones set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Retenciones).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(retenciones, null));
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
            sql += " where Id = " + retenciones.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return retenciones;
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
