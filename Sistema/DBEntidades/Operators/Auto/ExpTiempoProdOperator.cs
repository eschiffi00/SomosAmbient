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
    public partial class ExpTiempoProdOperator
    {

        public static ExpTiempoProd GetOneByIdentity(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExpTiempoProdBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            DataTable dt = db.GetDataSet("select " + columnas + " from ExpTiempoProd where ID = " + ID.ToString()).Tables[0];
            ExpTiempoProd expTiempoProd = new ExpTiempoProd();
            foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties())
            {
				object value = dt.Rows[0][prop.Name];
				if (value == DBNull.Value) value = null;
                try { prop.SetValue(expTiempoProd, value, null); }
                catch (System.ArgumentException) { }
            }
            return expTiempoProd;
        }

        public static List<ExpTiempoProd> GetAll()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExpTiempoProdBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ExpTiempoProd> lista = new List<ExpTiempoProd>();
            DataTable dt = db.GetDataSet("select " + columnas + " from ExpTiempoProd").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                ExpTiempoProd expTiempoProd = new ExpTiempoProd();
                foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties())
                {
					object value = dr[prop.Name];
					if (value == DBNull.Value) value = null;
					try { prop.SetValue(expTiempoProd, value, null); }
					catch (System.ArgumentException) { }
                }
                lista.Add(expTiempoProd);
            }
            return lista;
        }



        public class MaxLength
        {


        }

        public static ExpTiempoProd Save(ExpTiempoProd expTiempoProd)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExpTiempoProdSave")) throw new PermisoException();
            if (expTiempoProd.ID == -1) return Insert(expTiempoProd);
            else return Update(expTiempoProd);
        }

        public static ExpTiempoProd Insert(ExpTiempoProd expTiempoProd)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExpTiempoProdSave")) throw new PermisoException();
            string sql = "insert into ExpTiempoProd(";
            string columnas = string.Empty;
            string valores = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + ", ";
                valores += "@" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(expTiempoProd, null));
            }
            columnas = columnas.Substring(0, columnas.Length - 2);
            valores = valores.Substring(0, valores.Length - 2);
            sql += columnas + ") output inserted.ID values (" + valores + ")";
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
            expTiempoProd.ID = Convert.ToInt32(resp);
            return expTiempoProd;
        }

        public static ExpTiempoProd Update(ExpTiempoProd expTiempoProd)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoExpTiempoProdSave")) throw new PermisoException();
            string sql = "update ExpTiempoProd set ";
            string columnas = string.Empty;
            List<object> param = new List<object>();
            List<object> valor = new List<object>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (PropertyInfo prop in typeof(ExpTiempoProd).GetProperties())
            {
                if (prop.Name == "ID") continue; //es identity
                columnas += prop.Name + " = @" + prop.Name + ", ";
                param.Add("@" + prop.Name);
                valor.Add(prop.GetValue(expTiempoProd, null));
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
            sql += " where ID = " + expTiempoProd.ID;
            DB db = new DB();
            //db.execute_scalar(sql, parametros.ToArray());
            object resp = db.ExecuteScalar(sql, sqlParams.ToArray());
            return expTiempoProd;
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
