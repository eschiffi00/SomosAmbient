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
    public partial class CostoCanonOperator
    {

        public static CostoCanon GetOneByIdentity(int Id)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoCanonBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoCanon where Id = " + Id.ToString()).Tables[0];
            CostoCanon costoCanon = new CostoCanon();
            foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(costoCanon, value, null); }
                catch (System.ArgumentException) { }
            }
            return costoCanon;
        }

        public static List<CostoCanon> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoCanonBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<CostoCanon> lista = new List<CostoCanon>();
            DataTable dt = db.GetDataSet("select " + columnas + " from CostoCanon").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                CostoCanon costoCanon = new CostoCanon();
                foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(costoCanon, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(costoCanon);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static CostoCanon Save(CostoCanon costoCanon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoCanonSave")) throw new PermisoException();
            if (costoCanon.Id == -1) return Insert(costoCanon);
            else return Update(costoCanon);
        }

        public static CostoCanon Insert(CostoCanon costoCanon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoCanonSave")) throw new PermisoException();
            string sql = "insert into CostoCanon(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoCanon, null));
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
            costoCanon.Id = Convert.ToInt32(resp);
            return costoCanon;
        }

        public static CostoCanon Update(CostoCanon costoCanon)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoCostoCanonSave")) throw new PermisoException();
            string sql = "update CostoCanon set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(CostoCanon).GetProperties())
            {
                if (prop.Name == "Id") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(costoCanon, null));
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
            sql += " where Id = " + costoCanon.Id;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return costoCanon;
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
