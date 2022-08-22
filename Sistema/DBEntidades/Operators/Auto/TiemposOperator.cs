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
    public partial class TiemposOperator
    {

        public static Tiempos GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiemposBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Tiempos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from Tiempos where Id = " + Id.ToString()).Tables[0];
            Tiempos tiempos = new Tiempos();
            foreach (PropertyInfo prop in typeof(Tiempos).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tiempos, value, null); }
                catch (System.ArgumentException) { }
            }
            return tiempos;
        }

        public static List<Tiempos> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiemposBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Tiempos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<Tiempos> lista = new List<Tiempos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Tiempos").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Tiempos tiempos = new Tiempos();
                foreach (PropertyInfo prop in typeof(Tiempos).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tiempos, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tiempos);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 200;
			public static int ImagenMarcoSuperiorExtension { get; set; } = 10;


        }

        public static Tiempos Save(Tiempos tiempos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiemposSave")) throw new PermisoException();
            if (tiempos.Id == -1) return Insert(tiempos);
            else return Update(tiempos);
        }

        public static Tiempos Insert(Tiempos tiempos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiemposSave")) throw new PermisoException();
            string sql = "insert into Tiempos(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Tiempos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiempos, null));
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
            tiempos.Id = Convert.ToInt32(resp);
            return tiempos;
        }

        public static Tiempos Update(Tiempos tiempos)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiemposSave")) throw new PermisoException();
            string sql = "update Tiempos set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(Tiempos).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiempos, null));
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
            sql += " where Id = " + tiempos.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tiempos;
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
