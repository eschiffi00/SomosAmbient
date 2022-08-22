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
    public partial class TiposBarrasOperator
    {

        public static TiposBarras GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiposBarrasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TiposBarras where Id = " + Id.ToString()).Tables[0];
            TiposBarras tiposBarras = new TiposBarras();
            foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tiposBarras, value, null); }
                catch (System.ArgumentException) { }
            }
            return tiposBarras;
        }

        public static List<TiposBarras> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiposBarrasBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TiposBarras> lista = new List<TiposBarras>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TiposBarras").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TiposBarras tiposBarras = new TiposBarras();
                foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tiposBarras, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tiposBarras);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Descripcion { get; set; } = 300;
			public static int RangoEtareo { get; set; } = 50;


        }

        public static TiposBarras Save(TiposBarras tiposBarras)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiposBarrasSave")) throw new PermisoException();
            if (tiposBarras.Id == -1) return Insert(tiposBarras);
            else return Update(tiposBarras);
        }

        public static TiposBarras Insert(TiposBarras tiposBarras)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiposBarrasSave")) throw new PermisoException();
            string sql = "insert into TiposBarras(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiposBarras, null));
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
            tiposBarras.Id = Convert.ToInt32(resp);
            return tiposBarras;
        }

        public static TiposBarras Update(TiposBarras tiposBarras)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTiposBarrasSave")) throw new PermisoException();
            string sql = "update TiposBarras set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TiposBarras).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tiposBarras, null));
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
            sql += " where Id = " + tiposBarras.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tiposBarras;
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
