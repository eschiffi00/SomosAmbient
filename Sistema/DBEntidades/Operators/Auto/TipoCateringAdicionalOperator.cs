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
    public partial class TipoCateringAdicionalOperator
    {

        public static TipoCateringAdicional GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringAdicionalBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringAdicional where Id = " + Id.ToString()).Tables[0];
            TipoCateringAdicional tipoCateringAdicional = new TipoCateringAdicional();
            foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoCateringAdicional, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoCateringAdicional;
        }

        public static List<TipoCateringAdicional> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringAdicionalBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoCateringAdicional> lista = new List<TipoCateringAdicional>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringAdicional").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoCateringAdicional tipoCateringAdicional = new TipoCateringAdicional();
                foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoCateringAdicional, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoCateringAdicional);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static TipoCateringAdicional Save(TipoCateringAdicional tipoCateringAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringAdicionalSave")) throw new PermisoException();
            if (tipoCateringAdicional.Id == -1) return Insert(tipoCateringAdicional);
            else return Update(tipoCateringAdicional);
        }

        public static TipoCateringAdicional Insert(TipoCateringAdicional tipoCateringAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringAdicionalSave")) throw new PermisoException();
            string sql = "insert into TipoCateringAdicional(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringAdicional, null));
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
            tipoCateringAdicional.Id = Convert.ToInt32(resp);
            return tipoCateringAdicional;
        }

        public static TipoCateringAdicional Update(TipoCateringAdicional tipoCateringAdicional)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringAdicionalSave")) throw new PermisoException();
            string sql = "update TipoCateringAdicional set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringAdicional).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringAdicional, null));
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
            sql += " where Id = " + tipoCateringAdicional.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoCateringAdicional;
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
