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
    public partial class TipoLogisticaOperator
    {

        public static TipoLogistica GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoLogisticaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoLogistica where Id = " + Id.ToString()).Tables[0];
            TipoLogistica tipoLogistica = new TipoLogistica();
            foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoLogistica, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoLogistica;
        }

        public static List<TipoLogistica> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoLogisticaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoLogistica> lista = new List<TipoLogistica>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoLogistica").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoLogistica tipoLogistica = new TipoLogistica();
                foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoLogistica, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoLogistica);
            }
            return lista;
        }



        public class MaxLength
        {
			public static int Concepto { get; set; } = 200;


        }

        public static TipoLogistica Save(TipoLogistica tipoLogistica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoLogisticaSave")) throw new PermisoException();
            if (tipoLogistica.Id == -1) return Insert(tipoLogistica);
            else return Update(tipoLogistica);
        }

        public static TipoLogistica Insert(TipoLogistica tipoLogistica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoLogisticaSave")) throw new PermisoException();
            string sql = "insert into TipoLogistica(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoLogistica, null));
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
            tipoLogistica.Id = Convert.ToInt32(resp);
            return tipoLogistica;
        }

        public static TipoLogistica Update(TipoLogistica tipoLogistica)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoLogisticaSave")) throw new PermisoException();
            string sql = "update TipoLogistica set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoLogistica).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoLogistica, null));
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
            sql += " where Id = " + tipoLogistica.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoLogistica;
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
