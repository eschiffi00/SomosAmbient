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
    public partial class TipoServicioAdicionalOperator
    {

        public static TipoServicioAdicional GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServicioAdicionalBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoServicioAdicional where Id = " + Id.ToString()).Tables[0];
            TipoServicioAdicional tipoServicioAdicional = new TipoServicioAdicional();
            foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoServicioAdicional, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoServicioAdicional;
        }

        public static List<TipoServicioAdicional> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServicioAdicionalBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoServicioAdicional> lista = new List<TipoServicioAdicional>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoServicioAdicional").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoServicioAdicional tipoServicioAdicional = new TipoServicioAdicional();
                foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoServicioAdicional, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoServicioAdicional);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static TipoServicioAdicional Save(TipoServicioAdicional tipoServicioAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServicioAdicionalSave")) throw new PermisoException();
            if (tipoServicioAdicional.Id == -1) return Insert(tipoServicioAdicional);
            else return Update(tipoServicioAdicional);
        }

        public static TipoServicioAdicional Insert(TipoServicioAdicional tipoServicioAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServicioAdicionalSave")) throw new PermisoException();
            string sql = "insert into TipoServicioAdicional(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoServicioAdicional, null));
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
            tipoServicioAdicional.Id = Convert.ToInt32(resp);
            return tipoServicioAdicional;
        }

        public static TipoServicioAdicional Update(TipoServicioAdicional tipoServicioAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoServicioAdicionalSave")) throw new PermisoException();
            string sql = "update TipoServicioAdicional set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoServicioAdicional).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoServicioAdicional, null));
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
            sql += " where Id = " + tipoServicioAdicional.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoServicioAdicional;
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
