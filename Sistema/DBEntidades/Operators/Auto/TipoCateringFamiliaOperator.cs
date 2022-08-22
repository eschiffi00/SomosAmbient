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
    public partial class TipoCateringFamiliaOperator
    {

        public static TipoCateringFamilia GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringFamiliaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringFamilia where Id = " + Id.ToString()).Tables[0];
            TipoCateringFamilia tipoCateringFamilia = new TipoCateringFamilia();
            foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(tipoCateringFamilia, value, null); }
                catch (System.ArgumentException) { }
            }
            return tipoCateringFamilia;
        }

        public static List<TipoCateringFamilia> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringFamiliaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<TipoCateringFamilia> lista = new List<TipoCateringFamilia>();
            DataTable dt = db.GetDataSet("select " + columnas + " from TipoCateringFamilia").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                TipoCateringFamilia tipoCateringFamilia = new TipoCateringFamilia();
                foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(tipoCateringFamilia, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(tipoCateringFamilia);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static TipoCateringFamilia Save(TipoCateringFamilia tipoCateringFamilia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringFamiliaSave")) throw new PermisoException();
            if (tipoCateringFamilia.Id == -1) return Insert(tipoCateringFamilia);
            else return Update(tipoCateringFamilia);
        }

        public static TipoCateringFamilia Insert(TipoCateringFamilia tipoCateringFamilia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringFamiliaSave")) throw new PermisoException();
            string sql = "insert into TipoCateringFamilia(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringFamilia, null));
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
            tipoCateringFamilia.Id = Convert.ToInt32(resp);
            return tipoCateringFamilia;
        }

        public static TipoCateringFamilia Update(TipoCateringFamilia tipoCateringFamilia)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoTipoCateringFamiliaSave")) throw new PermisoException();
            string sql = "update TipoCateringFamilia set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(TipoCateringFamilia).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(tipoCateringFamilia, null));
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
            sql += " where Id = " + tipoCateringFamilia.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return tipoCateringFamilia;
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
